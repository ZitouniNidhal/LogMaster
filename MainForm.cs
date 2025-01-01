using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

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
        private ComboBox cmbLogLevel;
        private Button btnFilter;

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
            
    //Create a PictureBox for displaying the logo
        picLogo = new PictureBox();
        picLogo.Image = Image.FromFile("341.png"); // Replace with your image path
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
        // Créer un TextBox pour l'entrée de fichier
        txtInput = new TextBox();
        txtInput.Location = new Point((this.ClientSize.Width - 350) / 2, 170); // Centrer horizontalement
        txtInput.Size = new Size(350, 30);
        txtInput.Font = new Font("Segoe UI", 10);
        txtInput.BackColor = Color.White;
        txtInput.ForeColor = Color.FromArgb(50, 50, 50); // Couleur de texte sombre
        txtInput.BorderStyle = BorderStyle.FixedSingle;
        txtInput.Text = "Entrez le chemin du fichier..."; // Texte d'espace réservé
        txtInput.GotFocus += (sender, e) => { if (txtInput.Text == "Entrez le chemin du fichier...") txtInput.Text = ""; };
        txtInput.LostFocus += (sender, e) => { if (string.IsNullOrEmpty(txtInput.Text)) txtInput.Text = "Entrez le chemin du fichier..."; };
        this.Controls.Add(txtInput);
// Ajouter une bordure arrondie (optionnel, nécessite un contrôle personnalisé ou un PictureBox)
        var roundedBorder = new PictureBox();
        roundedBorder.Size = new Size(352, 32);
        roundedBorder.Location = new Point(txtInput.Location.X - 1, txtInput.Location.Y - 1);
        roundedBorder.BackColor = Color.Transparent;
        roundedBorder.Paint += (sender, e) =>
{
    using (GraphicsPath path = new GraphicsPath())
    {
        int radius = 10; // Rayon des coins arrondis
        Rectangle rect = new Rectangle(0, 0, roundedBorder.Width, roundedBorder.Height);
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();
        e.Graphics.DrawPath(new Pen(Color.FromArgb(200, 200, 200), 2), path); // Couleur de bordure grise
    }
};
this.Controls.Add(roundedBorder);
roundedBorder.BringToFront();
txtInput.BringToFront();
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
            selectFileButton.Size = new Size(150, 30);
            selectFileButton.BackColor = Color.FromArgb(0, 120, 215); // Couleur bleue moderne
            selectFileButton.ForeColor = Color.White;
            selectFileButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            selectFileButton.FlatStyle = FlatStyle.Flat; // Style de bouton plat
            selectFileButton.FlatAppearance.BorderSize = 1; // Taille de la bordure
            selectFileButton.FlatAppearance.BorderColor = Color.White; // Couleur de la bordure

            // Centrer le bouton horizontalement
int centerX = (this.ClientSize.Width - selectFileButton.Width) / 2;
selectFileButton.Location = new Point(centerX, 210);

// Ajouter un gestionnaire d'événements pour dessiner les coins arrondis
selectFileButton.Paint += (sender, e) =>
{
    using (GraphicsPath path = new GraphicsPath())
    {
        int radius = 15; // Rayon des coins arrondis
        Rectangle rect = new Rectangle(0, 0, selectFileButton.Width, selectFileButton.Height);

        // Créer un chemin avec des coins arrondis
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();

        // Appliquer le chemin au bouton
        selectFileButton.Region = new Region(path);
    }
};
           this.Controls.Add(selectFileButton);
            // Create a PictureBox to display the selected image
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point(10, 250);
            pictureBox.Size = new Size(460, 100);
            this.Controls.Add(pictureBox);

            // Create a ComboBox for log level filtering
    cmbLogLevel = new ComboBox();
    cmbLogLevel.Items.AddRange(new string[] { "Info", "Warning", "Error" });
    cmbLogLevel.Location = new Point(10, 210);
    cmbLogLevel.Size = new Size(150, 30);
    cmbLogLevel.DropDownStyle = ComboBoxStyle.DropDownList;
    this.Controls.Add(cmbLogLevel);

    // Create a button to apply the filter
    btnFilter = new Button();
    btnFilter.Text = "Filtrer";
    btnFilter.Location = new Point(170, 210);
    btnFilter.Size = new Size(100, 30);
    btnFilter.BackColor = Color.FromArgb(0, 120, 215);
    btnFilter.ForeColor = Color.White;
    btnFilter.Font = new Font("Segoe UI", 10, FontStyle.Bold);
    btnFilter.FlatStyle = FlatStyle.Flat;
    btnFilter.Click += BtnFilter_Click;
    this.Controls.Add(btnFilter);
        }
private void BtnFilter_Click(object sender, EventArgs e)
{
    string selectedLevel = cmbLogLevel.SelectedItem?.ToString();
    if (string.IsNullOrEmpty(selectedLevel))
    {
        MessageBox.Show("Veuillez sélectionner un niveau de log.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    try
    {
        LogParser logParser = new LogParser("yyyy-MM-dd HH:mm:ss");
        List<string> filteredLogs = logParser.FilterByLevel(new List<string>(File.ReadAllLines(txtInput.Text)), selectedLevel);
        MessageBox.Show($"Logs filtrés par niveau '{selectedLevel}':\n{string.Join("\n", filteredLogs)}", "Résultat", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur lors du filtrage des logs : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
        // Function to handle the click event for the file selection button
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
                    // Lire le contenu du fichier (par exemple, texte, données, etc.)
                    string fileContent = File.ReadAllText(filePath); // Si c'est un fichier texte
                    MessageBox.Show($"Fichier chargé avec succès ! Contenu :\n{fileContent.Substring(0, Math.Min(500, fileContent.Length))}", 
                                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors du traitement du fichier : {ex.Message}", 
                                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show("Veuillez entrer un fichier .", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
