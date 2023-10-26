using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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

        // What this method is doing is not clear???
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;

            string asterics = "";

            StringBuilder input = new();

            while (true)
            {
                if (isPrompt)
                {
                    Console.WriteLine(prompt);
                }

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
                else
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }
            }

            return input.ToString();
        }

        public static void PrintMessage(string msg, bool success = true)
        {
            Console.ForegroundColor = success ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            PressEnterToContinue();
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine()!;
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
            Console.Write("\n\nPress Enter To Continue... ");
            Console.ReadLine();
            Console.Write(Environment.NewLine);
        }

        public static string FormatAmount(decimal amt)
        {
            return String.Format(culture, "{0:c2}", amt);
        }

        public static bool ContainsNumber(string input)
        {
            Match match = Regex.Match(input, @"\d");
            return match.Success;
        }

        public static string GenerateNumber(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Length must be greater than 0.");
            }

            string characters = "0123456789";
            Random random = new();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = characters[random.Next(0, characters.Length)];
            }

            return new string(result);
        }
    }
}
