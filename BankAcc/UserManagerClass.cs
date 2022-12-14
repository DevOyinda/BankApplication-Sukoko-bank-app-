using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BankAcc
{
    public class UserManagerClass
    {
        public string Email;
        public string FullName;
        public string Password;
        public string FirstName;
        public string LastName;
        public int selection;
        public string AccNum;
        public string AccountBalance;
        public int Pin;
        public int deposit;
        public int withdraw;
        public string FilePath = @"C:\Users\oyins\Desktop\Oyinda\FilesFolder\";
        public string folderName = @"C:\Users\oyins\Desktop\Oyinda\BankDetails\";

        public void HomeScreen()
        {
            Console.WriteLine("Welcome to Sukoko App\nPlease Select a valid Option:\nPress 1 for Registration\nPress 2 for Login\nPress 3 to close the application.");
            selection = Convert.ToInt32(Console.ReadLine());
            if (selection == 1)
            {
                UserRegister();
            }
            else if (selection == 2)
            {
                UserLogin();
            }
            else if (selection == 3)
            {
                Console.WriteLine("Thank you for using our application");
                Thread.Sleep(3000);
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Incorrect Entry.Try Again");
            }
        }

        private void UserRegister()
        {
            Console.WriteLine("Please fill in your details.");

            Console.WriteLine("FullName:");
            FullName = Console.ReadLine();
            Console.WriteLine("Email Address:");
            Email = Console.ReadLine();
            Console.WriteLine("Password:");
            Password = Console.ReadLine();

            if (File.Exists($"{FilePath}{Email}.txt"))
            {
                Console.WriteLine("You have already registered");
                HomeScreen();
            }
            else
            {
                using (StreamWriter sw = File.AppendText($"{FilePath}{Email}.txt"))
                {
                    sw.WriteLine(FullName);
                    sw.WriteLine(Email);
                    sw.WriteLine(Password);
                }
                Console.WriteLine("Thank you for Registering to our app");
                HomeScreen();
            }
        }

        private void UserLogin()
        {
            Console.WriteLine("Welcome to the Login Screen.\nPlease provide your login details.");

            Console.WriteLine("Email Address:");
            Email = Console.ReadLine();
            Console.WriteLine("Password:");
            Password = Console.ReadLine();

            if (File.Exists($"{FilePath}{Email}.txt"))
            {
                string[] checkfile = File.ReadAllLines($"{FilePath}{Email}.txt");

                if (Password == checkfile[2])
                {
                    BankAppMenuScreen();
                }
                else
                {
                    Console.WriteLine("Incorrect Password..Try again.\n====\n");
                    HomeScreen();
                }
            }
            else
            {
                Console.WriteLine("Account not found");
                Console.WriteLine("Please proceed to registeration first");
                HomeScreen();
            }
        }

        private void BankAppMenuScreen()
        {
            while (true)
            {
                Console.WriteLine("\n====================\nWelcome to Oyin's Bank\nWhat would you like to do?\nPress 1 to CreateAccount" +
                "\nPress 2 to Login\nPress 3 to go back to the Main Menu\nPress 4 to close application.");
                selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    CreateAccount();
                }
                else if (selection == 2)
                {
                    AccountLogin();
                }
                else if (selection == 3)
                {
                    Console.WriteLine("\n============================================\n");
                    HomeScreen();
                }
                else if (selection == 4)
                {
                    Console.WriteLine("You are logging out.\nThank you for using our application");
                    Thread.Sleep(3000);
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Incorrect Entry.Try Again");
                }
            }
        }

        private void CreateAccount()
        {
            Console.WriteLine("Please create an account with us.");

            Console.WriteLine("First Name:");
            FirstName = Console.ReadLine();
            Console.WriteLine("Last Name:");
            LastName = Console.ReadLine();
            Console.WriteLine("Pin:");
            Pin = Convert.ToInt32(Console.ReadLine());

            Random random = new Random();
            string r = "";
            int i;
            for (i = 0; i < 9; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            AccNum = ("3" + r);

            if (File.Exists($"{folderName}{AccNum}.txt"))
            {
                Console.WriteLine("You have an account with us");
                AccountLogin();
            }
            else
            {
                using (StreamWriter sw = File.AppendText($"{folderName}{AccNum}.txt"))
                {
                    sw.WriteLine(FirstName);
                    sw.WriteLine(LastName);
                    sw.WriteLine(AccNum);
                    sw.WriteLine(Pin);
                }
                using (StreamWriter accountBalanceFile = File.CreateText($"{folderName}{AccNum}ab.txt"))
                {
                    accountBalanceFile.WriteLine(1000);
                }
                using (var bankStatementFile = File.Create($"{folderName}{AccNum}bs.txt"))
                {
                    
                }
            }

            Console.WriteLine("Account Created Successfully.\n========================\n" + AccNum);
            Console.WriteLine("\n========================\n ");
            BankAppMenuScreen();
        }

        private void AccountLogin()
        {
            Console.WriteLine("Please login to your account.");

            Console.WriteLine("Account Number");
            AccNum = (Console.ReadLine());
            Console.WriteLine("Pin");
            Pin = Convert.ToInt32(Console.ReadLine());

            if (File.Exists($"{folderName}{AccNum}.txt"))
            {
                string[] filecheck = File.ReadAllLines($"{folderName}{AccNum}.txt");

                List<string> list = new List<string>();

                list.Add(AccNum);
                list.Add(Pin.ToString());

                if (list[1] == filecheck[3])
                {
                    Console.WriteLine("Login Successful\n====================\n");
                    AtmMenuScreen();
                }
                else
                {
                    Console.WriteLine("Wrong Pin");

                }
            }
            else
            {
                Console.WriteLine("Incorrect Details");
                Console.WriteLine("Create an account with us before using our app");
                BankAppMenuScreen();
            }
        }

        private void AtmMenuScreen()
        {
            Console.WriteLine("Hey " + FirstName + ", WELCOME!!!");
            Console.WriteLine(AccNum);

            var Balance = File.ReadAllText($"{folderName}{AccNum}ab.txt");
            Console.WriteLine("Account Balance: " + Balance);

            while (true)
            {
                Console.WriteLine("What would you like to do?\nPress 1 to Deposit" +
                "\nPress 2 to Withdraw\nPress 3 to go to home screen\nPress 4 to print bank statement\nPress 5 to close application.");
                Console.Write("Enter your Choice: ");
                selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    Deposit();
                }
                else if (selection == 2)
                {
                    Withdrawal();
                }
                else if (selection == 3)
                {
                    Console.WriteLine("\n============================================\n");
                    AtmMenuScreen();
                }
                else if (selection == 4)
                {
                    Console.WriteLine("\n============================================\n");
                    PrintBankStatement();
                }
                else if (selection == 5)
                {
                    Console.WriteLine("You are logging out.\nThank you for using our application");
                    Thread.Sleep(3000);
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Incorrect Entry.Try Again");
                    AtmMenuScreen();
                }
            }
        }

        private void Deposit()
        {
            Console.WriteLine("Enter the amount to be deposited");

            var Balance = File.ReadAllText($"{folderName}{AccNum}ab.txt");
            var accountBalance = Convert.ToInt32(Balance);
            deposit = Convert.ToInt32(Console.ReadLine());
            var currentBalance = accountBalance + deposit;

            using (StreamWriter accountBalanceFile = File.CreateText($"{folderName}{AccNum}ab.txt"))
            {
                accountBalanceFile.WriteLine(currentBalance);
            }

            using (StreamWriter bankStatementFile = File.AppendText($"{folderName}{AccNum}bs.txt"))
            {
                bankStatementFile.WriteLine("Credited- " + deposit);
                bankStatementFile.WriteLine("Current Balance is " + currentBalance);

            }

            Console.WriteLine("The current balance in the account is " + currentBalance);
        }

        private void Withdrawal()
        {
            Console.WriteLine("Enter the amount to withdraw");

            var Balance = File.ReadAllText($"{folderName}{AccNum}ab.txt");
            var accountBalance = Convert.ToInt32(Balance);
            withdraw = Convert.ToInt32(Console.ReadLine());


            if (accountBalance > withdraw)
            {
                if (withdraw % 10 == 0)
                {
                    var currentBalance = accountBalance - withdraw;
                    Console.WriteLine("Please collect your cash " + withdraw);

                    Console.WriteLine("The current balance is now " + currentBalance);

                    using (StreamWriter accountBalanceFile = File.CreateText($"{folderName}{AccNum}ab.txt"))
                    {
                        accountBalanceFile.WriteLine(currentBalance);
                    }

                    using (StreamWriter bankStatementFile = File.AppendText($"{folderName}{AccNum}bs.txt"))
                    {
                        bankStatementFile.WriteLine("Debited - " + withdraw);
                        bankStatementFile.WriteLine("Current Balance is " + currentBalance);
                    }
                }
                else
                    Console.WriteLine("Please enter the amount in multiples of 10");
            }
            else
                Console.WriteLine("Your account doesn't have sufficient balance");
        }

        private void PrintBankStatement()
        {
            var printFile = File.ReadAllText($"{folderName}{AccNum}bs.txt");
            Console.WriteLine(printFile);
        }
    }
}
