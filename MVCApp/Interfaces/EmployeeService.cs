using MVCApp.Models;

namespace MVCApp.Interfaces;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class EmployeeService
{
    Employee Login(string pin);
    IEnumerable<Customer> GetAllCustomers();
    Customer GetCustomer(int customerId);
    void CreateRandomCustomer();
    void CreateRandomCustomerWithZeroBalance();
    void CreateCustomCustomer(string firstName, string lastName, string email);
    void DeleteCustomer(int customerId);
    void AddEmployee();
}