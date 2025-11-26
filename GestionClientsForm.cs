using GestionDesMedicaments.Classes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public partial class GestionClientsForm : Form
    {
        private BindingList<Client> clientsBinding = new BindingList<Client>();

        public GestionClientsForm()
        {
            InitializeComponent();
            LoadClients();
            HookEvents();
        }

        private void HookEvents()
        {
            btnAjouter.Click += BtnAjouter_Click;
            btnModifier.Click += BtnModifier_Click;
            btnSupprimer.Click += BtnSupprimer_Click;
            btnNouveau.Click += BtnNouveau_Click;
            btnRetour.Click += BtnRetour_Click;
            txtRecherche.TextChanged += TxtRecherche_TextChanged;
            dataGridViewClients.CellDoubleClick += DataGridViewClients_CellDoubleClick;
        }

        private void LoadClients(string filter = "")
        {
            clientsBinding = new BindingList<Client>(Client.GetAll(filter));
            dataGridViewClients.DataSource = clientsBinding;
            if (dataGridViewClients.Columns.Contains("Id")) dataGridViewClients.Columns["Id"].Visible = false;
            if (dataGridViewClients.Columns.Contains("MotDePasse")) dataGridViewClients.Columns["MotDePasse"].Visible = false;
        }

        private void TxtRecherche_TextChanged(object sender, EventArgs e)
        {
            LoadClients(txtRecherche.Text.Trim());
        }

        private void DataGridViewClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var client = dataGridViewClients.Rows[e.RowIndex].DataBoundItem as Client;
            if (client != null) LoadClientToForm(client);
        }

        private void LoadClientToForm(Client c)
        {
            txtHiddenId.Text = c.Id.ToString();
            txtNomUtilisateur.Text = c.NomUtilisateur;
            txtMotDePasse.Text = c.MotDePasse;
            txtNom.Text = c.Nom;
            txtPrenom.Text = c.Prenom;
            txtAdresse.Text = c.Adresse;
            txtTelephone.Text = c.Telephone;
            txtEmail.Text = c.Email;
        }

        private void ClearForm()
        {
            txtHiddenId.Text = "";
            txtNomUtilisateur.Text = "";
            txtMotDePasse.Text = "";
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtAdresse.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            var c = new Client
            {
                NomUtilisateur = txtNomUtilisateur.Text.Trim(),
                MotDePasse = txtMotDePasse.Text.Trim(),
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Adresse = txtAdresse.Text.Trim(),
                Telephone = txtTelephone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (c.Ajouter())
            {
                MessageBox.Show("Client ajouté !");
                LoadClients();
                ClearForm();
            }
            else MessageBox.Show("Erreur lors de l'ajout !");
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHiddenId.Text, out int id)) { MessageBox.Show("Sélectionnez un client !"); return; }
            var c = new Client
            {
                Id = id,
                NomUtilisateur = txtNomUtilisateur.Text.Trim(),
                MotDePasse = txtMotDePasse.Text.Trim(),
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Adresse = txtAdresse.Text.Trim(),
                Telephone = txtTelephone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (c.Modifier())
            {
                MessageBox.Show("Client modifié !");
                LoadClients();
                ClearForm();
            }
            else MessageBox.Show("Erreur lors de la modification !");
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHiddenId.Text, out int id)) { MessageBox.Show("Sélectionnez un client !"); return; }
            if (MessageBox.Show("Voulez-vous supprimer ce client ?", "Confirmer", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Client.Supprimer(id))
                {
                    MessageBox.Show("Client supprimé !");
                    LoadClients();
                    ClearForm();
                }
                else MessageBox.Show("Erreur lors de la suppression !");
            }
        }

        private void BtnNouveau_Click(object sender, EventArgs e) => ClearForm();

        private void BtnRetour_Click(object sender, EventArgs e)
        {
            try
            {
                // Cherche une instance existante du DashboardPharmacien
                var dashboard = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.GetType().Name == "DashboardPharmacien");

                if (dashboard != null)
                {
                    dashboard.Show();
                    dashboard.BringToFront();
                }
                else
                {
                    // Si aucune instance n'existe, créer une nouvelle instance
                    dashboard = new DashboardPharmacien();
                    dashboard.Show();
                }

                // Ferme la fenêtre actuelle GestionClientsForm
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ouvrir le Dashboard :\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
