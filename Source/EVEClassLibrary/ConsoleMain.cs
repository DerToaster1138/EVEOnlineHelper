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
        public static ESIRunner esirunner = new ESIRunner();

        public ConsoleMain() 
        {
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

        public static int TradehubIDMap()
        {
            int ret;
            Console.WriteLine("Please Select the Region you'd like to check");
            Console.WriteLine("[J]ita");
            Console.WriteLine("[A]marr");
            Console.WriteLine("[T]ash-Murkon");
            Console.WriteLine("[R]ens");
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            ret = esirunner.KeyToTradehub(pressed.Key);

            return ret;
        }

        public static void MarketCheck()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Marketchecker");
            Console.WriteLine("Do you want to Filter for for only [S]ell, [B]uy, or viel [A]ll orders?");
        }

        public static string getTypeIDInput() 
        {
            string typeID;
            Console.WriteLine("Please enter the TypeID of the requested Item");
            typeID = Console.ReadLine();
            return typeID;
        }

    }
}
