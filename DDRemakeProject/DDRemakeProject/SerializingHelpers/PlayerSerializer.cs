using System.Windows;

namespace DDRemakeProject.SerializingHelpers
{
    public class PlayerSerializer
    {
        public PlayerSerializer(Vector position)
        {
            Position = position;
        }

        public Vector Position { get; set; }    
    }
}