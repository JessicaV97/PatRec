using Happify.User;
using System.Collections;
using System.Collections.Generic;
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
    public Sprite NoAudioPerception;
    public Sprite AudioPerception;

    public Button BlindSet;
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

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangeUser()
    {
        SelectUserPanel.SetActive(true);
    }

    public void AddUser()
    {
        AddUserPanel.SetActive(true);
    }

    public void CancelAddUser()
    {
        AddUserPanel.SetActive(false);
    }

    public void PreviousUser()
    {
        if (userIndex == 0)
            userIndex = allUsers.Count - 1; 
        else
            userIndex--;
        PlayerName.text = allUsers[userIndex].Name;
    }

    public void NextUser()
    {
        if (userIndex == allUsers.Count - 1)
            userIndex = 0; 
        else 
            userIndex++;
        PlayerName.text = allUsers[userIndex].Name;

    }

    public void setUser()
    {
        UserManager.Instance.SetCurrentUser(allUsers[userIndex]);
        currentUser = UserManager.Instance.CurrentUser;
        CurrentPlayerName.text = currentUser.Name;
        if (currentUser.RemainingHearing)
            DeafSet.image.overrideSprite = AudioPerception;
        else
            DeafSet.image.overrideSprite = NoAudioPerception;
        if (currentUser.RemainingVision)
            BlindSet.image.overrideSprite = VisualPerception;
        else
            BlindSet.image.overrideSprite = NoVisualPerception;
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
        if (DeafSet.image.overrideSprite.Equals("doof"))
        {
            remainingHearing = true;
            DeafSet.image.overrideSprite = AudioPerception;
        }
        else
            DeafSet.image.overrideSprite = NoAudioPerception;
 
        bool remainingVision = false;
        if (BlindSet.image.overrideSprite.Equals("blind"))
        {
            remainingVision = true;
            BlindSet.image.overrideSprite = VisualPerception;
        }
        else
            BlindSet.image.overrideSprite = NoVisualPerception;
            
        UserManager.Instance.AddUser(new UserDescription(name, Level.Easy, Level.Easy, 3, remainingHearing, remainingVision));
        currentUser = UserManager.Instance.CurrentUser;
   
        AddUserPanel.SetActive(false);
    }


}
