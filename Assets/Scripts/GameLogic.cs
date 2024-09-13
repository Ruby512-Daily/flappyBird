using System;
using System.Threading.Tasks;
using Solana.Unity.Programs.Utilities;
using Solana.Unity.Programs;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.SDK;
using Solana.Unity.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolanaWallet
{
    private readonly Wallet _wallet;
    private readonly IRpcClient _rpcClient;


    public SolanaWallet(string privateKey)
    {
        _wallet = new Wallet(privateKey);
        _rpcClient = ClientFactory.GetClient(Cluster.MainNet); // Use MainNet or DevNet as appropriate
    }



    public decimal GetBalance()
    {
        var balanceResult = _rpcClient.GetBalanceAsync(_wallet.Account.PublicKey);
        if (balanceResult.IsCompleted)
        {
            return SolHelper.ConvertToSol(balanceResult.Result.Result.Value);
        }

        throw new Exception("Failed to retrieve balance.");
    }

/*    public TransactionResult SendTransaction(decimal amount, string recipient)
    {
        var transactionBuilder = new TransactionBuilder()
            .SetRecentBlockHash(_rpcClient.GetRecentBlockHash().Result.Value.Blockhash)
            .SetFeePayer(_wallet.Account)
            .AddInstruction(SystemProgram.Transfer(
                _wallet.Account.PublicKey,
                new PublicKey(recipient),
                SolHelper.ConvertToLamports(amount)));

        var transaction = transactionBuilder.Build(_wallet.Account);
        var result = _rpcClient.SendTransaction(transaction);

        return result;
    }*/

    private async Task<decimal> GetOrcasPriceInUSD()
    {
        // Fetch Orcas price in USD from a reliable API
        // Return a mocked value for now
        return 0.5m; // Example value
    }



    
}


public class GameLogic : MonoBehaviour
{
    public Button walletBtn;
    public Text statusText;
    string privateKey = "8XetjVNqCjirMnXdUAFahv3tb5p87EyN9ydEruDQwACq"; // Load securely

    public void Start()
    {
        
        var wallet = new SolanaWallet(privateKey);
        decimal balance = wallet.GetBalance();
        Debug.Log($"Wallet balance: {balance} SOL");
    }
    private void OnEnable()
    {
        Web3.OnLogin += OnLogin;
    }



    private void OnDisable()
    {
        Web3.OnLogin -= OnLogin;
    }


    private void OnLogin(Account account)
    {
        string publicKey = account.PublicKey.ToString();
        string formattedKey = publicKey.Substring(0, 4) + "..." + publicKey.Substring(publicKey.Length - 4);
        walletBtn.GetComponentInChildren<TextMeshProUGUI>().text = formattedKey;
        Debug.Log(account.PublicKey);
    }
}
