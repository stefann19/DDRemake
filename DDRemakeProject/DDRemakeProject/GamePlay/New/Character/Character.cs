using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.Annotations;
using DDRemakeProject.GamePlay.New.Character.Logic;
using DDRemakeProject.GamePlay.Old;

namespace DDRemakeProject.GamePlay.New.Character
{
    public class Character:INotifyPropertyChanged
    {
        private string _name;

        public Character(CharacterLogic characterLogic)
        {
            CharacterLogic = characterLogic;
        }

        public Character(Traits traits)
        {
            CharacterLogic = new CharacterLogic(traits);
        }
        public Character(int strength, int agility, int intelligence, int endurance)
        {
            CharacterLogic = new CharacterLogic(new Traits(strength,agility,intelligence,endurance));
            /*CharacterUi = AvatarSpotsManager.GetSpot(CharacterTypes.Type.Ally);*/
        }
        public Character(Races race,int level,int strength, int agility, int intelligence, int endurance,string name="John")
        {
            CharacterLogic = new CharacterLogic(race,level,strength, agility, intelligence, endurance);
            Name = name;
            
            /*CharacterUi = AvatarSpotsManager.GetSpot(CharacterTypes.Type.Ally);*/
        }

        public CharacterLogic CharacterLogic { get; set; }
        public CharacterUi CharacterUi { get; set; }

        public string Name {
            get => _name;
            set {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
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
