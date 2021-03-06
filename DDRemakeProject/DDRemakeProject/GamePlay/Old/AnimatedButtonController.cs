﻿using System;
using System.Windows.Media.Imaging;

namespace DDRemakeProject.GamePlay.Old
{
    public class AnimatedButtonController
    {
        public ActionTypes.ActionType ActionType;

        public AnimatedButton AnimatedButton { get; set; }

        private ButtonStatesImages ButtonStatesImages { get; set; }
        public BattleEngine BattleEngine;

        public AnimatedButtonController(ButtonStatesImages buttonStatesImages,AnimatedButton animatedButton,ActionTypes.ActionType actionType,BattleEngine battleEngine)
        {
            this.ButtonStatesImages = buttonStatesImages;
            this.AnimatedButton = animatedButton;
            this.ActionType = actionType;
            AnimatedButton.ActionType = actionType;
            this.BattleEngine = battleEngine;
            SetImageSourcesForAnimatedButton();
            
        }

        public void SetImageSourcesForAnimatedButton()
        {
            string file = System.IO.Path.Combine(Environment.CurrentDirectory, ButtonStatesImages.NormalStateImage);
            AnimatedButton.Button.Resources["NImg"] = new BitmapImage(new Uri(file));
            file = System.IO.Path.Combine(Environment.CurrentDirectory, ButtonStatesImages.HoverStateImage);
            AnimatedButton.Button.Resources["HImg"] = new BitmapImage(new Uri(file));
            file = System.IO.Path.Combine(Environment.CurrentDirectory, ButtonStatesImages.PressedStateImage);
            AnimatedButton.Button.Resources["PImg"] = new BitmapImage(new Uri(file));
        }
    }
}
