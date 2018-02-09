using System.Windows;

namespace DDRemakeProject.World
{
    public class RoomSpace
    {
        #region Properties

        public int LeftX => (int)(Position.X - Size.X / 2f);
        public int RightX => (int)(Position.X + Size.X / 2f);
        public int TopY => (int)(Position.Y + Size.Y / 2f);
        public int BotY => (int)(Position.Y - Size.Y / 2f);

        public int Area => (int) (Size.X * Size.Y);

        /// <summary>
        /// Middle Position
        /// </summary>
        public Vector Position { get; set; }
        public Vector Size { get; set; }

        #endregion

        #region Constructors

        public RoomSpace(Vector position,Vector size)
        {
            this.Position = position;
            this.Size = size;
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
