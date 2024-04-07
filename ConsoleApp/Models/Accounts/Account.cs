namespace ConsoleApp.Models.Accounts;
/*
 * Student name: Carlos Sucapuca
 * ID: 71930
 * Project: Banking ConsoleApp
 * Lecture: John Rowley
 */
public abstract class Account
    { 
       // Method to deposit money into the account
        public static void Deposit(string? customerAccDetails)
        {
            string path = "./BankFiles";
            bool optionLoop = true;
            while (optionLoop)
            {
                Console.WriteLine("1. Deposit in Current Account");
                Console.WriteLine("2. Deposit in Savings Account");
                Console.Write("Option: ");
                if (Int32.TryParse(Console.ReadLine(), out int optionUser))
                {
                    Finder.Separator(); // Unsure what Finder.Separator() does, make sure it's defined somewhere
                    switch (optionUser)
                    {
                        case 1:
                            optionLoop = false;
                            string currentToWrite = $"{path}/{customerAccDetails}-current.txt"; // Check if file exists before proceeding
                            Console.Write("Please, Enter Amount: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal currentDepositAmount) && currentDepositAmount > 0)
                            {
                                try
                                {
                                    decimal balanceAccount = GetCurrentBalance(customerAccDetails);
                                    decimal updatedCurrentBalance = balanceAccount + currentDepositAmount;
                                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                                    using (StreamWriter sw = new StreamWriter(currentToWrite, true))
                                    {
                                        string[] currentDeposit = { date + "|" + "Deposit" + "|" + currentDepositAmount + "|" + updatedCurrentBalance };
                                        foreach (string cd in currentDeposit)
                                        {
                                            sw.WriteLine(cd);
                                        }
                                    }
                                    Console.WriteLine("Transaction Succeed!");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"The {currentToWrite} file could not be written");
                                    Console.WriteLine(e.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please, Enter a Valid Amount");
                                optionLoop = true;
                            }
                            break;

                        case 2:
                            optionLoop = false;
                            string savingsToWrite = $"{path}/{customerAccDetails}-savings.txt"; // Check if file exists before proceeding
                            Console.Write("Please, Enter Amount: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal savingsDepositAmount) && savingsDepositAmount > 0)
                            {
                                try
                                {
                                    decimal balanceAccount = GetSavingsBalance(customerAccDetails);
                                    decimal updatedSavingsBalance = balanceAccount + savingsDepositAmount;
                                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                                    using (StreamWriter sw = new StreamWriter(savingsToWrite, true))
                                    {
                                        string[] savingsDeposit = { date + "|" + "Deposit" + "|" + savingsDepositAmount + "|" + updatedSavingsBalance };
                                        foreach (string sd in savingsDeposit)
                                        {
                                            sw.WriteLine(sd);
                                        }
                                    }
                                    Console.WriteLine("Transaction Succeed!");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"The {savingsToWrite} file could not be written");
                                    Console.WriteLine(e.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please, Enter a Valid Amount");
                                optionLoop = true;
                            }
                            break;

                        default:
                            optionLoop = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please, choose a valid option.");
                    Finder.Separator(); // Simulate new screen
                }
            }
        }

        // Method to withdraw money from the account

    public static void Withdrawl(string? customerAccDetails) 
    {
        string path = "./BankFiles";
        bool optionLoop = true;
        while (optionLoop)
        {
            Console.WriteLine("1. Withdrawl in Current Account");
            Console.WriteLine("2. Withdrawl in Savings Account");
            Console.Write("Option: ");
            if (Int32.TryParse(Console.ReadLine(), out int optionUser))
            {
                Finder.Separator();
                switch (optionUser)
                {
                    case 1:
                        optionLoop = false;
                        string currentToWrite =
                            $"{path}/{customerAccDetails}-current.txt"; // have to check if file exist before going on
                        Console.Write("Please, Enter Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal currentWithdrawlAmount) &&
                            currentWithdrawlAmount > 0 &&
                            currentWithdrawlAmount - GetCurrentBalance(customerAccDetails) <= 0)
                        {
                            try
                            {
                                decimal balanceAccount = GetCurrentBalance(customerAccDetails);
                                decimal updatedCurrentBalance = balanceAccount - currentWithdrawlAmount;
                                string date = DateTime.Now.ToString("yyyy-MM-dd");
                                StreamWriter sw = new(currentToWrite, true);
                                string[] currentWithdrawl =
                                {
                                    date + "|" + "Withdrawl" + "|" + -currentWithdrawlAmount + "|" +
                                    updatedCurrentBalance
                                };
                                foreach (string cw in currentWithdrawl)
                                {
                                    sw.WriteLine(cw);
                                    sw.Close();
                                }

                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"The {currentToWrite} file could not be written");
                                Console.WriteLine(e.Message);
                            }

                            Console.WriteLine("Transaction Succeed!");
                        }
                        else
                        {
                            Console.WriteLine("Please, Enter a Valid Amount");
                            optionLoop = true;
                        }

                        break;

                    case 2:
                        optionLoop = false;
                        string savingsToWrite =
                            $"{path}/{customerAccDetails}-savings.txt"; // have to check if file exist before going on
                        Console.Write("Please, Enter Amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal savingsWithdrawlAmount) &&
                            savingsWithdrawlAmount > 0 &&
                            savingsWithdrawlAmount - GetSavingsBalance(customerAccDetails) <= 0)
                        {
                            try
                            {
                                decimal balanceAccount = GetSavingsBalance(customerAccDetails);
                                decimal updatedSavingsBalance = balanceAccount - savingsWithdrawlAmount;
                                string date = DateTime.Now.ToString("yyyy-MM-dd");
                                StreamWriter sw = new(savingsToWrite, true);
                                string[] savingsWithdrawl =
                                {
                                    date + "|" + "Withdrawl" + "|" + -savingsWithdrawlAmount + "|" +
                                    updatedSavingsBalance
                                };
                                foreach (string swith in savingsWithdrawl)
                                {
                                    sw.WriteLine(swith);
                                    sw.Close();
                                }

                                sw.Close();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"The {savingsToWrite} file could not be written");
                                Console.WriteLine(e.Message);
                            }

                            Console.WriteLine("Transaction Succeed!");
                        }
                        else
                        {
                            Console.WriteLine("Please, Enter a Valid Amount");
                            optionLoop = true;
                        }

                        break;

                    default:
                        optionLoop = true;
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please, choose a valid option.");
                Finder.Separator(); // Simulate new screen
            }
        }
    }
    
    // Method to get current balance from the current account

    public static decimal GetCurrentBalance(string? customerAccDetails)
        {
            string path = "./BankFiles";
            string currentAccount = $"{customerAccDetails}-current.txt";
            string loadCurrentAccount = $"{path}/{currentAccount}";
            string[] currentData = File.ReadAllText(loadCurrentAccount).Split("|");
            int finalPosition = currentData.Length - 1;
            decimal loadCurrentBalance = decimal.Parse(currentData[finalPosition]);
            return loadCurrentBalance;

        } //read current.txt file and getting the balance at last position.

        // Method to get savings balance from the savings account

        public static decimal GetSavingsBalance(string? customerAccDetails)
        {
            string path = "./BankFiles";
            string savingsAccount = $"{customerAccDetails}-savings.txt";
            string loadSavingsAccount = $"{path}/{savingsAccount}";
            string[] savingsData = File.ReadAllText(loadSavingsAccount).Split("|");
            int finalPosition = savingsData.Length - 1;
            decimal loadSavingsBalance = decimal.Parse(savingsData[finalPosition]);
            return loadSavingsBalance;

        }//read savings.txt file and getting the balance aka last position

        // Method to read current account transactions
        public static void ReadCurrentTransactions(string? customerAccDetails)
        {
            string path = "./BankFiles";
            string currentAccount = $"{customerAccDetails}-current.txt";
            string loadCurrentAccount = $"{path}/{currentAccount}";
            try
            {
                StreamReader sr = new (loadCurrentAccount);
                string line;
                while (!string.IsNullOrEmpty((line = sr.ReadLine())))
                {
                    string[] currentData = line.Split("|");
                    string first = String.Format("{0,-10}", currentData[0]);
                    Console.WriteLine($"{first}\t{currentData[1]}\t{currentData[2]}\t{currentData[3]}");
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {loadCurrentAccount} file could not be read");
                Console.WriteLine(e.Message);
            }

        }

        // Method to read savings account transactions
        public static void ReadSavingsTransactions(string? customerAccDetails)
        {
            string path = "./BankFiles";
            string savingsAccount = $"{customerAccDetails}-savings.txt";
            string loadSavingsAccount = $"{path}/{savingsAccount}";
            try
            {
                StreamReader sr = new (loadSavingsAccount);
                string line;
                while ((line = sr.ReadLine()) is not null)
                {
                    string[] currentData = line.Split("|");
                    string first = String.Format("{0,-10}", currentData[0]);
                    Console.WriteLine($"{first}\t{currentData[1]}\t{currentData[2]}\t{currentData[3]}");
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"The {loadSavingsAccount} file could not be read");
                Console.WriteLine(e.Message);
            }

        }
    }

