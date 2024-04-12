using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    internal class Class1
    {
        private List<Ingredient> ingredients = new List<Ingredient>();
        private List<Step> steps = new List<Step>();
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

        class Step
        {
            public string Description { get; set; }
            public Step(string description)
            {
                Description = description;
            }
        }

        public void RecipeDetails()
        {
            Console.Write("please enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());
            string name = "";
            double quantity = 0;
            string unit = "";

            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Enter ingredient {i + 1}'s name: ");
                name = Console.ReadLine();

                bool QuantityValid;
                do
                {
                    Console.Write($"Enter ingredient {i + 1}'s quantity: ");
                    QuantityValid = double.TryParse(Console.ReadLine(), out quantity);
                    if (!QuantityValid)
                    {
                        Console.WriteLine("Please enter only a number.");
                    }
                } while (!QuantityValid);

                Console.Write($"Enter ingredient {i + 1}'s unit of measurement: ");
                unit = Console.ReadLine();
                
                Ingredient ingredient = new Ingredient(name, quantity, unit);
                ingredients.Add(ingredient);
            }

            Console.WriteLine("Please enter the nuber of steps required.");
            int numStep = int.Parse(Console.ReadLine());

            for (int i = 0; i < numStep; i++)
            {
                Console.WriteLine($"Please enter step {i + 1}: ");
                string stepDisc = Console.ReadLine();
                Step step = new Step(stepDisc);
                steps.Add(step);
            }
            //Console.Clear();

            /*for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Ingredient {i + 1}:" + name);
                Console.WriteLine($"Quantity of ingredient {i + 1}:" + quantity);
                Console.WriteLine($"Unit of measurement {i + 1}:" + unit);
            }*/



        }
    }
}
