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
        public static E_SceneType nowScene = E_SceneType.BeginScene;
        static void Main(string[] args)
        {
            GameScence gameScence = new GameScence();
            gameScence.Play();
            
        }
    }
}
