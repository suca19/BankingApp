namespace ConsoleApp.Models.Accounts
{
    /*
     * Student name: Carlos Sucapuca
     * ID: 71930
     * Project: Banking ConsoleApp
     * Lecture: John Rowley
     */
    
    public abstract class SavingsAccount : Account
    {
        // Method to create a new savings account file
        public static void CreateSavingsAcc(string customerAccDetails)
        {
            string savingsAccFile = $"{customerAccDetails}-savings.txt";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string[] customerSavingsAcc = { date + "|" + "Opening" + "|" + "Action" + "|" + 0 };
            CreateSavingsFile(savingsAccFile, customerSavingsAcc);
        }

        // Method to create a new savings file with provided data
        public static void CreateSavingsFile(string savingsAccFile, string[] customerSavingsAcc)
        {
            string path = "./BankFiles";
            string fileToWrite = $"{path}/{savingsAccFile}";

            try
            {
                using (StreamWriter sw = new StreamWriter(fileToWrite, true))
                {
                    foreach (string saving in customerSavingsAcc)
                    {
                        sw.WriteLine(saving);
                    }
                }
                Console.WriteLine("Savings Account file created successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {fileToWrite} file could not be written");
                Console.WriteLine(e.Message);
            }
        }
    }
}