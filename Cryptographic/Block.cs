using System.Security.Cryptography;
using System.Text;

namespace CapiCoin
{
    public class Block
    {
        public int Index { get; set; }
        public long Nonce { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block(DateTime timeStamp, string previousHash, List<Transaction> transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            var hash = string.Empty;
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Transactions}-{Nonce}");

            using (var myHash = SHA256Managed.Create())
            {

                var byteArrayResult = myHash.ComputeHash(inputBytes);

                hash = string.Concat(Array.ConvertAll(byteArrayResult, h => h.ToString("X2")));
            }

            return hash;
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                Nonce++;
                Hash = CalculateHash();
            }
        }
    }
}
