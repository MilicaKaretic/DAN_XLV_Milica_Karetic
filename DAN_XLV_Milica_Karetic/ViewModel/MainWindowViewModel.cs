using DAN_XLV_Milica_Karetic.Commands;
using DAN_XLV_Milica_Karetic.Model;
using DAN_XLV_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Milica_Karetic.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        MainWindow main;

        #region Properties

        private Product product;
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        private List<Product> productList;
        public List<Product> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        private Visibility viewProduct = Visibility.Visible;
        public Visibility ViewProduct
        {
            get
            {
                return viewProduct;
            }
            set
            {
                viewProduct = value;
                OnPropertyChanged("ViewProduct");
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="mainOpen">Main window</param>
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            using (WarehouseDBEntities context = new WarehouseDBEntities())
            {
                ProductList = context.Products.ToList();
            }
        }
        #endregion

        #region Commands

        private ICommand addProduct;
        /// <summary>
        /// Add item command
        /// </summary>
        public ICommand AddProduct
        {
            get
            {
                if (addProduct == null)
                {
                    addProduct = new RelayCommand(param => AddProductExecute(), param => CaAddProductExecute());
                }
                return addProduct;
            }
        }

        /// <summary>
        /// Add student execute
        /// </summary>
        private void AddProductExecute()
        {
            try
            {
                AddProduct addProduct = new AddProduct();
                addProduct.ShowDialog();
                if ((addProduct.DataContext as AddProductViewModel).IsUpdateProduct == true)
                {
                    using (WarehouseDBEntities wcf = new WarehouseDBEntities())
                    {
                        ProductList = wcf.Products.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can Add item execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CaAddProductExecute()
        {          
                return true;
        }

        #endregion
    }
}
