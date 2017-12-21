using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace DDRemakeProject.GamePlay
{
    public class Character : IComparable<Character>
    {
        public BattleEngine BattleEngine { get; }
        public CharacterStats CharacterStats { get; set; }
        public CharacterTypes.Status Status { get; set; }
        public CharacterTypes.Type Type { get; set; }

        public CharacterUi CharacterUiControl { get; set; }


        public void CharacterDied()
        {
            Status = CharacterTypes.Status.Dead;
            CharacterUiControl.CharAvatarControl.Visibility = Visibility.Hidden;
        }

        public Character(CharacterStats characterStats, CharacterTypes.Type type, BattleEngine battleEngine)
        {
            CharacterStats = characterStats;
            CharacterStats.CharacterParent = this;
            Status = CharacterTypes.Status.Alive;
            Type = type;
            BattleEngine = battleEngine;
            CharacterUiControl = AvatarSpotsManager.GetSpot(Type);
            CharacterSetup();
            CharacterUiControl.AssignCharactersToUi(this);
        }


        private void CharacterSetup()
        {
            string path = Path.Combine(Environment.CurrentDirectory, CharacterStats.CharacterPng);

            ImageBehavior.SetAnimatedSource(CharacterUiControl.CharAvatarControl.AvatarImage,
                new BitmapImage(new Uri(path)));
            path = Path.Combine(Environment.CurrentDirectory, CharacterStats.CharacterIconPng);
            CharacterUiControl.CharIconControl.Icon.Source = new BitmapImage(new Uri(path));
            UpdateHpUi();
        }

        public void UpdateHpUi()
        {
            CharacterUiControl.CharIconControl.HpValue.Text = $"{CharacterStats.CurrentHp} / {CharacterStats.Hp}";
            CharacterUiControl.CharIconControl.HpValueRect.Width =
                Math.Max(0d, CharacterStats.CurrentHp * 78f / CharacterStats.Hp);
        }


        public int CompareTo(Character ch)
        {
            return CharacterStats.Inteligence - ch.CharacterStats.Inteligence;
        }
    }
}