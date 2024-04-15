using PROG6221POEPart1.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PROG6221POEPart1.Class.Class1 cls1 = new Class.Class1();

            Class1 recipeManager = new Class1();
            bool conLoop = true;
            Console.ForegroundColor = ConsoleColor.Yellow;

            while(conLoop)
            {
                Console.WriteLine("1. Enter recipe details");
                Console.WriteLine("2. Display recipe");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset recipe quantities");
                Console.WriteLine("5. Clear recipe data");
                Console.WriteLine("6. Exit");

                int choice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch(choice)
                {
                    case 1: recipeManager.RecipeDetails();
                        DisplayMenu(recipeManager);
                        break;
                    case 2: recipeManager.DisplayRecipe();
                        DisplayMenu(recipeManager);
                        break;
                    case 3: recipeManager.scaleRecipe();
                        DisplayMenu(recipeManager);
                        break;
                    case 4: recipeManager.ResetQuantities();
                        DisplayMenu(recipeManager);
                        break;
                    case 6: conLoop = false;
                        break;
                    default: Console.WriteLine("Please enter a vaid option.");
                        break;

                }
            }

          
        }
        
        static void DisplayMenu(Class1 recipe)
        {
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
