using DAN_XLV_Milica_Karetic.Commands;
using DAN_XLV_Milica_Karetic.Model;
using DAN_XLV_Milica_Karetic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Milica_Karetic.ViewModel
{
    class StorekeeperViewModel : BaseViewModel
    {
        Storekeeper storekeeper;

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
        /// <param name="storeOpen">Storekeeper window</param>
        public StorekeeperViewModel(Storekeeper storeOpen)
        {
            storekeeper = storeOpen;
            using (WarehouseDBEntities context = new WarehouseDBEntities())
            {
                ProductList = context.Products.ToList();
            }
            service.GetCurrentWarehouseQuantity();
        }
        #endregion

        #region Commands

        private ICommand storeProduct;
        /// <summary>
        /// Store product command
        /// </summary>
        public ICommand StoreProduct
        {
            get
            {
                if (storeProduct == null)
                {
                    storeProduct = new RelayCommand(param => StoreProductExecute(), param => CanStoreProductExecute());
                }
                return storeProduct;
            }
        }

        /// <summary>
        /// Store product execute
        /// </summary>
        private void StoreProductExecute()
        {
            try
            {
                if (Product != null)
                {
                    int productID = Product.ProductID;
                    int num = 100 - Service.currentWarehouseQuantity;
                    
                    if (Product.Stored == "stored")
                    {
                        service.NotifyUser("Product is already stored");
                    }
                    else
                    {
                        if (Product.Quantity <= num)
                        {
                            service.StoreProduct(productID);
                            service.NotifyUser("Product has been stored");
                        }
                        else
                        {
                            service.NotifyUser("There is no enough space in warehouse");
                        }                      
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
        /// Can store product execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanStoreProductExecute()
        {
            if (Product == null)
                return false;
            else
                return true;
        }

        private ICommand logOut;
        /// <summary>
        /// logout command
        /// </summary>
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        /// <summary>
        /// logout execute
        /// </summary>
        private void LogOutExecute()
        {
            Login log = new Login();
            log.Show();
            storekeeper.Close();
        }

        /// <summary>
        /// Can logout execute
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanLogOutExecute()
        {
            return true;
        }
        #endregion
    }
}
