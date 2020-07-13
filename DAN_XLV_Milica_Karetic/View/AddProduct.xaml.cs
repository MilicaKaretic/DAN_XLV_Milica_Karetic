using DAN_XLV_Milica_Karetic.Model;
using DAN_XLV_Milica_Karetic.ViewModel;
using System.Windows;


namespace DAN_XLV_Milica_Karetic.View
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            this.DataContext = new AddProductViewModel(this);
        }

        public AddProduct(Product productEdit)
        {
            InitializeComponent();
            this.DataContext = new AddProductViewModel(this, productEdit);
        }
    }
}
