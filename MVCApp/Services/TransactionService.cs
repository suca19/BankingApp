using Microsoft.EntityFrameworkCore;
using MVCApp.Data;
using MVCApp.Interfaces;
using MVCApp.Models;
using MVCApp.Models.Abstract;

namespace MVCApp.Services;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class TransactionService : Interfaces.TransactionService
{
    
    private readonly DataContent _content;
    
    public TransactionService(DataContent content)
    {
        _content = content;
    }
    
    public void Deposit(int userId, decimal amount, string typeOfAccount, int currentUser)
    {
        var user = GetCustomer(userId);

        switch (typeOfAccount)
        {
            case "SavingsAccount":
                user!.SavingsAccount.Balance += amount;
                var transaction = CreateTransaction(userId, amount, typeOfAccount, "Deposit", currentUser);
                _content.Transactions.Add(transaction);
                break;
            case "CurrentAccount":
                user!.CurrentAccount.Balance += amount;
                var transaction2 = CreateTransaction(userId, amount, typeOfAccount, "Deposit", currentUser);
                _content.Transactions.Add(transaction2);
                break;
        }

        
        _content.SaveChanges();
        
    }

    public void Withdraw(int userId, decimal amount, string typeOfAccount, int currentUser)
    {
        var user = GetCustomer(userId);

        switch (typeOfAccount)
        {
            case "SavingsAccount":
                if (user!.SavingsAccount.Balance < amount)
                {
                    throw new InvalidOperationException("Insufficient funds!");
                }
                user!.SavingsAccount.Balance -= amount;
                var transaction = CreateTransaction(userId, amount, typeOfAccount, "Withdraw", currentUser);
                _content.Transactions.Add(transaction);
                break;
            
            case "CurrentAccount":
                if (user!.CurrentAccount.Balance < amount)
                {
                    throw new InvalidOperationException("Insufficient funds!");
                }
                user!.CurrentAccount.Balance -= amount;
                var transaction2 = CreateTransaction(userId, amount, typeOfAccount, "Withdraw", currentUser);
                _content.Transactions.Add(transaction2);
                break;
        }

    
        _content.SaveChanges();
    }
    
    public ICollection<Transaction> GetAllTransactions()
    {
        return _content.Transactions.ToList();
    }
    
    private Customer GetCustomer(int userId)
    {
        return _content.Customers
            .Include(e => e.SavingsAccount)
            .Include(e => e.CurrentAccount)
            .SingleOrDefault(e => e.CustomerId == userId) ?? throw new InvalidOperationException("Customer not found!");
    }
    
    private Transaction CreateTransaction(int userId, decimal amount, string typeOfAccount, string transactionType, int currentUser)
    {
        var user = GetCustomer(userId);
        var createdBy = _content.Employees.SingleOrDefault(e => e.EmployeeId == currentUser)?.FirstName;
        var account = typeOfAccount == "SavingsAccount" ? (Account)user.SavingsAccount : user.CurrentAccount;

        if (createdBy != "Bank")
        {
            createdBy = _content.Customers.SingleOrDefault(e => e.CustomerId == currentUser)?.Email;
        }
        
        var transaction = new Transaction
        {
            AccountNumber = user.SavingsAccount.AccountNumber,
            TransactionType = transactionType,
            TransactionDate = DateTime.Now,
            CreatedBy = createdBy!,
            Amount = amount,
            AccountType = typeOfAccount,
            UserEmail = user.Email,
            BalanceAfterTransaction = account.Balance
        };
        return transaction;
    }
    
    
}