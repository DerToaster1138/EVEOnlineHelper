using System;
using System.Runtime;
using System.Reflection;
using System.IO;
using Newtonsoft;
using System.Runtime.CompilerServices;

namespace EVEClassLibrary
{
    /// <summary>
    /// Main Console Class, Handles the Basic User Input
    /// </summary>
    public class ConsoleMain
    {
        //TODO: CodeReview: is this field supposed to be public or would you rather have this private? Private might be better for the future
        public static ESIRunner esirunner = new ESIRunner();

        /// <summary>
        /// Standard Constructor
        /// </summary>
        public ConsoleMain() 
        {
            //TODO: CodeReview: if you don have any other constructor i'd rather not make a parameterless empty constructor and let .net do its thing. Since all functions here are static i'd remove this
        }
        /// <summary>
        /// Initializing Console
        /// </summary>
        public static void init()
        {
            Creds();
            Spacer(2);
            StartInfo();
            ParseCommand();
        }

        /// <summary>
        /// Credits of the Library
        /// </summary>
        public static void Creds()
        {
            System.Type type = typeof(ConsoleMain);

            //TODO: CodeReview: these calls might trigger nullreferences. Suggestion: write attributes first into variables to be able to check if they are null. Use small function to avoid writing duplicate code
            string Author = type.Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            Console.Title = type.Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
            string version = type.Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version.ToString();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"written by {Author}");
            Console.WriteLine($"Beta {version}");
            Console.WriteLine("DO NOT REDISTRIBUTE");
            Console.WriteLine("Work in progress");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Pseudo Menu
        /// </summary>
        public static void StartInfo()
        {
            Console.WriteLine("Available Commands:");
            //TODO: CodeReview: What you print here and what you process ParseCommand might mismatch when further expanding. consider introducing you own enum and map the enums first value to the console
            Console.WriteLine("   [M]arketinformations");
            Console.WriteLine("   [T]ypeinformation");
            Console.WriteLine("   [C]lear Screen");
            Spacer(2);
        }


        /// <summary>
        /// Spacing Method, created empty lines with a simple command
        /// </summary>
        /// <param name="space"> the int representation of the lines to be kept empty</param>
        public static void Spacer(int space)
        {
            for (int i = 0; i < space; i++)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Clears the Screen
        /// </summary>
        public static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Parses the Key presses for the Main Menu
        /// </summary>
        public static void ParseCommand()
        {
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            try
            {
                switch (pressed.Key)
                {

                    case ConsoleKey.C:
                        ClearScreen();
                        StartInfo();
                        break;

                    case ConsoleKey.M:
                        MarketCheck();
                        Console.WriteLine(esirunner.MarketUrl());
                        break;

                    case ConsoleKey.T:
                        //Set color to green, as destinctive Coloration
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Please enter the TypeID you want to check for:");
                        Console.WriteLine(esirunner.TypeCheck());
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Returning to Main");
                        break;

                    case ConsoleKey.F9:
                        if (System.OperatingSystem.IsWindows())
                        {
                            Console.Beep(220, 250);
                            Console.Beep(247, 250);
                            Console.Beep(262, 250);
                            Console.Beep(294, 250);
                            Console.Beep(330, 250);
                            Console.Beep(349, 250);
                            Console.Beep(392, 250);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Unkown Error occured: " + ex.ToString());
            }
            if (pressed.Key != ConsoleKey.Escape)
            {
                ParseCommand();
            }
            else
            {
                Console.WriteLine("Exiting");
            }


        }

        /// <summary>
        /// Console Output for Tradehubs, used in MarketCheck
        /// </summary>
        /// <returns>int of the Tradehub</returns>
        public static int TradehubIDMap()
        {
            int ret;
            Console.WriteLine("Please Select the Region you'd like to check");

            //TODO: CodeReview: What you print here and what you process in ESI-Runner might mismatch when expanding the code further. Consider using an Enum of your own  and matching the first letter of the ToString()-value with ConsolePressedKey
            Console.WriteLine("[J]ita");
            Console.WriteLine("[A]marr");
            Console.WriteLine("[T]ash-Murkon");
            Console.WriteLine("[R]ens");
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            ret = esirunner.KeyToTradehub(pressed.Key);

            return ret;
        }

        /// <summary>
        /// Starts the MarketCheck functions
        /// </summary>
        public static void MarketCheck()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Marketchecker");
            Console.WriteLine("Do you want to Filter for for only [S]ell, [B]uy, or viel [A]ll orders?");
        }

        /// <summary>
        /// Check for TypeID
        /// </summary>
        /// <returns>The TypeID as string</returns>
        public static string getTypeIDInput() 
        {
            Console.WriteLine("Please enter the TypeID of the requested Item");
            string typeID = Console.ReadLine();
            return typeID;
        }

    }
}
