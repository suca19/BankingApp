namespace ConsoleApp.Models;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */
public abstract class Finder
{
    //Method to find the first letter of customer name
        public static int DriverFindLetter(string? findLetterAt) 
        {
            string s1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string s2 = findLetterAt.ToUpper();
            int letterAt = s1.IndexOf(s2)+1;
            return letterAt;
        }

        //Method to output a line to separate information
        public static void Separator() 
        {
            Console.WriteLine("=".PadRight(80, '='));
        }
    
}