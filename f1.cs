/*using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogMasterAnalyzer
{
    public partial class f1 : Form
    {
        // Déclare une variable pour l'image
        private PictureBox pictureBox;

        public MainForm()
        {
            InitializeComponent();
            
            // Configuration du formulaire
            this.Text = "LogMaster Analyzer";
            this.Size = new System.Drawing.Size(800, 600);
        }

        private void InitializeComponent()
        {
            // Initialize form components here

            // Crée un bouton pour sélectionner le fichier
            Button selectFileButton = new Button();
            selectFileButton.Text = "Sélectionner une image";
            selectFileButton.Location = new Point(20, 20);
            selectFileButton.Click += SelectFileButton_Click;

            // Ajoute le bouton au formulaire
            this.Controls.Add(selectFileButton);

            // Crée un PictureBox pour afficher l'image
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Location = new Point(20, 60);
            pictureBox.Size = new Size(760, 500);
            
            // Ajoute le PictureBox au formulaire
            this.Controls.Add(pictureBox);
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            // Crée une instance de OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";  // Filtre pour les fichiers image
            openFileDialog.Title = "Sélectionner une image";

            // Ouvre la boîte de dialogue de sélection de fichier
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Charge et affiche l'image dans le PictureBox
                    string filePath = openFileDialog.FileName;
                    pictureBox.Image = Image.FromFile(filePath);
                    MessageBox.Show("Image chargée avec succès!");
                }
                catch (Exception ex)
                {
                    // Affiche un message d'erreur si le fichier ne peut pas être chargé
                    MessageBox.Show($"Erreur lors du chargement de l'image : {ex.Message}");
                }
            }
        }
    }
}*/