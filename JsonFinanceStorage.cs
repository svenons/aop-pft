using System.Text.Json;

namespace PersonalFinanceTracker {
    public class JsonFinanceStorage : IFinanceStorage{
        private static readonly string filePath = "transaction.json";
        
        public List<Transaction>? Transaction {get;set;} 
        
        public List<Transaction> Load()
        {
            try{
                if(File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var transaction = JsonSerializer.Deserialize<List<Transaction>>(json);
                    return transaction ?? new List<Transaction>();
                   
                }
                else{
                    return new List<Transaction>();
                }
            }catch(Exception e){
                Console.WriteLine($"Error loading transactions: {e.Message}");
                return new List<Transaction>();
            }
        }
        public bool Save(List<Transaction> storage)
        {
            try{
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var json = JsonSerializer.Serialize(storage, options);
                File.WriteAllText(filePath, json);
                return true;

            }catch(Exception e){
                Console.WriteLine($"Error saving transaction: {e.Message}");
                return false;   
            }
        }
    }
}
