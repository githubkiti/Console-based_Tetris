using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 俄罗斯方块
{
    class Input
    {
        private static Input instance = new Input();

        public event Action InputEvent;

        private Thread InputThread;

        public static Input Instance
        {
            get => instance;
        }

        private Input() 
        {
            InputThread = new Thread(CheckInp);
            InputThread.IsBackground = true;
            InputThread.Start();
        }

        private void CheckInp()
        {
            while (true)
            {
                if (Program.nowScene == E_SceneType.GameScene)
                {
                    InputEvent?.Invoke();
                }
            }
        }

    }
}
