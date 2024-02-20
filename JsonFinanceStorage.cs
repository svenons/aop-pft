using System.ComponentModel;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;
using System.IO; 

namespace PersonalFinanceTracker {
    public class JsonFinanceStorage : IFinanceStorage{
        private static readonly string filePath = "transaction.json";
        
        public List<IFinanceStorage>? Transaction {get;set;} 
        
        public static List<IFinanceStorage> Load()
        {
            try{
                if(File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var transaction = JsonSerializer.Deserialize<List<IFinanceStorage>>(json);
                    return transaction ?? new List<IFinanceStorage>();
                   
                }
                else{
                    return new List<IFinanceStorage>();
                }
            }catch(Exception e){
                Console.WriteLine($"Error loading transactions: {e.Message}");
                return new List<IFinanceStorage>();
            }
        }
        public static bool Save(List<IFinanceStorage> storage)
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

        void IFinanceStorage.Save()
        {
            throw new NotImplementedException();
        }

        void IFinanceStorage.Load()
        {
            throw new NotImplementedException();
        }
    }
}
