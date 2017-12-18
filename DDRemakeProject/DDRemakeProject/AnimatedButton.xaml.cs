using System.Windows.Controls;
using DDRemakeProject.GamePlay;
using Action = DDRemakeProject.GamePlay.Action;

namespace DDRemakeProject
{
    /// <summary>
    /// Interaction logic for AnimatedButton.xaml
    /// </summary>
    public partial class AnimatedButton : UserControl
    {
        public ActionTypes.ActionType ActionType  { get; set; }
        public Action Action { get; set; }
        public BattleEngine BattleEngine { get; set; }
        public AnimatedButton()
        {
            InitializeComponent();
        }
    }
}
