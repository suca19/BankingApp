using ConsoleApp.Models.User;
using ConsoleApp.Services;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */


// Create directory BankFiles if it doesn't exist
string pathBankFilesFolder = @"./BankFiles";
string bankFiles = Path.Combine(pathBankFilesFolder);
Directory.CreateDirectory(bankFiles);

//Creating customers.txt file
Customer.FirstTimeCustomerFile();
            
//Opening bank system menu
Login.BankSystemLogin();

       