using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    internal class Class1
    {
        public static void Ingredients()
        {
            Console.WriteLine("Please enter the amount of ingredients");
            String numIngr = Console.ReadLine();

            int numIngredients;

            if (int.TryParse(numIngr, out numIngredients)) //convert string to int
            {
                string numberAsString = numIngredients.ToString(); // Convert int to string
                Console.WriteLine($"You entered: {numberAsString}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }

        }
    }
}
