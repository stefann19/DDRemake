namespace DDRemakeProject.World
{
    public class RoomSpace
    {
        #region Properties
        public int LeftX { get; set; }
        public int RightX { get; set; }
        public int TopY { get; set; }
        public int BotY { get; set; }

        #endregion

        #region Constructors

        public RoomSpace(int lX, int rX, int tY, int bY)
        {
            this.LeftX = lX;
            this.RightX = rX;
            this.TopY = tY;
            this.BotY = bY;
        }
        public RoomSpace()
        {

        }

        #endregion

        #region Methods

        public Point GetTopLeft()
        {
            return new Point(LeftX, TopY);
        }
        public Point GetSize()
        {
            return new Point(RightX - LeftX, BotY - TopY);
        }

        #endregion


    }
}
