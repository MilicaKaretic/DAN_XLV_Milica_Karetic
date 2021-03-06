﻿using DAN_XLV_Milica_Karetic.Commands;
using DAN_XLV_Milica_Karetic.Model;
using DAN_XLV_Milica_Karetic.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Milica_Karetic.ViewModel
{
    /// <summary>
    /// Add product view model class
    /// </summary>
    class AddProductViewModel : BaseViewModel
    {
        Service service = new Service();

        AddProduct addProduct;

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

        private bool isUpdateProduct;
        public bool IsUpdateProduct
        {
            get
            {
                return isUpdateProduct;
            }
            set
            {
                isUpdateProduct = value;
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="addProductOpen">Add product view</param>
        public AddProductViewModel(AddProduct addProductOpen)
        {
            product = new Product();
            addProduct = addProductOpen;
        }

        /// <summary>
        /// Constructor with two parameters
        /// </summary>
        /// <param name="addProductOpen">Add Product vie2</param>
        /// <param name="productEdit">Product to edit</param>
        public AddProductViewModel(AddProduct addProductOpen, Product productEdit)
        {
            product = productEdit;
            addProduct = addProductOpen;
        }
        #endregion

        #region Commands

        private ICommand save;

        /// <summary>
        /// Save command
        /// </summary>
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// Save execute - how to save product
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                if(Product.Quantity <= 0 || Product.Price <= 0)
                {
                    MessageBox.Show("Invalid data input. Please try again.");
                }
                else
                {
                    Product product = service.AddProduct(Product);
                    if (product != null)
                    {
                        if (Service.action == "added")
                        {
                            //event
                            service.Notify("Added " + product.ProductName + ", code " + product.ProductCode + ", quantity " + product.Quantity + ", price " + product.Price);
                           // MessageBox.Show("Product has been added");
                        }
                        else if (Service.action == "updated")
                        {
                            //event
                            service.Notify("Updated " + product.ProductName + ", code " + product.ProductCode + ", quantity " + product.Quantity + ", price " + product.Price); ;
                            //MessageBox.Show("Product has been updated");
                        }
                        isUpdateProduct = true;
                        addProduct.Close();
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can save
        /// </summary>
        /// <returns>can or cannot</returns>
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(product.ProductCode) ||
                String.IsNullOrEmpty(product.ProductName) ||
                String.IsNullOrEmpty(product.Quantity.ToString()) ||
                String.IsNullOrEmpty(product.Price.ToString())
                )
            {
                return false;
            }
            else
                return true;
        }

        private ICommand close;

        /// <summary>
        /// Close command
        /// </summary>
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }

                return close;
            }
        }

        /// <summary>
        /// Close execute
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                addProduct.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can close
        /// </summary>
        /// <returns>Can or cannot</returns>
        private bool CanCloseExecute()
        {
            return true;
        }

        #endregion

    }
}
