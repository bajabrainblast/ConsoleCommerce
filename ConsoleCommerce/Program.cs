using Library.ConsoleCommerce.Services;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // create the establishment of commerce 
            var store = new Commerce();
            
            Console.WriteLine("Welcome to Console Commerce.");
            
            while (true)
            {
                Console.WriteLine("\nWould you like to:\n\t(1) Enter as an Employee\n\t(2) Enter as a Customer\n\t(0) Exit");
                var input = store.TakeInput(0, 2);

                if (input == 1)
                    store.EmployeeLogic();
                else if (input == 2)
                    store.CustomerLogic();
                else
                    break;
            }

            Console.WriteLine("Goodbye.");
        }
    }
}