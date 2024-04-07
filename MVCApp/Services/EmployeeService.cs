using FakerDotNet;
using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Models;

namespace MVCApp.Services;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class EmployeeService
{
    private readonly DataContent _content;
    
    public EmployeeService(DataContent content)
    {
        _content = content;
    }
    
    // 
    public Employee Login(string pin)
    {
        var employee = _content.Employees.FirstOrDefault(x => x.Pin == pin);        
        return employee ?? throw new InvalidOperationException("Invalid PIN");
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _content.Customers
            .Include(c => c.CurrentAccount)
            .Include(c => c.SavingsAccount)
            .ToList();
    }

    public Customer GetCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public void CreateRandomCustomer()
    {
        var random = new Random();
        var firstName = Faker.Name.FirstName();
        var lastName = Faker.Name.LastName();
        var accountNumber = GenerateAccountNumber(firstName, lastName);
        var pinCode = GeneratePin(firstName, lastName);
        var email = Faker.Internet.Email();
        
        
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CurrentAccount = new CurrentAccount()
            {
                AccountNumber = accountNumber,
                Balance = random.Next(0, 9999)
            },
            SavingsAccount = new SavingAccount()
            {
                AccountNumber = accountNumber,
                Balance = random.Next(0, 9999)
            },
            Pin = pinCode
        };
        
        _content.Customers.Add(customer);
        _content.SaveChanges();
        
    }

    public void CreateRandomCustomerWithZeroBalance()
    {
        var firstName = Faker.Name.FirstName();
        var lastName = Faker.Name.LastName();
        var accountNumber = GenerateAccountNumber(firstName, lastName);
        var pinCode = GeneratePin(firstName, lastName);
        var email = Faker.Internet.Email();
        
        
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CurrentAccount = new CurrentAccount()
            {
                AccountNumber = accountNumber,
                Balance = 0
            },
            SavingsAccount = new SavingAccount()
            {
                AccountNumber = accountNumber,
                Balance = 0
            },
            Pin = pinCode
        };
        
        _content.Customers.Add(customer);
        _content.SaveChanges();
    }

    public void CreateCustomCustomer(string firstName, string lastName, string email)
    {
        var customer = new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CurrentAccount = new CurrentAccount()
            {
                AccountNumber = GenerateAccountNumber(firstName, lastName),
                Balance = 0
            },
            SavingsAccount = new SavingAccount()
            {
                AccountNumber = GenerateAccountNumber(firstName, lastName),
                Balance = 0
            },
            Pin = GeneratePin(firstName, lastName)
        };
        
        _content.Customers.Add(customer);
        _content.SaveChanges();
    }

    public void DeleteCustomer(int customerId)
    {
        var customer = _content.Customers
            .Include(bankCustomer => bankCustomer.CurrentAccount)
            .Include(bankCustomer => bankCustomer.SavingsAccount)
            .FirstOrDefault(x => x.CustomerId == customerId);
        
        if(customer!.CurrentAccount.Balance > 0 || customer.SavingsAccount.Balance > 0)
        {
            throw new InvalidOperationException("Customer has a balance and cannot be deleted.");
        }
        
        _content.Customers.Remove(customer);
        _content.SaveChanges();
    }

    public void AddEmployee()
    {
        var employeeExists = _content.Employees.Any();
        if (employeeExists)
        {
            throw new Exception("Bank Employee already exists! Try logging with A1234 as the PIN.");
        }
        var employee = new Employee()
        {
            FirstName = "Bank",
            LastName = "Employee",
            Pin = "A1234"
        };
        
        _content.Employees.Add(employee);
        _content.SaveChanges();

    }


    /*
     * Private Methods
     * Only used within the BankEmployeeService class
     */
    //Generate a random customer
    private static string GenerateAccountNumber(string firstName, string lastName)
    {
        // Calculate the initials
        var initials = firstName.Substring(0, 1).ToLower() + lastName.Substring(0, 1).ToLower();

        // Calculate the length of the total name
        var nameLength = firstName.Length + lastName.Length;

        
        // Calculate the alphabetical positions of the initials
        var firstInitialPosition = firstName[0] - 'A' + 1;
        var secondInitialPosition = lastName[0] - 'A' + 1;

        // Construct the account number
        var accountNumber = $"{initials}-{nameLength}-{firstInitialPosition}-{secondInitialPosition}";

        return accountNumber;
    }
    
    //Generate a pin
    private static string GeneratePin(string firstName, string lastName)
    {
        
        var firstInitialPosition = firstName[0] - 'A' + 1;
        var secondInitialPosition = lastName[0] - 'A' + 1;

        
        var pinCode = $"{firstInitialPosition}{secondInitialPosition}";

        return pinCode;
    }
}