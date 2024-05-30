using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; private set; }
        public string Unit { get; set; }
        private double originalQuantity;
        private string originalUnit;

        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        /// <summary>
        /// constructor for the ingredient class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        public Ingredient(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            originalQuantity = quantity;
            originalUnit = unit;
        }



        /// <summary>
        /// scales the quantity of the ingredient by the scaling factor
        /// </summary>
        /// <param name="scalingFactor"></param>
        public void ScaleQuantity(double scalingFactor)
        {
            double scaledQuantity = originalQuantity * scalingFactor;

            //if tablespoon or tablespoons are the entered unit of measurement and the quanity must be converted to cups it is done here
            if ((Unit.Equals("tablespoon", StringComparison.OrdinalIgnoreCase) || (Unit.Equals("tablespoons", StringComparison.OrdinalIgnoreCase))))
            {   //16 tablespoons = 1 cup
                if (scaledQuantity >= 16)
                {
                    int cups = (int)(scaledQuantity / 16);
                    scaledQuantity -= cups * 16;
                    Quantity = cups;
                    Unit = "cup";
                    if (scaledQuantity > 0)
                    {
                        Quantity += scaledQuantity / 16.0;
                        //this is if it is required to say 1 cup and 7 tablespons for example
                        Unit += "s and " + scaledQuantity % 16 + " tablespoon(s)";
                    }
                }
                else
                {
                    Quantity = scaledQuantity;
                }
            } //same as tablespoon to cup this time it is just teaspoon to tablespoon
            else if ((Unit.Equals("teaspoon", StringComparison.OrdinalIgnoreCase) || (Unit.Equals("teaspoons", StringComparison.OrdinalIgnoreCase))))
            {
                //3 teaspoons = 1 tablespoon
                if (scaledQuantity >= 3)
                {
                    int tablespoons = (int)(scaledQuantity / 3);
                    //adjust scaled quantity to remove portion represented by whole tablespoons
                    scaledQuantity -= tablespoons * 3;
                    //update quantity property to store the number of tablespoons  
                    Quantity = tablespoons;
                    Unit = "tablespoon";
                    //check to see if there are any remaining teaspoons after converting
                    if (scaledQuantity > 0)
                    {
                        Quantity += scaledQuantity / 3.0;
                        // to erts if there are leftover teaspoons
                        Unit += "s and " + scaledQuantity % 3 + " teaspoon(s)";
                    }
                }
                else
                {// Assign the scaled quantity directly to the Quantity property if the quantity is less than 3 teaspoons
                    Quantity = scaledQuantity;
                }
            }
            else
            {// Assign the scaled quantity directly to the Quantity property if the unit is not "teaspoon(s)"
                Quantity = scaledQuantity;
            }
        }


        /// <summary>
        /// resets the quantity and unit of the ingredient to their original values
        /// </summary>
        public void ResetQuantity()
        {//resets to original quantity
            Quantity = originalQuantity;
            //resets to orginal unit    
            Unit = originalUnit;
        }
    }
}
