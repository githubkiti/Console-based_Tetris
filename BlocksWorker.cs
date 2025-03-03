using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class BlocksWorker
    {
        List<DrawObject> blocks;
        Random r = new Random();
        int randomType = 1;
        E_BlockType nowType;
        public BlocksWorker()
        {
            blocks = new List<DrawObject>();
            
        }

        private E_BlockType RandomType()
        {
            randomType = r.Next(1, 8);
            return (E_BlockType)randomType;
        }

        public void SetBlocksInfo()
        {
            nowType = RandomType();
            for (int i = 0; i < 4; i++)
            {
                blocks.Add(new DrawObject(nowType));
            }

            blocks[0].pos.x = 10;
            blocks[0].pos.y = 20;

            for (int i = 1; i < 4; i++)
            {
                blocks[i].pos = blocks[0].pos + ObjectInfo.ChangerInfo[RandomType()][0][i-1];
            }

        }

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
