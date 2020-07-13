using DAN_XLV_Milica_Karetic.ViewModel;
using System.Windows;


namespace DAN_XLV_Milica_Karetic.View
{
    /// <summary>
    /// Interaction logic for Storekeeper.xaml
    /// </summary>
    public partial class Storekeeper : Window
    {
        public Storekeeper()
        {
            InitializeComponent();
            this.DataContext = new StorekeeperViewModel(this);
        }
    }
}
