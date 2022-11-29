namespace CapiCoin
{
    public class Blockchain
    {
        public IList<Block> Chain { get; set; }
        public int Difficulty { get; set; } = 4;
        public int Reward { get; set; } = 10;
        List<Transaction> PendingTransactions = new List<Transaction>();

        public Blockchain()
        {
            InitializedChain();
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, null);
        }

        public void InitializedChain()
        {
            Chain = new List<Block>();
        }

        public Block GetLatestBlock()
        {
            return Chain.LastOrDefault();
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            CreateTransaction(new Transaction(null, minerAddress, Reward));
            Block block = new Block(DateTime.Now, GetLatestBlock()?.Hash ?? "", PendingTransactions);
            AddBlock(block);

            PendingTransactions = new List<Transaction>();
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = Chain.LastOrDefault();
            block.Index = latestBlock?.Index + 1 ?? 0;
            block.TimeStamp = DateTime.Now;
            block.PreviousHash = latestBlock?.Hash ?? string.Empty;
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
