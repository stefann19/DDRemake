using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Level
    {
        private int _currentXp;

        public CharacterLogic CharacterLogic { get; set; }
        public Level(CharacterLogic characterLogic,int currentLevel, Func<double> calculate)
        {
            CurrentLevel = currentLevel;
            Calculate = calculate;
            _currentXp = 0;

        }

        public int CurrentLevel { get; set; }
        public int TargetXp { get; set; }
        public Func<double> Calculate { get; set; }


        public int CurrentXp
        {
            get => _currentXp;
            set
            {
                _currentXp = value;
                if (TargetXp < _currentXp)
                {
                    _currentXp = TargetXp - CurrentXp;
                    TargetXp = TargetXp + 2000;
                    CurrentLevel++;
                    CharacterLogic.LevelUp();
                }
            }

        }
    }
}
