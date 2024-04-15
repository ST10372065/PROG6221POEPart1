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
        public class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; private set; }
            public string Unit { get; set; }
            private double originalQuantity;
            private string originalUnit;

            public Ingredient(string name, double quantity, string unit)
            {
                Name = name;
                Quantity = quantity;
                Unit = unit;
                originalQuantity = quantity;
                originalUnit = unit;
            }
            public void ScaleQuantity(double scalingFactor)
            {
                double scaledQuantity = originalQuantity * scalingFactor;

                // Conversions for common units
                if (Unit.Equals("tablespoon", StringComparison.OrdinalIgnoreCase))
                {
                    if (scaledQuantity >= 16)
                    {
                        int cups = (int)(scaledQuantity / 16);
                        scaledQuantity -= cups * 16;
                        Quantity = cups;
                        Unit = "cup";
                        if (scaledQuantity > 0)
                        {
                            Quantity += scaledQuantity / 16.0;
                            Unit += "s and " + scaledQuantity % 16 + " tablespoon(s)";
                        }
                    }
                    else
                    {
                        Quantity = scaledQuantity;
                    }
                }
                else if (Unit.Equals("teaspoon", StringComparison.OrdinalIgnoreCase))
                {
                    if (scaledQuantity >= 3)
                    {
                        int tablespoons = (int)(scaledQuantity / 3);
                        scaledQuantity -= tablespoons * 3;
                        Quantity = tablespoons;
                        Unit = "tablespoon";
                        if (scaledQuantity > 0)
                        {
                            Quantity += scaledQuantity / 3.0;
                            Unit += "s and " + scaledQuantity % 3 + " teaspoon(s)";
                        }
                    }
                    else
                    {
                        Quantity = scaledQuantity;
                    }
                }
                else
                {
                    Quantity = scaledQuantity;
                }
            }

            public void ResetQuantity()
            {
                Quantity = originalQuantity;
                Unit = originalUnit;
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

            public void scaleRecipe(double scale)
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.ScaleQuantity(scale);
                }
            }

            public void displayIngredients()
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
                }
            }

            public void displayRecipe()
            {
                Console.WriteLine($"Recipe: {Name}");
                Console.WriteLine($"Ingredients: ");

                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Name}");

                }
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Quantity of ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit}");
                }

                Console.WriteLine("\nSteps");
                int stepNumber = 1;
                foreach (Step step in steps)
                {
                    Console.WriteLine($"{stepNumber}. {step.Description}");
                    stepNumber++;
                }
            }

            public void resetQuantity()
            {
                foreach(Ingredient ingredient in ingredients)
                {
                    ingredient.ResetQuantity();
                }
            }
        }

        public void DisplayRecipe() // Making the method public
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }

            Console.WriteLine("Select a recipe to display: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());

            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.displayRecipe();
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
                    Console.Write($"Enter " + name + "'s quantity: ");
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

            Console.WriteLine("Please enter the number of steps required.");
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

        public void scaleRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
            }

            Console.WriteLine("Select a recipe to scale: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());

            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            double scalingFactor;
            bool validScalingFactor;

            do
            {
                Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                string factor = Console.ReadLine();
                validScalingFactor = double.TryParse(factor, out scalingFactor) && (scalingFactor == 0.5 || scalingFactor == 2 || scalingFactor == 3);

                if (!validScalingFactor)
                {
                    Console.WriteLine("Invalid scaling factor. Please enter 0.5, 2, or 3.");
                }
            } while (!validScalingFactor);

            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.scaleRecipe(scalingFactor);

            Console.WriteLine($"\nRecipe '{selectedRecipe.Name}' has been scaled by a factor of {scalingFactor}.");
            Console.WriteLine("Updated ingredient quantities: ");
            selectedRecipe.displayIngredients();
        }

        public void ResetQuantities()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }

            Console.WriteLine("Select a recipe to reset quantities: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());

            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.resetQuantity();

            Console.WriteLine($"Ingredient quantities for '{selectedRecipe.Name}' have been reset to original values.");
        }

    }
}
