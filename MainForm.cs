using System;
using System.Drawing;
using System.Windows.Forms;

namespace LogMaster
{
    public class MainForm : Form
    {
        private Button btnStart;
        private TextBox txtInput;
        private Label lblMessage;
        private PictureBox picLogo;

        public MainForm()
        {
            // Définir les propriétés de la fenêtre
            this.Text = "LogMaster Analyzer";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Arial", 10);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Ajouter un logo ou une image
            picLogo = new PictureBox();
            picLogo.Image = Image.FromFile("path_to_logo.png"); // Remplacez avec le chemin de votre image
            picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            picLogo.Location = new Point(150, 10);
            picLogo.Size = new Size(100, 100);
            this.Controls.Add(picLogo);

            // Ajouter un label
            lblMessage = new Label();
            lblMessage.Text = "Veuillez entrer un fichier de log.";
            lblMessage.Location = new Point(10, 130);
            lblMessage.Size = new Size(380, 30);
            lblMessage.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblMessage);

            // Ajouter un champ de texte pour l'entrée de fichier
            txtInput = new TextBox();
            txtInput.Location = new Point(10, 160);
            txtInput.Size = new Size(250, 30);
            this.Controls.Add(txtInput);

            // Ajouter un bouton pour démarrer l'analyse
            btnStart = new Button();
            btnStart.Text = "Analyser";
            btnStart.Location = new Point(270, 160);
            btnStart.Size = new Size(100, 30);
            btnStart.BackColor = Color.LightBlue;
            btnStart.ForeColor = Color.White;
            btnStart.Font = new Font("Arial", 10, FontStyle.Bold);
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);
        }

        // Fonction pour gérer l'événement de clic sur le bouton
        private void BtnStart_Click(object sender, EventArgs e)
        {
            string filePath = txtInput.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Veuillez entrer un fichier de log.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Simulation d'un processus d'analyse (ici vous pouvez ajouter votre logique)
            lblMessage.Text = "Analyse en cours...";
            lblMessage.ForeColor = Color.DarkGreen;
            // Ajoutez ici votre logique d'analyse
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
