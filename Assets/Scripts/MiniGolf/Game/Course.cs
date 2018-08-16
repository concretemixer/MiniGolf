using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGolf.Game
{
    public class Course
    {
        private string[] levelNames;
        private int maxBalls = 0;

        public int ballsLeft;

        public string LevelAt(int i)
        {
            return levelNames[i];
        }

        public int LevelCount
        {
            get
            {
                return levelNames.Length;
            }
        }

        public int MaxBalls
        {
            get
            {
                return maxBalls;
            }
        }

        public int currentLevelIndex;
        
        public Course(List<string> levels, int balls)
        {
            maxBalls = balls;
            levelNames = levels.ToArray();
            ballsLeft = maxBalls;
            currentLevelIndex = 0;
        }

        public Course(string[] levels, int balls)
        {
            maxBalls = balls;
            levelNames = levels;
            ballsLeft = maxBalls;
            currentLevelIndex = 0;
        }

    }
}
