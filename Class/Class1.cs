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
            //how to declare a generic collection: https://www.tutorialsteacher.com/csharp/csharp-collection
            private List<Ingredient> ingredients = new List<Ingredient>();
            private List<Step> steps = new List<Step>();
            public ExceededCaloriesDelegate ExceededCalories { get; set; }

            public Recipe(string name)
            {
                Name = name;
            }

            /// <summary>
            /// total calories is calculated by summing the calories of all the ingredients in the recipe
            /// </summary>
            public int TotalCalories
            {
                get
                {
                    return ingredients.Sum(ingredient => ingredient.Calories);
                }
            }

            /// <summary>
            /// ingredients are added to the recipe
            /// </summary>
            /// <param name="ingredient"></param>
            public void addIngredient(Ingredient ingredient)
            {
                ingredients.Add(ingredient);
            }

            /// <summary>
            /// steps are added to the recipe
            /// </summary>
            /// <param name="step"></param>
            public void addStep(Step step)
            {
                steps.Add(step);
            }


            /// <summary>
            /// the recipe is scaled by the entered factor. the quantity of each ingredient is multiplied by the scaling factor
            /// </summary>
            /// <param name="scale"></param>
            public void scaleRecipe(double scale)
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.ScaleQuantity(scale);
                }
            }

            //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// displays the ingredients of the recipe
            /// </summary>
            public void displayIngredients()
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
                }
            }

            //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// displays the recipe. the name, ingredients, quantity of each ingredient, steps, and total calories are displayed. a warning is displayed if the total calories exceed 300
            /// how to invoke a delegate : https://www.tutorialsteacher.com/csharp/csharp-delegates#:~:text=After%20setting%20a%20target%20method,or%20using%20the%20()%20operator.&text=del.,full%20example%20of%20a%20delegate.
            /// </summary>
            public void displayRecipe()
            {
                Console.WriteLine($"Recipe: {Name}");
                Console.WriteLine($"Ingredients: ");
                //displays the name of each ingredient
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Name}");
                }
                //displays the quantity of each ingredient
                for (int i = 0; i < ingredients.Count; i++)
                {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine($"Quantity of ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit}");
                }
                //displays the steps
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
                    ExceededCalories?.Invoke("Warning: The total calories of this recipe exceed 300.");
                }
            }

            //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

            /// <summary>
            /// resets the quantity of each ingredient to the original quantity
            /// </summary>
            public void resetQuantity()
            {
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.ResetQuantity();
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// represents an ingredient in a recipe
        /// </summary>
        /// <param name="recipe"></param>
        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            recipes.Sort((recipe1, recipe2) => recipe1.Name.CompareTo(recipe2.Name));
        }

        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// displays all the recipes. the user is prompted to select a recipe to display. the recipe is then displayed
        /// </summary>
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
            //error handling for invalid recipe number
            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }
            //displays the selected recipe from the list of recipes
            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.displayRecipe();
        }

        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// recipe details contains all the inforation about the recipe. all the data entered by the user is handled here. all the error handling is also done here. Many while loops used as they are used to ensure that
        /// user is prompted to enter the correct data.
        /// </summary>
        public void RecipeDetails()
        {
            string name = "";
            double quantity = 0;
            string unit = "";
            Recipe newRecipe;

            while (true)
            {
                //user enters the recipe name
                Console.WriteLine("Please enter the recipe name: ");
                string recipeName = Console.ReadLine();

                double temp;
                //checks if the recipe name is not a number. if a double is entered, it will prompt the user to enter a valid name
                if (!double.TryParse(recipeName, out temp))
                {
                    newRecipe = new Recipe(recipeName);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid recipe name. The name cannot be a number.");
                }
            }

            int numIngredients;
            while (true)
            {
                //user enters the number of ingredients
                Console.Write("Please enter the number of ingredients: ");

                //checks if the number of ingredients is a valid number
                if (int.TryParse(Console.ReadLine(), out numIngredients) && numIngredients >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid number of ingredients. Please enter a valid number.");
                }
            }


            for (int i = 0; i < numIngredients; i++)
            {
                while (true)
                {
                    //user enters the ingredient name
                    Console.Write($"Enter ingredient {i + 1}'s name: ");
                    name = Console.ReadLine();

                    double temp;
                    //checks if the ingredient name is not a number
                    if (!double.TryParse(name, out temp)) 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid ingredient name. The name cannot be a number.");
                    }
                }

                bool QuantityValid;

                do
                {
                    //user enters the quantity of the ingredient. QuantityValid is used to check if the quantity is a valid number
                    Console.Write($"Enter " + name + "'s quantity: ");
                    QuantityValid = double.TryParse(Console.ReadLine(), out quantity);
                    if (!QuantityValid)
                    {
                        Console.WriteLine("Please enter only a number.");
                    }
                } while (!QuantityValid);

                
                while (true)
                {
                    //user enters the unit of measurement
                    Console.Write($"Enter " + name + "'s unit of measurement: ");
                    unit = Console.ReadLine();

                    double temp;
                    //checks if the unit of measurement is not a number
                    if (!double.TryParse(unit, out temp)) 
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid unit of measurement. The unit cannot be a number.");
                    }
                }

                int calories;
                while (true)
                {
                    //user enters the number of calories
                    Console.Write($"Enter " + name + "'s calories: ");
                    //checks if the number of calories is a valid number
                    if (int.TryParse(Console.ReadLine(), out calories) && calories >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of calories. Please enter a valid number.");
                    }
                }

                string foodGroup;
                while (true)
                {
                    //user enters the food group
                    Console.Write($"Enter " + name + "'s food group: ");
                    foodGroup = Console.ReadLine();

                    double temp;
                    //checks if the food group is not a number
                    if (!double.TryParse(foodGroup, out temp))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid food group. The food group cannot be a number.");
                    }
                }

                //creates a new ingredient object with the entered data
                Ingredient ingredient = new Ingredient(name, quantity, unit)
                {
                    Calories = calories,
                    FoodGroup = foodGroup
                };
                //adds the ingredient to the recipe
                newRecipe.addIngredient(ingredient);
            }

            int numStep;
            while (true)
            {
                //user enters the number of steps
                Console.Write("Please enter the number of steps: ");
                //checks if the number of steps is a valid number
                if (int.TryParse(Console.ReadLine(), out numStep) && numStep >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid number of steps. Please enter a non-negative number.");
                }
            }

            //user enters the steps
            for (int i = 0; i < numStep; i++)
            {
                Console.WriteLine($"Please enter step {i + 1}: ");
                string stepDisc = Console.ReadLine();
                Step step = new Step(stepDisc);
                newRecipe.addStep(step);
            }
            //adds the recipe to the list of recipes
            AddRecipe(newRecipe);
            //clears the console
            Console.Clear();
        }



        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// this method is used to scale the recipe. the user is prompted to enter the recipe number and the scaling factor. the recipe is then scaled by the entered factor.
        /// </summary>
        public void scaleRecipe()
        {
            //checks if there are any recipes in the list
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }
            //displays the list of recipes
            Console.WriteLine("Select a recipe to scale: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }
            //user enters the recipe number
            Console.Write("Enter the recipe number: ");
            int recipeNumber;
            if (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            double scalingFactor;
            bool validScalingFactor;
            //user enters the scaling factor
            do
            {
                Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                string factor = Console.ReadLine();
                //checks if the scaling factor is a valid number
                validScalingFactor = double.TryParse(factor, out scalingFactor) && (scalingFactor == 0.5 || scalingFactor == 2 || scalingFactor == 3);
                if (!validScalingFactor)
                {
                    Console.WriteLine("Invalid scaling factor. Please enter 0.5, 2, or 3.");
                }
            } while (!validScalingFactor);
            //the selected recipe is scaled by the entered factor
            Recipe selectedRecipe = recipes[recipeNumber - 1];
            //the recipe is scaled
            selectedRecipe.scaleRecipe(scalingFactor);
            //the updated ingredient quantities are displayed
            Console.WriteLine($"\nRecipe '{selectedRecipe.Name}' has been scaled by a factor of {scalingFactor}.");
            Console.WriteLine("Updated ingredient quantities: ");
            selectedRecipe.displayIngredients();
        }

        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// resets the quantities of the ingredients in the recipe to the original quantities
        /// </summary>

        public void resetQuantities()
        {
            //checks if there are any recipes in the list
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }
            //displays the list of recipes
            Console.WriteLine("Select a recipe to reset quantities: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }
            //user enters the recipe number
            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());
            //error handling for invalid recipe number
            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }
            //the quantities of the ingredients in the selected recipe are reset
            Recipe selectedRecipe = recipes[recipeNumber - 1];
            selectedRecipe.resetQuantity();
            //the updated ingredient quantities are displayed
            Console.WriteLine($"Ingredient quantities for '{selectedRecipe.Name}' have been reset to original values.");
        }

        //////////////////////////////////////////////////////////////////////////////method break///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// clears the recipe. the user is prompted to select a recipe to remove. the recipe is then removed from the list of recipes
        /// </summary>

        public void clearRecipe()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }
            //displays the list of recipes
            Console.WriteLine("Select a recipe to remove: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }
            //user enters the recipe number
            Console.Write("Enter the recipe number: ");
            int recipeNumber;
            if (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }
            //the selected recipe is removed from the list of recipes
            Recipe selectedRecipe = recipes[recipeNumber - 1];
            //confirmation is asked from the user before removing the recipe
            Console.WriteLine($"Are you sure you want to remove the recipe '{selectedRecipe.Name}'? (yes/no)");
            string confirmation = Console.ReadLine();
            //error handling for invalid confirmation
            if (string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
            {
                recipes.Remove(selectedRecipe);
                Console.WriteLine($"Recipe '{selectedRecipe.Name}' has been removed.");
            }
            //if the user enters 'no', the recipe is not removed
            else if (string.Equals(confirmation, "no", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Recipe was not removed.");
            }
            else
            {
                Console.WriteLine("Please enter 'yes' or 'no'.");
            }
        }
    }
}

