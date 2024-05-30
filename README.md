# Part 2

## Update

Changes implemented from part one inclucde things such as, adding references, updating comments and small other bits. Ensured that all error handling was implemented so that a user can only enter valid data into the program when entering a recipe.
Major change made was splitting code into different classes to better coding standards.
Also emsured that .NET Framework 4.8 was being used.
Almost all the error handling is done by While loops, as if the incorrect information is entered the user will be prompted to enter the information again. This is mainly done in the recipeDetails method as that is where the user enteres the most data.




## Recipe management application

This application is used to store and scale recipes. This applications allows the user to enter recipe of their choice and then lets the user decided on what they would like the application to do. This includes things such as: 
* Entering recipe name and ingredients
* Displaying recipes
* Scaling recipe
* Resting to original factors
* Clearing recipes

### Using the application
Apon starting the application the user is given 6 prompts which they can enter. Only one will work as a recipe needs to be entered before any other methods are able to be used.

After entering a recipe name the user is given prompts to enter how many ingredients, name of ingredient, quantity of the ingredient and finally the unit of measurement of that ingredient.

Once complete user can display the recipe they have just entered. This displays all the information in a neat and readable format.

When scaling the recipe the user has the choice to scale the recipe by half, two or three. Meaning this will either half , double or triple the amount of ingredients that were entered in the begining.

If the recipe has been scaled and the user wants to use the original values again they can simply reset the values.

If the user would like to clear all the saved recipes they can also do so and they will be required to confirm when doing this.

### Repository link

https://github.com/ST10372065/ST10372065_PROG6221_PART1_ZackStangroom
