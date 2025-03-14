using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class GameScence
    {
        UI ui;
        Wall wall;
        public BlocksWorker BW;

        public GameScence()
        {
            ui = new UI();
            wall = new Wall();
            BW = new BlocksWorker(wall);
            Input.Instance.InputEvent += CheckInput;
            BW.getPoints += ui.CountPoints;
            ConsoleSetup.Run();
            ui.SetupUI();

        }

        public void Play()
        {
            while (BW.GameOver())
            {
                switch (Program.nowScene)
                {
                    case E_SceneType.BeginScene:
                    case E_SceneType.OverScene:
                        ui.SetupUI();
                        break;
                    case E_SceneType.GameScene:
                        ui.SetupUI();
                        wall.Draw();
                        BW.DrawBlocks();
                        BW.DestroyBlocks();
                        lock (BW)
                        {
                            BW.ClearBlocks();
                            if (BW.CanMove())
                            {
                                BW.Move();
                            }
                            else
                            {
                                BW.GiveToDynamicWall();
                            }
                            //BW.GiveToDynamicWall(wall);
                            BW.DrawBlocks();
                        }
                        Thread.Sleep(100);
                        break;
                }
                
            }
        }

        private void CheckInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.R:
                    lock (BW)
                    {
                        if (BW.CanRotate())
                        {
                            BW.SetRotateState();
                        }

                    }
                    break;
                case ConsoleKey.A:
                    lock (BW)
                    {
                        if (BW.CanMove_L())
                        {
                            BW.Move_L();
                        }

                    }
                    break;
                case ConsoleKey.D:
                    lock (BW)
                    {
                        if (BW.CanMove_R())
                        {
                            BW.Move_R();
                        }
                    }
                    break;
                case ConsoleKey.S:
                    lock (BW)
                    {
                        if (BW.CanMove())
                        {
                            BW.Move();
                        }
                    }
                    break;
            }
        }
            
    }
}
