using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDRemakeProject.Controls
{
    /// <summary>
    /// Interaction logic for TextValueArrowsSelect.xaml
    /// </summary>
    public partial class TextValueArrowsSelect : UserControl
    {
        public static readonly DependencyProperty PropTitleProperty = DependencyProperty.Register("PropTitle", typeof(string), typeof(TextValueArrowsSelect), new PropertyMetadata("PropName"));
        public static readonly DependencyProperty PropValueProperty = DependencyProperty.Register("PropValue", typeof(string), typeof(TextValueArrowsSelect), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PropLowerValueProperty = DependencyProperty.Register("PropLowerValue", typeof(ICommand), typeof(TextValueArrowsSelect), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty PropRaiseValueProperty = DependencyProperty.Register("PropRaiseValue", typeof(ICommand), typeof(TextValueArrowsSelect), new PropertyMetadata(default(ICommand)));

        public TextValueArrowsSelect()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }


        public string PropTitle {
            get { return (string) GetValue(PropTitleProperty); }
            set { SetValue(PropTitleProperty, value); }
        }

        public string PropValue {
            get { return (string) GetValue(PropValueProperty); }
            set { SetValue(PropValueProperty, value); }
        }

        public ICommand PropLowerValue {
            get { return (ICommand) GetValue(PropLowerValueProperty); }
            set { SetValue(PropLowerValueProperty, value); }
        }

        public ICommand PropRaiseValue {
            get { return (ICommand) GetValue(PropRaiseValueProperty); }
            set { SetValue(PropRaiseValueProperty, value); }
        }
    }
}
