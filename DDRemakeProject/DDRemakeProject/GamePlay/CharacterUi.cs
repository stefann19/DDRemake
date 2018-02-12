using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using DDRemakeProject.UserControls;

namespace DDRemakeProject.GamePlay
{
    public class CharacterUi
    {
        public enum ScaleType
        {
            NoScaling,
            MouseOver,
            Selected
        }

        public Character ParentCharacter { get; set; }
        private ScaleType _scalingType;
        public CharAvatar CharAvatarControl { get; }
        public CharIcon CharIconControl { get; }


        public CharacterUi(CharAvatar charAvatarControl, CharIcon charIconControl)
        {
            CharAvatarControl = charAvatarControl;
            CharIconControl = charIconControl;

            CharAvatarControl.HitBox.MouseEnter += HitBoxOnMouseEnter;
            CharAvatarControl.HitBox.MouseLeave += HitBoxOnMouseLeave;
            CharAvatarControl.HitBox.MouseDown += HitBox_MouseDown;
            _scalingType = ScaleType.NoScaling;
        }


        public void AssignCharactersToUi(Character character)
        {
            ParentCharacter = character;
            CharAvatarControl.Character = ParentCharacter;
        }

        public ScaleType ScalingType
        {
            get => _scalingType;
            set
            {
                if (value == ScalingType) return;
                _scalingType = value;
                Scale(_scalingType);
            }
        }

        private void Scale(ScaleType scalingType)
        {
            if (scalingType == ScaleType.NoScaling)
            {
                CharAvatarControl.Width = 60;
                CharAvatarControl.Height = 60;
            }
            else
            {
                CharAvatarControl.Width = 100;
                CharAvatarControl.Height = 100;
            }
        }


        private void HitBoxOnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            Rectangle rectangle = sender as Rectangle;
            Grid grid = rectangle?.Parent as Grid;
            CharAvatar charAvatar = grid?.Parent as CharAvatar;
            Character character = charAvatar?.Character;
            if (character == null) return;
            if (character.CharacterUiControl.ScalingType != ScaleType.Selected)
                character.CharacterUiControl.ScalingType = ScaleType.NoScaling;
            if (ParentCharacter.BattleEngine.SelectedCharacter != null)
                ParentCharacter.BattleEngine.SelectChar(ParentCharacter.BattleEngine.SelectedCharacter);
        }

        private void HitBoxOnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            Rectangle rectangle = sender as Rectangle;
            Grid grid = rectangle?.Parent as Grid;
            CharAvatar charAvatar = grid?.Parent as CharAvatar;
            Character character = charAvatar?.Character;
            if (character == null) return;
            if (character.CharacterUiControl.ScalingType == ScaleType.Selected) return;


            character.CharacterUiControl.ScalingType = ScaleType.MouseOver;
            ParentCharacter.BattleEngine.SelectChar(character);
        }

        private void HitBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            Grid grid = rectangle?.Parent as Grid;
            CharAvatar charAvatar = grid?.Parent as CharAvatar;
            Character character = charAvatar?.Character;

            if (character == null) return;
            if (character.CharacterUiControl.ScalingType == ScaleType.Selected) return;

            if (ParentCharacter.BattleEngine.SelectedCharacter != null)
                ParentCharacter.BattleEngine.SelectedCharacter.CharacterUiControl.ScalingType = ScaleType.NoScaling;

            ParentCharacter.BattleEngine.SelectedCharacter = character;
            character.CharacterUiControl.ScalingType = ScaleType.Selected;
        }
    }
}