using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class Challenge6 : ContentPage
    {
        private const string EmbeddedFilePath = "MauiApp1.Resources.Files.Challenge6Data.txt";

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
            string fileContent = LoadEmbeddedResource(filePath);

            if (string.IsNullOrWhiteSpace(fileContent))
                throw new InvalidOperationException("The file is empty or invalid.");

            byte[] cipherText = Convert.FromBase64String(fileContent);

            int bestKeySize = FindBestKeySize(cipherText);
            return Decrypt(cipherText, bestKeySize);
        }

        private string LoadEmbeddedResource(string filePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(filePath);
            if (stream == null)
                throw new FileNotFoundException("Embedded resource file not found.");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private int HammingDistance(byte[] a, byte[] b)
        {
            return a.Zip(b, (x, y) => x ^ y)
                    .Sum(xor => Convert.ToString(xor, 2).Count(bit => bit == '1'));
        }

        private double NormalizedHammingDistance(byte[] cipherText, int keySize)
        {
            int numBlocks = cipherText.Length / keySize;
            if (numBlocks < 2) return double.MaxValue;

            var distances = Enumerable.Range(0, numBlocks - 1)
                .Select(i =>
                {
                    byte[] block1 = cipherText.Skip(i * keySize).Take(keySize).ToArray();
                    byte[] block2 = cipherText.Skip((i + 1) * keySize).Take(keySize).ToArray();
                    return (double)HammingDistance(block1, block2) / keySize;
                });

            return distances.Average();
        }

        private int FindBestKeySize(byte[] cipherText)
        {
            return Enumerable.Range(2, 39) // Key sizes from 2 to 40
                .Select(size => new { KeySize = size, Distance = NormalizedHammingDistance(cipherText, size) })
                .OrderBy(pair => pair.Distance)
                .First().KeySize;
        }

        private byte FindSingleByteXORKey(byte[] block)
        {
            return Enumerable.Range(0, 256)
                .Select(key => (byte)key)
                .OrderBy(key => ScorePlaintext(block.Select(b => (char)(b ^ key)).ToArray()))
                .First();
        }

        private int ScorePlaintext(char[] text)
        {
            // Frequency analysis weights
            const string frequentChars = "etaoin shrdlu";
            return text.Sum(c => frequentChars.Contains(char.ToLower(c)) ? -1 : 1);
        }

        private byte[][] TransposeBlocks(byte[] cipherText, int keySize)
        {
            return Enumerable.Range(0, keySize)
                .Select(i => cipherText.Where((_, index) => index % keySize == i).ToArray())
                .ToArray();
        }

        private string Decrypt(byte[] cipherText, int keySize)
        {
            byte[][] transposedBlocks = TransposeBlocks(cipherText, keySize);
            byte[] key = transposedBlocks.Select(FindSingleByteXORKey).ToArray();

            return new string(cipherText.Select((b, i) => (char)(b ^ key[i % keySize])).ToArray());
        }
    }
}
