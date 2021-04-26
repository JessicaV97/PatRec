using Happify.User;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUser : MonoBehaviour
{
    public GameObject nameInput;
    public GameObject AddUserPanel;
    public GameObject SelectUserPanel;

    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI CurrentPlayerName;

    public Button DeafSet;
    public Button DeafSetSettings;
    public Sprite NoAudioPerception;
    public Sprite AudioPerception;

    public Button BlindSet;
    public Button BlindSetSettings;
    public Sprite NoVisualPerception;
    public Sprite VisualPerception;

    private int userIndex = 0;

    private UserDescription currentUser;
    IReadOnlyList<UserDescription> allUsers;



    void Awake()
    {
        currentUser= UserManager.Instance.CurrentUser;
        allUsers = UserManager.Instance.AllUsers;
    }
    // Start is called before the first frame update
    void Start()
    {
        AddUserPanel.SetActive(false);
        SelectUserPanel.SetActive(false);
        PlayerName.text = currentUser.Name;
        CurrentPlayerName.text = currentUser.Name;
        userIndex = GetIndex.IndexOf(allUsers, currentUser);
  
    }

    public void ChangeUser()
    {
        SelectUserPanel.SetActive(true);
        //PlayerName.text = currentUser.Name;
        //CurrentPlayerName.text = currentUser.Name;
    }

    public void AddUser()
    {
        AddUserPanel.SetActive(true);
        //PlayerName.text = currentUser.Name;
        //CurrentPlayerName.text = currentUser.Name;
    }

    public void CancelAddUser()
    {
        AddUserPanel.SetActive(false);
    }

    public void PreviousUser()
    {
        allUsers = UserManager.Instance.AllUsers;
        allUsers = allUsers.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        //Debug.Log(allUsers.Count);
        if (userIndex == 0)
            userIndex = allUsers.Count - 1; 
        else
            userIndex--;
        PlayerName.text = allUsers[userIndex].Name;
    }
    
    public void NextUser()
    {
        allUsers = UserManager.Instance.AllUsers;
        allUsers = allUsers.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        Debug.Log(allUsers.Count);
        if (userIndex == allUsers.Count - 1)
            userIndex = 0; 
        else 
            userIndex++;
        PlayerName.text = allUsers[userIndex].Name;
    }

    public void SwitchUser()
    {
        UserManager.Instance.SetCurrentUser(allUsers[userIndex]);
        currentUser = UserManager.Instance.CurrentUser;
        PlayerName.text = currentUser.Name;
        CurrentPlayerName.text = currentUser.Name;
        //if (currentUser.RemainingHearing)
        //    DeafSetSettings.image.overrideSprite = AudioPerception;
        //else
        //    DeafSetSettings.image.overrideSprite = NoAudioPerception;
        //if (currentUser.RemainingVision)
        //    BlindSetSettings.image.overrideSprite = VisualPerception;
        //else
        //    BlindSetSettings.image.overrideSprite = NoVisualPerception;
        UpdateSettings();
        SelectUserPanel.SetActive(false);
    }

    public void ChangeDeaf()
	{
		if (DeafSet.image.overrideSprite != NoAudioPerception)
			DeafSet.image.overrideSprite = NoAudioPerception;
		else
			DeafSet.image.overrideSprite = AudioPerception;
	}

	public void ChangeBlind()
	{
		if (BlindSet.image.overrideSprite != NoVisualPerception)
			BlindSet.image.overrideSprite = NoVisualPerception;
		else
			BlindSet.image.overrideSprite = VisualPerception;
	}

    public void ConfirmedAddUser()
    {
        string name = nameInput.GetComponent<TMP_InputField>().text;
        nameInput.GetComponent<TMP_InputField>().text = "";
        
        bool remainingHearing = false;

        //if (DeafSet.image.sprite.name.Equals( "Doof"))
        if (DeafSet.image.overrideSprite.name == "Doof")
        {
            remainingHearing = true;
            DeafSetSettings.image.overrideSprite = AudioPerception;
        }
        else
            DeafSetSettings.image.overrideSprite = NoAudioPerception;
 
        bool remainingVision = false;
        if (BlindSet.image.overrideSprite.name == "Blind")
        {
            remainingVision = true;
            BlindSetSettings.image.overrideSprite = VisualPerception;
        }
        else
            BlindSetSettings.image.overrideSprite = NoVisualPerception;
            
        UserManager.Instance.AddUser(new UserDescription(name, Level.Easy, Level.Easy, 3, remainingHearing, remainingVision, 0, 0));
        Debug.Log("rh" + remainingHearing);
        Debug.Log("rv " + remainingVision);
        UserManager.Instance.Save();
        currentUser = UserManager.Instance.CurrentUser;
        
        AddUserPanel.SetActive(false);
    }

    public void UpdateSettings()
    {

        if (currentUser.RemainingVision)
            BlindSetSettings.image.overrideSprite = VisualPerception;
        else
            BlindSetSettings.image.overrideSprite = NoVisualPerception;

        if (currentUser.RemainingHearing)
            DeafSetSettings.image.overrideSprite = AudioPerception;
        else
            DeafSetSettings.image.overrideSprite = NoAudioPerception;
    }

    public void BackToSettings()
    {
        UpdateSettings();
        SelectUserPanel.SetActive(false);
    }


}
