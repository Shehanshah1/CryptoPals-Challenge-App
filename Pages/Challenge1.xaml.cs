using System;
using System.Text; // Add this namespace for StringBuilder
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class Challenge1 : ContentPage
    {
        // Base64 characters table
        private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public Challenge1()
        {
            InitializeComponent();
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await App.GoBack();
        }
        private async void OnNextButtonClicked(object sender, EventArgs e)
        {
            await App.NavigateToPage(new Challenge2());
        }

        // Function to convert a hex string to a binary string
        public static string HexToBinary(string hex)
        {
            var binary = new StringBuilder();

            // Convert each hex digit to a 4-bit binary string
            foreach (char c in hex)
            {
                int value = Convert.ToInt32(c.ToString(), 16);
                binary.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
            }

            return binary.ToString();
        }

        // Function to convert a binary string to a Base64 string
        public static string BinaryToBase64(string binary)
        {
            var base64 = new StringBuilder();
            int i = 0;

            // Process 6-bit chunks of the binary string
            while (i + 6 <= binary.Length)
            {
                int value = Convert.ToInt32(binary.Substring(i, 6), 2);
                base64.Append(base64Chars[value]);
                i += 6;
            }

            // Handle remaining bits
            int remainingBits = binary.Length - i;
            if (remainingBits > 0)
            {
                int value = Convert.ToInt32(binary.Substring(i, remainingBits).PadRight(6, '0'), 2);
                base64.Append(base64Chars[value]);

                // Add padding
                base64.Append(new string('=', (6 - remainingBits) / 2));
            }

            return base64.ToString();
        }

        // Function to convert hex to Base64
        public static string HexToBase64(string hex)
        {
            string binary = HexToBinary(hex);
            return BinaryToBase64(binary);
        }

        // Solve button logic
        private void OnSolveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                
                var hexInput = ((Entry)this.FindByName("EntryBox")).Text;

                
                var base64Output = HexToBase64(hexInput);

                
                AnswerLabel.Text = base64Output;
            }
            catch (Exception ex)
            {
                // Handle potential exceptions and display an error
                AnswerLabel.Text = $"Error: {ex.Message}";
            }
        }
    }
}
