using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Maui.Storage;

namespace MauiApp1;

public partial class Challenge4 : ContentPage
{
    private string defaultFilePath = "MauiApp1.Resources.Files.Challenge4Data.txt";


    // English letter frequencies
    private static readonly Dictionary<char, double> englishFrequencies = new Dictionary<char, double>
    {
        {'a', 0.0651738}, {'b', 0.0124248}, {'c', 0.0217339}, {'d', 0.0349835},
        {'e', 0.1041442}, {'f', 0.0197881}, {'g', 0.0158610}, {'h', 0.0492888},
        {'i', 0.0558094}, {'j', 0.0009033}, {'k', 0.0050529}, {'l', 0.0331490},
        {'m', 0.0202124}, {'n', 0.0564513}, {'o', 0.0596302}, {'p', 0.0137645},
        {'q', 0.0008606}, {'r', 0.0497563}, {'s', 0.0515760}, {'t', 0.0729357},
        {'u', 0.0225134}, {'v', 0.0082903}, {'w', 0.0171272}, {'x', 0.0013692},
        {'y', 0.0145984}, {'z', 0.0007836}, {' ', 0.1918182}
    };

    public Challenge4()
    {
        InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new Challenge5());
    }

 
    private void ProcessFile(string filePath)
    {
        try
        {
            string fileContent;

            // Load content from embedded resource or user-provided file
            if (filePath == defaultFilePath)
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(defaultFilePath))
                using (var reader = new StreamReader(stream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
            else
            {
                fileContent = File.ReadAllText(filePath);
            }

            string solution = FindBestXorDecryption(fileContent);
            SolutionLabel.Text = solution;
        }
        catch (Exception ex)
        {
            SolutionLabel.TextColor = Colors.Red;
            SolutionLabel.Text = "Error: " + ex.Message;
        }
    }

    private string FindBestXorDecryption(string fileContent)
    {
        double bestScore = -1;
        string bestPlaintext = null;
        char bestKey = '\0';

        // Process each hex-encoded line in the file
        foreach (string hexInput in fileContent.Split('\n'))
        {
            if (string.IsNullOrWhiteSpace(hexInput))
                continue;

            var bytes = HexToBytes(hexInput.Trim());

            for (int key = 0; key < 256; ++key)
            {
                string decrypted = XorWithKey(bytes, (char)key);
                double score = ScoreText(decrypted);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestPlaintext = decrypted;
                    bestKey = (char)key;
                }
            }
        }

        return $"Best Key: {bestKey}\nDecrypted Message: {bestPlaintext}";
    }

    private static List<byte> HexToBytes(string hex)
    {
        var bytes = new List<byte>();
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes.Add(Convert.ToByte(hex.Substring(i, 2), 16));
        }
        return bytes;
    }

    private static string XorWithKey(List<byte> bytes, char key)
    {
        var result = new StringBuilder();
        foreach (byte b in bytes)
        {
            result.Append((char)(b ^ key));
        }
        return result.ToString();
    }

    private static double ScoreText(string text)
    {
        double score = 0;
        foreach (char c in text.ToLower())
        {
            if (englishFrequencies.TryGetValue(c, out double freq))
            {
                score += freq;
            }
        }
        return score;
    }

    private void OnSolutionButtonClicked(object sender, EventArgs e)
    {
        ProcessFile(defaultFilePath);
    }
}
