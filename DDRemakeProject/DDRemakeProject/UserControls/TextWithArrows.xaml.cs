using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDRemakeProject.CharacterCreation;
using DDRemakeProject.GamePlay.New.Character.Logic;

namespace DDRemakeProject.UserControls
{

    public enum BasicPropType
    {
        Integer,
        Race
    }
    /// <summary>
    /// Interaction logic for TextWithArrows.xaml
    /// </summary>
    public partial class TextWithArrows : UserControl
    {
        public BasicProperty BasicIntegerProperty { get; set; }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(TextWithArrows), new PropertyMetadata("Title"));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(TextWithArrows), new PropertyMetadata("10"));
        public static readonly DependencyProperty TypeeProperty = DependencyProperty.Register("Typee", typeof(object), typeof(TextWithArrows), new PropertyMetadata(Races.Bat));


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (BasicIntegerProperty != null)
            {
                return;
            }

            switch (Typee)
            {
                case BasicPropType.Integer:
                    BasicIntegerProperty = new BasicIntegerProperty(this);
                    break;
                case BasicPropType.Race:
                    BasicIntegerProperty = new RaceProperty(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public TextWithArrows()
        {
            InitializeComponent();
        }

        public object Title {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public object Value
        {
            get { return (string) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value.ToString()); }
        }
        public BasicPropType Typee
        {
            get {
                if (GetValue(TypeeProperty) == null) return BasicPropType.Integer;
                Enum.TryParse(GetValue(TypeeProperty) as string, true, out BasicPropType p);
                return p;
            }

            set
            {
                SetValue(TypeeProperty, value);
            }
        }


        private void LowerTheValue(object sender, RoutedEventArgs e)=> BasicIntegerProperty.LowerValue();

        private void RaiseTheValue(object sender, RoutedEventArgs e)
        {
            BasicIntegerProperty.RaiseValue();
        } 

    }
}
