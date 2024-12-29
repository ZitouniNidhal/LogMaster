using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace LogMasterAnalyzer
{
    public class MainForm : Form
    {
        private Button btnStart;
        private Button selectFileButton; // Button to select the file
        private TextBox txtInput;
        private Label lblMessage;
        private PictureBox picLogo;
        private PictureBox pictureBox;

        public MainForm()
        {
            InitializeComponent();
            // Set window properties
            this.Text = "LogMaster Analyzer";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White; // Clean white background
            this.Font = new Font("Segoe UI", 10); // Modern font
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void InitializeComponent()
        {
            // Create a PictureBox for displaying the logo
            picLogo = new PictureBox();
            picLogo.Image = Image.FromFile("C:/Users/nidha/OneDrive/Bureau/1.png"); // Replace with your image path
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.Location = new Point(175, 10);
            picLogo.Size = new Size(150, 100);
            this.Controls.Add(picLogo);

            // Create a label
            lblMessage = new Label();
            lblMessage.Text = "Veuillez entrer un fichier de log.";
            lblMessage.Location = new Point(10, 130);
            lblMessage.Size = new Size(460, 30);
            lblMessage.ForeColor = Color.FromArgb(50, 50, 50); // Dark gray for modern look
            lblMessage.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            this.Controls.Add(lblMessage);

            // Create a text box for file input
            txtInput = new TextBox();
            txtInput.Location = new Point(10, 170);
            txtInput.Size = new Size(350, 30);
            txtInput.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtInput);

            // Create a button to start analysis
            btnStart = new Button();
            btnStart.Text = "Analyser";
            btnStart.Location = new Point(370, 170);
            btnStart.Size = new Size(100, 30);
            btnStart.BackColor = Color.FromArgb(0, 120, 215); // Modern blue color
            btnStart.ForeColor = Color.White;
            btnStart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnStart.FlatStyle = FlatStyle.Flat; // Flat button style
            btnStart.Click += BtnStart_Click; // Ensure this is connected
            this.Controls.Add(btnStart);

            // Create a button to select a file
            selectFileButton = new Button();
            selectFileButton.Text = "Sélectionner un fichier";
            selectFileButton.Location = new Point(10, 210);
            selectFileButton.Size = new Size(150, 30);
            selectFileButton.BackColor = Color.FromArgb(0, 120, 215); // Same modern blue
            selectFileButton.ForeColor = Color.White;
            selectFileButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            selectFileButton.FlatStyle = FlatStyle.Flat; // Flat button style
            selectFileButton.Click += SelectFileButton_Click;
            this.Controls.Add(selectFileButton);

            // Create a PictureBox to display the selected image
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point(10, 250);
            pictureBox.Size = new Size(460, 100);
            this.Controls.Add(pictureBox);
        }

        // Function to handle the click event for the file selection button
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Title = "Sélectionner un fichier";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (File.Exists(filePath))
                {
                    try
                    {
                        pictureBox.Image = Image.FromFile(filePath);
                        MessageBox.Show("Fichier chargé avec succès!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors du chargement du fichier : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Le fichier sélectionné n'existe pas.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Function to handle the click event for the analyze button
        private void BtnStart_Click(object sender, EventArgs e)
        {
            string filePath = txtInput.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Veuillez entrer un fichier de log.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Console.WriteLine("Loading and analyzing the file...");
                string[] lines = File.ReadAllLines(filePath);

                // Check if lines are loaded
                if (lines.Length == 0)
                {
                    MessageBox.Show("Le fichier est vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LogAnalyzer logAnalyzer = new LogAnalyzer();
                logAnalyzer.AnalyzeLogs(filePath);

                Console.WriteLine("Analysis complete.");
                lblMessage.Text = "Analyse terminée avec succès.";
                lblMessage.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'analyse du fichier : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}