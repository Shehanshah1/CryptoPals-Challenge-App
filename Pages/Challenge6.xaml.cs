using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MauiApp1
{
    public partial class Challenge6 : ContentPage
    {
        private string defaultFilePath = "MauiApp1.Resources.Files.Challenge6Data.txt";
        public Challenge6()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await App.GoBack();
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await App.NavigateToPage(new Challenge7());
        }

        private void OnSolutionButtonClicked(string filePath)
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

                string base64Text = File.ReadAllText(filePath);
                byte[] cipherText = Convert.FromBase64String(base64Text);

                // Step 1: Find the best key size
                int bestKeySize = FindBestKeySize(cipherText);

                // Step 2: Decrypt the ciphertext
                string decryptedText = Decrypt(cipherText, bestKeySize);

                // Display the decrypted text (or a summary if too long)
                SolutionLabel.Text = decryptedText.Length > 500
                    ? decryptedText.Substring(0, 500) + "..."
                    : decryptedText;
            }
            catch (Exception ex)
            {
                SolutionLabel.Text = $"Error: {ex.Message}";
            }
        }


        // Step 1: Calculate Hamming distance between two byte arrays
        private int HammingDistance(byte[] a, byte[] b)
        {
            int distance = 0;
            for (int i = 0; i < a.Length; i++)
            {
                byte xor = (byte)(a[i] ^ b[i]);
                while (xor != 0)
                {
                    distance += xor & 1;
                    xor >>= 1;
                }
            }
            return distance;
        }

        // Step 2: Normalize the Hamming distance
        private double NormalizedHammingDistance(byte[] cipherText, int keySize)
        {
            int blocks = cipherText.Length / keySize;
            double totalDistance = 0;

            for (int i = 0; i < blocks - 1; i++)
            {
                byte[] block1 = cipherText.Skip(i * keySize).Take(keySize).ToArray();
                byte[] block2 = cipherText.Skip((i + 1) * keySize).Take(keySize).ToArray();
                totalDistance += HammingDistance(block1, block2);
            }

            return totalDistance / (keySize * (blocks - 1));
        }

        // Step 3: Find the best key size
        private int FindBestKeySize(byte[] cipherText)
        {
            double bestDistance = double.MaxValue;
            int bestKeySize = 0;

            for (int keySize = 2; keySize <= 40; keySize++)
            {
                double distance = NormalizedHammingDistance(cipherText, keySize);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestKeySize = keySize;
                }
            }
            return bestKeySize;
        }

        // Step 4: Transpose blocks of key size
        private byte[][] TransposeBlocks(byte[] cipherText, int keySize)
        {
            int blockCount = cipherText.Length / keySize;
            byte[][] transposedBlocks = new byte[keySize][];

            for (int i = 0; i < keySize; i++)
            {
                transposedBlocks[i] = new byte[blockCount];
                for (int j = 0; j < blockCount; j++)
                {
                    transposedBlocks[i][j] = cipherText[i + j * keySize];
                }
            }
            return transposedBlocks;
        }

        // Step 5: Solve each block as a single-byte XOR problem
        private byte FindSingleByteXORKey(byte[] block)
        {
            int bestScore = int.MaxValue;
            byte bestKey = 0;
            for (byte key = 0; key < 256; key++)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var b in block)
                {
                    sb.Append((char)(b ^ key));
                }

                int score = ScorePlaintext(sb.ToString());
                if (score < bestScore)
                {
                    bestScore = score;
                    bestKey = key;
                }
            }
            return bestKey;
        }

        // Simple scoring function based on character frequency
        private int ScorePlaintext(string text)
        {
            int score = 0;
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \t\n\r";
            foreach (char c in text)
            {
                if (!validChars.Contains(c))
                    score++;
            }
            return score;
        }

        // Decrypt the ciphertext
        private string Decrypt(byte[] cipherText, int keySize)
        {
            byte[][] transposedBlocks = TransposeBlocks(cipherText, keySize);
            byte[] key = new byte[keySize];

            // Find the key for each block
            for (int i = 0; i < keySize; i++)
            {
                key[i] = FindSingleByteXORKey(transposedBlocks[i]);
            }

            // Decrypt the entire ciphertext using the key
            StringBuilder decryptedText = new StringBuilder();
            for (int i = 0; i < cipherText.Length; i++)
            {
                decryptedText.Append((char)(cipherText[i] ^ key[i % keySize]));
            }
            return decryptedText.ToString();
        }
    }
}
