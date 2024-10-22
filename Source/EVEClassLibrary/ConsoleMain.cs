using System;
using System.Runtime;
using System.Reflection;
using System.IO;
using Newtonsoft;

namespace EVEClassLibrary
{
    /// <summary>
    /// Main Console Class, Handles the Basic User Input
    /// </summary>
    public class ConsoleMain
    {
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

            string Author =  type.Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            Console.Title =  type.Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
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
            switch (pressed.Key)
            {

                case ConsoleKey.C:
                    ClearScreen();
                    StartInfo();
                    break;

                case ConsoleKey.M:
                    ESIRunner runner = new ESIRunner();
                    Console.WriteLine(runner.MarketUrl());
                    break;

                case ConsoleKey.T:
                    //implement Type Checker
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
            if (pressed.Key != ConsoleKey.Escape)
            {
                ParseCommand();
            }
            else
            {
                Console.WriteLine("Exiting");
            }

        }
    }
}
