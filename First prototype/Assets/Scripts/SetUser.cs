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

    // Start is called before the first frame update
    void Start()
    {
        AddUserPanel.SetActive(false);
        SelectUserPanel.SetActive(false);
        IReadOnlyList<UserDescription> allUsers = UserManager.Instance.AllUsers;
        UserDescription currentUser = UserManager.Instance.CurrentUser;
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
        IReadOnlyList<UserDescription>  allUsers = UserManager.Instance.AllUsers;
        if (userIndex == 0)
            userIndex = allUsers.Count - 1; 
        else
            userIndex--;
        PlayerName.text = allUsers[userIndex].Name;
    }

    public void NextUser()
    {
        IReadOnlyList<UserDescription> allUsers = UserManager.Instance.AllUsers;
        if (userIndex == allUsers.Count - 1)
            userIndex = 0; 
        else 
            userIndex++;
        PlayerName.text = allUsers[userIndex].Name;

    }

    public void setUser()
    {
        IReadOnlyList<UserDescription> allUsers = UserManager.Instance.AllUsers;
        UserManager.Instance.SetCurrentUser(allUsers[userIndex]);
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
        if (DeafSet.image.overrideSprite == AudioPerception)
            remainingHearing = true;
        bool remainingVision = false;
        if (BlindSet.image.overrideSprite == VisualPerception)
            remainingVision = true;
        UserManager.Instance.AddUser(new UserDescription(name, Level.Easy, Level.Easy, 3, remainingHearing, remainingVision));
        UserDescription currentUser = UserManager.Instance.CurrentUser;
        CurrentPlayerName.text = currentUser.Name;
        AddUserPanel.SetActive(false);
    }


}
