using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public class AppScreen
    {

        internal const string cur = "N ";
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
    }
}
