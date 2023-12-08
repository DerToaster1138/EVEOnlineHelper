using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEMarketeer_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Creds();
            Spacer(2);
            StartInfo();
            ParseCommand();

        }

        public static void Creds()
        {
            Console.Title = "EVE ONLINE CONSOLE V0.1";
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("EVEOnline Helper");
            Console.WriteLine("By DerToaster1138/Tanam Rotsuda");
            Console.WriteLine("DO NOT REDISTRIBUTE");
            Console.WriteLine("Work in progress");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void StartInfo()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("   [M]arketinformations");
            Console.WriteLine("   [T]ypeinformation");
            Console.WriteLine("   [C]lear Screen");
            Spacer(2);
        }

        public static void Spacer(int space)
        {
            for (int i = 0; i < space; i++)
            {
                Console.WriteLine();
            }
        }
        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void ParseCommand()
        {
            ConsoleKeyInfo pressed = Console.ReadKey();
            switch(pressed.Key)
            {

                case ConsoleKey.C:
                    ClearScreen();
                    break;

                case ConsoleKey.M:
                    //implement marketChecker
                    break;

                case ConsoleKey.T:
                    //implement Type Checker
                    break;

                case ConsoleKey.F9:
                    Console.Beep(2000, 5);
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
