using FubuMVC.Core.View.Attachment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApp.Data;
using MVCApp.Interfaces;
using OneTrueError.Client.Uploaders;

namespace MVCApp.Controllers;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */

public class TransactionController
{
    private readonly TransactionService _transactionService;
    private readonly DataContent _content;

    public TransactionController(TransactionService transactionService, DataContent content)
    {
        _transactionService = transactionService;
        _content = content;
    }

    [HttpPost]
    public IActionResult Transaction(int userId, decimal amount, string typeOfAccount, string transactionType)
    {
        try
        {
            var currentUserIdNullable = HttpContext.Session.GetInt32("UserId");
            if (currentUserIdNullable == null)
            {
                throw new Exception("User is not logged in");
            }
            var currentUserId = currentUserIdNullable.Value;

            switch (transactionType)
            {
                case "Deposit":
                    _transactionService.Deposit(userId, amount, typeOfAccount, currentUserId);
                    TempData["SuccessMessage"] = "Deposit successful.";
                    break;
                case "Withdraw":
                    _transactionService.Withdraw(userId, amount, typeOfAccount, currentUserId);
                    TempData["SuccessMessage"] = "Withdrawal successful.";
                    break;
            }
            var lastTransaction = _content.Transactions.OrderByDescending(t => t.TransactionId).FirstOrDefault();
            if (lastTransaction!.CreatedBy == "Bank")
            {
                return RedirectToAction("GoToEmployeeIndex", "Employee");
            }

            return RedirectToAction("GoToCustomerIndex", "Customer");

        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            ViewBag.UserId = userId;
            return View("Transaction");
        }
    }
    
    public IActionResult Transaction(int userId)
    {
        ViewBag.UserId = userId;
        return View("Transaction");
    }
}