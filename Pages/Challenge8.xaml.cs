using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MauiApp1;

public partial class Challenge8 : ContentPage
{
    private const string EmbeddedFilePath = "MauiApp1.Resources.Files.Challenge8Data.txt"; // Update with the correct path to the embedded resource

    public Challenge8()
    {
        InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new MainPage());
    }

    private async void OnSolutionButtonClicked(object sender, EventArgs e)
    {
        SolutionLabel.Text = "Processing...";
        try
        {
            string result = await Task.Run(() => ProcessFile(EmbeddedFilePath));
            SolutionLabel.Text = result;
        }
        catch (Exception ex)
        {
            SolutionLabel.Text = $"Error: {ex.Message}";
        }
    }

    private string ProcessFile(string filePath)
    {
        List<string> ciphertexts = LoadEmbeddedResourceLines(filePath);
        if (ciphertexts == null || ciphertexts.Count == 0)
        {
            throw new FileNotFoundException("The file is empty or invalid.");
        }

        int blockSize = 16; // AES block size in bytes
        for (int i = 0; i < ciphertexts.Count; i++)
        {
            byte[] ciphertextBytes = HexToBytes(ciphertexts[i]);
            if (HasRepeatedBlocks(ciphertextBytes, blockSize))
            {
                return $"ECB-encrypted ciphertext found at index {i}\nCiphertext: {ciphertexts[i]}";
            }
        }

        return "No ECB-encrypted ciphertext detected.";
    }

    private List<string> LoadEmbeddedResourceLines(string filePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(filePath);
        if (stream == null)
            throw new FileNotFoundException("Embedded resource file not found.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd()
                     .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                     .ToList();
    }

    private byte[] HexToBytes(string hex)
    {
        int length = hex.Length / 2;
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }

    private bool HasRepeatedBlocks(byte[] ciphertext, int blockSize)
    {
        HashSet<string> uniqueBlocks = new HashSet<string>();
        for (int i = 0; i < ciphertext.Length; i += blockSize)
        {
            byte[] block = ciphertext.Skip(i).Take(blockSize).ToArray();
            string blockString = BitConverter.ToString(block);
            if (uniqueBlocks.Contains(blockString))
            {
                return true; // Repeated block found
            }
            uniqueBlocks.Add(blockString);
        }
        return false;
    }
}
