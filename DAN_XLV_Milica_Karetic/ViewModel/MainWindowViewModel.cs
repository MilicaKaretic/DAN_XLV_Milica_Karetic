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
        Service service = new Service();

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
        /// Add product execute
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

        private ICommand editProduct;
        /// <summary>
        /// Edit product command
        /// </summary>
        public ICommand EditProduct
        {
            get
            {
                if (editProduct == null)
                {
                    editProduct = new RelayCommand(param => EditProductExecute(), param => CanEditProductExecute());
                }
                return editProduct;
            }
        }

        /// <summary>
        /// Edit product execute
        /// </summary>
        private void EditProductExecute()
        {
            try
            {
                if (Product != null)
                {
                    AddProduct addProduct = new AddProduct(Product);
                    addProduct.ShowDialog();
                    if ((addProduct.DataContext as AddProductViewModel).IsUpdateProduct == true)
                    {
                        using (WarehouseDBEntities wcf = new WarehouseDBEntities())
                        {
                            ProductList = wcf.Products.ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can edit product execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanEditProductExecute()
        {
            if (Product == null)
                return false;
            else
                return true;
        }

        private ICommand deleteProduct;
        /// <summary>
        /// Delete product command
        /// </summary>
        public ICommand DeleteProduct
        {
            get
            {
                if (deleteProduct == null)
                {
                    deleteProduct = new RelayCommand(param => DeleteProductExecute(), param => CanDeleteProductExecute());
                }
                return deleteProduct;
            }
        }

        /// <summary>
        /// Delete product execute
        /// </summary>
        private void DeleteProductExecute()
        {
            try
            {
                if (Product != null)
                {
                    int productID = Product.ProductID;
                    if(Product.Stored == "stored")
                    {
                        MessageBox.Show("Can't delete stored product");
                    }
                    else
                    {
                        service.DeleteProduct(productID);
                        MessageBox.Show("Product has been deleted");
                    }

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
        /// Can delete product execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanDeleteProductExecute()
        {
            if (Product == null)
                return false;
            else
                return true;
        }

        #endregion
    }
}
