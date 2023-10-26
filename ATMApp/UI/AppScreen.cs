using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;

namespace ATMApp.UI
{
    public class AppScreen
    {
        GeneratorClass GeneratorClass = new();

        public const string cur = "N ";

        public static bool InitialWelcome()
        {
            InformUserToRegister();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    bool keySwitch = keyInfo.Key == ConsoleKey.D1;

                    return keySwitch;
                }
            }
        }

        public static void Welcome()
        {
            Console.Clear();
            Console.Title = "My ATM App";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n-----------------------Weclome to My ATM App-----------------------");
            Console.WriteLine("Please Inert your ATM Card");
            Console.WriteLine("Note: Actual ATM machine will accept and validate a physical ATM card, read the card number and validate the card");

            Utility.PressEnterToContinue();
        }

        public static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new()
            {
                CardNumber = "",
                CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card PIN"))
            };

            return tempUserAccount;
        }

        public static void LoginProgress()
        {
            Console.WriteLine("\n\nChecking card number and PIN...");
            Utility.PrintDotAnimation(10);
        }

        public static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to the nearest branch to unlock you account. Thank you", true);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }

        public static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcmwelcome back, {fullName}");
            Utility.PressEnterToContinue();
        }

        public static void InformUserToRegister()
        {
            Console.WriteLine("\n\nPress 1 to register if you dont have an account");
            Console.WriteLine("\nPress Enter to proceed to ATM");
        }

        public static void DisplayAppMenu()
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

        public static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using my ATM App");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

        public static void SelectAmountOption()
        {
            Console.WriteLine("");
            Console.WriteLine("1.{0}500          5:10000{0}", cur);
            Console.WriteLine("2.{0}1000         6:150000{0}", cur);
            Console.WriteLine("3.{0}2000         7:200000{0}", cur);
            Console.WriteLine("4.{0}5000         8:400000{0}", cur);
            Console.WriteLine("0.Other");
            Console.WriteLine("");
        }

        public static decimal SelectAmount()
        {
            int selectedAmount = Validator.Convert<int>("option");
            var amount = selectedAmount switch
            {
                1 => 500,
                2 => 1000,
                3 => 2000,
                4 => 5000,
                5 => 10000,
                6 => 15000,
                7 => 20000,
                8 => 40000,
                0 => 0,
                _ => -1
            };

            return amount;
        }

        public InternalTransfer InternaTransferForm()
        {
            var internalTransfer = new InternalTransfer();

            internalTransfer.RecipientBankAccountNumber = "";
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("Recipient name");
            return internalTransfer;
        }
    }
}
