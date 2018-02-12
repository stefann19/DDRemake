using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace DDRemakeProject.GamePlay
{
    public class SkillEffects
    {
        private readonly BattleEngine _battleEngine;
        private string _gifEffectPath;
        private bool _sendTrigger;

        public SkillEffects(string gifEffectPath, BattleEngine battleEngine)
        {
            _gifEffectPath = gifEffectPath;
            _battleEngine = battleEngine;
            _sendTrigger = true;
            GifImage = new Image
            {
                Margin = new Thickness(0, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            string path = Path.Combine(Environment.CurrentDirectory, gifEffectPath);
            GifImage.IsHitTestVisible = false;
            GifImage.Stretch = Stretch.UniformToFill;
            GifImage.Width = 50;
            GifImage.Height = 50;
            Panel.SetZIndex(GifImage, 2);
            ImageSource imageSource = new BitmapImage(new Uri(path));
            ImageBehavior.SetAnimatedSource(GifImage, imageSource);
            ImageBehavior.AddAnimationCompletedHandler(GifImage, AnimationCompletedEvent);
            ImageBehavior.SetRepeatBehavior(GifImage, new RepeatBehavior(1));
            Grid.SetRow(GifImage, 1);
            BattleEngine.WaitForAnimation = true;
            //_battleEngine.

            BattleEngine.BattleWindowUi.Grid.Children.Add(GifImage);
        }

        public Image GifImage { get; set; }

        private void AnimationCompletedEvent(object sender, RoutedEventArgs e)
        {
            BattleEngine.BattleWindowUi.Grid.Children.Remove(GifImage);
            if (_sendTrigger)
            {
                _battleEngine.SetNextTurn();
                _sendTrigger = false;
            }
        }
    }
}