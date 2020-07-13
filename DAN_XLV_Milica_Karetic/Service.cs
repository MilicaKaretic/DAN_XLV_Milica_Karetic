using DAN_XLV_Milica_Karetic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLV_Milica_Karetic
{
    public class Service
    {

        public Service()
        {
            onNotification = log.WriteActionToFile;
        }

        Logger log = new Logger();

        #region Delegate logger

        //delegate for notification user
        public delegate void Notification(string text);
        //event based on that delegate
        public event Notification onNotification;

        /// <summary>
        /// Raise an event
        /// </summary>
        internal virtual void Notify(string text)
        {
            if (onNotification != null)
            {
                onNotification(text);
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

        public Product AddProduct(Product product)
        {
            try
            {
                using (WarehouseDBEntities context = new WarehouseDBEntities())
                {
                    if(product.ProductID == 0)
                    {
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
                        //edit
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
