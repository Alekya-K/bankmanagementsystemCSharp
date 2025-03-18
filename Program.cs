using System;
using System.Globalization; 

namespace BankManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of accounts to create: ");
            int numberOfAccounts = GetValidIntegerInput();

            int[] accountNumbers = new int[numberOfAccounts];
            string[] names = new string[numberOfAccounts];
            string[] accountTypes = new string[numberOfAccounts];
            double[] balances = new double[numberOfAccounts];

            int accountId = 0;
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nWelcome to Bank Management System");
                Console.WriteLine("What would you like to do:");
                Console.WriteLine("1. Create account");
                Console.WriteLine("2. Show account information");
                Console.WriteLine("3. Deposit into account");
                Console.WriteLine("4. Withdraw from account");
                Console.WriteLine("5. Show all accounts");
                Console.WriteLine("6. Clear screen");
                Console.WriteLine("7. Exit");

                Console.WriteLine("Enter your option: ");
                int option = GetValidIntegerInput();

                switch (option)
                {
                    case 1:
                        if (accountId >= numberOfAccounts)
                        {
                            Console.WriteLine("Maximum number of accounts created.");
                        }
                        else
                        {
                            accountId = CreateAccount(accountNumbers, names, accountTypes, balances, accountId);
                        }
                        break;
                    case 2:
                        ShowAccountInformation(accountNumbers, names, accountTypes, balances, accountId);
                        break;
                    case 3:
                        DepositIntoAccount(accountNumbers, balances, accountId);
                        break;
                    case 4:
                        WithdrawFromAccount(accountNumbers, balances, accountId);
                        break;
                    case 5:
                        ShowAllAccounts(accountNumbers, names, accountTypes, balances, accountId);
                        break;
                    case 6:
                        ClearScreen();
                        break;
                    case 7:
                        isRunning = false;
                        Console.WriteLine("Exiting the program. Bye!Happy Coding");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

       
        static int GetValidIntegerInput()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            return result;
        }

       
        static double GetValidDoubleInput()
        {
            double result;
            while (!double.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return result;
        }

       
        static int CreateAccount(int[] accountNumbers, string[] names, string[] accountTypes, double[] balances, int accountId)
        {
            Console.WriteLine("Enter account number: ");
            int newAccountNumber = GetValidIntegerInput();

            if (IsAccountExists(accountNumbers, accountId, newAccountNumber))
            {
                Console.WriteLine("Account number already exists.");
                return accountId;
            }

            accountNumbers[accountId] = newAccountNumber;

            Console.WriteLine("Enter name: ");
            names[accountId] = Console.ReadLine();

            Console.WriteLine("Enter account type (savings/current): ");
            accountTypes[accountId] = GetValidAccountType();

            Console.WriteLine("Enter balance: ");
            balances[accountId] = GetValidDoubleInput();

            Console.WriteLine("Account created successfully!");
            return accountId + 1;
        }

        
        static bool IsAccountExists(int[] accountNumbers, int accountId, int accountNumber)
        {
            for (int i = 0; i < accountId; i++)
            {
                if (accountNumbers[i] == accountNumber)
                {
                    return true;
                }
            }
            return false;
        }

       
        static string GetValidAccountType()
        {
            string accountType;
            do
            {
                accountType = Console.ReadLine().ToLower();
                if (accountType != "savings" && accountType != "current")
                {
                    Console.WriteLine("Invalid account type. Please enter 'savings' or 'current': ");
                }
            } while (accountType != "savings" && accountType != "current");

            return accountType;
        }

        
        static void ShowAccountInformation(int[] accountNumbers, string[] names, string[] accountTypes, double[] balances, int accountId)
        {
            Console.WriteLine("Enter your account number to view information: ");
            int accountNumber = GetValidIntegerInput();

            int accountIndex = FindAccountIndex(accountNumbers, accountId, accountNumber);
            if (accountIndex != -1)
            {
                Console.WriteLine("\nYour Account Information:");
                Console.WriteLine($"Account Number: {accountNumbers[accountIndex]}");
                Console.WriteLine($"Name: {names[accountIndex]}");
                Console.WriteLine($"Account Type: {accountTypes[accountIndex]}");
                Console.WriteLine($"Balance: {balances[accountIndex].ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}"); 
            }
            else
            {
                Console.WriteLine("Account does not exist.");
            }
        }

        
        static void DepositIntoAccount(int[] accountNumbers, double[] balances, int accountId)
        {
            Console.WriteLine("Enter account number to deposit: ");
            int accountNumber = GetValidIntegerInput();

            int accountIndex = FindAccountIndex(accountNumbers, accountId, accountNumber);
            if (accountIndex != -1)
            {
                Console.WriteLine("Enter deposit amount: ");
                double depositAmount = GetValidDoubleInput();

                if (depositAmount > 0)
                {
                    balances[accountIndex] += depositAmount;
                    Console.WriteLine("Deposit successful. New balance: " + balances[accountIndex].ToString("C", CultureInfo.CreateSpecificCulture("en-US"))); // Fix here
                }
                else
                {
                    Console.WriteLine("Deposit amount must be greater than 0.");
                }
            }
            else
            {
                Console.WriteLine("Account does not exist.");
            }
        }

        
        static void WithdrawFromAccount(int[] accountNumbers, double[] balances, int accountId)
        {
            Console.WriteLine("Enter account number for withdrawal: ");
            int accountNumber = GetValidIntegerInput();

            int accountIndex = FindAccountIndex(accountNumbers, accountId, accountNumber);
            if (accountIndex != -1)
            {
                Console.WriteLine("Enter withdrawal amount: ");
                double withdrawalAmount = GetValidDoubleInput();

                if (withdrawalAmount > 0 && balances[accountIndex] >= withdrawalAmount)
                {
                    balances[accountIndex] -= withdrawalAmount;
                    Console.WriteLine("Withdrawal successful. New balance: " + balances[accountIndex].ToString("C", CultureInfo.CreateSpecificCulture("en-US"))); // Fix here
                }
                else
                {
                    Console.WriteLine("Insufficient balance or invalid withdrawal amount.");
                }
            }
            else
            {
                Console.WriteLine("Account does not exist.");
            }
        }

        
        static void ShowAllAccounts(int[] accountNumbers, string[] names, string[] accountTypes, double[] balances, int accountId)
        {
            if (accountId == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }

            Console.WriteLine("\nAll Accounts:");
            for (int i = 0; i < accountId; i++)
            {
                Console.WriteLine($"Account ID: {i + 1}");
                Console.WriteLine($"Account Number: {accountNumbers[i]}");
                Console.WriteLine($"Name: {names[i]}");
                Console.WriteLine($"Account Type: {accountTypes[i]}");
                Console.WriteLine($"Balance: {balances[i].ToString("C", CultureInfo.CreateSpecificCulture("en-US"))}"); 
                Console.WriteLine();
            }
        }

       
        static void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine("Screen cleared.");
        }

      
        static int FindAccountIndex(int[] accountNumbers, int accountId, int accountNumber)
        {
            for (int i = 0; i < accountId; i++)
            {
                if (accountNumbers[i] == accountNumber)
                {
                    return i;
                }
            }
            return -1; 
        }
    }
}