using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class PausedState : IState
    {
        public int State(int isGameRunning)
        {
            isGameRunning = 0;
            return isGameRunning;
        }
    }
}
