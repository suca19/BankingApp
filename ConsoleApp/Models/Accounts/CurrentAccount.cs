namespace ConsoleApp.Models.Accounts
{
    /*
     * Student name: Carlos Sucapuca
     * ID: 71930
     * Project: Banking ConsoleApp
     * Lecture: John Rowley
     */
    public abstract class CurrentAccount : Account
    {
        // Method to create a new current account file
        public static void CreateCurrentAcc(string customerAccDetails)
        {
            string currentAccFile = $"{customerAccDetails}-current.txt";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string[] customerCurrentAcc = { date + "|" + "Opening" + "|" + "Action" + "|" + 0 };
            CreateCurrentFile(currentAccFile, customerCurrentAcc);
        }

        // Method to create a new current file with provided data
        public static void CreateCurrentFile(string currentAccFile, string[] customerCurrentAcc)
        {
            string path = "./BankFiles";
            string fileToWrite = $"{path}/{currentAccFile}";

            try
            {
                using (StreamWriter sw = new StreamWriter(fileToWrite, true))
                {
                    foreach (string current in customerCurrentAcc)
                    {
                        sw.WriteLine(current);
                    }
                }
                Console.WriteLine("Current Account file created successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {fileToWrite} file could not be written");
                Console.WriteLine(e.Message);
            }
        }
    }
}