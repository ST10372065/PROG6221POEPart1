using PROG6221POEPart1.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POEPart1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PROG6221POEPart1.Class.Class1 cls1 = new Class.Class1();
            Class1.Ingredients();
            bool conLoop = true;

            while(conLoop)
            {
                Console.WriteLine("1. Enter recipe details");
                Console.WriteLine("2. Display recipe");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset recipe quantities");
                Console.WriteLine("5. Clear recipe data");
                Console.WriteLine("6. Exit");
            }

            
        }
    }
}
