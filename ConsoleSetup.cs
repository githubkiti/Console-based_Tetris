using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class ConsoleSetup
    {
        private static ConsoleSetup consoleWindow = new ConsoleSetup();
        private static int height = 60;
        private static int width = 60;

        public static int Height
        {
            get => height;
        }

        public static int Width
        {
            get => width;
        }

        private ConsoleSetup()
        {
            
        }

        public static void Run()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }
    }
}
