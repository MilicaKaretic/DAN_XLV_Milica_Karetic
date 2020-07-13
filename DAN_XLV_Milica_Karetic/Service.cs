using DAN_XLV_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAN_XLV_Milica_Karetic
{
    public class Service
    {
        public Service()
        {
            //events
            onNotification = log.WriteActionToFile;
            onNotificationStore = mess.ShowMessageToUser;
        }

        Logger log = new Logger();
        ShowMessage mess = new ShowMessage();

        #region Delegate logger

        //delegate for notification user
        public delegate void Notification(string text);
        //event based on that delegate
        public event Notification onNotification;

        //another event for store product notification
        public event Notification onNotificationStore;

        /// <summary>
        /// Raise an event for logging manager actions to file
        /// </summary>
        internal virtual void Notify(string text)
        {
            if (onNotification != null)
            {
                onNotification(text);
            }
        }

        /// <summary>
        /// Raise an event for notify user if storing is successful or not
        /// </summary>
        /// <param name="text"></param>
        internal virtual void NotifyUser(string text)
        {
            if(onNotificationStore != null)
            {
                onNotificationStore(text);
            }
        }
        #endregion

        /// <summary>
        /// track current product quantity in warehouse
        /// </summary>
        public static int currentWarehouseQuantity;
        public static string action;

        /// <summary>
        /// Get and update current product quantity in warehouse
        /// </summary>
        public void GetCurrentWarehouseQuantity()
        {
            int sum = 0;
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    List<Product> products = new List<Product>();
                    products = context.Products.ToList();

                    for (int i = 0; i < products.Count; i++)
                    {
                        if(products[i].Stored == "stored")
                            sum += products[i].Quantity;
                    }
                    
                }
                currentWarehouseQuantity = sum;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
        }

        /// <summary>
        /// Add or edit product in warehouse
        /// </summary>
        /// <param name="product">Product to add or edit</param>
        /// <returns>Added or updated product</returns>
        public Product AddProduct(Product product)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    if(product.ProductID == 0)
                    {
                        //add product
                        Product p = new Product();
                        p.ProductName = product.ProductName;
                        p.ProductCode = product.ProductCode;
                        p.Quantity = product.Quantity;
                        p.Price = product.Price;
                        p.Stored = "not stored";

                        context.Products.Add(p);
                        context.SaveChanges();

                        product.ProductID = p.ProductID;
                        
                        action = "added";

                        return product;
                    }
                    else
                    {
                        //edit product
                        Product productToEdit = (from x in context.Products where x.ProductID == product.ProductID select x).FirstOrDefault();
                        productToEdit.ProductName = product.ProductName;
                        productToEdit.ProductCode = product.ProductCode;
                        productToEdit.Price = product.Price;
                        productToEdit.Quantity = product.Quantity;
                        productToEdit.ProductID = product.ProductID;
                        context.SaveChanges();

                        action = "updated";

                        return product;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            

        }

        /// <summary>
        /// Delete product from warehouse
        /// </summary>
        /// <param name="id">Product id</param>
        public void DeleteProduct(int id)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    Product productToDelete = (from r in context.Products where r.ProductID == id select r).First();
                    context.Products.Remove(productToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Store not stored product in warehpuse
        /// </summary>
        /// <param name="id"></param>
        public void StoreProduct(int id)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    Product productToStore = (from r in context.Products where r.ProductID == id select r).First();
                    productToStore.Stored = "stored";
                    context.SaveChanges();

                    GetCurrentWarehouseQuantity();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
