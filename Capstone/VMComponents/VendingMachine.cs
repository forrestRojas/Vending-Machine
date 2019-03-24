using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DeliveryApp.CLIs;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents a Vending Machine.
    /// </summary>
    public class VendingMachine
    {
        /// <summary>
        /// Creates a log for the machine.
        /// </summary>
        private readonly Log _log = new Log();

        /// <summary>
        /// The current balance for the machine.
        /// </summary>
        public decimal CurrentBalance { get; private set; }

        /// <summary>
        /// A List that represents the products purchased by the customer BEFORE the finish transaction method is called.
        /// </summary>
        protected List<VendingMachineProduct> purchasedProducts = new List<VendingMachineProduct>();

        /// <summary>
        /// Represents the inventory of the machine based on slotCode and product.
        /// </summary>
        public Dictionary<string, VendingMachineProduct> inventory = new Dictionary<string, VendingMachineProduct>();

        /// <summary>
        /// Creates a Vending Machine.
        /// </summary>
        public VendingMachine()
        {
            this.CurrentBalance = 0;
            Inventory inv = new Inventory();
            inv.StockMachine(inventory);
        }

        /// <summary>
        /// Creates a Vending Machine based off a custom inventory file.
        /// </summary>
        /// <param name="path">the inventory file path</param>
        public VendingMachine(string path)
        {
            this.CurrentBalance = 0;
            Inventory inv = new Inventory();
            inv.StockMachine(inventory, path);
        }

        /// <summary>
        /// Method for adding money to Vending Machine.
        /// </summary>
        public void FeedMoney(int dollarsPutIn)
        {
            int currentMoneyProvided = 0;

            int[] validTender = new int[4] { 1, 2, 5, 10 };

            if (!validTender.Contains(dollarsPutIn))
            {
                CLI.ClearCurrentConsoleLine();
                Console.WriteLine("Please enter a valid bill ($1, $2, $5, $10)");
                Console.SetCursorPosition(23, 6);
            }
            else
            {               
                currentMoneyProvided += dollarsPutIn;              
            }
            this.CurrentBalance += currentMoneyProvided;

            Console.SetCursorPosition(0, 4);
            CLI.ClearCurrentConsoleLine();
            Console.Write($"Current money provided: {this.CurrentBalance:C2}\n");
            Console.SetCursorPosition(0, 6);

            LogEntry entry = new LogEntry("FEED MONEY: ", dollarsPutIn, this.CurrentBalance);
            this._log.AddLogEntry(entry);

        }

        /// <summary>
        /// Buys a product from the machine.
        /// </summary>
        /// <param name="slotCode">Slot code is passed in.</param>
        public void Buy(string slotCode)
        {
            // Adds method code for Buy method
            slotCode = slotCode.ToUpper();
            if (inventory.TryGetValue(slotCode.ToUpper(), out VendingMachineProduct selectedProduct))
            {
                if (selectedProduct == null)
                {
                    CLI.ClearCurrentConsoleLine();
                    Console.WriteLine("Product does not exist.");
                    Console.SetCursorPosition(0, 6);

                    return;
                }
                if (selectedProduct.Quantity == 0)
                {
                    CLI.ClearCurrentConsoleLine();
                    Console.WriteLine("Product is SOLD OUT.");
                    Console.SetCursorPosition(0, 6);

                    return;
                }
                 
                // Check if the user has enough money to buy the product.
                if (this.CurrentBalance < selectedProduct.Price)
                {
                    CLI.ClearCurrentConsoleLine();
                    Console.WriteLine("Not enough credits.");
                    Console.SetCursorPosition(0, 6);

                    return;
                }

                decimal loggedBalanceBeforeTransaction = this.CurrentBalance;

                this.CurrentBalance -= selectedProduct.Price;
                selectedProduct.Quantity--;

                Console.SetCursorPosition(0, 4);
                CLI.ClearCurrentConsoleLine();
                Console.Write($"Current money provided: {this.CurrentBalance:C2}\n");
                Console.SetCursorPosition(0, 6);

                LogEntry entry = new LogEntry($"{selectedProduct.Name} {slotCode}", loggedBalanceBeforeTransaction, this.CurrentBalance);

                this._log.AddLogEntry(entry);

                // Adds selected product to customer's inventory.
                this.purchasedProducts.Add(selectedProduct);

                // At the end of the buy method we reasigned the updated version of that product.
                this.inventory[slotCode] = selectedProduct;
            }
            else
            {
                Console.WriteLine("Not a valid slot code.");
                Console.SetCursorPosition(0, 6);
            }
        }

        /// <summary>
        /// This finalizes the transaction.
        /// </summary>
        /// <returns></returns>
        public void FinishTransaction()
        {
            decimal loggedBalanceBeforeTransaction = this.CurrentBalance;

            // Call change method.
            Change change = new Change();
            change.MakeChange(this.CurrentBalance);

            // Set machine's current balance to 0.
            this.CurrentBalance = 0;

            LogEntry entry = new LogEntry("GIVE CHANGE: ", loggedBalanceBeforeTransaction, this.CurrentBalance);

            this._log.AddLogEntry(entry);
            this._log.WriteToLog();

            // Print message based on snacks purchased.
            foreach (VendingMachineProduct product in purchasedProducts)
            {
                Console.WriteLine($"{product.ConsumedMessage()}");
            }

            // Gets list ready for next transaction.
            purchasedProducts.Clear();
            Console.ReadLine();
        }

        /// <summary>
        /// Displays machine inventory.
        /// </summary>
        /// <param name="inventory">Inventory dictionary passed in.</param>
        public void DisplayVendingMachineItems(Dictionary<string, VendingMachineProduct> inventory)
        {
            foreach (KeyValuePair<string, VendingMachineProduct> product in inventory)
            {
                string slotCode = product.Key;
                VendingMachineProduct currentProduct = product.Value;
                string name = currentProduct.Name;
                decimal price = currentProduct.Price; 
                string quantity = currentProduct.Quantity.ToString();
                if (quantity == "0")
                {
                    quantity = "SOLD OUT";
                } 
                Console.WriteLine($"{slotCode}|{name, -20}{price:C2}\tQuantity: {quantity}");
            }          
        }
    }
}
