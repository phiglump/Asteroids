using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class RunningState : IState
    {
        public int State(int isGameRunning)
        {
            isGameRunning = 1;
            return isGameRunning;
        }
    }
}
