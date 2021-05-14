using Happify.User;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Happify.Scores
{
    public class HighScoreList : MonoBehaviour
    {
        // Create instance of achievements collector
        private static HighScoreList _instance;
        public static HighScoreList Instance => _instance;

        // Collect list of users. Create lists to organize them based on nr of badges or amount of earned experience points. 
        private List<UserDescription> _userList;
        private List<UserDescription> _sortedOnBadges;
        private List<UserDescription> _sortedOnXP;
        
        // Select fields In GUI to provide the corresponding information
        [SerializeField]
        private TextMeshProUGUI _namesSpace;
        [SerializeField]
        private TextMeshProUGUI _xpSpace;
        [SerializeField]
        private TextMeshProUGUI _badgesSpace;

        /// <summary>
        /// Create list of highscores instance, used in AchievementsCollector.
        /// </summary>
        private void Awake()
        {
            _instance = this; 
        }

        /// <summary>
        /// Create the list for scoreboard
        /// </summary>
        public void CreateList(/*List<UserDescription> list*/)
        {
            _userList = (List<UserDescription>)UserManager.Instance.AllUsers;
            _userList = _userList.GroupBy(x => x.Name).Select(x => x.First()).ToList(); //If names are double it only uses the first occurence. 
            
            _sortedOnBadges = _userList.OrderByDescending(o => o.NumberOfObtainedBadges).ToList();
            _sortedOnXP = _userList.OrderByDescending(o => o.ExperiencePoints).ToList();

            //replace SortedOnBadges with SortedOnXP to order the scoreboard based on XP.
            foreach (UserDescription user in _sortedOnBadges) 
            {
                _namesSpace.text = _namesSpace.text + user.Name + "\n";
                _xpSpace.text = _xpSpace.text + user.ExperiencePoints + "\n";
                _badgesSpace.text = _badgesSpace.text + user.NumberOfObtainedBadges + "\n";
            }
        }

    }
}
