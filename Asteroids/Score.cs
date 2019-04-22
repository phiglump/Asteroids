using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Score : GameScreen
    {
        // sets the score equal to 0
        int score = 0;
        // set the score ro have an increment of 20
        const int score_increment = 20;
        public void GameScore()
        {
            if (true == int.TryParse(scoredisplayed.Text, out score))
            {
                // sets the new score to the score + the score increment which is 20. this will add 20 points to the score
                score += score_increment;
                //this will display the score
                scoredisplayed.Text = (score).ToString();
                
            }
        }
    }
}
