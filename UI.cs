using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    enum E_SceneType
    {
        BeginScene,
        GameScene,
        OverScene
    }
    class UI
    {
        private int points;
        private int choseCount;
        private string title;
        private string startOrContinueGame;
        private string exitGame;

        public UI()
        {
            points = 0;
            choseCount = 0;
            title = "俄罗斯方块";
            startOrContinueGame = "开始游戏";
            exitGame = "退出游戏";
        }

        public void SetupUI()
        {
            switch (Program.nowScene)
            {
                case E_SceneType.BeginScene:
                    Console.Clear();
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - title.Length, ConsoleSetup.Height - 35);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(title);
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - startOrContinueGame.Length, ConsoleSetup.Height - 30);
                    Console.ForegroundColor = choseCount == 0 ?  ConsoleColor.Red : ConsoleColor.White;
                    Console.Write(startOrContinueGame);
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - exitGame.Length, ConsoleSetup.Height - 28);
                    Console.ForegroundColor = choseCount == 0 ? ConsoleColor.White : ConsoleColor.Red;
                    Console.Write(exitGame);
                    Chose();
                    break;
                case E_SceneType.GameScene:
                    Console.Clear();
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 14);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("得分：");
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 13);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(points);
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 10);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("操作说明：");
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 8);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("A、D键左右移动 <-- -->");
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 6);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("S键向下加速移动");
                    Console.SetCursorPosition(2, ConsoleSetup.Height - 4);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("R键旋转方块");
                    break;
                case E_SceneType.OverScene:
                    Console.Clear();
                    title = "游戏结束";
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - title.Length, ConsoleSetup.Height - 35);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(title);
                    startOrContinueGame = "继续游戏";
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - startOrContinueGame.Length, ConsoleSetup.Height - 30);
                    Console.Write(startOrContinueGame);
                    Console.SetCursorPosition(ConsoleSetup.Width / 2 - exitGame.Length, ConsoleSetup.Height - 28);
                    Console.Write(exitGame);
                    Chose();
                    break;
            }
        }
        
        private void Chose()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --choseCount;
                    if (choseCount < 0)
                    {
                        choseCount = 0;
                    }
                    break;
                case ConsoleKey.S:
                    --choseCount;
                    if (choseCount > 1)
                    { 
                        choseCount = 1;
                    }
                    break;
                case ConsoleKey.J:
                    if (choseCount == 0)
                    {
                        Program.nowScene = E_SceneType.GameScene;
                        Console.Clear();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
            }
        }

        public void CountPoints()
        {
            Console.SetCursorPosition(2, ConsoleSetup.Height - 13);
            Console.Write("      ");
            ++points;
            Console.SetCursorPosition(2, ConsoleSetup.Height - 13);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(points);
        }
    }
}
