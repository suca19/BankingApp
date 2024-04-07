using System.ComponentModel.DataAnnotations.Schema;
using MVCApp.Models.Abstract;

namespace MVCApp.Models;

/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking MVCApp
 * Lecture: John Rowley
 */
    public class CurrentAccount : Account
    {
        public int CurrentAccountId { get; set; }
        
        
    }