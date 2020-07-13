using DAN_XLV_Milica_Karetic.Model;
using DAN_XLV_Milica_Karetic.ViewModel;
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
using System.Windows.Shapes;

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
