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
        /// <summary>
        /// track current product quantity in warehouse
        /// </summary>
        public static int currentWarehouseQuantity;

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
    }
}
