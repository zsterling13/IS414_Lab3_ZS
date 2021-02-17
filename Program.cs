using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EllipticCurve;


namespace IS414_Lab3_ZS
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generate private and public key pairs
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            //Create the blockchain for bisquecoin
            Blockchain bisquecoin = new Blockchain(3,100);

            //Allow Wallet 1 to mine the first blocks
            Console.WriteLine("Start the Miner.");
            bisquecoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + bisquecoin.GetBalanceOfWallet(wallet1).ToString());

            //bisquecoin.AddBlock(new Block(1, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 50"));
            //bisquecoin.AddBlock(new Block(2, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 200"));

            //Create a transaction where wallet 1 gives wallet 2 10 bisquecoins
            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            bisquecoin.addPendingTransaction(tx1);

            //Let Wallet 2 mine the newly created transaction
            Console.WriteLine("Start the Miner.");
            bisquecoin.MinePendingTransactions(wallet2);

            //Print out results
            Console.WriteLine("\nBalance of wallet1 is $" + bisquecoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + bisquecoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(bisquecoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            //bisquecoin.GetLatestBlock().PreviousHash = "12345";

            if (bisquecoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is Valid!!!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid.");
            }
        }
    }

}
