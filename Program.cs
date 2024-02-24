namespace PersonalFinanceTracker {
    class Program {
        private UserInterface UserInterface = new();
        static void Main(string[] args) {
                while(true) {
                UserInterface.InitialiseUI();
            }
        }
    }
}