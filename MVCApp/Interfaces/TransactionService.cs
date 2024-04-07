using MVCApp.Models;

namespace MVCApp.Interfaces;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class TransactionService
{
    void Deposit(int userId, decimal amount, string typeOfAccount, int currentUser)
    {
        throw new NotImplementedException();
    }

    void Withdraw(int userId, decimal amount, string typeOfAccount, int currentUser);
    public ICollection<Transaction> GetAllTransactions();
}