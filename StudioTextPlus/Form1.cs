using System;
using System.IO;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace StudioText__
{
    public partial class Form1 : Form
    {
        IKeyboardMouseEvents m_GlobalHook;

        public Form1()
        {
            InitializeComponent();

            KeyPreview = true;
            Focus();

            CheckForIllegalCrossThreadCalls = false;
            Subscribe();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Focus();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                openFileDialog.Title = "Select your counter .txt file";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                }
            }
        }

        private void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        private void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            m_GlobalHook.Dispose();
        }

        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad4)
            {
                if (textBox1.Text.Length > 0)
                {
                    // Read the current number from the file
                    string filePath = textBox1.Text;
                    int number = int.Parse(File.ReadAllText(filePath));

                    // Increment the number and update the text box and file
                    number++;
                    File.WriteAllText(filePath, number.ToString());
                }
                else
                {
                    // Create the Piff folder in Program Files x86 if it doesn't exist
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                    folderPath = Path.Combine(folderPath, "Piff");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Create the StudioTextPlus folder inside the Piff folder if it doesn't exist
                    folderPath = Path.Combine(folderPath, "StudioTextPlus");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Create the 3 text files inside the StudioTextPlus folder
                    for (int i = 1; i <= 3; i++)
                    {
                        string filePath = Path.Combine(folderPath, i.ToString() + ".txt");
                        File.WriteAllText(filePath, "0");
                    }

                    // Repopulate textBox1, textBox2, and textBox3 with the file paths to the 3 text files
                    textBox1.Text = Path.Combine(folderPath, "1.txt");
                    textBox2.Text = Path.Combine(folderPath, "2.txt");
                    textBox3.Text = Path.Combine(folderPath, "3.txt");
                }
            }
            else if (e.KeyCode == Keys.NumPad1)
            {
                // Read the current number from the file
                string filePath = textBox1.Text;
                int number = int.Parse(File.ReadAllText(filePath));

                // Decrease the number and update the text box and file
                number--;
                File.WriteAllText(filePath, number.ToString());
            }

            ////////////////////////////////////////////////

            if (e.KeyCode == Keys.NumPad5)
            {
                // Read the current number from the file
                string filePath2 = textBox2.Text;
                int number = int.Parse(File.ReadAllText(filePath2));

                // Increment the number and update the text box and file
                number++;
                File.WriteAllText(filePath2, number.ToString());
            }
            else if (e.KeyCode == Keys.NumPad2)
            {
                // Read the current number from the file
                string filePath2 = textBox2.Text;
                int number = int.Parse(File.ReadAllText(filePath2));

                // Decrease the number and update the text box and file
                number--;
                File.WriteAllText(filePath2, number.ToString());
            }

            ////////////////////////////////////////////////

            if (e.KeyCode == Keys.NumPad6)
            {
                // Read the current number from the file
                string filePath2 = textBox3.Text;
                int number = int.Parse(File.ReadAllText(filePath2));

                // Increment the number and update the text box and file
                number++;
                File.WriteAllText(filePath2, number.ToString());
            }
            else if (e.KeyCode == Keys.NumPad3)
            {
                // Read the current number from the file
                string filePath2 = textBox3.Text;
                int number = int.Parse(File.ReadAllText(filePath2));

                // Decrease the number and update the text box and file
                number--;
                File.WriteAllText(filePath2, number.ToString());
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Unsubscribe();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unsubscribe();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                openFileDialog.Title = "Select your counter .txt file";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = openFileDialog.FileName;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                openFileDialog.Title = "Select your counter .txt file";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox3.Text = openFileDialog.FileName;
                }
            }
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            // Get the value of the TrackBar control
            int value = trackBar1.Value;

            // Convert the value to a floating-point number between 0 and 1
            float opacity = (float)value / 100;

            // Set the opacity of Form1
            Opacity = opacity;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) 
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ShowInTaskbar = false;
            }
            else
            {
                ShowInTaskbar = true;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose your text files containg the numbers, then use the corresponding numpad keys to actively change the number while in game. This also update the number on OBS.", "StudioText+", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
