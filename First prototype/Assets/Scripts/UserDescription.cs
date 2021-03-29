using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Happify.User
{
    public class UserDescription
    {
        public string Name { get; set; }
        public int EmotionsLevel { get; set; }
        public int GeneralLevel { get; set; }
        public int NrOfLives { get; set; }
        public bool RemainingHearing { get; set; }
        public bool RemainingVision { get; set; }

        public UserDescription (string name, int el, int gl, int lives, bool hearing, bool vision)
        {
            Name = name;
            EmotionsLevel = el;
            GeneralLevel = gl;
            NrOfLives = lives;
            RemainingHearing = hearing;
            RemainingVision = vision;
        }
    }
}
