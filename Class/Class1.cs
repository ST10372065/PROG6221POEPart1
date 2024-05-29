using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1.Class
{
    public class Class1
    {
        private List<Recipe> recipes = new List<Recipe>();
        public delegate void ExceededCaloriesDelegate(string message);

        public class Recipe
        {
            public string Name { get; set; }
            private List<Ingredient> ingredients = new List<Ingredient>();
            private List<Step> steps = new List<Step>();
            public ExceededCaloriesDelegate ExceededCalories { get; set; }

            public Recipe(string name)
            {
                Name = name;
            }

            public int TotalCalories
            {
                get
                {
                    return ingredients.Sum(ingredient => ingredient.Calories);
                }
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

                int totalCalories = ingredients.Sum(ingredient => ingredient.Calories);
                Console.WriteLine($"Total calories: {totalCalories}");
                if (totalCalories > 300)
                {
                    Console.WriteLine("Warning: The total calories of this recipe exceed 300.");
                }
            }

            public void resetQuantity()
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.ResetQuantity();
                }
            }
        }

        public void DisplayRecipe()
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

                Console.Write($"Enter " + name + "'s calories: ");
                int calories = int.Parse(Console.ReadLine());
                Console.Write($"Enter " + name + "'s food group: ");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient(name, quantity, unit)
                {
                    Calories = calories,
                    FoodGroup = foodGroup
                };
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
                return;
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

        public void resetQuantities()
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

        public void clearRecipe()
        {
            Console.WriteLine("Are you sure you want to clear ALL recipe data? (yes/no)");
            string confirmation = Console.ReadLine();

            if (string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
            {
                recipes.Clear();
                Console.WriteLine("All recipe data has been cleared.");
            }
            else if (string.Equals(confirmation, "no", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Recipes were not deleted");
            }
            else
            {
                Console.WriteLine("Please enter 'yes' or 'no'.");
            }
        }
    }



}
    
        //public class Ingredient
        //{
        //    public string Name { get; set; }
        //    public double Quantity { get; private set; }
        //    public string Unit { get; set; }
        //    private double originalQuantity;
        //    private string originalUnit;
                
        //    //constructor for ingredient properties
        //    public Ingredient(string name, double quantity, string unit)    
        //    {
        //        Name = name;
        //        Quantity = quantity;
        //        Unit = unit;
        //        originalQuantity = quantity;
        //        originalUnit = unit;
        //    }

        //    //method to scale quantities of the ingredients
        //    public void ScaleQuantity(double scalingFactor) 
        //    {
        //        double scaledQuantity = originalQuantity * scalingFactor;

        //        //if tablespoon or tablespoons are the entered unit of measurement and the quanity must be converted to cups it is done here
        //        if ((Unit.Equals("tablespoon", StringComparison.OrdinalIgnoreCase) || (Unit.Equals("tablespoons", StringComparison.OrdinalIgnoreCase))))    
        //        {   //16 tablespoons = 1 cup
        //            if (scaledQuantity >= 16)   
        //            {
        //                int cups = (int)(scaledQuantity / 16);
        //                scaledQuantity -= cups * 16;
        //                Quantity = cups;
        //                Unit = "cup";
        //                if (scaledQuantity > 0)
        //                {
        //                    Quantity += scaledQuantity / 16.0;
        //                    //this is if it is required to say 1 cup and 7 tablespons for example
        //                    Unit += "s and " + scaledQuantity % 16 + " tablespoon(s)";  
        //                }
        //            }
        //            else
        //            {
        //                Quantity = scaledQuantity;
        //            }
        //        } //same as tablespoon to cup this time it is just teaspoon to tablespoon
        //        else if ((Unit.Equals("teaspoon", StringComparison.OrdinalIgnoreCase) || (Unit.Equals("teaspoons", StringComparison.OrdinalIgnoreCase))))  
        //        {
        //            //3 teaspoons = 1 tablespoon
        //            if (scaledQuantity >= 3)    
        //            {
        //                int tablespoons = (int)(scaledQuantity / 3);
        //                //adjust scaled quantity to remove portion represented by whole tablespoons
        //                scaledQuantity -= tablespoons * 3;
        //                //update quantity property to store the number of tablespoons  
        //                Quantity = tablespoons;     
        //                Unit = "tablespoon";
        //                //check to see if there are any remaining teaspoons after converting
        //                if (scaledQuantity > 0)     
        //                {
        //                    Quantity += scaledQuantity / 3.0;
        //                    // to erts if there are leftover teaspoons
        //                    Unit += "s and " + scaledQuantity % 3 + " teaspoon(s)";     
        //                }
        //            }
        //            else
        //            {// Assign the scaled quantity directly to the Quantity property if the quantity is less than 3 teaspoons
        //                Quantity = scaledQuantity;  
        //            }
        //        }
        //        else
        //        {// Assign the scaled quantity directly to the Quantity property if the unit is not "teaspoon(s)"
        //            Quantity = scaledQuantity;  
        //        }
        //    }

        //    public void ResetQuantity()
        //    {//resets to original quantity
        //        Quantity = originalQuantity;
        //        //resets to orginal unit    
        //        Unit = originalUnit;            
        //    }
        //}


        //class Step
        //{
        //    //getter and setter
        //    public string Description { get; set; } 
        //    public Step(string description)
        //    {
        //        Description = description;
        //    }
        //}

