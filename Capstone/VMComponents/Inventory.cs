﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents an inventory class
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Stocks the Machine based on input file
        /// </summary>
        /// <param name="inventory">The Machine's inventory that need to populated</param>
        public void StockMachine(Dictionary<string, VendingMachineProduct> inventory)
        {
            using (StreamReader sr = new StreamReader("VendingMachine.csv"))
            {
                while (!sr.EndOfStream)
                {
                    int i = 1;
                    string currentLine = sr.ReadLine();
                    string[] itemInfo = currentLine.Split('|');

                    try
                    {
                        VendingMachineProduct vendingMachineProduct = Assembly.GetExecutingAssembly().CreateInstance("Capstone.VMComponents." + itemInfo[itemInfo.Length - 1], true, BindingFlags.Default, null, new string[] { itemInfo[1], itemInfo[2] }, null, null) as VendingMachineProduct;

                        if (vendingMachineProduct != null )
                        {
                             inventory.Add(itemInfo[0], vendingMachineProduct);
                        }                      
                    }
                    catch(Exception ex)
                    {
                        Console.Write("Inventory could not fully load.");
                        Console.Write($"Line number: {i} could not be deserialized.");
                    }               
                    i++;
                }
            }
        }
    }
}
