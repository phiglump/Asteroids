using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Score : GameOverScreen
    {
        int score = 0;
        public void getScore()
        {
            if (true == int.TryParse(finalscore.Text, out score))
            {
                //this will display the score
                finalscore.Text = (score).ToString();

            }
        }
    }
}
