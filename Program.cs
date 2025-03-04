using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class Program
    {
        static void Main(string[] args)
        {
            string lock_s = "";
            ConsoleSetup.Run();
            Wall wall = new Wall();
            wall.Draw();
            BlocksWorker BW = new BlocksWorker();
            BW.DrawBlocks();
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            lock (lock_s)
                            {
                                BW.SetRotateState();
                            }
                            break;
                    }
                }
            });
            t.Start();
            while (true)
            {

                //lock () { }
                
                Thread.Sleep(200);
                if (BW.CanMove(wall))
                {
                    BW.Move();
                }
                
                BW.ClearBlocks();
                BW.DrawBlocks();
                
            }
        }
        
    }
}
