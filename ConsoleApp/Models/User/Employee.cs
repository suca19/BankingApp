using ConsoleApp.Models.Accounts;
using ConsoleApp.Services;

namespace ConsoleApp.Models.User;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */
public abstract class Employee
{
    //Employee method opening
    public static void EmployeeLogin()
    {
        bool loginLoop = true;
        while (loginLoop)
        {
            Console.WriteLine("Welcome Bank Employee");
            Console.Write("Password: ");
            string? employeePassword = Console.ReadLine();
            if (AuthenticationService.ValidateEmployee(employeePassword))
            {
                loginLoop = false; //Desativation of loop

                Console.WriteLine("Welcome to 71930 Bank System");
                Console.WriteLine("Opening menu...");
                EmployeeMenu();
            }
            else
            {
                // wrong answer keeps looping
                Console.WriteLine("Please, try again.");

                EmployeeLogin();
            }
        }
    }

    //employee method menu to show the options available
    private static void EmployeeMenu()
    {
        bool optionLoop = true;
        while (optionLoop)
        {
            
            Console.WriteLine("1. Create Customer Account");
            Console.WriteLine("2. Delete Customer Account");
            Console.WriteLine("3. Transaction Withdrawl");
            Console.WriteLine("4. Transaction Deposit");
            Console.WriteLine("5. List Customers Accounts");
            Console.WriteLine("6. View Customers Balance");
            Console.Write("Option: ");
            if (Int32.TryParse(Console.ReadLine(), out int optionEmployee))
            {
                switch (optionEmployee)
                {
                    case 1:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("Create Customer Account");
                        Console.Write("Input customer's First Name: ");
                        string? firstName = Console.ReadLine();
                        Console.Write("Input customer's Last Name: ");
                        string? lastName = Console.ReadLine();
                        Console.Write("Input customer's Email Address: ");
                        string? email = Console.ReadLine();
                        string? letterFirstName = firstName?.Substring(0, 1);
                        string? letterLastName = lastName?.Substring(0, 1);
                        int lenghtFullName = firstName.Length + lastName.Length;
                        int positionFirst = Finder.DriverFindLetter(letterFirstName);
                        int positionLast = Finder.DriverFindLetter(letterLastName);
                        string customerAccDetails =
                            $"{letterFirstName.ToUpper()}{letterLastName.ToUpper()}-{lenghtFullName}-{positionFirst}-{positionLast}";
                        SavingsAccount.CreateSavingsAcc(customerAccDetails);
                        CurrentAccount.CreateCurrentAcc(customerAccDetails);
                        CreateCustomer(customerAccDetails, firstName.ToUpper(), lastName.ToUpper(), email);
                        Finder.Separator();
                        Console.WriteLine("Account Created successfully!");
                        Console.WriteLine($"Account Number: {customerAccDetails}");
                        Console.WriteLine($"Account PIN Number: {positionFirst}{positionLast}");
                        break;

                    case 2:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("Delete Customer Account");
                        Console.WriteLine("Insert the account code to be deleted: ");
                        Console.Write("Account code: ");
                        string accountToDelete = Console.ReadLine();
                        DeleteCustomerAcc(accountToDelete.ToUpper());
                        break;

                    case 3:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("Transaction Withdrawl");
                        Console.Write("Please, Input the Account Code: ");
                        string? accountForWith = Console.ReadLine(); // file exist to check if account exist?
                        Account.Withdrawl(accountForWith);
                        break;

                    case 4:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("Transaction Deposit");
                        Console.Write("Please, Input the Account Code: ");
                        string? accountForDep = Console.ReadLine(); // file exist to check if account exist?
                        Account.Deposit(accountForDep);
                        break;

                    case 5:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("List Customers Accounts");
                        ListCustomers();
                        break;

                    case 6:
                        optionLoop = false;
                        Finder.Separator();
                        Console.WriteLine("View Customer Balance");
                        Console.Write("Please, Input the Account Code: ");
                        string accountForView = Console.ReadLine();
                        Finder.Separator();
                        Console.WriteLine("1. for Savings Account");
                        Console.WriteLine("2. for Current Account");
                        Console.Write("Option: ");
                        if (Int32.TryParse(Console.ReadLine(), out int loopBalance))
                        {
                            switch (loopBalance)
                            {
                                case 1:
                                    Console.WriteLine(
                                        $"Available balance: {Account.GetSavingsBalance(accountForView)}");
                                    break;

                                case 2:
                                    Console.WriteLine(
                                        $"Available balance: {Account.GetCurrentBalance(accountForView)}");
                                    break;

                                default:
                                    optionLoop = true;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Valid Option.");
                        }

                        break;

                    default:
                        optionLoop = true;
                        break;
                }

                EmployeeMenu();
            }
            else
            {
                // wrong answer keeps looping
                Console.WriteLine("Please, choose a valid option.");
                Finder.Separator();
                EmployeeMenu();
            }

        }
    }

    //a method to list all the customers
    public static void ListCustomers() // read customers.txt file and out put a list with all of them
        {
            string path = "./BankFiles";
            string customersFile = "customers.txt";
            string customerToRead = $"{path}/{customersFile}";
            try
            {
                StreamReader sr = new(customerToRead);
                string line;
                while ((line = sr.ReadLine()) is not null)
                {
                    string?[] customerData = line.Split("|");
                    string? customerAcc = customerData[0];
                    Console.WriteLine(
                        $"{customerData[0]}\t{customerData[1]}\t{customerData[2]}\t{customerData[3]}\tTotal Account Balance: {CurrentAccount.GetCurrentBalance(customerAcc) + SavingsAccount.GetSavingsBalance(customerAcc)}");
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {customerToRead} file could not be read");
                Console.WriteLine(e.Message);
            }
        }

    //method to create a new customer
    public static void CreateCustomer(string customerAccDetails, string firstName, string lastName, string? email)// creating customers.txt file
    {
        string customersFile = "customers.txt";

        string[] bankCustomers = { customerAccDetails + "|" + firstName + "|" + lastName + "|" + email };

        Customer.CreateCustomerFile(customersFile, bankCustomers);
        //creating customers
    }

    //method to perform a deletion of a customer
    private static void DeleteCustomerAcc(string? customerAccDetails)
    {
        Finder.Separator();
        if (Account.GetCurrentBalance(customerAccDetails) + Account.GetSavingsBalance(customerAccDetails) > 0)
        {
            Console.WriteLine($"The account {customerAccDetails} has positive balances.");
            Console.WriteLine($"Available balance in Current Account: " +
                              Account.GetCurrentBalance(customerAccDetails));
            Console.WriteLine($"Available balance in Savings Account: " +
                              Account.GetSavingsBalance(customerAccDetails));
            Console.WriteLine("Access Denied.");
            EmployeeLogin();
        }
        else
        {
            Console.WriteLine("1. To Confirm.");
            Console.WriteLine("2. To Cancel.");
            Console.Write("Option: ");
            int confirmAciton = int.Parse(Console.ReadLine() ?? string.Empty);
            switch (confirmAciton)
            {
                case 1:
                    string path = "./BankFiles";
                    string customersFile = "customers.txt";
                    string customerToRead = $"{path}/{customersFile}";
                    string newCustomerFile = "new-customers.txt";
                    string customerToWrite = $"{path}/{newCustomerFile}";
                    string customerSavings = $"{path}/{customerAccDetails}-savings.txt";
                    string customerCurrent = $"{path}/{customerAccDetails}-current.txt";
                    using (var sr = new StreamReader(customerToRead))
                    using (var sw = new StreamWriter(customerToWrite))
                    {
                        string eachLine;
                        while ((eachLine = sr.ReadLine()) != null)
                        {
                            if (!eachLine.Contains(customerAccDetails))
                            {
                                sw.WriteLine(eachLine);
                            }
                        }

                        sw.Close();
                        sr.Close();
                    }

                    File.Delete(customerToRead);
                    File.Delete(customerSavings);
                    File.Delete(customerCurrent);
                    File.Move(customerToWrite, customerToRead);
                    Console.WriteLine($"Account: {customerAccDetails} has been deleted.");
                    break;

                case 2:
                    EmployeeMenu();
                    break;

                default:
                    DeleteCustomerAcc(customerAccDetails);
                    break;
            }
        }
        
    }
}

    




        

        
            
        
        
       
    
