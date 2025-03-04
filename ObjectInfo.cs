using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    class ObjectInfo
    {
        private static ObjectInfo oi = new ObjectInfo();
        private List<Position[]> relativePos_O;
        private List<Position[]> relativePos_I;
        private List<Position[]> relativePos_T;
        private List<Position[]> relativePos_Z;
        private List<Position[]> relativePos_ZM;
        private List<Position[]> relativePos_J;
        private List<Position[]> relativePos_JM;
        private static Dictionary<E_BlockType,List<Position[]>> changerInfo;
        private ObjectInfo()
        {

            #region relativePos List
            //初始化所有变形信息
            relativePos_O = new List<Position[]>();
            relativePos_I = new List<Position[]>();
            relativePos_T = new List<Position[]>();
            relativePos_Z = new List<Position[]>();
            relativePos_ZM = new List<Position[]>();
            relativePos_J = new List<Position[]>();
            relativePos_JM = new List<Position[]>();
            #endregion

            #region List添加Position数组信息
            //添加每种变形方块的相对坐标
            relativePos_O.Add(new Position[3]
                    {
                        new Position(2, 0),
                        new Position(0, 1),
                        new Position(2, 1),
                    });

            relativePos_I.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(0, 1),
                        new Position(0, 2),
                    });
            relativePos_I.Add(new Position[3]
            {
                        new Position(-4, 0),
                        new Position(-2, 0),
                        new Position(2, 0),
            });
            relativePos_I.Add(new Position[3]
            {
                        new Position(0,-2),
                        new Position(0, -1),
                        new Position(0, 1),
            });
            relativePos_I.Add(new Position[3]
            {
                        new Position(-2, 0),
                        new Position(2, 0),
                        new Position(4, 0),
            });

            relativePos_T.Add(new Position[3]
                    {
                        new Position(-2, 0),
                        new Position(2, 0),
                        new Position(0, 1),
                    });
            relativePos_T.Add(new Position[3]
            {
                        new Position(0,-1),
                        new Position(-2, 0),
                        new Position(0, 1),
            });
            relativePos_T.Add(new Position[3]
            {
                        new Position(0,-1),
                        new Position(-2, 0),
                        new Position(2, 0),
            });
            relativePos_T.Add(new Position[3]
            {
                        new Position(0,-1),
                        new Position(2, 0),
                        new Position(0, 1),
            });

            relativePos_Z.Add(new Position[3]
                    {
                        new Position(0, -1),
                        new Position(2, 0),
                        new Position(2, 1),
                    });
            relativePos_Z.Add(new Position[3]
            {
                        new Position(2, 0),
                        new Position(-2, 1),
                        new Position(0, 1),
            });
            relativePos_Z.Add(new Position[3]
            {
                        new Position(-2, -1),
                        new Position(-2, 0),
                        new Position(0, 1),
            });
            relativePos_Z.Add(new Position[3]
            {
                        new Position(0, -1),
                        new Position(2, -1),
                        new Position(-2, 0),
            });

            relativePos_ZM.Add(new Position[3]
                    {
                        new Position(0, -1),
                        new Position(-2, 0),
                        new Position(-2, 1),
                    });
            relativePos_ZM.Add(new Position[3]
            {
                        new Position(-2, -1),
                        new Position(0, 1),
                        new Position(2, 0),
            });
            relativePos_ZM.Add(new Position[3]
            {
                        new Position(-2, -1),
                        new Position(-2, 0),
                        new Position(0, 1),
            });
            relativePos_ZM.Add(new Position[3]
            {
                        new Position(-2, 0),
                        new Position(0, 1),
                        new Position(2, 1),
            });

            relativePos_J.Add(new Position[3]
                    {
                        new Position(-2, -1),
                        new Position(0, -1),
                        new Position(0, 1),
                    });
            relativePos_J.Add(new Position[3]
            {
                        new Position(2, 1),
                        new Position(2, 0),
                        new Position(-2, 0),
            });
            relativePos_J.Add(new Position[3]
            {
                        new Position(0, -1),
                        new Position(0, 1),
                        new Position(2, 1),
            });
            relativePos_J.Add(new Position[3]
            {
                        new Position(-2, 0),
                        new Position(2, 0),
                        new Position(-2, 1),
            });

            relativePos_JM.Add(new Position[3]
                    {
                        new Position(0, -1),
                        new Position(2, -1),
                        new Position(0, 1),
                    });
            relativePos_JM.Add(new Position[3]
            {
                        new Position(-2, 0),
                        new Position(2, 0),
                        new Position(2, 1),
            });
            relativePos_JM.Add(new Position[3]
            {
                        new Position(0, -1),
                        new Position(-2, 1),
                        new Position(0, 1),
            });
            relativePos_JM.Add(new Position[3]
            {
                        new Position(-2, 0),
                        new Position(-2, -1),
                        new Position(2, 0),
            });
            #endregion

            #region 通过字典访问不同类型的变形信息
            changerInfo = new Dictionary<E_BlockType, List<Position[]>>();
            changerInfo.Add(E_BlockType.OShape, relativePos_O);
            changerInfo.Add(E_BlockType.IShape, relativePos_I);
            changerInfo.Add(E_BlockType.TShape, relativePos_T);
            changerInfo.Add(E_BlockType.ZShape, relativePos_Z);
            changerInfo.Add(E_BlockType.ZShape_M, relativePos_ZM);
            changerInfo.Add(E_BlockType.JShape, relativePos_J);
            changerInfo.Add(E_BlockType.JShape_M, relativePos_JM);
            #endregion

        }

        public static Dictionary<E_BlockType, List<Position[]>> ChangerInfo
        {
            get => changerInfo;
        }
    }
}
