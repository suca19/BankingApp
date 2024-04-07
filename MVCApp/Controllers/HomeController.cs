using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Interfaces;
using MVCApp.Models;

namespace MVCApp.Controllers;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmployeeService _EmployeeService;

    public HomeController(ILogger<HomeController> logger, EmployeeService EmployeeService)
    {
        _logger = logger;
        _EmployeeService = EmployeeService;
    }
    
    [HttpPost]
    public IActionResult CreateBankEmployee()
    {
        try
        {
            _EmployeeService.AddEmployee();
            TempData["EmployeeCreated"] = "A new bank employee has been created.";
            return View("Index");
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View("Index");
        }

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}