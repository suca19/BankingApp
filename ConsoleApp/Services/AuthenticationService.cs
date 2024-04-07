using ConsoleApp.Models;
using ConsoleApp.Models.User;

namespace ConsoleApp.Services;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */

// Abstract class representing authentication service
public abstract class AuthenticationService
{
    // Method to validate employee PIN
    public static bool ValidateEmployee(string? pin)
    {
        return pin == "A1234"; // Hardcoded PIN validation for employee
    }

    // Method to validate customer credentials
    public static void ValidateCustomer(string? firstName, string? lastName, string? accountCode, string? pinNumber)
    {
        // File paths and initialization
        string path = "/Users/carlos/RiderProjects/BankingConsoleApp/Services";
        string customersFile = "customers.txt";
        string customerToRead = $"{path}/{customersFile}";

        // Extracting first letters of first name and last name
        string? letterFirstName = firstName.Substring(0, 1);
        string? letterLastName = lastName.Substring(0, 1);

        // Generating password based on first letters' positions
        int positionFirst = Finder.DriverFindLetter(letterFirstName);
        int positionLast = Finder.DriverFindLetter(letterLastName);
        string password = positionFirst + "" + positionLast;

        // Reading customer information from file
        string[] customersInfo = File.ReadAllText(customerToRead).Split("|");

        // Iterating through customer info to validate credentials
        for (int i = 0; i < customersInfo.Length; i++)
        {
            if (customersInfo[i].Contains(firstName))
            {
                if (customersInfo[i + 1].Contains(lastName))
                {
                    if (customersInfo[i - 1].Contains(accountCode))
                    {
                        if (password == pinNumber)
                        {
                            // Successful validation
                            Console.WriteLine($"Welcome to Bank 71930 - {firstName} {lastName}");
                            Console.WriteLine("Opening Menu...");
                            Customer.CustomerMenu(accountCode.ToUpper());
                        }

                        Console.WriteLine("PIN number is wrong"); // Incorrect PIN
                    }

                    Console.WriteLine("Account does not exist"); // Account not found
                }
            }
        }
    }
}
