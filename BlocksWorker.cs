using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class BlocksWorker
    {
        /// <summary>
        /// blocks轴心块位置
        /// </summary>
        Position piviotBlockPos;

        Position nextPiviotBlockPos;
        public bool isMove = true;
        List<DrawObject> blocks;
        Random r = new Random();
        int randomType = 0;
        int nowRotateState = 0;
        E_BlockType nowType;
        List<DrawObject> nextBlocks = new List<DrawObject>();
        public BlocksWorker()
        {
            piviotBlockPos = new Position(10, 5);
            blocks = new List<DrawObject>();
            nowType = RandomType();
            for (int i = 0; i < 4; i++)
            {
                blocks.Add(new DrawObject(nowType));
                nextBlocks.Add(new DrawObject(nowType));
            }
        }

        /// <summary>
        /// 随机blocks类型
        /// </summary>
        private E_BlockType RandomType()
        {
            randomType = r.Next(1, 8);
            return (E_BlockType)randomType;
        }

        /// <summary>
        /// 设置旋转状态
        /// </summary>
        public void SetRotateState()
        {
            if (nowType  == E_BlockType.OShape)
            {
                return;
            }
            ++nowRotateState;
            if (nowRotateState > 3)
                nowRotateState = 0;
        }

        /// <summary>
        /// 设置Blocks信息
        /// </summary>
        public void SetBlocksInfo()
        {
            nextPiviotBlockPos = piviotBlockPos + new Position(0, 1);
            //调试设置的轴心方块位置
            blocks[0].pos = piviotBlockPos;
            nextBlocks[0].pos = nextPiviotBlockPos;
            for (int i = 1; i < 4; i++)
            {
                //Console.WriteLine(ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1].x);
                //Console.WriteLine(ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1].y);
                blocks[i].pos = blocks[0].pos + ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1];
                nextBlocks[i].pos = nextBlocks[0].pos + ObjectInfo.ChangerInfo[nowType][nowRotateState][i - 1];
            }
        }

        /// <summary>
        /// 清除上一帧blocks画面
        /// </summary>
        public void ClearBlocks()
        {
            foreach (var item in blocks)
            {
                item.Clear();
            }
        }

        /// <summary>
        /// 是否能移动
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        public bool CanMove(Wall wall)
        {
            bool temp = true;
            foreach (var b in nextBlocks)
            {
                foreach (var w in wall.staticWalls)
                {
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
            }
            if (!temp)
            {
                return false;
            }
            foreach (var b in nextBlocks)
            {
                foreach (var w in wall.dynamicWalls)
                {
                    if (b.pos == w.pos)
                    {
                        temp = false;
                        break;
                    }
                }
            }
            return temp;
        }

        /// <summary>
        /// 自动下落
        /// </summary>
        public void Move()
        {
            piviotBlockPos = piviotBlockPos + new Position(0, 1);
            
        }

        /// <summary>
        /// 渲染Blocks
        /// </summary>
        public void DrawBlocks()
        {
            SetBlocksInfo();
            foreach (var item in blocks)
            {
                item.Draw();
            }
        }
        
    }
}
