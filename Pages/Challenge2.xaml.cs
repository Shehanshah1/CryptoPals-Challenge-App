using System;
using System.Text;

namespace MauiApp1
{
    public partial class Challenge2 : ContentPage
    {
        public Challenge2()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await App.GoBack();
        }

        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await App.NavigateToPage(new Challenge3());
        }

        private void OnSolutionButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Retrieve input strings
                string hexInput1 = EntryBox1.Text?.Trim();
                string hexInput2 = EntryBox2.Text?.Trim();

                // Validate input strings
                if (string.IsNullOrWhiteSpace(hexInput1) || string.IsNullOrWhiteSpace(hexInput2))
                {
                    SolutionLabel.Text = "Error: Both input strings are required.";
                    return;
                }
                if (hexInput1.Length != hexInput2.Length)
                {
                    SolutionLabel.Text = "Error: Input strings must be of equal length.";
                    return;
                }

                // Convert hex strings to binary
                string bin1 = HexToBinary(hexInput1);
                string bin2 = HexToBinary(hexInput2);

                // XOR the two binary strings
                string xorResultBinary = ToXOR(bin1, bin2);

                // Convert XORed binary result back to hex
                string xorResultHex = BinaryToHex(xorResultBinary);

                // Display the result
                SolutionLabel.Text = $"XOR Result: {xorResultHex.ToUpper()}";
            }
            catch (Exception ex)
            {
                SolutionLabel.Text = $"Error: {ex.Message}";
            }
        }

        // Function to convert a hex string to binary string
        private static string HexToBinary(string hex)
        {
            var binary = new StringBuilder();
            foreach (char c in hex)
            {
                int value = Convert.ToInt32(c.ToString(), 16);
                binary.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
            }
            return binary.ToString();
        }

        // Function to convert binary string to hex string
        private static string BinaryToHex(string binary)
        {
            var hex = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 4)
            {
                int value = Convert.ToInt32(binary.Substring(i, 4), 2);
                hex.Append(value.ToString("x"));
            }
            return hex.ToString();
        }

        // Function to XOR two binary strings of equal length
        private static string ToXOR(string bin1, string bin2)
        {
            var xorResult = new StringBuilder();
            for (int i = 0; i < bin1.Length; ++i)
            {
                xorResult.Append(bin1[i] == bin2[i] ? '0' : '1');
            }
            return xorResult.ToString();
        }
    }
}
