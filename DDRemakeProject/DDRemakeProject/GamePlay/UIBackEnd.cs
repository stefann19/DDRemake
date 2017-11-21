using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DDRemakeProject.GamePlay
{
    public class UIBackEnd
    {
        private PopOutWindow pop;
        private BattleEngine _battleEngine;
        public  Grid _popOutWindow;

        public UIBackEnd(BattleEngine battleEngine)
        {
            _battleEngine = battleEngine;
            
        }

        public void SetPopOutWindow(Grid popOutWindow)
        {
            pop = new PopOutWindow(popOutWindow);
            _popOutWindow = popOutWindow;
            PopOutQuit();
        }

        public void PopedChar(int index)
        {
            _popOutWindow.Visibility = Visibility.Visible;
            pop.PopOutChararacter = index < 3 ? _battleEngine.AllyCharacters.ElementAt(index) : _battleEngine.EnemyCharacters.ElementAt(index-3);
        }

        public void PopOutQuit()
        {
            _popOutWindow.Visibility = Visibility.Hidden;
        }
    }
}
