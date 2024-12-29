using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace LogMasterAnalyzer
{
    public class MainForm : Form
    {
        private Button btnStart;
        private Button selectFileButton; // Bouton pour sélectionner le fichier
        private TextBox txtInput;
        private Label lblMessage;
        private PictureBox picLogo;
        private PictureBox pictureBox;

        public MainForm()
        {
            InitializeComponent();
            // Définir les propriétés de la fenêtre
            this.Text = "LogMaster Analyzer";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Arial", 10);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void InitializeComponent()
        {
            // Crée un PictureBox pour afficher l'image
            picLogo = new PictureBox();
            picLogo.Image = Image.FromFile("C:/Users/nidha/OneDrive/Bureau/1.png"); // Remplacez avec le chemin de votre image
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.Location = new Point(150, 10);
            picLogo.Size = new Size(100, 100);
            this.Controls.Add(picLogo);

            // Crée un label
            lblMessage = new Label();
            lblMessage.Text = "Veuillez entrer un fichier de log.";
            lblMessage.Location = new Point(10, 130);
            lblMessage.Size = new Size(380, 30);
            lblMessage.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblMessage);

            // Crée un champ de texte pour l'entrée de fichier
            txtInput = new TextBox();
            txtInput.Location = new Point(10, 160);
            txtInput.Size = new Size(250, 30);
            this.Controls.Add(txtInput);

            // Crée un bouton pour démarrer l'analyse
            btnStart = new Button();
            btnStart.Text = "Analyser";
            btnStart.Location = new Point(270, 160);
            btnStart.Size = new Size(100, 30);
            btnStart.BackColor = Color.LightBlue;
            btnStart.ForeColor = Color.White;
            btnStart.Font = new Font("Arial", 10, FontStyle.Bold);
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            // Crée un bouton pour sélectionner un fichier
            selectFileButton = new Button();
            selectFileButton.Text = "Sélectionner un fichier";
            selectFileButton.Location = new Point(20, 200);
            selectFileButton.Size = new Size(150, 30);
            selectFileButton.Click += SelectFileButton_Click;
            this.Controls.Add(selectFileButton);

            // Crée un PictureBox pour afficher l'image sélectionnée
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point(20, 240);
            pictureBox.Size = new Size(760, 500);
            this.Controls.Add(pictureBox);
        }

        // Fonction pour gérer l'événement de clic sur le bouton de sélection de fichier
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            // Crée une instance de OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.Title = "Sélectionner un fichier";

            // Ouvre la boîte de dialogue de sélection de fichier
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Vérifie si le fichier existe avant de tenter de le charger
                if (File.Exists(filePath))
                {
                    try
                    {
                        // Essaye de charger l'image
                        pictureBox.Image = Image.FromFile(filePath);
                        MessageBox.Show("Fichier chargé avec succès!");
                    }
                    catch (Exception ex)
                    {
                        // Si une erreur se produit, affiche un message
                        MessageBox.Show($"Erreur lors du chargement du fichier : {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Le fichier sélectionné n'existe pas.");
                }
            }
        }

        // Fonction pour gérer l'événement de clic sur le bouton d'analyse
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
                // Charge les lignes du fichier et lance l'analyse
                Console.WriteLine("Loading and analyzing the file...");
                string[] lines = File.ReadAllLines(filePath);

                // Utilise LogAnalyzer pour analyser le fichier
                LogAnalyzer logAnalyzer = new LogAnalyzer();
                logAnalyzer.AnalyzeLogs(lines);

                Console.WriteLine("Analysis complete.");

                // Affiche un message dans le label après l'analyse
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
