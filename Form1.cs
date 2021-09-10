using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Reads the user's file.
            using (OpenFileDialog userFile = new OpenFileDialog()
            {
                Filter = "Text Documents|*.txt", // Looks for txt documents only.
                ValidateNames = true, // Validates the file names.
                Multiselect = false // Only one file may be selected at a time.
            })
                if (userFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader stread = new StreamReader(userFile.FileName))
                    {
                        textBox1.Text = await stread.ReadToEndAsync(); // Converts from the text box.
                    }
                }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Saves an output file.
            using (SaveFileDialog outputFile = new SaveFileDialog() {
                // Looks for txt documents only and validates the file names.
                Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                // Section for splitting the input file into a string array.
                String reading = textBox1.Text;
                string[] divide = reading.Split(' ');
                int y = divide.Length;

                // Alphabetically sorts the words for later.
                Array.Sort(divide);

                // Section for finding the longest word.
                string term = "";
                int count = 0;
                foreach (String str in divide) // For loop to sort by length.
                {
                    if (str.Length > count)
                    {
                        term = str;
                        count = str.Length;
                    }
                }

                // Y is not included as a potential vowel here for the sake of simplicity.
                char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
                string wordVowels = "";
                var maximumVowels = 0;

                for (int x = 0; x < divide.Length; x++)
                {
                    var division = divide[x];
                    var vowelCount = 0;
                    // Section to make the current word with the most vowels take the maximumVowels slot.
                    foreach (var vowel in vowels)
                    {
                        if (division.Contains(vowel)) vowelCount++;
                    }

                    if (maximumVowels < vowelCount)
                    {
                        maximumVowels = x;
                        wordVowels = division;
                    }
                }

                // Outputs everything to a user's file.
                if (outputFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter stwriter = new StreamWriter(outputFile.FileName))
                    {
                        await stwriter.WriteLineAsync(reading.ToLower() + Environment.NewLine
                            + "First word alphabetically: " + divide.First().ToLower() + Environment.NewLine + "Last word alphabetically: " + divide.Last().ToLower() + Environment.NewLine
                            + "Longest word: " + term.ToLower() + Environment.NewLine
                            + "Most vowels: " + wordVowels.ToLower());
                        MessageBox.Show("You have successfully saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
