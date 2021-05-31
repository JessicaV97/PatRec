using Happify.User;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to switch user or add a new one. 
/// </summary>
public class SetUser : MonoBehaviour
{
    //Collect game objects used in this script
    [SerializeField]
    private GameObject _nameInput;
    [SerializeField]
    private GameObject _addUserPanel;
    [SerializeField]
    private GameObject _selectUserPanel;
    [SerializeField]
    private TextMeshProUGUI _playerName;
    [SerializeField]
    private TextMeshProUGUI _currentPlayerName;

    [SerializeField]
    private Button _deafSet;
    [SerializeField]
    private Button _deafSetSettings;
    [SerializeField]
    private Sprite _noAudioPerception;
    [SerializeField]
    private Sprite _audioPerception;

    [SerializeField]
    private Button _blindSet;
    [SerializeField]
    private Button _blindSetSettings;
    [SerializeField]
    private Sprite _noVisualPerception;
    [SerializeField]
    private Sprite _visualPerception;

    private int userIndex = 0;

    private UserDescription currentUser;
    IReadOnlyList<UserDescription> allUsers;

    /// <summary>
    /// Collect current user information and get list with all available users.
    /// </summary>
    void Awake()
    {
        currentUser= UserManager.Instance.CurrentUser;
        allUsers = UserManager.Instance.AllUsers;
    }

    /// <summary>
    /// Set irrelevant panels to inactive. 
    /// Adjust information on screen to current user. 
    /// </summary>
    void Start()
    {
        _addUserPanel.SetActive(false);
        _selectUserPanel.SetActive(false);
        _playerName.text = currentUser.Name;
        _currentPlayerName.text = currentUser.Name;
        userIndex = GetIndex.IndexOf(allUsers, currentUser);
  
    }

    /// <summary>
    /// Function to activate panel where you can switch the user account. 
    /// </summary>
    public void ChangeUser()
    {
        _playerName.text = currentUser.Name;
        _currentPlayerName.text = currentUser.Name;
        _selectUserPanel.SetActive(true);
    }

    /// <summary>
    /// Fuction to activate panel where you can create a new user account
    /// </summary>
    public void AddUser()
    {
        _addUserPanel.SetActive(true);
    }

    /// <summary>
    /// Deactivate addUser panel without storing what has been entered yet.
    /// </summary>
    public void CancelAddUser()
    {
        _addUserPanel.SetActive(false);
    }

    /// <summary>
    /// Move to the previous user in the list of all users. Update index. 
    /// </summary>
    public void PreviousUser()
    {
        allUsers = UserManager.Instance.AllUsers;
        allUsers = allUsers.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        //Debug.Log(allUsers.Count);
        if (userIndex == 0)
            userIndex = allUsers.Count - 1; 
        else
            userIndex--;
        _playerName.text = allUsers[userIndex].Name;
    }

    /// <summary>
    /// Move to the next user in the list of all users. Update index. 
    /// </summary>
    public void NextUser()
    {
        allUsers = UserManager.Instance.AllUsers;
        allUsers = allUsers.GroupBy(x => x.Name).Select(x => x.First()).ToList();
        Debug.Log(allUsers.Count);
        if (userIndex == allUsers.Count - 1)
            userIndex = 0; 
        else 
            userIndex++;
        _playerName.text = allUsers[userIndex].Name;
    }

    /// <summary>
    /// set current user to be the one that has been selected. 
    /// Probe update function to update visualizations and text in the environment. 
    /// </summary>
    public void SwitchUser()
    {
        UserManager.Instance.SetCurrentUser(allUsers[userIndex]);
        currentUser = UserManager.Instance.CurrentUser;
        _playerName.text = currentUser.Name;
        _currentPlayerName.text = currentUser.Name;
        UpdateSettings();
        _selectUserPanel.SetActive(false);
    }

    /// <summary>
    /// Adjust auditory perception setting
    /// </summary>
    public void ChangeDeaf()
	{
		if (_deafSet.image.overrideSprite != _noAudioPerception)
			_deafSet.image.overrideSprite = _noAudioPerception;
		else
			_deafSet.image.overrideSprite = _audioPerception;
	}

    /// <summary>
    /// Adjust visual perception setting
    /// </summary>
    public void ChangeBlind()
	{
		if (_blindSet.image.overrideSprite != _noVisualPerception)
			_blindSet.image.overrideSprite = _noVisualPerception;
		else
			_blindSet.image.overrideSprite = _visualPerception;
	}

    /// <summary>
    /// Add the user and its information to the list of users.
    /// Store the data.
    /// Adapt visualizations and text in the environment. 
    /// Deactivate the panel.
    /// </summary>
    public void ConfirmedAddUser()
    {
        string name = _nameInput.GetComponent<TMP_InputField>().text;
        _nameInput.GetComponent<TMP_InputField>().text = "";
        
        bool remainingHearing = false;

        if (_deafSet.image.overrideSprite.name == "Doof")
        {
            remainingHearing = true;
            _deafSetSettings.image.overrideSprite = _audioPerception;
        }
        else
            _deafSetSettings.image.overrideSprite = _noAudioPerception;
 
        bool remainingVision = false;
        if (_blindSet.image.overrideSprite.name == "Blind")
        {
            remainingVision = true;
            _blindSetSettings.image.overrideSprite = _visualPerception;
        }
        else
            _blindSetSettings.image.overrideSprite = _noVisualPerception;
            
        UserManager.Instance.AddUser(new UserDescription(name, Level.Easy, Level.Easy, 3, remainingHearing, remainingVision, 0, 0));
        UserManager.Instance.Save();
        currentUser = UserManager.Instance.CurrentUser;
        _playerName.text = currentUser.Name;
        _currentPlayerName.text = currentUser.Name;

        _addUserPanel.SetActive(false);
    }

    /// <summary>
    /// Update visualizations of settings when switching user.
    /// </summary>
    public void UpdateSettings()
    {

        if (currentUser.RemainingVision)
            _blindSetSettings.image.overrideSprite = _visualPerception;
        else
            _blindSetSettings.image.overrideSprite = _noVisualPerception;

        if (currentUser.RemainingHearing)
            _deafSetSettings.image.overrideSprite = _audioPerception;
        else
            _deafSetSettings.image.overrideSprite = _noAudioPerception;
    }

    /// <summary>
    /// Leave the panel where you can switch user without switching user.
    /// </summary>
    public void BackToSettings()
    {
        UpdateSettings();
        _selectUserPanel.SetActive(false);
    }


}
