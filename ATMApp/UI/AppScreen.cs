using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public class AppScreen
    {

        GeneratorClass GeneratorClass = new GeneratorClass();

        internal const string cur = "N ";

     



        internal static bool InitialWelcome ()
        {
            InformUserToRegister();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    if (keyInfo.Key == ConsoleKey.D1)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
        }

        internal static void Welcome()
        {
            //clears the console screen
            Console.Clear();
            //sets the title of the console window
            Console.Title = "My ATM App";
            //sets the text color or foreground color to white
            Console.ForegroundColor = ConsoleColor.White;



            //set the welcome message 
            Console.WriteLine("\n\n-----------------------Weclome to My ATM App-----------------------");
            //prompts the user to insert atm card
            Console.WriteLine("Please Inert your ATM Card");
            Console.WriteLine("Note: Actual ATM machine will accept and validate a physical ATM card, read the card number and validate the card");

            Utility.PressEnterToContinue();

           
        }

        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Enter Your card Number");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN"));
            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\n\nChecking card number and PIN...");
            Utility.PrintDotAnimation(10);
        }

        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to the nearest branch to unlock you account. Thank you", true);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcmwelcome back, {fullName}");
            Utility.PressEnterToContinue();
        }

        internal static void InformUserToRegister()
        {
            Console.WriteLine("\n\nPress 1 to register if you dont have an account");
            Console.WriteLine("\nPress Enter to proceed to ATM");
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-------My ATM App Menu-------");
            Console.WriteLine(":                            ");
            Console.WriteLine("1. Account Balance           ");
            Console.WriteLine("2. Cash Deposit              ");
            Console.WriteLine("3. Withdrwal                 ");
            Console.WriteLine("4. Transfer                  ");
            Console.WriteLine("5. Transactions              ");
            Console.WriteLine("6. Logout                    ");

        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using my ATM App");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine("1.{0}500          5:10000{0}", cur);
            Console.WriteLine("2.{0}1000         6:150000{0}", cur);
            Console.WriteLine("3.{0}2000         7:200000{0}", cur);
            Console.WriteLine("4.{0}5000         8:400000{0}", cur);
            Console.WriteLine("0.Other");
            Console.WriteLine("");

            int selectedAmount = Validator.Convert<int>("option");

            switch(selectedAmount)
            {
                case 1:
                    return 500;
                    break;
                case 2:
                    return 1000;
                    break;
                case 3:
                    return 2000;
                     break;

                case 4:
                    return 5000;
                     break;

                case 5:
                    return 10000;
                     break;

                case 6:
                    return 15000;
                     break;
                case 7:
                    return 20000;
                     break;
                case 8:
                    return 40000;
                     break;

                case 0:
                    return 0;
                     break;
                default:
                    Utility.PrintMessage("Invalid Input. Try again", false);
                    return -1;
                        break;
            }
        }

        internal InternalTransfer InternaTransferForm()
        {
            var internalTransfer = new InternalTransfer();

            internalTransfer.RecipientBankAccountNumber = Validator.Convert<long>("Recipient account number");
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("Recipient name");
            return internalTransfer;
        }

        internal UserAccount RegisterUser()
        {
            var userToBeRegistered = new UserAccount();

            string firstName = Validator.Convert<string>("Firstnme");


            //refactor this later
            while(string.IsNullOrWhiteSpace(firstName) || Utility.ContainsNumber(firstName))
            {
                bool result = Utility.ContainsNumber(firstName);
                if (result)
                {
                    Utility.PrintMessage("Name cannot contain numbers", false);
                    firstName = Validator.Convert<string>("Firstnme");
                } else
                {
                    Utility.PrintMessage("Invalid Input. Firstname cannot be empty space", false);
                    firstName = Validator.Convert<string>("Firstnme");
                }
            }



            string lastName = Validator.Convert<string>("Lastname");
            while(string.IsNullOrWhiteSpace(lastName) || Utility.ContainsNumber(lastName))
            {
                bool result = Utility.ContainsNumber(lastName);
                if (result)
                {
                    Utility.PrintMessage("Name cannot contain numbers", false);
                }
                else
                {
                    Utility.PrintMessage("Invalid Input. Firstname cannot be empty space", false);
                    firstName = Validator.Convert<string>("Firstnme");
                }
            }


            int gender = Validator.Convert<int>("Gender \n1 for Male. \n2 for Female.");

            while (gender > 2 || gender < 1)
            {
                Utility.PrintMessage($"The only vlid inuputs are 1 and 2", false);
                gender = Validator.Convert<int>("Gender \n1 for Male. \n2 for Female");
            }

            int accountType = Validator.Convert<int>("Account type \n1 For Savings Account. \n2 For Current Account. \n3 For Credit Account");
            while (accountType > 3 || accountType < 1)
            {
                Utility.PrintMessage($"The only vlid inuputs are 1, 2 and 3", false);
                accountType = Validator.Convert<int>("Account type \n1 For Savings Account. \n2 For Current Account. \n3 For Credit Account");
            }

            int initialAccountBalance = Validator.Convert<int>("Inital deposit amount");

            
            int accountPin = Validator.Convert<int>("Desired Pin");
            while (accountPin.ToString().Length != 5)
            {
                Utility.PrintMessage($"Your pin has to be six digits long", false);
                accountPin = Validator.Convert<int>("Desired Pin");
            }

            userToBeRegistered.Id = GeneratorClass.GenerateId();
            userToBeRegistered.CardNumber = GeneratorClass.GenerateCardNumber();
            userToBeRegistered.AccountNumber = GeneratorClass.GenerateAccountNumber();
            userToBeRegistered.FullName = $"{lastName} {firstName}";
            userToBeRegistered.Gender = (GenderType)gender;
            userToBeRegistered.AccountType = (AccountType)accountType;
            userToBeRegistered.AccoutnBalance = initialAccountBalance;
            userToBeRegistered.CardPin = accountPin;
            userToBeRegistered.IsLocked = false;
            userToBeRegistered.TotalLogin = 0;

            return userToBeRegistered;
        }


    }
}
