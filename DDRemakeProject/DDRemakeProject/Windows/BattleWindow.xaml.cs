using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DDRemakeProject.GamePlay;
using WpfAnimatedGif;
using Image = System.Drawing.Image;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
using Action = DDRemakeProject.GamePlay.Action;


namespace DDRemakeProject
{

  

    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        private readonly BattleEngine _currentBattleEngine;
        public BattleWindow(BattleEngine currentBattleEngine)
        {
            InitializeComponent();
            SelectedCharacterIndex = -1;
            this._currentBattleEngine = currentBattleEngine;
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

    

        public void InitializeBattleUi(List<Character> characters)
        {
         
            #region foreach character
            for (int i = 0; i < characters.Count; i++)
            {
                if(characters[i].Type == CharacterTypes.Type.Ally)
                characters[i].ImageGif = CharacterSetup(characters[i].CharacterStats,i,"");
            }

            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].Type == CharacterTypes.Type.Enemy)

                    characters[i].ImageGif = CharacterSetup(characters[i].CharacterStats,(i-3),"e");
            }
            #endregion


        }

        private System.Windows.Controls.Image CharacterSetup(CharacterStats character, int i, string e)
        {
            System.Windows.Controls.Image CharacterImage;
            string file = System.IO.Path.Combine(Environment.CurrentDirectory, character.CharacterPng);
            CharacterImage = (this.FindName("B" + e + "Char" + (i + 1)) as System.Windows.Controls.Image);
            ImageBehavior.SetAnimatedSource(CharacterImage,new BitmapImage(new Uri(file)));

            //set icon image
            file = System.IO.Path.Combine(Environment.CurrentDirectory, character.CharacterIconPng);
            ContentControl cc = (this.FindName("Icon" +( i + (e == "e" ? 3 : 0))) as System.Windows.Controls.ContentControl);
            cc.Resources["Img"] = new BitmapImage(new Uri(file));
            cc.Resources["width"] = (double)(character.CurrentHp * 78f/character.Hp);
            cc.Resources["hpValue"] = character.CurrentHp.ToString() +"/"+ character.Hp.ToString();

            return CharacterImage;
        }


        public int SelectedCharacterIndex;

        private void SelectChar(object sender, MouseButtonEventArgs e)
        {


            Rectangle senderImage = sender as Rectangle;
            SelectChar((sender as Rectangle).Name,true);
            int enemy = (senderImage.Name.Contains("e") ? 1 : 0);

            SelectedCharacterIndex = senderImage.Name.Last() - '1' + enemy * 3;
        }

        private void ShowAvailableActions(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Button button = sender as System.Windows.Controls.Button;
            ActionTypes.ActionType action = (ActionTypes.ActionType) Enum.Parse(typeof(ActionTypes.ActionType), button.Name);
            GenerateActionButtons(_currentBattleEngine.Characters[_currentBattleEngine.currentCharacterIndex].CharacterStats, action);
        }

        private void GenerateActionButtons(CharacterStats ch, ActionTypes.ActionType actionType)
        {
            ActionGrid.Children.Clear();
            List<Action> actionsList = _currentBattleEngine.GetAvailableActions(actionType);

            ActionGrid.Width = actionsList.Count * 40;
            int left = 0;
            foreach (Action action in actionsList)
            {
                Button bt = new Button();
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, action.Icon);
                bt.Background = new ImageBrush(new BitmapImage(new Uri(file)));
                string actionIndex = ch.Actions.IndexOf(action).ToString();
                bt.Name = "Action"+ actionIndex;
                bt.Width = 40;
                bt.HorizontalAlignment = HorizontalAlignment.Left;
                bt.Margin = new Thickness(left, 0,0,0);
                left += 40;
                bt.Click += Bt_Click;
                ActionGrid.Children.Add(bt);
            }

        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            ActionPressed(sender);
        }



        private void MouseEntered(object sender, MouseEventArgs e)
        {
            SelectChar((sender as Rectangle).Name,false);
       
        }


        private System.Windows.Controls.Image _selectedCharacterImage;
        private System.Windows.Controls.Image _hoveredCharImage;
        private System.Windows.Controls.Image _currentTurnCharImage;

        private void SelectChar(string name,bool selected)
        {

            string aux = name;
            int enemy = (aux.Contains("e") ? 1 : 0);
            aux = aux.Substring(0, 1 + enemy) + aux.Substring(2 + enemy);
            int index = name.Last() - '1' + enemy * 3;

            _currentBattleEngine.SelectChar(index);

            if (_currentTurnCharImage==null || !_currentTurnCharImage.Equals(FindName(aux) as System.Windows.Controls.Image))
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

        public void SelectTurnChar(System.Windows.Controls.Image image)
        {

            if (_currentTurnCharImage != null)
            {
                _currentTurnCharImage.Width = 100f;
                _currentTurnCharImage.Height = 100f;
            }
            _currentTurnCharImage = image;
            _currentTurnCharImage.Width = 150f;
            _currentTurnCharImage.Height = 150f;
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

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ActionPressed(sender);
        }

        private void ActionPressed(object sender)
        {
            int actionIndex = int.Parse((sender as Button).Name.Last().ToString());
            Action action = _currentBattleEngine.Characters[_currentBattleEngine.currentCharacterIndex].CharacterStats.Actions[actionIndex];
            _currentBattleEngine.Fight(_currentBattleEngine.Characters[_currentBattleEngine.currentCharacterIndex], action);
            _currentBattleEngine.SelectChar(SelectedCharacterIndex);
            _currentBattleEngine.ReloadChar();
        }

        private void Attack_MouseLeave(object sender, MouseEventArgs e)
        {
            //ActionGrid.Children.Clear();
        }

    }
}
