using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay
{
    class BattleEngine
    {
        public List<Character> AllyCharacters { get; private set; }
        public List<Character> EnemyCharacters { get; private set; }

        public BattleWindow BWindow { get; set; }

        public BattleEngine(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
            AllyCharacters = allyCharacters;
            EnemyCharacters = enemyCharacters;
            BWindow = new BattleWindow();
            GenerateMap();
        }

        private void GenerateMap()
        {
            BWindow.InitializeBattleUI(AllyCharacters,EnemyCharacters);
        }

        private void SelectCharacter()
        {
            
        }

        private void Battle()
        {
            
        }

    }
}
