using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DDRemakeProject.ViewModels;

namespace DDRemakeProject.Views
{
    /// <summary>
    /// Interaction logic for StatsProjectionPanelView.xaml
    /// </summary>
    public partial class StatsProjectionPanelView : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(StatsProjectionPanelView), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<StatProjectionViewModel>), typeof(StatsProjectionPanelView), new PropertyMetadata(default(ObservableCollection<StatProjectionViewModel>)));

        public StatsProjectionPanelView()
        {
            InitializeComponent();
            RootLayout.DataContext = this;
        }

        public string Title {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ObservableCollection<StatProjectionViewModel> Items {
            get { return (ObservableCollection<StatProjectionViewModel>) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }


    }
}
