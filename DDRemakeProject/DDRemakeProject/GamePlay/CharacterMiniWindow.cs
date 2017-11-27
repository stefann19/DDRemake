using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DDRemakeProject.GamePlay
{
    public class CharacterMiniWindow
    {
        private readonly ContentControl _characterContentControl;
        private string _hp;
        private string _mp;
        private string _ap;
        private string _name;
        private string _iconPath;
        private string _selectType;

        public CharacterMiniWindow(ContentControl characterContentControl)
        {
            _characterContentControl = characterContentControl;
        }

        public void SetStats(CharacterStats ch)
        {
            Hp = ch.CurrentHp + "/" + ch.Hp;
            Mp = ch.CurrentMp + "/" + ch.Mp;
            Ap = ch.CurrentAp + "/" + ch.Ap;
            Name = ch.Name;
            IconPath = ch.CharacterIconPng;
        }

        public string SelectType
        {
            get { return _selectType; }
            set
            {
                _selectType = value;
                _characterContentControl.Resources["selectType"] = _selectType;
            }
        }

        public string Hp
        {
            get { return _hp; }
            set
            {
                _hp = value;
                SetUi("hp", _hp);
            }
        }

        public string Mp
        {
            get { return _mp; }
            set
            {
                _mp = value;
                SetUi("mp", _mp);
            }
        }

        public string Ap
        {
            get { return _ap; }
            set
            {
                _ap = value;
                SetUi("ap", _ap);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _characterContentControl.Resources["charName"] = _name;
            }
        }

        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                _iconPath = value;
                string file = System.IO.Path.Combine(Environment.CurrentDirectory, _iconPath);
                _characterContentControl.Resources["icon"] = new BitmapImage(new Uri(file)); 

            }
        }

        private const int BarLength = 75;

        private float StringStatToPercentage(string stat)
        {
            string[] aux = stat.Split('/');
            return (float)int.Parse(aux[0]) / int.Parse(aux[1]);
        }

        private void SetUi(string statString, string stat)
        {
            
            _characterContentControl.Resources[statString + "BarWidth"] = (double)(StringStatToPercentage(stat) * BarLength);
            _characterContentControl.Resources[statString + "BarValue"] = stat;
        }

        public void SetVisibility(Visibility visibility)
        {
            _characterContentControl.Visibility = visibility;
        }

    }
}
