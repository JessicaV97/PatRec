using System;

/// <summary>
/// Class to create a user object, storing a user's name, level of experience per topic, number of lives, impairment settings,
/// number of earned badges and the last time he has lost a live. 
/// </summary>
namespace Happify.User
{
    public enum Level
    {
        Easy = 1,
        Moderate,
        Hard
    }

    public class UserDescription
    {
        private int _numberOfLives;

        public string Name { get; set; }
        public Level EmotionsLevel { get; set; }
        public Level GeneralLevel { get; set; }

        public int NrOfLives
        {
            get => _numberOfLives;
            set
            {
                if (_numberOfLives == value)
                    return;

                _numberOfLives = value;
                LastLifeReceivedTimestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }

        public bool RemainingHearing { get; set; }
        public bool RemainingVision { get; set; }
        public int ExperiencePoints { get; set; }

        public int NumberOfObtainedBadges { get; set; }

        /// <summary>
        /// Stores the last time a user received a new life as a unix timestamp.
        /// </summary>
        public double LastLifeReceivedTimestamp { get; set; }

        /// <summary>
        /// Function to create a new user object. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="emotionsLevel"></param>
        /// <param name="generalLevel"></param>
        /// <param name="lives"></param>
        /// <param name="hearing"></param>
        /// <param name="vision"></param>
        /// <param name="XP"></param>
        /// <param name="nrOfBadges"></param>
        public UserDescription (string name, Level emotionsLevel, Level generalLevel, int lives, bool hearing, bool vision, int XP, int nrOfBadges)
        {
            Name = name;
            EmotionsLevel = emotionsLevel;

            if (generalLevel > Level.Moderate)
                throw new ArgumentException("General level can only be Easy or Moderate", nameof(generalLevel));
            GeneralLevel = generalLevel;
            NrOfLives = lives;
            RemainingHearing = hearing;
            RemainingVision = vision;
            ExperiencePoints = XP;
            NumberOfObtainedBadges = nrOfBadges;
        }
    }
}
