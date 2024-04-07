using ConsoleApp.Models.User;

namespace ConsoleApp.Services;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */

// Class responsible for handling the login process to the bank system
public static class Login
{
    // Method to initiate the login process
    public static void BankSystemLogin()
    {
        // Welcome message
        Console.WriteLine("==========================| Welcome to Bank 71930 |===========================");

        // Loop to keep the login process running until successful login
        bool loginLoop = true;
        while (loginLoop)
        {
            // Prompting user to choose between employee and customer login
            Console.WriteLine("Type 1. Bank Employee");
            Console.WriteLine("Type 2. Bank Customer");
            Console.Write("Option: ");
            
            // Parsing user input to integer to avoid exceptions
            if (Int32.TryParse(Console.ReadLine(), out int userOption))
            {
                switch (userOption)
                {
                    case 1:
                        loginLoop = false;
                        Employee.EmployeeLogin(); // Initiating employee login process
                        break;

                    case 2:
                        loginLoop = false;
                        Customer.CustomerLogin(); // Initiating customer login process
                        break;

                    default:
                        loginLoop = true; // Prompting user to enter a valid option
                        break;
                }
            }
        }
    }
}