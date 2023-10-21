using ATMApp.Domain.Entities;
using ATMApp.Domain.Enums;
using ATMApp.Domain.Interfaces;
using ATMApp.UI;
using ConsoleTables;
using System.Data;
using System.Text;

namespace ATMApp.App
{
    public class ATMApp : IUserLogin, IUserAccountActions, ITransaction
    {


        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        private List<Transaction> _listOfTransactions;
        private const decimal minimumKeptAmount = 500;

        private readonly AppScreen screen;

        public ATMApp()
        {
            screen = new AppScreen();   
        }

        public void CheckUserCardNumberAndPassword()
        {
            bool isCorrecLogin = false;
            while (isCorrecLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach (UserAccount account in userAccountList)
                {
                    selectedAccount = account;

                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;
                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;

                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                //Print a locked message
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrecLogin = true;
                                break;
                            }
                        }
                    }
                }
                if (isCorrecLogin == false)
                {
                    Utility.PrintMessage("\nInvalid card number or PIN", false);
                    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                    if (selectedAccount.IsLocked)
                    {
                        AppScreen.PrintLockScreen();
                    }
                }
                Console.Clear();
            }
        }


        public void Run()
        {
            bool willregister = AppScreen.InitialWelcome();

            if(willregister == true)
            {
                Console.WriteLine("Fill the following form to register\n\n");
                RegisterUserAccount(screen.RegisterUser());
            } else
            {
                AppScreen.Welcome();
                CheckUserCardNumberAndPassword();
                AppScreen.WelcomeCustomer(selectedAccount.FullName);
            }

            AppScreen.InitialWelcome();
            CheckUserCardNumberAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);

            while (true)
            {
                AppScreen.DisplayAppMenu();

                ProcessMenuOption();
                Utility.PressEnterToContinue();
            }

        }

        private void ProcessMenuOption()
        {
            switch (Validator.Convert<int>("an option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;

                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;

                case (int)AppMenu.MakeWithdrawal:
                    MakeWithdrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternaTransferForm();
                    ProcessInternalTransfer(internalTransfer);  
                    break;
                case (int)AppMenu.ViewTransaction:
                    ViewTransaction();
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogoutProgress();
                    Utility.PrintMessage("You have successfully logged out. Please collect your AATM card");
                    Run();
                    break;

                default:
                    Utility.PrintMessage("Invalid Option", false);
                    break;
            }

        }



        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount
                {
                    Id = 1, FullName = "Segunmaru Faozan", AccountNumber = 123456, CardNumber = 321321, CardPin = 123123, AccoutnBalance = 5000000.00m, IsLocked=false
                },new UserAccount
                {
                    Id = 2, FullName = "Segunmaru Fridaoz", AccountNumber = 456789, CardNumber = 654654, CardPin = 456456, AccoutnBalance = 400000.00m, IsLocked=false
                },new UserAccount
                {
                    Id = 1, FullName = "Segunmaru Fawaz", AccountNumber = 123555, CardNumber = 987987, CardPin = 789789, AccoutnBalance = 3400000.00m, IsLocked=false
                }
            };

            _listOfTransactions = new List<Transaction>();
        }

        public void RegisterUserAccount(UserAccount userAccount)
        {
            userAccountList.Add(userAccount);
            Console.Clear();

            Utility.PrintDotAnimation();

            var table = new ConsoleTable("", "");
            table.AddRow("Account name:", userAccount.FullName);
            table.AddRow("Account number:", userAccount.AccountNumber);
            table.AddRow("Card Number:", userAccount.CardNumber);
            table.AddRow("Card Pin:", userAccount.CardPin);
            table.AddRow("Account Balance:", userAccount.AccoutnBalance);
            table.AddRow("Account Type:", (AccountType)userAccount.AccountType);
            table.AddRow("Gender:", (GenderType)userAccount.Gender);

            Console.WriteLine($"User successfully registered.\n\nPlease write the following information down:\n{table.ToMarkDownString()}");
            Console.WriteLine("\nThank you for banking with us");

            Utility.PressEnterToContinue();
            Console.Clear();
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your account balance is {Utility.FormatAmount(selectedAccount.AccoutnBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiples of 500 and 1000 naira allowed");
            var transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");

            //simulate counting
            Console.WriteLine("\nChecking ancd counting bank notes.");
            Utility.PrintDotAnimation();
            Console.WriteLine("");


            //some guard clause
            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than 0, try again", false);
                return;
            }

            if(transaction_amt % 500 != 0)
            {
                Utility.PrintMessage($"Enter deposit amount in mutiple of 500 or 1000, try again", false);
                return;
            }

            if(PreviewBankNotesCount(transaction_amt) ==false)
            {
                Utility.PrintMessage($"You have cancelled you action", false);
                return;
            }


            //bind transaction details to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amt, "");

            //Update account balance
            selectedAccount.AccountNumber += transaction_amt;

            //print success message
            Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transaction_amt)} was successful", true);

        }

        public void MakeWithdrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmount();

            if(selectedAmount == -1)
            {
                MakeWithdrawal();
                return;
            } else if(selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            } else
            {
                transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");
            }

            //input validation
            if(transaction_amt <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than zero, try again", false); ;
                return;
            }

            if(transaction_amt % 500 != 0)
            {
                Utility.PrintMessage("You can only withdraw amounts in multiples of 500 or 1000. Try again", false);
                return;
            }  

            //Business Logic validations
            if(transaction_amt > selectedAccount.AccoutnBalance)
            {
                Utility.PrintMessage($"Withdrawal failed, Your balance is too low to withdraw {Utility.FormatAmount(transaction_amt)}", false);
                return;
            }

            if((selectedAccount.AccoutnBalance - transaction_amt) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Withdrwal failed. Your account needs to have minimum {Utility.FormatAmount(minimumKeptAmount)}");
                return;
            }

            //BBind withdrawal details to transaction object
            InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, transaction_amt, "");

            //update account balance
            selectedAccount.AccoutnBalance -= transaction_amt;

            //success mesasge
            Utility.PrintMessage($"You have successfully withdrawn {Utility.FormatAmount(transaction_amt)}", true);


        }

        private bool PreviewBankNotesCount(int amount)
        {
            int thousandNotesCount = amount / 1000;
            int fivehundredNotesCount = (amount % 1000) / 500;

            Console.WriteLine("\nSummary");
            Console.WriteLine("-------");
            Console.WriteLine($"{AppScreen.cur}1000 X {thousandNotesCount} = {1000 * thousandNotesCount}");
            Console.WriteLine($"{AppScreen.cur}500 X {fivehundredNotesCount} = {500 * fivehundredNotesCount}");
            Console.WriteLine($"Total Amount: {Utility.FormatAmount(amount)} \n\n");

            int opt = Validator.Convert<int>("1 to confirm");
           
            return opt.Equals(1);
        }

        public void InsertTransaction(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _desc)
        {
            //create  anew transaction object
            Transaction transaction = new()
            {
                TransactionId = Utility.GetTransactionId(),
                UserBankAccountID = _UserBankAccountId,
                TransctionDate = DateTime.Now,
                TransactionType = _tranType,
                TransactionAmount = _tranAmount,
                Description = _desc
            };

            //add transaction object to the list
            _listOfTransactions.Add(transaction);
        }

        public void ViewTransaction()
        {
            var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountID == selectedAccount.Id).ToList();
            //check if there is a transaction
            if(filteredTransactionList.Count <= 0)
            {
                Utility.PrintMessage("You have no transactions yet", true);
            } else
            {
                var table = new ConsoleTable("Id", "Transaction Date", "Type", "Descriptions", "Amount" + AppScreen.cur);
                foreach(var tran in filteredTransactionList)
                {
                    table.AddRow(tran.TransactionId, tran.TransctionDate, tran.TransactionType, tran.Description, tran.TransactionAmount);
                }

                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMessage($"You have {filteredTransactionList.Count} transaction(s)", true);
            }
        }

        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            if(internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than zero. Try again", false);
                return;
            }

            //check senders account balance
            if(internalTransfer.TransferAmount > selectedAccount.AccoutnBalance)
            {
                Utility.PrintMessage($"Transfer failed. You do not have enough balance to transfer {Utility.FormatAmount(internalTransfer.TransferAmount)}");
                return;
            }

            //check the minimum kept amount
            if((selectedAccount.AccoutnBalance - internalTransfer.TransferAmount) < minimumKeptAmount)
            {
                Utility.PrintMessage($"Transfer failed. Your account needs to have minimum {Utility.FormatAmount(minimumKeptAmount)}", false);
                return;
            }

            //check recievers accoutn number is valid
            var selectedBankAccountReciever = (from userAcc in userAccountList
                                                   where userAcc.AccountNumber == internalTransfer.RecipientBankAccountNumber
                                                   select userAcc).FirstOrDefault();

            if(selectedBankAccountReciever == null)
            {
                Utility.PrintMessage("Transfer failed. Reciever bank account number is invalid.", false);
                return;
            }

            //check reciever's name
            if(selectedBankAccountReciever.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("Transfer Failed. Recipient's bank account name does not match.", false);
                return;
            }

            //add transaction to transaction record
            InsertTransaction(selectedAccount.Id, TransactionType.Transfer, -internalTransfer.TransferAmount, $"Transferred to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})");

            //update senders account balance
            selectedAccount.AccoutnBalance -= internalTransfer.TransferAmount;

            //add transaction record-reciever
            InsertTransaction(selectedBankAccountReciever.Id, TransactionType.Transfer, internalTransfer.TransferAmount, $"Transferred from ${selectedAccount.AccountNumber} ({selectedAccount.FullName})");


            //update reciever's account balance
            selectedBankAccountReciever.AccoutnBalance += internalTransfer.TransferAmount;


            //print success message
            Utility.PrintMessage($"You have successfully transferred {Utility.FormatAmount(internalTransfer.TransferAmount)} to {internalTransfer.RecipientBankAccountName}", true);


        }
    }
}