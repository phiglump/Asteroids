using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Score : GameScreen
    {
        int score = 0;
        const int score_increment = 20;
        private void GameScore(object sender, EventArgs e)
        {
            if (true == int.TryParse(scoredisplayed.Text, out score))
            {
                score = score + score_increment;
                scoredisplayed.Text = (score).ToString();
                
            }
        }
    }
}
