using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Solana.Unity.SDK;
using Solana.Unity.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolanaWallet : MonoBehaviour
{
    public Button walletBtn;
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
