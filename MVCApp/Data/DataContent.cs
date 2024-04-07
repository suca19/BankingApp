using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCApp.Models;
namespace MVCApp.Data;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class DataContent : IdentityDbContext
{
    public DataContent(DbContextOptions<DataContent> options)
        : base(options)
    {
    }
    
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SavingAccount> SavingAccounts { get; set; }
    public DbSet<CurrentAccount> CurrentAccounts { get; set; }
    
}