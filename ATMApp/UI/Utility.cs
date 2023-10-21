using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ATMApp.UI
{

    
    public static class Utility
    {
        private static long tranId;

        private static CultureInfo culture = new CultureInfo("IG-NG");

        public static long GetTransactionId()
        {
            return ++tranId;
        }
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;

            string asterics = "";

            StringBuilder input = new StringBuilder();

            while(true)
            {
                if (isPrompt)
                    Console.WriteLine(prompt);
                    isPrompt = false;

                    ConsoleKeyInfo inputKey = Console.ReadKey(true);

                    if (inputKey.Key == ConsoleKey.Enter)
                    {
                        if (input.Length == 6)
                        {
                            break;
                        }
                        else
                        {
                            PrintMessage("\nPlease enter 6 digits", false);
                            isPrompt = true;
                            input.Clear();
                            continue;
                        }

                    }

                    if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                    }
                    else if (inputKey.Key != ConsoleKey.Backspace)
                    {
                        input.Append(inputKey.KeyChar);
                        Console.Write(asterics + "*");
                    }
                
            }

            return input.ToString();
        }
        public static void PrintMessage(string msg, bool success = true)
        {
            if(success)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red; 
            }

            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
            PressEnterToContinue();
                }

        public static string GetUserInput(string prompt)
        {   
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }
        public static void PrintDotAnimation(int timer = 10)
        {
            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.Clear();
        }
        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nPress Enter To Continue...");
            Console.ReadLine();
        }

        public static string FormatAmount(decimal amt)
        {
            return String.Format(culture, "{0:c2}", amt);
        }

        public static bool IsEmptyOrWhiteSpace(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool ContainsNumber(string input)
        {
            // Use a regular expression to check for a number in the input string
            // \d matches any digit (0-9)
            Match match = Regex.Match(input, @"\d");

            // If a match is found, it means the string contains a number
            return match.Success;
        }
    } 
}
 