using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;


public class UserManagerClass
{
    public static string Email;
    public static string FullName;
    public static string Password;
    public static string FirstName;
    public static string LastName;
    public static int selection;
    public static string AccNum;
    public static string AccountBalance;
    public static int Pin;
    public static int amount = 1000;
    public static int deposit;
    public static int current;
    public static int withdraw;
    public static string FilePath = @"C:\Users\oyins\Desktop\Oyinda\FilesFolder\";
    public static string folderName = @"C:\Users\oyins\Desktop\Oyinda\BankDetails\";

    public static void HomeScreen()
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

    public static void UserRegister()
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

    public static void UserLogin()
    {
        Console.WriteLine("Welcome to the Login Screen.\nPlease provide your login details.");

        Console.WriteLine("Email Address:");
        Email = Console.ReadLine();
        Console.WriteLine("Password:");
        Password = Console.ReadLine();

        if (File.Exists($"{FilePath}{Email}.txt"))
        {
            /*var accountnumber = "";

            var accountDetails = $"{FilePath}{accountnumber}.txt";

            var accountBalanceFile = $"{FilePath}{accountnumber}1.txt";*/


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

    public static void BankAppMenuScreen()
    {
        while (true)
        {
            Console.WriteLine("Welcome to Oyin's Bank\nWhat would you like to do?\nPress 1 to CreateAccount" +
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

    public static void CreateAccount()
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
                sw.WriteLine(amount);
            }
            //File.Create($"{FilePath}{AccNum}ab.txt"); Create another file for account tracker
        }

        //Generating a 10 digit account number.
        //Use the account number generated to create a file e.g (9878654328.txt)
        //Save account number into file.
        //Create a account balance tracker file e.g $"{9878654328}ab.txt"
        //and you gift the person 1000 naira.
        Console.WriteLine("Account Created Successfully.\n========================\n AccNum");
        Console.WriteLine("\n========================\n ");
        BankAppMenuScreen();
    }

    public static void AccountLogin()
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

    public static void AtmMenuScreen()
    {
        Console.WriteLine("Hey" + FirstName + ", WELCOME!!!");
        Console.WriteLine(AccNum);
        Console.WriteLine("Account Balance: " + current);

        while (true)
        {
            Console.WriteLine("What would you like to do?\nPress 1 to Deposit" +
            "\nPress 2 to Withdraw\nPress 3 to go to home screen\nPress 4 to close application.");
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
                AtmMenuScreen();
            }
        }
    }

    public static void Deposit()
    {
        Console.WriteLine("Enter the amount to be deposited");
        deposit = Convert.ToInt32(Console.ReadLine());
        current = amount + deposit;
        amount = current;

        //var currentAsAString = Convert.ToString(current);
        //var amountAsAString = Convert.ToString(amount);
        //string text = File.ReadAllText($"{folderName}{AccNum}.txt");
        //text = text.Replace(amountAsAString, currentAsAString);
        //File.WriteAllText($"{folderName}{AccNum}.txt", text);

        //var filePath = $"{folderName}3151275815";
        //string[] filecheck = File.ReadAllLines($"{filePath}.txt");
        //foreach (var item in filecheck)
        //{
        //    if (item == Convert.ToString(amount))
        //    {
        //        item.Replace(amountAsAString, currentAsAString);
        //    }
        //}
        //foreach (var item in filecheck)
        //{
        //    Console.WriteLine(item);
        //}
        string[] filecheck = File.ReadAllLines($"{folderName}{AccNum}.txt");
        using (StreamWriter sw = File.AppendText($"{folderName}{AccNum}.txt"))

        {
            sw.WriteLine($"{folderName}{AccNum}.txt", amount);
        }

        Console.WriteLine("The current balance in the account is " + current);
    }

    public static void Withdrawal()
    {
        Console.WriteLine("Enter the amount to withdraw");
        withdraw = Convert.ToInt32(Console.ReadLine());
        if (amount > withdraw)
        {
            if (withdraw % 10 == 0)
            {
                Console.WriteLine("Please collect your cash " + withdraw);
                current = amount - withdraw;
                Console.WriteLine("The current balance is now " + current);
            }
            else
                Console.WriteLine("Please enter the amount in multiples of 10");
        }
        else
            Console.WriteLine("Your account doesn't have sufficient balance");
    }
}


class Program
{
    static void Main()
    {
        UserManagerClass.HomeScreen();
    }
}


