using FubuMVC.Core.View.Attachment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using  MVCApp.Interfaces;
using OneTrueError.Client.Uploaders;

namespace MVCApp.Controllers;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class EmployeeController
{
    private readonly EmployeeService _EmployeeService;
    private readonly CurrentUserService _currentUserService;
    private readonly TransactionService _transactionService;
    
    public EmployeeController(EmployeeService EmployeeService, CurrentUserService currentUserService, TransactionService transactionService)
    {
        _EmployeeService = EmployeeService;
        _currentUserService = currentUserService;
        _transactionService = transactionService;
    }
    
    
    [HttpPost]
    public ActionResult Login(string pin)
    {
        try
        {
            var employee = _EmployeeService.Login(pin);
            HttpContext.Session.SetInt32("UserId", employee.EmployeeId);
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId")!;

            return RedirectToAction("GoToEmployeeIndex");
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View("~/Views/Home/Index.cshtml");
        }
        
    }
    
    public IActionResult GoToEmployeeIndex()
    {
        
        var customers = _EmployeeService.GetAllCustomers();
        ViewBag.Customers = customers;
        return View("EmployeeIndex");

    }
    
    public IActionResult SeeTransactions()
    {
        var transactions = _transactionService.GetAllTransactions();
        ViewBag.Transactions = transactions;
        return View("~/Views/Transaction/SeeAllTransactions.cshtml");
    }
    
    [HttpPost]
    public IActionResult CreateRandomCustomer()
    {
        _EmployeeService.CreateRandomCustomer();
        TempData["UserCreated"] = "You have a created a customer.";
        return View("GoToEmployeeIndex");
    }
    
    [HttpPost]
    public IActionResult CreateRandomCustomerWithZeroBalance()
    {
        _EmployeeService.CreateRandomCustomerWithZeroBalance();
        TempData["UserCreated"] = "You have a created a customer with zero balance .";
        return RedirectToAction("GoToEmployeeIndex");
    }
    
    [HttpPost]
    public IActionResult CreateCustomCustomer(string firstName, string lastName, string email)
    {
        _EmployeeService.CreateCustomCustomer(firstName, lastName, email);
        TempData["UserCreated"] = "A new custom customer has been created.";
        return RedirectToAction("GoToEmployeeIndex");
    }
    
    public IActionResult DeleteCustomer(int userId)
    {
        try
        {
            _EmployeeService.DeleteCustomer(userId);
            TempData["UserDeleted"] = "The customer has been deleted.";
            return RedirectToAction("GoToEmployeeIndex");
        }
        catch (InvalidOperationException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("GoToEmployeeIndex");
        }
    }
    
    public IActionResult AddCustomCustomer()
    {
        return View("CreateCustomCustomer");
    }
    
    public IActionResult ManageTransaction(int userId)
    {
        return RedirectToAction("Transaction", "Transaction", new { userId = userId });
    }
}