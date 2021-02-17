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
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain bisquecoin = new Blockchain(2,100);

            Console.WriteLine("Start the Miner.");
            bisquecoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + bisquecoin.GetBalanceOfWallet(wallet1).ToString());

            //bisquecoin.AddBlock(new Block(1, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 50"));
            //bisquecoin.AddBlock(new Block(2, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 200"));

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            bisquecoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the Miner.");
            bisquecoin.MinePendingTransactions(wallet2);

            Console.WriteLine("\nBalance of wallet1 is $" + bisquecoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + bisquecoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(bisquecoin, Formatting.Indented);
            Console.WriteLine(blockJSON);


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
