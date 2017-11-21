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


namespace DDRemakeProject
{

  

    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window 
    {
        public UIBackEnd UiBackEnd;
        public BattleWindow(UIBackEnd uiBackEnd)
        {
            InitializeComponent();
            UiBackEnd = uiBackEnd;
            SelectedCharacterIndex = -1;
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

    
        private string _message;

        public void InitializeBattleUI(List<Character> allyCharacters, List<Character> enemyCharacters)
        {
         
            #region foreach character
            for (int i = 0; i < allyCharacters.Count; i++)
            {
                CharacterSetup(allyCharacters[i],i,"");
            }

            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                CharacterSetup(enemyCharacters[i],i,"e");
            }
            #endregion


        }

        private void CharacterSetup(Character character, int i, string e)
        {
            string file = System.IO.Path.Combine(Environment.CurrentDirectory, character.CharacterPng);
            ImageBehavior.SetAnimatedSource((this.FindName("B" + e + "Char" + (i + 1)) as System.Windows.Controls.Image),
                new BitmapImage(new Uri(file)));

            //set icon image
            file = System.IO.Path.Combine(Environment.CurrentDirectory, character.CharacterIconPng);
            ContentControl cc = (this.FindName("Icon" +( i + (e == "e" ? 3 : 0))) as System.Windows.Controls.ContentControl);
            cc.Resources["Img"] = new BitmapImage(new Uri(file));
            cc.Resources["width"] = (double)(character.CurrentHp * 78f/character.Hp);
            cc.Resources["hpValue"] = character.CurrentHp.ToString() +"/"+ character.Hp.ToString();
        }


        public int SelectedCharacterIndex;

        private void SelectChar(object sender, MouseButtonEventArgs e)
        {


            //Rectangle senderImage = sender as Rectangle;
            SelectChar((sender as Rectangle).Name,true);
            //int enemy = (senderImage.Name.Contains("e") ? 1 : 0);

            //SelectedCharacterIndex = senderImage.Name.Last() - '1' + enemy * 3;
        }

        private void ShowAvailableActions(object sender, MouseEventArgs e)
        {

        }

        private void MouseEntered(object sender, MouseEventArgs e)
        {
            SelectChar((sender as Rectangle).Name,false);
            //Rectangle senderImage = sender as Rectangle;
            //string aux = senderImage.Name;
            //int enemy = (aux.Contains("e") ? 1 : 0);
            //aux = aux.Substring(0, 1 + enemy) + aux.Substring(2+enemy);
            //System.Windows.Controls.Image charImage = FindName(aux) as System.Windows.Controls.Image;
            ////PopOutWindow.Margin = new Thickness(charImage.Margin.Left, charImage.Margin.Top-25,0,0);
            //int index = senderImage.Name.Last() - '1' + enemy*3;
            //UiBackEnd.PopedChar(index);
        }


        private System.Windows.Controls.Image _selectedCharacterImage;
        private System.Windows.Controls.Image _hoveredCharImage;

        private void SelectChar(string name,bool selected)
        {

            string aux = name;
            int enemy = (aux.Contains("e") ? 1 : 0);
            aux = aux.Substring(0, 1 + enemy) + aux.Substring(2 + enemy);
            int index = name.Last() - '1' + enemy * 3;
            UiBackEnd.PopedChar(index);

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

        private void DeselectChar()
        {
            UiBackEnd.PopOutQuit();
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
            //PopOutWindow.IsEnabled = false;
            if (SelectedCharacterIndex == -1)
            {
                DeselectChar();
                //UiBackEnd.PopOutQuit();
                //selectedCharacterImage.Width = 100f;
                //selectedCharacterImage.Height = 100f;
            }
            else
            {
                UiBackEnd.PopedChar(SelectedCharacterIndex);
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
    }
}
