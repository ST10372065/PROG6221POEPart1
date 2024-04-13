using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    internal class Class1
    {
        private List<Recipe> recipes = new List<Recipe>();
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

        class Recipe
        {
            public string Name { get; set; }
            private List<Ingredient> ingredients = new List<Ingredient>();
            private List<Step> steps = new List<Step>();

            public Recipe(string name)
            {
                Name = name;
            }

            public void addIngredient(Ingredient ingredient)
            {
                ingredients.Add(ingredient);

            }

            public void addStep(Step step)
            {
                steps.Add(step);
            }

            public void displayRecipe()
            {
                Console.WriteLine($"Recipe: {Name}");
                Console.WriteLine($"Ingredients: ");
                
                for(int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Name}");
                    Console.WriteLine($"Quantity of ingredient {i + 1}: {ingredient.Quantity}{ingredient.Unit}");
                }

                Console.WriteLine("\nSteps");
                int stepNumber = 1;
                foreach(Step step in steps)
                {
                    Console.WriteLine($"{stepNumber}. {step.Description}");
                    stepNumber++;
                }
            }
        }

        public void RecipeDetails()
        {
            string name = "";
            double quantity = 0;
            string unit = "";

            Console.WriteLine("Please enter the recipe name: ");
            string recipeName = Console.ReadLine();

            Recipe newRecipe = new Recipe(recipeName);

            Console.Write("please enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());
            

            for (int i = 0; i < numIngredients; i++)
            {
                Console.Write($"Enter ingredient {i + 1}'s name: ");
                name = Console.ReadLine();

                bool QuantityValid;
                do
                {
                    Console.Write($"Enter " + name +"'s quantity:");
                    QuantityValid = double.TryParse(Console.ReadLine(), out quantity);
                    if (!QuantityValid)
                    {
                        Console.WriteLine("Please enter only a number.");
                    }
                } while (!QuantityValid);

                Console.Write($"Enter " + name + "'s unit of measurement: ");
                unit = Console.ReadLine();
                
                Ingredient ingredient = new Ingredient(name, quantity, unit);
                newRecipe.addIngredient(ingredient);
            }

            Console.WriteLine("Please enter the nuber of steps required.");
            int numStep = int.Parse(Console.ReadLine());

            for (int i = 0; i < numStep; i++)
            {
                Console.WriteLine($"Please enter step {i + 1}: ");
                string stepDisc = Console.ReadLine();
                Step step = new Step(stepDisc);
                newRecipe.addStep(step);
            }
            recipes.Add(newRecipe);
            Console.Clear();
        }

        public void Displayrecipe()
        {

        }
    }
}
