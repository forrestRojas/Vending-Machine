using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents a Vending Machine Product
    /// </summary>
    public abstract class VendingMachineProduct
    {
        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Creates a Vending machine product
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public VendingMachineProduct(string name, string price)
        {
            this.Quantity = 5;
            this.Name = name;
            this.Price = decimal.Parse(price);
        }

        /// <summary>
        /// Returns the consumed Message.
        /// </summary>
        /// <returns></returns>
        public abstract string ConsumedMessage();
    }
}
