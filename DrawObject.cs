using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    enum E_BlockType
    {
        Wall,
        OShape,
        IShape,
        TShape,
        ZShape,
        ZShape_M,
        JShape,
        JShape_M
    }

    class DrawObject : IDraw
    {
        private E_BlockType blockType;

        public Position pos;  
        public DrawObject(E_BlockType blockType)
        {
            this.blockType = blockType;
        }

        public DrawObject(E_BlockType blockType, int x, int y):this(blockType)
        {
            pos.x = x;
            pos.y = y;
        }


        public void Clear()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("  ");
        }

        public void Draw()
        {
            switch (blockType)
            {
                case E_BlockType.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_BlockType.OShape:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case E_BlockType.IShape:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_BlockType.TShape:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case E_BlockType.ZShape:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_BlockType.ZShape_M:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case E_BlockType.JShape:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_BlockType.JShape_M:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    break;
            }
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("■");

        }
    }
}
