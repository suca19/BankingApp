using MVCApp.Data;
using MVCApp.Interfaces;
using MVCApp.Models;

namespace MVCApp.Services;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */
public class CustomerService : ICustomerService
{
    private readonly DataContent _content;
    
    public CustomerService(DataContent content)
    {
        _content = content;
    }
    
    // Login method for BankCustomer
    public Customer Login(string firstName, string lastName, string accountNumber, string pin)
    {
        return _content.Customers.FirstOrDefault(x => 
            x.FirstName == firstName 
            && x.LastName == lastName 
            && x.SavingsAccount.AccountNumber == accountNumber 
            && x.Pin == pin) ?? throw new InvalidOperationException("No Customer found with the given credentials.");
    }

    // Method to get from the DB the CurrentAccount of the user
    public CurrentAccount GetCurrentAccount(int userId)
    {
        var accountId = _content.CurrentAccounts.FirstOrDefault(x => x.CurrentAccountId == userId)?.CurrentAccountId;
        return _content.CurrentAccounts.FirstOrDefault(x => x.CurrentAccountId == accountId) ?? throw new InvalidOperationException("No account found");
    }

    // Method to get from the DB the SavingAccount of the user
    public SavingAccount GetSavingAccount(int userId)
    {
        var accountId = _content.CurrentAccounts.FirstOrDefault(x => x.CurrentAccountId == userId)?.CurrentAccountId;
        return _content.SavingAccounts.FirstOrDefault(x => x.SavingAccountId == accountId) ?? throw new InvalidOperationException("No Savings account found");
    }
    
    // Method to get from the DB the transactions of the user
    public IEnumerable<Transaction> GetUsersTransactions(int userId)
    {
        var usersEmail = _content.Customers.FirstOrDefault(x => x.CustomerId == userId)?.Email;
        return _content.Transactions.Where(x => x.UserEmail == usersEmail).ToList() ?? throw new InvalidOperationException("No transactions found");
    }
}