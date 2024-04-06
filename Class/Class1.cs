using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    internal class Class1
    {
        class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; private set; }
            public string Unit { get; set; }
            private double originalQuantity;

            public Ingredient(string name, double quantity, string unit)
            {
                Name = name;
                Quantity = quantity;
                Unit = unit;
                originalQuantity = quantity;
            }
        }

        public void RecipeDetails()
        {
            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Enter ingredient {i + 1} name: ");
                string name = Console.ReadLine();
                Console.Write($"Enter ingredient {i + 1} quantity: ");
                double quantity = double.Parse(Console.ReadLine());
                Console.Write($"Enter ingredient {i + 1} unit of measurement: ");
                string unit = Console.ReadLine();
            }


        }
    }
}
