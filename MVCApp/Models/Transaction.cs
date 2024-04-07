namespace MVCApp.Models;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

    public class Transaction
    {
        public int TransactionId { get; set; }
        public string? AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CreatedBy { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
        public decimal Amount { get; set; }
        public string AccountType { get; set; }
        public string UserEmail { get; set; }
    }