using FubuMVC.Core.View.Attachment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApp.Interfaces;
using OneTrueError.Client.Uploaders;

namespace MVCApp.Controllers;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class CustomerController
{
    // Injecting the Bank Customer Service
    private readonly ICustomerService _CustomerService;
    
    // Constructor
    public CustomerController(ICustomerService CustomerService)
    {
        _CustomerService = CustomerService;
    }
    
    
    [HttpPost]
    // Login method
    public ActionResult Login(string firstName, string lastName, string accountNumber, string pin)
    {
        try
        {
            var employee = _CustomerService.Login(firstName, lastName, accountNumber, pin);
            HttpContext.Session.SetInt32("UserId", employee.CustomerId);
            //ViewBag.UserId = HttpContext.Session.GetInt32("UserId")!;

            return RedirectToAction("GoToCustomerIndex");
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View("~/Views/Home/Index.cshtml");
        }
        
    }
    
    // Go to the Customer Index
    public ActionResult GoToCustomerIndex()
    {
        
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            return RedirectToAction("Index", "Home");
        }
        
        var userSavingAccount = _CustomerService.GetSavingAccount(HttpContext.Session.GetInt32("UserId")!.Value);
        var userCurrentAccount = _CustomerService.GetCurrentAccount(HttpContext.Session.GetInt32("UserId")!.Value);
        ViewBag.SavingAccount = userSavingAccount;
        ViewBag.CurrentAccount = userCurrentAccount;
        
        return View("~/Views/Customer/CustomerIndex.cshtml");
    }
    
    
    public IActionResult ManageTransaction()
    {
        var userIdFromSession = HttpContext.Session.GetInt32("UserId")!.Value;
        return RedirectToAction("Transaction", "Transaction", new { userId = userIdFromSession });
    }

    public IActionResult GetUsersTransactions()
    {
        var userTransaction = _CustomerService.GetUsersTransactions(HttpContext.Session.GetInt32("UserId")!.Value);
        ViewBag.Transactions = userTransaction;
        return View("~/Views/Transaction/SeeAllTransactions.cshtml");
    }
}