using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    struct Position
    {
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Position v1,Position v2)
        {
            return v1.x == v2.x && v1.y == v2.y;

        }

        public static bool operator !=(Position v1, Position v2)
        {

            return !(v1 == v2);
        }

        public static Position operator +(Position v1,Position v2)
        {
            return new Position(v1.x + v2.x, v1.y + v2.y);
        }

        public static Position operator -(Position v1, Position v2)
        {
            return new Position(v1.x - v2.x, v1.y - v2.y);
        }
    }
}
