using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using DDRemakeProject.GamePlay.New;
using WpfAnimatedGif;

namespace DDRemakeProject.GamePlay.Old
{
    public class Character : IComparable<Character>
    {
        public BattleEngine BattleEngine { get; }
        public CharacterLogic CharacterLogic { get; set; }
        public CharacterTypes.Status Status { get; set; }
        public CharacterTypes.Type Type { get; set; }

        public CharacterUi CharacterUiControl { get; set; }

        public string Name { get; set; }    

        public void CharacterDied()
        {
            Status = CharacterTypes.Status.Dead;
            CharacterUiControl.CharAvatarControl.Visibility = Visibility.Hidden;
            
        }

        public Character(CharacterLogic characterLogic, CharacterTypes.Type type, BattleEngine battleEngine)
        {
            CharacterLogic = characterLogic;
            CharacterLogic.CharacterParent = this;
            Status = CharacterTypes.Status.Alive;
            Type = type;
            BattleEngine = battleEngine;
            CharacterUiControl = AvatarSpotsManager.GetSpot(type: Type);
            CharacterUiControl.AssignCharactersToUi(character: this);
        }


     

       


        public int CompareTo(Character ch)
        {
            return (int)(CharacterLogic.Traits.IntelligenceTrait.Value - ch.CharacterLogic.Traits.IntelligenceTrait.Value);
        }
    }
}