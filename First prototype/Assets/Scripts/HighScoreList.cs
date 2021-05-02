using Happify.User;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


namespace Happify.Scores
{
    public class HighScoreList : MonoBehaviour
    {
        private static HighScoreList _instance;

        public static HighScoreList Instance => _instance;

        private List<UserDescription> UserList;
        private List<UserDescription> SortedOnBadges;
        private List<UserDescription> SortedOnXP;

        public TextMeshProUGUI NamesSpace;
        public TextMeshProUGUI XpSpace;
        public TextMeshProUGUI BadgesSpace;


        private void Awake()
        {
            _instance = this; 
        }

        public void CreateList(/*List<UserDescription> list*/)
        {
            UserList = (List<UserDescription>)UserManager.Instance.AllUsers;
            UserList = UserList.GroupBy(x => x.Name).Select(x => x.First()).ToList(); //To do: Fix all double users
            SortedOnBadges = UserList.OrderByDescending(o => o.NumberOfObtainedBadges).ToList();
            SortedOnXP = UserList.OrderByDescending(o => o.ExperiencePoints).ToList();
            foreach (UserDescription user in SortedOnBadges)
            {
                NamesSpace.text = NamesSpace.text + user.Name + "\n";
                XpSpace.text = XpSpace.text + user.ExperiencePoints + "\n";
                BadgesSpace.text = BadgesSpace.text + user.NumberOfObtainedBadges + "\n";
            }
        }

    }
}
