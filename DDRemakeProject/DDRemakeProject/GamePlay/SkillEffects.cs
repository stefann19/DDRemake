using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DDRemakeProject.GamePlay
{
    public class SkillEffects
    {
        private string _gifEffectPath;
        public Image GifImage { get; set; }
        private BattleEngine _battleEngine;
        private bool _sendTrigger;
        public SkillEffects(string gifEffectPath, BattleEngine battleEngine)
        {
            this._gifEffectPath = gifEffectPath;
            _battleEngine = battleEngine;
            _sendTrigger = true;
            GifImage = new Image {Margin = new Thickness(0, 0, 0, 0) ,VerticalAlignment = VerticalAlignment.Center , HorizontalAlignment = HorizontalAlignment.Center };
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, gifEffectPath);
            GifImage.IsHitTestVisible = false;
            GifImage.Stretch = Stretch.UniformToFill;
            GifImage.Width = 50;
            GifImage.Height = 50;
            Panel.SetZIndex(GifImage,2);
            ImageSource imageSource = new BitmapImage(new Uri(path));
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(GifImage,imageSource);
            WpfAnimatedGif.ImageBehavior.AddAnimationCompletedHandler (GifImage,AnimationCompletedEvent ) ;
            WpfAnimatedGif.ImageBehavior.SetRepeatBehavior(GifImage, new RepeatBehavior(1));
            Grid.SetRow(GifImage,1);
            BattleEngine.WaitForAnimation = true;
            //_battleEngine.

            BattleEngine.BattleWindowUi.Grid.Children.Add(GifImage);
        }

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
