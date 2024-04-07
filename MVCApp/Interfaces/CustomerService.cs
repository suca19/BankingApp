
using MVCApp.Models;

namespace MVCApp.Interfaces;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public interface ICustomerService
{
    Customer Login(string firstName, string lastName, string accountNumber, string pin);
    CurrentAccount GetCurrentAccount(int userId);
    SavingAccount GetSavingAccount(int userId);
    IEnumerable<Transaction> GetUsersTransactions(int userId);
}