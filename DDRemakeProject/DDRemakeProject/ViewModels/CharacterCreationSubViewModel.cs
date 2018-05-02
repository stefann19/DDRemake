using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDRemakeProject.GamePlay.New.Character;
using DDRemakeProject.GamePlay.New.Character.Logic;

namespace DDRemakeProject.ViewModels
{
    public class CharacterCreationSubViewModel
    {
        public CharacterCreationSubViewModel()
        {
        }

        public CharacterCreationSubViewModel( Character character)
        {
            Character = character;
            LevelSelectionViewModel = new LevelSelectionViewModel(character);
            StatsSelectionViewModel = new StatsSelectionViewModel();
        }

        public LevelSelectionViewModel LevelSelectionViewModel { get; set; }
        public StatsSelectionViewModel StatsSelectionViewModel { get; set; }

        public Character Character { get; set; }
    }
}
