using DAN_XLV_Milica_Karetic.ViewModel;
using System.Windows;

namespace DAN_XLV_Milica_Karetic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
        }
    }
}
