using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DDRemakeProject.Annotations;
using DDRemakeProject.Commands;
using DDRemakeProject.GamePlay.New.Character;

namespace DDRemakeProject.ViewModels
{
    public class LevelSelectionViewModel 
    {
       
        public ICommand LowerValueCommand { get; set; }

        public LevelSelectionViewModel()
        {

            LowerValueCommand = new RelayCommand(param =>
            {
                Character ch = ((object[])param)[0] as Character;
                ch.CharacterLogic.Level.CurrentLevel--;
            }, param =>
            {
                Character ch = ((object[])param)[0] as Character;
                return ch?.CharacterLogic.Level.CurrentLevel > 1;
            });

            RaiseValueCommand = new RelayCommand(param =>
            {
                Character ch = ((object[])param)[0] as Character;
                ch.CharacterLogic.Level.CurrentLevel++;
            }, param =>
            {
                Character ch = ((object[])param)[0] as Character;
                return ch?.CharacterLogic.Level.CurrentLevel < 20;
            });

           /* LowerValueCommand = new RelayCommand(param =>
            {
/*
                Character ch = ((object[])param)[0] as Character;
#1#
                Character.CharacterLogic.Level.CurrentLevel--;
            }, param =>
            {
/*
                Character ch = ((object[])param)[0] as Character;
#1#
                return Character?.CharacterLogic.Level.CurrentLevel > 1;
            });

            RaiseValueCommand = new RelayCommand(param =>
            {
/*
                Character ch = ((object[])param)[0] as Character;
#1#
                Character.CharacterLogic.Level.CurrentLevel++;
            }, param =>
            {
/*
                Character ch = ((object[])param)[0] as Character;
#1#
                return Character?.CharacterLogic.Level.CurrentLevel < 20;
            });*/
        }


        public ICommand RaiseValueCommand { get; set; }

        public LevelSelectionViewModel(Character character):this()
        {
            Character = character;
        }

        public Character Character { get; set; }
    }
}