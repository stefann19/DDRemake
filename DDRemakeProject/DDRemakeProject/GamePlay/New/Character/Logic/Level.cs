using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.Annotations;

namespace DDRemakeProject.GamePlay.New.Character.Logic
{
    public class Level :INotifyPropertyChanged
    {
        private int _currentXp;
        private int _currentLevel;

        public CharacterLogic CharacterLogic { get; set; }
        public Level(CharacterLogic characterLogic,int currentLevel, Func<double> calculate)
        {
            CharacterLogic = characterLogic;
            _currentLevel = currentLevel;
            Calculate = calculate;
            _currentXp = 0;
        }

        public int CurrentLevel {
            get => _currentLevel;
            set {
                if (value == _currentLevel) return;
                _currentLevel = value;
                OnPropertyChanged();
                CharacterLogic.LevelUp();
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
