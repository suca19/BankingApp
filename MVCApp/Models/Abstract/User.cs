namespace MVCApp.Models.Abstract;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

    public abstract class User
    {
        //public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Pin { get; set; }
        //public List<Transaction>? Transactions { get; set; }
    }