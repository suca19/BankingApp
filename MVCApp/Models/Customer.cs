using MVCApp.Models.Abstract;

namespace MVCApp.Models;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */
    public class Customer : User
    {
     
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public SavingAccount SavingsAccount { get; set; }
        public CurrentAccount CurrentAccount { get; set; }
        
    }