using CapiCoin;
using System.Text.Json;

var startTime = DateTime.Now;
Blockchain CapiCoin = new Blockchain();

CapiCoin.CreateTransaction(new Transaction("Kamil", "Adam", 10));
CapiCoin.ProcessPendingTransactions("Miner1");

CapiCoin.CreateTransaction(new Transaction("Kamil", "Lukasz", 20));
CapiCoin.CreateTransaction(new Transaction("Adam", "Ash", 5));
CapiCoin.ProcessPendingTransactions("Miner2");

var endTime = DateTime.Now;
Console.WriteLine($"Duration: {endTime - startTime}");
var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(CapiCoin, options));