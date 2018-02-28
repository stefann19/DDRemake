using System.Windows;
using System.Windows.Controls;

namespace DDRemakeProject.GamePlay.Old
{
    public class UIBackEnd
    {
        private BattleEngine _battleEngine;
        public Grid _popOutWindow;
        private CharacterMiniWindow pop;

        public UIBackEnd(BattleEngine battleEngine)
        {
            _battleEngine = battleEngine;
        }

        public void SetPopOutWindow(Grid popOutWindow)
        {
            //pop = new PopOutWindow(popOutWindow);
            _popOutWindow = popOutWindow;
            PopOutQuit();
        }

        public void PopedChar(int index)
        {
            _popOutWindow.Visibility = Visibility.Visible;
            //pop.PopOutChararacter = index < 3 ? _battleEngine.AllyCharacters.ElementAt(index) : _battleEngine.EnemyCharacters.ElementAt(index-3);
        }

        public void PopOutQuit()
        {
            _popOutWindow.Visibility = Visibility.Hidden;
        }
    }
}