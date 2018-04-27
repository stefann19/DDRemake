using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.GamePlay.New;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.GamePlay.New.Character.Logic;

namespace DDRemakeProject.ViewModels
{
    public class CharacterCreationViewModel
    {
        public CharacterCreationViewModel()
        {
            CharacterLogicCollection = new ObservableCollection<Character>(){ new Character(Races.Bat, 12, 10, 10, 10, 10,"bat") ,
                new Character(Races.Bowman, 8, 10, 10, 10, 10,"bowman"),new Character(Races.Boar, 6, 10, 10, 10, 10,"boar") };
        }

        public ObservableCollection<Character> CharacterLogicCollection { get; set; }
    }
}
