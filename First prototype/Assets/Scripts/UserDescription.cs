using System;

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
        public string Name { get; set; }
        public Level EmotionsLevel { get; set; }
        public Level GeneralLevel { get; set; }
        public int NrOfLives { get; set; }
        public bool RemainingHearing { get; set; }
        public bool RemainingVision { get; set; }

        /// <summary>
        /// Stores the last time a user received a new life as a unix timestamp.
        /// </summary>
        public double LastLifeReceivedTimestamp { get; set; }

        public UserDescription (string name, Level emotionsLevel, Level generalLevel, int lives, bool hearing, bool vision)
        {
            Name = name;
            EmotionsLevel = emotionsLevel;

            if (generalLevel > Level.Moderate)
                throw new ArgumentException("General level can only be Easy or Moderate", nameof(generalLevel));
            GeneralLevel = generalLevel;
            NrOfLives = lives;
            RemainingHearing = hearing;
            RemainingVision = vision;
        }
    }
}
