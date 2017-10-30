using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject
{
    public class RoomSpace
    {
        public int LeftX { get; set; }
        public int RightX { get; set; }
        public int TopY { get; set; }
        public int BotY { get; set; }

        public RoomSpace(int lX,int rX,int tY,int bY)
        {
            this.LeftX = lX;
            this.RightX = rX;
            this.TopY = tY;
            this.BotY = bY;
        }
        public RoomSpace()
        {

        }

        public Point GetTopLeft()
        {
            return new Point(LeftX, TopY);
        }
        public Point GetSize()
        {
            return new Point(RightX-LeftX, BotY-TopY);
        }
    }
}
