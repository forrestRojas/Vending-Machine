using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.VMComponents
{
    /// <summary>
    /// Represents a Change Class
    /// </summary>
    public class Change
    {
        /// <summary>
        /// The Value of each coin
        /// </summary>
        private readonly int[] _coinValues = { 25, 10, 5 };

        /// <summary>
        /// Represents the number of Quarters
        /// </summary>
        public int Quarters { get; private set; }
        
        /// <summary>
        /// Represents the number of Dimes
        /// </summary>
        public int Dimes { get; private set; }

        /// <summary>
        /// Represents the number of Nickels
        /// </summary>
        public int Nickels { get; private set; }


        /// <summary>
        /// Creates a Change class
        /// </summary>
        public Change()
        {
            this.Quarters = 0;
            this.Dimes = 0;
            this.Nickels = 0;
        }

        /// <summary>
        /// Generates the quantity of each coin
        /// </summary>
        /// <param name="balance"></param>
        public void MakeChange(decimal balance)
        {
            // Rounding erorr fix by adding ()
            int workingBalance = 0;
            decimal comparingBalance = balance * 100;

            for (int i = 0; i < this._coinValues.Length; i++)
            {
                while (workingBalance < comparingBalance)
                {
                    // Fixed when next coin shold be used, 
                    if ((workingBalance + this._coinValues[i]) > comparingBalance)
                    {
                        break;
                    }
                    workingBalance += this._coinValues[i];
                    if (i == 0)
                    {
                        this.Quarters++;
                    }
                    else if (i == 1)
                    {
                        this.Dimes++;
                    }
                    else
                    {
                        this.Nickels++;
                    }
                }
            }
            balance = 0;
        }
    }
}
