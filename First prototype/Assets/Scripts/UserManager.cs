using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Happify.User
{
    public class UserManager : MonoBehaviour
    {
        private const string UserDatabaseFilename = "Users.json";

        [SerializeField]
        private float _maximumLives = 3;

        [SerializeField, Tooltip("The time it takes in seconds to receive a new life.")]
        private float _newLifeDuration = 10.0f; // in seconds

        private static UserManager _instance;
        private List<UserDescription> _allUsers = new List<UserDescription>();
        private UserDescription _currentUser;

        private float _difference;

        public float Difference => _difference; // Added
        public float NewLifeDuration => _newLifeDuration; //Added

        /// <summary>
        /// Gets all users.
        /// </summary>
        public IReadOnlyList<UserDescription> AllUsers => _allUsers;

        /// <summary>
        /// Gets the current user. Returns null if no user has been loaded.
        /// </summary>
        public UserDescription CurrentUser => _currentUser;

        public static UserManager Instance => _instance;

        /// <summary>
        /// An event that gets invoked when the current user's lives changes.
        /// </summary>
        public event Action CurrentUserLivesChanged;

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
                // Ensure that script does not get destroyed when changing scene.
                DontDestroyOnLoad(this);
                Load();
                AddUser(new UserDescription("Jessica", Level.Easy, Level.Easy, 3, false, false)); //Maybe position this elsewhere
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddUser(UserDescription user, bool setAsCurrentUser = true)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Add to list
            _allUsers.Add(user);

            if (setAsCurrentUser)
                SetCurrentUser(user);
        }

        public void SetCurrentUser(UserDescription user)
        {
            // Save current user if not null => writing to disk
            _currentUser = user;
        }

        private void Load()
        {
            try
            {
                string path = Path.Combine(Application.persistentDataPath, UserDatabaseFilename);
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    Save();
                }

                string json = File.ReadAllText(path);
                if (string.IsNullOrWhiteSpace(json))
                    _allUsers = new List<UserDescription>();
                else
                    _allUsers = JsonConvert.DeserializeObject<List<UserDescription>>(json);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_allUsers);
                string path = Path.Combine(Application.persistentDataPath, UserDatabaseFilename);
                File.WriteAllText(path, json);
            }
            catch(Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void Update()
        {
            UpdateCurrentUserLives();
        }

        private void UpdateCurrentUserLives()
        {
            if (_currentUser == null || _currentUser.NrOfLives == _maximumLives)
                return;

            // Check if time has passed
            double timeNow = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;


            _difference = Convert.ToSingle(timeNow - _currentUser.LastLifeReceivedTimestamp); // added
            // Increment the number of lives
            if(timeNow - _currentUser.LastLifeReceivedTimestamp >= _newLifeDuration)
            {
                _currentUser.NrOfLives++;
                Save();
                _currentUser.LastLifeReceivedTimestamp = timeNow; //added
                CurrentUserLivesChanged?.Invoke();
            }
        }

        public void decreaseLives()
        {
            _currentUser.NrOfLives--;
        }
    }
}