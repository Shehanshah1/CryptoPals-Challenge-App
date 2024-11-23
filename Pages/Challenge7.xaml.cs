using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class Challenge7 : ContentPage
    {
        private const string EmbeddedFilePath = "MauiApp1.Resources.Files.Challenge7Data.txt";
        private const string Key = "YELLOW SUBMARINE";

        public Challenge7()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await App.GoBack();
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await App.NavigateToPage(new Challenge8());
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
            return DecryptAES(cipherText, Key);
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

        private string DecryptAES(byte[] cipherText, string key)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = new byte[16]; // ECB mode does not use IV
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
