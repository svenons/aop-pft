using System.Runtime.CompilerServices;

namespace PersonalFinanceTracker {
    class Program {
        private UserInterface UserInterface = new();
        static void Main(string[] args) {
            UserInterface.InitialiseUI();
            UserInterface.MainMenu();
        }
    }
}