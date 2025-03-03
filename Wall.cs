using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class Wall : IDraw
    {
        List<DrawObject> staticWalls;
        List<DrawObject> dynamicWalls;
        public Wall()
        {
            //实例化静态墙壁的储存容器并添加墙壁
            staticWalls = new List<DrawObject>();
            for (int i = 0; i < ConsoleSetup.Height - 15; i++)
            {
                staticWalls.Add(new DrawObject(E_BlockType.Wall, 0, i));
                staticWalls.Add(new DrawObject(E_BlockType.Wall, ConsoleSetup.Width - 2, i));
            }
            for (int i = 2; i < ConsoleSetup.Width; i+=2)
            {
                staticWalls.Add(new DrawObject(E_BlockType.Wall, i, ConsoleSetup.Height - 16));
            }
            //实例化动态墙壁容器
            dynamicWalls = new List<DrawObject>();
        }




        //绘制墙壁
        public void Draw()
        {
            foreach (var item in staticWalls)
            {
                item.Draw();
            }
        }
    }
}
