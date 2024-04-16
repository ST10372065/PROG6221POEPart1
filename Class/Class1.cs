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
                
            //constructor for ingredient properties
            public Ingredient(string name, double quantity, string unit)    
            {
                Name = name;
                Quantity = quantity;
                Unit = unit;
                originalQuantity = quantity;
                originalUnit = unit;
            }

            //method to scale quantities of the ingredients
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

            public void ResetQuantity()
            {//resets to original quantity
                Quantity = originalQuantity;
                //resets to orginal unit    
                Unit = originalUnit;            
            }
        }


        class Step
        {
            //getter and setter
            public string Description { get; set; } 
            public Step(string description)
            {
                Description = description;
            }
        }

        class Recipe
        {
            public string Name { get; set; }
            //private list to store ingredients of recipe
            private List<Ingredient> ingredients = new List<Ingredient>(); 
            //private list to store steps of recipe     
            private List<Step> steps = new List<Step>();        

            public Recipe(string name)
            {
                Name = name;
            }

            public void addIngredient(Ingredient ingredient)
            {
                //add the ingredients to the iingredient list
                ingredients.Add(ingredient);        

            }

            public void addStep(Step step)
            {
                //add the steps to step list
                steps.Add(step);        
            }

            public void scaleRecipe(double scale)
            {
                //loop through each ingredient in the recipe
                foreach (Ingredient ingredient in ingredients)  
                {
                    //scale quantity of current ingredient to the provied scaling factor
                    ingredient.ScaleQuantity(scale);        
                }   
            }

            public void displayIngredients()
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    //get the current ingredient
                    Ingredient ingredient = ingredients[i];         
                    //displays the detail of the ingredient
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");     
                }
            }

            public void displayRecipe()
            {
                //displays name
                Console.WriteLine($"Recipe: {Name}"); 
                //displays ingredients      
                Console.WriteLine($"Ingredients: ");        

                //loop through each ingredient in the recipe
                for (int i = 0; i < ingredients.Count; i++)     
                {
                    //gets current ingredient
                    Ingredient ingredient = ingredients[i];     
                    //displays the name of the current ingredient
                    Console.WriteLine($"Ingredient {i + 1}: {ingredient.Name}");        

                }
                //loops through each ingredient again to display their quantity
                for (int i = 0; i < ingredients.Count; i++) 
                {
                    Ingredient ingredient = ingredients[i];
                    //gets the quantity and unit of measurement for the current ingredient
                    Console.WriteLine($"Quantity of ingredient {i + 1}: {ingredient.Quantity} {ingredient.Unit}");      
                }

                Console.WriteLine("\nSteps");
                int stepNumber = 1;
                //loops through each step in the recipe
                foreach (Step step in steps)    
                {
                    //displays the step number and the step itself
                    Console.WriteLine($"{stepNumber}. {step.Description}");     
                    //increment step number
                    stepNumber++;       
                }
            }

            public void resetQuantity()
            {
                foreach(Ingredient ingredient in ingredients)
                {
                    //reset the quantity of current ingredient to original value
                    ingredient.ResetQuantity();     
                }
            }
        }

        // Making the method public
        public void DisplayRecipe() 
        {
            //checcks if there are recipes in the list
            if (recipes.Count == 0)     
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }

            Console.WriteLine("Select a recipe to display: ");

            //displays a list of available recipes
            for (int i = 0; i < recipes.Count; i++)     
            {
                //displays index and nae of each recipe
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");       
            }

            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());
            
            //checks if it is a valid recipe number
            if (recipeNumber < 1 || recipeNumber > recipes.Count)       
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }
            //gets recipe from object
            Recipe selectedRecipe = recipes[recipeNumber - 1]; 
            //displays details of selected recipe using displayRecipe     
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

            //loops through each ingredient
            for (int i = 0; i < numIngredients; i++)        
            {
                //enter name of ingredient
                Console.Write($"Enter ingredient {i + 1}'s name: ");        
                name = Console.ReadLine();

                //bool used for error handling
                bool QuantityValid;     
                //repeats until a process is valid
                do
                {       
                    Console.Write($"Enter " + name + "'s quantity: ");
                    QuantityValid = double.TryParse(Console.ReadLine(), out quantity);
                    //not a number an error message appears
                    if (!QuantityValid)     
                    {
                        Console.WriteLine("Please enter only a number.");   
                    }
                //continues to loop untila valid quantity is entered
                } while (!QuantityValid);       

                Console.Write($"Enter " + name + "'s unit of measurement: ");
                unit = Console.ReadLine();

                //create a new ingredient object with information from above
                Ingredient ingredient = new Ingredient(name, quantity, unit); 
                //add the new ingredient to the recipe being constructed  
                newRecipe.addIngredient(ingredient);        
            }

            Console.WriteLine("Please enter the number of steps required.");
            //user enters the number of steps required
            int numStep = int.Parse(Console.ReadLine());        
            //loops the amoun tof times the user entered for steps
            for (int i = 0; i < numStep; i++)       
            {
                //user enters the step
                Console.WriteLine($"Please enter step {i + 1}: ");      
                string stepDisc = Console.ReadLine();
                //create a new step object with collected description
                Step step = new Step(stepDisc);   
                //add the new step to the recipe being constructed 
                newRecipe.addStep(step);            
            }
            //adds newly created recipe to the list of recipes
            recipes.Add(newRecipe);     
            Console.Clear();
        }

        
        public void scaleRecipe()
        {
            //check if there are any recipes in the list. if not error message is given
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }
            //list of recipes to scale
            Console.WriteLine("Select a recipe to scale: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                //displays then index and name of each recipe
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.Write("Enter the recipe number: ");
            int recipeNumber = int.Parse(Console.ReadLine());

            //checks if valid recipe number
            if (recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid recipe number.");
                return;
            }

            //store scaling factor
            double scalingFactor;
            //store validity
            bool validScalingFactor;

            //loop to ensure that valid scaling factor
            do
            {   
                Console.Write("Enter scaling factor (0.5, 2, or 3): ");
                string factor = Console.ReadLine();
                //ensures that entered scaling factor is either 0.5, 2 or 3
                validScalingFactor = double.TryParse(factor, out scalingFactor) && (scalingFactor == 0.5 || scalingFactor == 2 || scalingFactor == 3);
                //if factor is not valid error message
                if (!validScalingFactor)
                {
                    Console.WriteLine("Invalid scaling factor. Please enter 0.5, 2, or 3.");
                }
            } while (!validScalingFactor);  //repeat until valid

            //get recipe wobject with correct selected number
            Recipe selectedRecipe = recipes[recipeNumber - 1];
            //scale the selected recipe
            selectedRecipe.scaleRecipe(scalingFactor);

            Console.WriteLine($"\nRecipe '{selectedRecipe.Name}' has been scaled by a factor of {scalingFactor}.");
            //display the updated ingredient quantities of scaled recipe
            Console.WriteLine("Updated ingredient quantities: ");
            selectedRecipe.displayIngredients();
        }

        public void resetQuantities()
        {
            //check if recipes exist
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found. Please enter a recipe first.");
                return;
            }
            //display a list of recipes 
            Console.WriteLine("Select a recipe to reset quantities: ");
            for (int i = 0; i < recipes.Count; i++)
            {
                //index and name of each recipe
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
            //reset the qauntities of ingredietns in the selected recipe
            selectedRecipe.resetQuantity();

            Console.WriteLine($"Ingredient quantities for '{selectedRecipe.Name}' have been reset to original values.");
        }

        public void clearRecipe()
        {
            Console.WriteLine("Are you sure you want to clear ALL recipe data? (yes/no)");
            string confirmation = Console.ReadLine();

            //check the users choice
            if (string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
            {
                //if yes then clears the list
                recipes.Clear();
                Console.WriteLine("All recipe data has been cleared.");
            }
            else if (string.Equals(confirmation, "no", StringComparison.OrdinalIgnoreCase))
            {
                //recipes are not delted if they
                Console.WriteLine("Recipes were not deleted");
            }
            else
            {
                //if invalid response is entered
                Console.WriteLine("Please enter 'yes' or 'no'.");
            }
        }
    }
}
