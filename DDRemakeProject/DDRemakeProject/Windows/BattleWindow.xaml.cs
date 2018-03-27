using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DDRemakeProject.GamePlay.New;
using DDRemakeProject.GamePlay.Old;
using Action = DDRemakeProject.GamePlay.Old.Action;


namespace DDRemakeProject
{

  

    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow
    {
        private readonly BattleEngine _currentBattleEngine;
        public BattleWindow(BattleEngine currentBattleEngine)
        {
            InitializeComponent();
            this._currentBattleEngine = currentBattleEngine;
            this.Closing += BattleWindow_Closing;
        }

        private void BattleWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
                
            }
            AvatarSpotsManager.FreeAllSpots();
        }


        //private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{

        //}


        //public int SelectedCharacterIndex;

        //private void SelectChar(object sender, MouseButtonEventArgs e)
        //{


        //    Rectangle senderImage = sender as Rectangle;
        //    SelectChar((sender as Rectangle).Name,true);
        //    int enemy = (senderImage.Name.Contains("e") ? 1 : 0);

        //    SelectedCharacterIndex = senderImage.Name.Last() - '1' + enemy * 3;
        //}

        private void ShowAvailableActions(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            ActionTypes.ActionType action = (ActionTypes.ActionType) Enum.Parse(typeof(ActionTypes.ActionType), button.Name);
            GenerateActionButtons(_currentBattleEngine.Characters[TurnSystem.CurrentCharIndex].CharacterLogic, action);
        }

        private void GenerateActionButtons(CharacterLogic ch, ActionTypes.ActionType actionType)
        {
            ActionGrid.Children.Clear();
            List<Action> actionsList = _currentBattleEngine.GetAvailableActions(actionType);

            ActionGrid.Width = actionsList.Count * 40;
            int left = 0;
            foreach (Action action in actionsList)
            {
                Button bt = new Button();
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, action.Icon.NormalStateImage);
                bt.Background = new ImageBrush(new BitmapImage(new Uri(file)));
                string actionIndex = ch.Actions.IndexOf(action).ToString();
                bt.Name = "Action" + actionIndex;
                bt.Width = 40;
                bt.HorizontalAlignment = HorizontalAlignment.Left;
                bt.Margin = new Thickness(left, 0, 0, 0);
                left += 40;
                bt.Click += Bt_Click;
                ActionGrid.Children.Add(bt);
            }

        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            ActionPressed(sender);
        }



        //        private void MouseEntered(object sender, MouseEventArgs e)
        //        {
        //            SelectChar((sender as Rectangle).Name, false);
        //
        //        }


        //        private System.Windows.Controls.Image _selectedCharacterImage;
        //        private System.Windows.Controls.Image _hoveredCharImage;
        //        private CharAvatar _currentTurnCharAvatarControl;
        /*

                private void SelectChar(string name,bool selected)
                {

                    string aux = name;
                    int enemy = (aux.Contains("e") ? 1 : 0);
                    aux = aux.Substring(0, 1 + enemy) + aux.Substring(2 + enemy);
                    int index = name.Last() - '1' + enemy * 3;

                    if(_currentBattleEngine.Characters.Count>index)
                    _currentBattleEngine.SelectChar(index);

                    if (_currentTurnCharAvatarControl==null || !_currentTurnCharAvatarControl.Equals(FindName(aux) as System.Windows.Controls.Image))
                    {
                        if (selected)
                        {
                            if (_selectedCharacterImage != null)
                            {
                                _selectedCharacterImage.Width = 100f;
                                _selectedCharacterImage.Height = 100f;
                            }

                            _selectedCharacterImage = FindName(aux) as System.Windows.Controls.Image;
                            _selectedCharacterImage.Width = 150f;
                            _selectedCharacterImage.Height = 150f;
                            SelectedCharacterIndex = index;

                        }
                        else
                        {
                            _hoveredCharImage = FindName(aux) as System.Windows.Controls.Image;
                            _hoveredCharImage.Width = 150f;
                            _hoveredCharImage.Height = 150f;
                        }
                    }

                }

                public void SelectTurnChar(CharAvatar charAvatar)
                {

                    if (_currentTurnCharAvatarControl != null)
                    {
                        _currentTurnCharAvatarControl.Width = 100f;
                        _currentTurnCharAvatarControl.Height = 100f;
                    }
                    _currentTurnCharAvatarControl = charAvatar;
                    _currentTurnCharAvatarControl.Width = 150f;
                    _currentTurnCharAvatarControl.Height = 150f;
                }

                private void DeselectChar()
                {
                    _currentBattleEngine.DeselectCHar();
                    SelectedCharacterIndex = -1;
                    if (_selectedCharacterImage != null)
                    {
                        _selectedCharacterImage.Width = 100f;
                        _selectedCharacterImage.Height = 100f;
                    }
                    if (_hoveredCharImage!=null && !_hoveredCharImage.Equals(_selectedCharacterImage))
                    {
                        _hoveredCharImage.Width = 100f;
                        _hoveredCharImage.Height = 100f;
                    }
                }

                private void MouseLeft(object sender, MouseEventArgs e)
                {
                    if (SelectedCharacterIndex == -1)
                    {
                        DeselectChar();
                    }
                    else
                    {
                        _currentBattleEngine.SelectChar(SelectedCharacterIndex);

                        if (_hoveredCharImage != null && !_hoveredCharImage.Equals(_selectedCharacterImage))
                        {
                            _hoveredCharImage.Width = 100f;
                            _hoveredCharImage.Height = 100f;
                        }
                    }
                }

                private void Image_MouseDown(object sender, MouseButtonEventArgs e)
                {
                    DeselectChar();
                }
        */

        //private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    ActionPressed(sender);
        //}

        private void ActionPressed(object sender)
        {
            int actionIndex = int.Parse((sender as Button).Name.Last().ToString());
            Action action = _currentBattleEngine.Characters[TurnSystem.CurrentCharIndex].CharacterLogic.Actions[actionIndex];
            _currentBattleEngine.Fight(_currentBattleEngine.SelectedCharacter, action);
            _currentBattleEngine.SelectChar(_currentBattleEngine.SelectedCharacter);
            //_currentBattleEngine.ReloadChar();
        }

        private void Attack_MouseLeave(object sender, MouseEventArgs e)
        {
            //ActionGrid.Children.Clear();
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            //if (ActionGrid.Children.Count > 0) ActionGrid.Children.Clear();
            
        }
       
    }
}
