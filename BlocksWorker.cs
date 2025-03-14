using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class BlocksWorker
    {

        public event Action getPoints;
        private Wall wall;
        int[] counts = new int[ConsoleSetup.Height - 16];
        /// <summary>
        /// blocks轴心块位置
        /// </summary>
        Position piviotBlockPos;
        /// <summary>
        /// 下一帧轴心方块的位置
        /// </summary>
        Position nextPiviotBlockPos;

        /// <summary>
        /// 下落方块集合
        /// </summary>
        List<DrawObject> blocks;
        Random r = new Random();
        int randomType = 0;
        int nowRotateState = 0;
        int nextRotateState = 0;

        /// <summary>
        /// 当前这一帧的形态
        /// </summary>
        E_BlockType nowType;
        /// <summary>
        /// 方块下一帧要移动到的位置形态
        /// </summary>
        List<DrawObject> nextBlocks;
        /// <summary>
        /// 方块下一帧要移动到的变换形态
        /// </summary>
        List<DrawObject> nextBlocks_Ro;

        /// <summary>
        /// 要被删除的动态墙壁方块
        /// </summary>
        List<DrawObject> deleteBlocks = new List<DrawObject>();

        public BlocksWorker(Wall wall)
        {
            this.wall = wall;
            SetBlocks();
        }
        
        /// <summary>
        /// 初始化方块信息
        /// </summary>
        private void SetBlocks()
        {
            //初始化轴心块的位置
            piviotBlockPos = RandomPiviot();
            nextPiviotBlockPos = piviotBlockPos;

            blocks = new List<DrawObject>();
            nextBlocks = new List<DrawObject>();
            nextBlocks_Ro = new List<DrawObject>();

            nowType = RandomType();
            for (int i = 0; i < 4; i++)
            {
                blocks.Add(new DrawObject(nowType));
                nextBlocks.Add(new DrawObject(nowType));
                nextBlocks_Ro.Add(new DrawObject(nowType));
            }
        }

        /// <summary>
        /// 随机blocks类型
        /// </summary>
        private E_BlockType RandomType()
        {
            randomType = r.Next(1, 8);
            if (randomType < 1)
            {
                randomType = 1;
            }
            else if (randomType > 7)
            {
                randomType = 7;
            }
            return (E_BlockType)randomType;
        }

        /// <summary>
        /// 设置旋转状态
        /// </summary>
        public void SetRotateState()
        {
            if (nowType  == E_BlockType.OShape )
            {
                return;
            }
            ++nowRotateState;
            nextRotateState = nowRotateState + 1;
            if (nowRotateState > 3)
                nowRotateState = 0;
            else if (nowRotateState < 0)
                nowRotateState = 0;

            if (nextRotateState > 3)
                nextRotateState = 0;
            else if (nextRotateState < 0)
                nextRotateState = 0;
        }

        /// <summary>
        /// 设置Blocks信息
        /// </summary>
        public void SetBlocksInfo()
        {
            //设置轴心方块位置
            blocks[0].pos = piviotBlockPos;
            nextBlocks[0].pos = nextPiviotBlockPos;
            nextBlocks_Ro[0].pos = piviotBlockPos;
            if (nowType == E_BlockType.OShape)
            {
                for (int i = 1; i < 4; i++)
                {
                    blocks[i].pos = blocks[0].pos + ObjectInfo.ChangerInfo[nowType][0][i - 1];
                    nextBlocks[i].pos = nextBlocks[0].pos + ObjectInfo.ChangerInfo[nowType][0][i - 1];
                }
            }
            else
            {
                for (int i = 1; i < 4; i++)
                {
                    //防止索引越界
                    //if (i <= 0)
                    //{
                    //    i = 1;
                    //}
                    //else if (i > 3)
                    //{
                    //    i = 3;
                    //}

                    blocks[i].pos = blocks[0].pos + ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1];
                    nextBlocks[i].pos = nextBlocks[0].pos + ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1];
                    nextBlocks_Ro[i].pos = nextBlocks_Ro[0].pos + ObjectInfo.ChangerInfo[nowType][nextRotateState][i - 1];
                }
            }
        }

        /// <summary>
        /// 传递到动态墙壁
        /// </summary>
        /// <param name="wall"></param>
        public void GiveToDynamicWall()
        {
            //if (CanMove(wall) != false)
            //{
            //    return;
            //}
            foreach (var item in blocks)
            {
                item.blockType = E_BlockType.Wall;
                wall.dynamicWalls.Add(item);
            }
            SetBlocks();
            wall.DrawDynamic();
        }

        /// <summary>
        /// 随机设置开始位置
        /// </summary>
        /// <returns></returns>
        public Position RandomPiviot()
        {
            int x = r.Next(4, ConsoleSetup.Width - 6);
            if (x % 2 != 0)
            {
                ++x;
            }
            int y = -4;
            return new Position(x, y);
        }

        /// <summary>
        /// 清除上一帧blocks画面
        /// </summary>
        public void ClearBlocks()
        {
            foreach (var item in blocks)
            {
                if (item.pos.y < 0)
                {
                    return;
                }
                item.Clear();
            }
        }

        public bool CanRotate()
        {
            bool temp = true;

            // 檢查邊界
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var item = nextBlocks[i];
                if (item.pos.x >= ConsoleSetup.Width - 4 ||
                    item.pos.y > ConsoleSetup.Height - 17 ||
                    item.pos.x <= 2)
                {
                    return false;
                }
            }

            // 檢查靜態牆體衝突
            for (int i = 0; i < nextBlocks_Ro.Count; i++)
            {
                var b = nextBlocks_Ro[i];
                for (int j = 0; j < wall.staticWalls_Below.Count; j++)
                {
                    var w = wall.staticWalls_Below[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;

                for (int j = 0; j < wall.staticWalls_R.Count; j++)
                {
                    var w = wall.staticWalls_R[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;

                for (int j = 0; j < wall.staticWalls_L.Count; j++)
                {
                    var w = wall.staticWalls_L[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;
            }

            if (!temp) return temp;

            // 檢查動態牆體衝突
            for (int i = 0; i < nextBlocks_Ro.Count; i++)
            {
                var b = nextBlocks_Ro[i];
                for (int j = 0; j < wall.dynamicWalls.Count; j++)
                {
                    var w = wall.dynamicWalls[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;
            }

            return temp;
        }

        public bool CanMove()
        {
            bool temp = true;

            // 檢查底部邊界
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var item = nextBlocks[i];
                if (item.pos.y > ConsoleSetup.Height - 17)
                {
                    return false;
                }
            }

            // 檢查牆體衝突
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var b = nextBlocks[i];
                for (int j = 0; j < wall.dynamicWalls.Count; j++)
                {
                    var w = wall.dynamicWalls[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;

                for (int j = 0; j < wall.staticWalls_Below.Count; j++)
                {
                    var w = wall.staticWalls_Below[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;
            }

            return temp;
        }

        public bool CanMove_R()
        {
            bool temp = true;

            // 檢查右邊界
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var item = nextBlocks[i];
                if (item.pos.x >= ConsoleSetup.Width - 4)
                {
                    return false;
                }
            }

            // 檢查牆體衝突
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var b = nextBlocks[i];
                for (int j = 0; j < wall.dynamicWalls.Count; j++)
                {
                    var w = wall.dynamicWalls[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;

                for (int j = 0; j < wall.staticWalls_R.Count; j++)
                {
                    var w = wall.staticWalls_R[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;
            }

            return temp;
        }

        public bool CanMove_L()
        {
            bool temp = true;

            // 檢查左邊界
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var item = nextBlocks[i];
                if (item.pos.x <= 2)
                {
                    return false;
                }
            }

            // 檢查牆體衝突
            for (int i = 0; i < nextBlocks.Count; i++)
            {
                var b = nextBlocks[i];
                for (int j = 0; j < wall.dynamicWalls.Count; j++)
                {
                    var w = wall.dynamicWalls[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;

                for (int j = 0; j < wall.staticWalls_L.Count; j++)
                {
                    var w = wall.staticWalls_L[j];
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
                if (!temp) break;
            }

            return temp;
        }

        /// <summary>
        /// 是否可以旋转
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        
        //public bool CanRotate()
        //{
        //    bool temp = true;
        //    foreach (var item in nextBlocks)
        //    {
        //        if (item.pos.x >= ConsoleSetup.Width - 4 || item.pos.y > ConsoleSetup.Height - 17 || item.pos.x <= 2)
        //        {
        //            return false;
        //        }
        //    }
        //    foreach (var b in nextBlocks_Ro)
        //    {
        //        foreach (var w in wall.staticWalls_Below)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //        foreach (var w in wall.staticWalls_R)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //        foreach (var w in wall.staticWalls_L)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //    }
        //    if (!temp)
        //    {
        //        return temp;
        //    }
        //    foreach (var b in nextBlocks_Ro)
        //    {
        //        foreach (var w in wall.dynamicWalls)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //    }
        //    return temp;
        //}

        ///// <summary>
        ///// 是否能移动
        ///// </summary>
        ///// <param name="wall"></param>
        ///// <returns></returns>
        //public bool CanMove()
        //{
        //    bool temp = true;
        //    //判断一开始是否重叠
        //    foreach (var item in nextBlocks)
        //    {
        //        if (item.pos.y > ConsoleSetup.Height - 17)
        //        {
        //            return false;
        //        }
        //    }
        //    foreach (var b in nextBlocks)
        //    {
        //        foreach (var w in wall.dynamicWalls)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //        foreach (var w in wall.staticWalls_Below)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }

        //    }
        //    return temp;
        //}

        //public bool CanMove_R()
        //{
        //    bool temp = true;
        //    foreach (var item in nextBlocks)
        //    {
        //        if (item.pos.x >= ConsoleSetup.Width - 4)
        //        {
        //            return false; 
        //        }
        //    }

        //    foreach (var b in nextBlocks)
        //    {
        //        foreach (var w in wall.dynamicWalls)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //        foreach (var w in wall.staticWalls_R)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }

        //    }
        //    return temp;
        //}

        //public bool CanMove_L()
        //{
        //    bool temp = true;
        //    foreach (var item in nextBlocks)
        //    {
        //        if (item.pos.x <= 2)
        //        {
        //            return false;
        //        }
        //    }

        //    foreach (var b in nextBlocks)
        //    {
        //        foreach (var w in wall.dynamicWalls)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }
        //        foreach (var w in wall.staticWalls_L)
        //        {
        //            if (b.pos == w.pos)
        //            {
        //                temp = false;
        //                break;
        //            }
        //        }

        //    }
        //    return temp;
        //}

        /// <summary>
        /// 自动下落
        /// </summary>
        public void Move()
        {
            if (piviotBlockPos.y > ConsoleSetup.Height - 17)
            {
                return;
            }
            piviotBlockPos = piviotBlockPos + new Position(0, 1);
            nextPiviotBlockPos = piviotBlockPos + new Position(0, 1);
        }

        /// <summary>
        /// 右移
        /// </summary>
        public void Move_R()
        {
            if (piviotBlockPos.x > ConsoleSetup.Width - 6)
            {
                return;
            }
            piviotBlockPos = piviotBlockPos + new Position(2,0);
            nextPiviotBlockPos = piviotBlockPos + new Position(2, 0);
        }

        /// <summary>
        /// 左移
        /// </summary>
        public void Move_L()
        {
            if ( piviotBlockPos.x < 2)
            {
                return;
            }
            piviotBlockPos = piviotBlockPos - new Position(2, 0);
            nextPiviotBlockPos = piviotBlockPos - new Position(2, 0);
        }

        /// <summary>
        /// 渲染Blocks
        /// </summary>
        public void DrawBlocks()
        {
            //渲染之前检测是否碰撞
            //是则转换成动态墙壁
            //GiveToDynamicWall(wall);
            SetBlocksInfo();
            foreach (var item in blocks)
            {
                //如果在画面之外就不渲染
                if (item.pos.y < 0 )
                {
                    return;
                }
                item.Draw();
            }
        }
        
        /// <summary>
        /// 消除方块
        /// </summary>
        /// <param name="wall"></param>
        public void DestroyBlocks()
        {
            //Console.WriteLine($"容量是{counts.Length}.....");
            //每次从0开始计数（每行方块的个数）
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 0;
            }
            //把每行拥有的方块数量存储在数组中，数组的索引即是行号
            foreach (var item in wall.dynamicWalls)
            {
                for (int i = 0; i < counts.Length; i++)
                {
                    if (item.pos.y == i)
                    {
                        ++counts[i];
                        //Console.WriteLine("计数+1");
                    }
                }
            }
            //删除满一行的方块并下移一行
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] == (ConsoleSetup.Width - 4) / 2)
                {
                    getPoints?.Invoke();
                    //Console.WriteLine("满一行");
                    for (int j = 0; j < wall.dynamicWalls.Count; j++)
                    {
                        if (wall.dynamicWalls[j].pos.y == i)
                        {
                            wall.dynamicWalls[j].Clear();
                            deleteBlocks.Add(wall.dynamicWalls[j]);
                            //不能边遍历边remove列表中的元素，否则count发生变化提前跳出循环
                            //先把要删除的元素存储到另一个容器
                        }
                    }
                    //遍历这个容器，在原来的容器中执行增删查改
                    foreach (var item in deleteBlocks)
                    {
                        wall.dynamicWalls.Remove(item);
                    }
                    deleteBlocks.Clear();
                    //消除方块的上一行往下位移一行
                    foreach (var item in wall.dynamicWalls)
                    {
                        if (item.pos.y < i)
                        {
                            //擦除旧方块的显示画面
                            item.Clear();
                            item.pos.y += 1;
                        }
                    }
                }
            }
            //更新动态墙壁绘制
            wall.DrawDynamic();
        }
        
        /// <summary>
        /// 结束逻辑
        /// </summary>
        public bool GameOver()
        {
            if (wall.dynamicWalls.Count > 0)
            {
                foreach (var item in wall.dynamicWalls)
                {
                    if (item.pos.y <= 1 
                        || Program.nowScene == E_SceneType.BeginScene 
                        || Program.nowScene == E_SceneType.OverScene)
                    {
                        Program.nowScene = E_SceneType.OverScene;
                        return true;
                    }
                }
            }
            return true;
        }
    }
}
