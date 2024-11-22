using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class Challenge3 : ContentPage
    {
        // Frequency of letters in English (normalized values)
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

        public Challenge3()
        {
            InitializeComponent();
        }

        // Convert a hex string to a list of bytes
        public List<byte> HexToBytes(string hex)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                string byteString = hex.Substring(i, 2);
                byte byteValue = Convert.ToByte(byteString, 16);
                bytes.Add(byteValue);
            }
            return bytes;
        }

        // XOR the byte list with a single character key
        public string XorWithKey(List<byte> bytes, char key)
        {
            var result = new StringBuilder();
            foreach (byte b in bytes)
            {
                result.Append((char)(b ^ key));
            }
            return result.ToString();
        }

        // Score the plaintext based on English letter frequencies
        public double ScoreText(string text)
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

        // Click event for the Solution button
        private void OnSolutionButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string hexInput = EntryBox1.Text;
                if (string.IsNullOrEmpty(hexInput))
                {
                    DisplayAlert("Error", "Please enter a hex-encoded string.", "OK");
                    return;
                }

                List<byte> bytes = HexToBytes(hexInput);

                double bestScore = -1;
                string bestPlaintext = null;
                char bestKey = '\0';

                // Try every possible single character (byte) as the XOR key
                for (int key = 0; key < 256; ++key)
                {
                    string decrypted = XorWithKey(bytes, (char)key);
                    double score = ScoreText(decrypted);

                    // Keep track of the highest-scoring (best) result
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestPlaintext = decrypted;
                        bestKey = (char)key;
                    }
                }
                SolutionLabel.Text = "Best Key: " + bestKey + "\n Decrypted Message: " + bestPlaintext;

            }
            catch (Exception ex)
            {
                SolutionLabel.Text = $"Error: {ex.Message}";
            }
        }

        // Back Button Clicked
        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the previous page
            Navigation.PopAsync();
        }

        // Next Button Clicked
        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            // Navigate to the next challenge (if implemented)
            Navigation.PushAsync(new Challenge4());
        }
    }
}
