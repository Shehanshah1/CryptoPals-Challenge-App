using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MauiApp1;

public partial class Challenge5 : ContentPage
{
    public Challenge5()
    {
        InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new Challenge6());
    }

    private void OnSolutionButtonClicked(object sender, EventArgs e)
    {
        try
        {
            // Get the input stanza and the key
            string input = EntryBox1.Text;
            string input1 = EntryBox2.Text;
            string key = "ICE"; // Fixed key for this challenge

            if (string.IsNullOrEmpty(input))
            {
                SolutionLabel.TextColor = Colors.Red;
                SolutionLabel.Text = "Error: Please provide input text.";
                return;
            }

            // Convert input to bytes
            List<byte> plaintextBytes = Encoding.UTF8.GetBytes(input).ToList();
            List<byte> plaintextBytes1 = Encoding.UTF8.GetBytes(input1).ToList();

            // Convert key to bytes
            List<byte> keyBytes = Encoding.UTF8.GetBytes(key).ToList();
            List<byte> keyBytes1 = Encoding.UTF8.GetBytes(key).ToList();

            // Encrypt the input using the repeating-key XOR logic
            List<byte> encryptedBytes = RepeatingKeyXor(plaintextBytes, keyBytes);
            List<byte> encryptedBytes1 = RepeatingKeyXor(plaintextBytes1, keyBytes1);

            // Convert the encrypted bytes to a hex string
            string encryptedHex = BytesToHex(encryptedBytes);
            string encryptedHex1 = BytesToHex(encryptedBytes1);

            // Display the result
            SolutionLabel.TextColor = Colors.Green;
                SolutionLabel.Text = $"Encrypted (hex): {encryptedHex}";

            SolutionLabel1.TextColor = Colors.Green;
            SolutionLabel1.Text = $"Encrypted (hex): {encryptedHex1}";

        }
        catch (Exception ex)
        {
            SolutionLabel.TextColor = Colors.Red;
            SolutionLabel.Text = "Error: " + ex.Message;
        }
    }

    // XOR the plaintext with the repeating key
    private List<byte> RepeatingKeyXor(List<byte> plaintext, List<byte> key)
    {
        List<byte> result = new List<byte>();
        int keyLen = key.Count;

        for (int i = 0; i < plaintext.Count; i++)
        {
            result.Add((byte)(plaintext[i] ^ key[i % keyLen]));
        }

        return result;
    }

    // Convert a list of bytes to a hex string
    private string BytesToHex(List<byte> bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Count * 2);
        foreach (byte b in bytes)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}
