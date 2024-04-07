using ConsoleApp.Models.Accounts;
using ConsoleApp.Services;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */
namespace ConsoleApp.Models.User
{
    // Customer class responsible for handling customer-related functionalities
    public class Customer
    {
        // Method for customer login
        public static void CustomerLogin()
        {
            bool loginLoop = true;
            while (loginLoop)
            {
                // Placeholder implementation for login validation
                loginLoop = false; // For demonstration purposes, exit loop after first iteration
                Console.WriteLine("Welcome Bank Customer");
                Console.WriteLine("Please, type in your details");
                Finder.Separator();
                Console.Write("Your First Name: ");
                string? customerFirstName = Console.ReadLine();
                Console.Write("Your Last Name: ");
                string? customerLastName = Console.ReadLine();
                Console.Write("Your Account Code: ");
                string? customerAccountCode = Console.ReadLine();
                Console.Write("Your Pin Number: ");
                string? customerPin = Console.ReadLine();
                Finder.Separator();
                AuthenticationService.ValidateCustomer(customerFirstName?.ToUpper(), customerLastName?.ToUpper(),
                    customerAccountCode?.ToUpper(), customerPin);
                CustomerLogin();
            }
        }

        // Method to display customer menu
        public static void CustomerMenu(string? customerAccountCode)
        {
            bool optionLoop = true;
            while (optionLoop)
            {
                Finder.Separator();
                Console.WriteLine("1. Transactions History");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdrawal");
                Console.WriteLine("4. Check balance");
                Console.Write("Option: ");
                if (Int32.TryParse(Console.ReadLine(), out int optionCustomer))
                {
                    switch (optionCustomer)
                    {
                        case 1:
                            optionLoop = false;
                            DisplayTransactionHistory(customerAccountCode);
                            break;
                        case 2:
                            optionLoop = false;
                            Console.WriteLine("Deposit");
                            Account.Deposit(customerAccountCode);
                            break;
                        case 3:
                            optionLoop = false;
                            Console.WriteLine("Withdrawal");
                            Account.Withdrawl(customerAccountCode);
                            break;
                        case 4:
                            optionLoop = false;
                            DisplayBalance(customerAccountCode);
                            break;
                        default:
                            optionLoop = true;
                            break;
                    }
                    CustomerMenu(customerAccountCode);
                }
                else
                {
                    // Handle invalid input
                    Console.WriteLine("Please, choose a valid option.");
                    Finder.Separator();
                    CustomerMenu(customerAccountCode);
                }
            }
        }

        // Method to display transaction history
        private static void DisplayTransactionHistory(string? customerAccountCode)
        {
            Finder.Separator();
            Console.WriteLine("Transactions History");
            Console.WriteLine("1. for Savings Account");
            Console.WriteLine("2. for Current Account");
            Console.Write("Option: ");
            if (Int32.TryParse(Console.ReadLine(), out int userAnswer))
            {
                Finder.Separator();
                switch (userAnswer)
                {
                    case 1:
                        Account.ReadSavingsTransactions(customerAccountCode);
                        break;
                    case 2:
                        Account.ReadCurrentTransactions(customerAccountCode);
                        break;
                    default:
                        Console.WriteLine("Please Enter Valid Option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please Enter Valid Option.");
            }
        }

        // Method to display account balance
        private static void DisplayBalance(string? customerAccountCode)
        {
            Finder.Separator();
            Console.WriteLine("Check Balance");
            Console.WriteLine("1. for Savings Account");
            Console.WriteLine("2. for Current Account");
            Console.Write("Option: ");
            if (Int32.TryParse(Console.ReadLine(), out int loopBalance))
            {
                switch (loopBalance)
                {
                    case 1:
                        Console.WriteLine($"Available balance: {Account.GetSavingsBalance(customerAccountCode)}");
                        break;
                    case 2:
                        Console.WriteLine($"Available balance: {Account.GetCurrentBalance(customerAccountCode)}");
                        break;
                    default:
                        Console.WriteLine("Please Enter Valid Option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please Enter Valid Option.");
            }
        }

        // Method to create the initial customers file
        public static void FirstTimeCustomerFile()
        {
            string customersFile = "customers.txt";
            string[] bankCustomers = Array.Empty<string>(); // Placeholder for customer data, if needed
            CreateCustomerFile(customersFile, bankCustomers);
        }

        // Method to create or append to the customers file
        public static void CreateCustomerFile(string customersFile, string[] bankCustomers)
        {
            string path = "./BankFiles";
            string fileToWrite = $"{path}/{customersFile}";

            try
            {
                using (StreamWriter sw = new StreamWriter(fileToWrite, true))
                {
                    foreach (string customer in bankCustomers)
                    {
                        sw.WriteLine(customer);
                    }
                }
                Console.WriteLine("Customer file created or updated successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {fileToWrite} file could not be written");
                Console.WriteLine(e.Message);
            }
        }
    }
}
