using System;

namespace PersonalFinanceTracker {
    public class UserInterface {

        public static void InitialiseUI() {
            Console.Clear();
            Console.CursorVisible = false;
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            if(windowHeight < 20 || windowWidth < 80) {
                Console.WriteLine("Your Console Window is too small to run this application. Please resize your window to at least 80x20.");
                Environment.Exit(1);
            }
            //Console.WriteLine($"W: {windowWidth}\nH: {windowHeight}");
        }
        public static int MainMenu() {
            int mainMenuWidth = 30;
            int mainMenuHeight = 6;
            int leftIndent = (Console.WindowWidth-mainMenuWidth)/2 -1;
            int topIndent = (Console.WindowHeight-mainMenuHeight)/2 -1;
            
            Console.SetCursorPosition(leftIndent + 3, topIndent - 1);
            Console.Write("Personal Finance Tracker");

            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u2557');

            Console.SetCursorPosition(leftIndent, topIndent);
            for(int _ = 0; _ <= mainMenuHeight -2; ++_) {
                Console.SetCursorPosition(leftIndent, Console.GetCursorPosition().Top + 1);
                Console.Write('\u2551');
                Console.SetCursorPosition(Console.GetCursorPosition().Left + mainMenuWidth - 1, Console.GetCursorPosition().Top);
                Console.Write('\u2551');
            }

            Console.SetCursorPosition(leftIndent, topIndent + mainMenuHeight);
            Console.Write('\u255A');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u255D');

            int currentSelection = 0;
            string[] menuItems = {"Add Transaction", "Remove Transaction", "List Transactions", "Financial Statement", "Exit"};
            while(true) {
                for(int _ = 0; _ <= 4; ++_) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + _);
                    Console.Write($"{(_ == currentSelection ? "> " : "")}{menuItems[_]}");
                }

                ConsoleKey userInput = Console.ReadKey().Key;
                switch(userInput) {
                    case ConsoleKey.DownArrow:
                        if(currentSelection == 4) currentSelection = 0;
                        else ++currentSelection;
                        break;
                    
                    case ConsoleKey.UpArrow:
                        if(currentSelection == 0) currentSelection =4;
                        else --currentSelection;
                        break;

                    default:
                        break;
                }
            }

            return 0;
        }

        public static void CreateTransactionMenu() {

        }

        public static void DeleteTransactionMenu() {

        }

    }
}
