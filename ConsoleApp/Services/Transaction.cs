namespace ConsoleApp.Services
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */

{
    // Record representing a transaction with its date, action, amount, and final balance
    public record Transaction(DateTime Date, string Action, double Amount, double FinalBalance);
}