using GestionDesMedicaments.Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public partial class GestionClientsForm : Form
    {
        private BindingList<Client> clientsBinding = new BindingList<Client>();
        private int selectedClientId = 0;

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
            dataGridViewClients.SelectionChanged += DataGridViewClients_SelectionChanged;
            btnEnregistrerPaiement.Click += BtnEnregistrerPaiement_Click;
        }

        private void DataGridViewClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count > 0)
            {
                var client = dataGridViewClients.SelectedRows[0].DataBoundItem as Client;
                if (client != null)
                {
                    selectedClientId = client.Id;
                    ChargerFactures(client.Id);
                }
            }
        }

        private void ChargerFactures(int clientId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string commandeUserColumn = Database.GetExistingColumn(conn, "Commande", "id_utilisateur", "id_client");

                    // Vérifier si la table Paiement existe
                    string checkTable = @"SELECT COUNT(*) 
                                         FROM INFORMATION_SCHEMA.TABLES 
                                         WHERE TABLE_NAME = 'Paiement'";
                    SqlCommand cmdCheck = new SqlCommand(checkTable, conn);
                    int tableExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    string query;
                    if (tableExists > 0)
                    {
                        // Si la table Paiement existe, calculer le montant payé et restant
                        query = $@"SELECT 
                                    f.id_facture,
                                    f.id_commande,
                                    f.date_facture,
                                    f.total,
                                    ISNULL(SUM(p.montant), 0) as MontantPaye,
                                    (f.total - ISNULL(SUM(p.montant), 0)) as MontantRestant,
                                    f.statut,
                                    c.date_commande as DateCommande
                                    FROM Facture f
                                    INNER JOIN Commande c ON f.id_commande = c.id_commande
                                    LEFT JOIN Paiement p ON f.id_facture = p.id_facture
                                    WHERE c.{commandeUserColumn} = @ClientId
                                    GROUP BY f.id_facture, f.id_commande, f.date_facture, f.total, f.statut, c.date_commande
                                    ORDER BY f.date_facture DESC";
                    }
                    else
                    {
                        // Si la table Paiement n'existe pas, afficher seulement les factures
                        query = $@"SELECT 
                                    f.id_facture,
                                    f.id_commande,
                                    f.date_facture,
                                    f.total,
                                    0 as MontantPaye,
                                    f.total as MontantRestant,
                                    f.statut,
                                    c.date_commande as DateCommande
                                    FROM Facture f
                                    INNER JOIN Commande c ON f.id_commande = c.id_commande
                                    WHERE c.{commandeUserColumn} = @ClientId
                                    ORDER BY f.date_facture DESC";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@ClientId", clientId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewFactures.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement factures: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEnregistrerPaiement_Click(object sender, EventArgs e)
        {
            if (dataGridViewFactures.SelectedRows.Count == 0)
            {
                MessageBox.Show("Sélectionnez une facture !", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int idFacture = Convert.ToInt32(dataGridViewFactures.SelectedRows[0].Cells["id_facture"].Value);
                decimal montant = decimal.TryParse(txtMontantPaiement.Text, out var m) ? m : 0;
                string modePaiement = comboBoxModePaiement.SelectedItem?.ToString() ?? "Espèces";

                if (montant <= 0)
                {
                    MessageBox.Show("Montant invalide !", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Vérifier si la table Paiement existe
                    string checkTable = @"SELECT COUNT(*) 
                                         FROM INFORMATION_SCHEMA.TABLES 
                                         WHERE TABLE_NAME = 'Paiement'";
                    SqlCommand cmdCheck = new SqlCommand(checkTable, conn);
                    int tableExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (tableExists > 0)
                    {
                        // Insérer le paiement
                        string queryPaiement = @"INSERT INTO Paiement (id_facture, montant, date_paiement, mode_paiement)
                                               VALUES (@id_facture, @montant, GETDATE(), @mode_paiement)";
                        SqlCommand cmdPaiement = new SqlCommand(queryPaiement, conn);
                        cmdPaiement.Parameters.AddWithValue("@id_facture", idFacture);
                        cmdPaiement.Parameters.AddWithValue("@montant", montant);
                        cmdPaiement.Parameters.AddWithValue("@mode_paiement", modePaiement);
                        cmdPaiement.ExecuteNonQuery();

                        // Calculer le total payé APRÈS l'insertion
                        string queryTotalPaye = @"SELECT ISNULL(SUM(montant), 0) 
                                                 FROM Paiement 
                                                 WHERE id_facture = @id_facture";
                        SqlCommand cmdTotal = new SqlCommand(queryTotalPaye, conn);
                        cmdTotal.Parameters.AddWithValue("@id_facture", idFacture);
                        decimal totalPaye = Convert.ToDecimal(cmdTotal.ExecuteScalar());

                        // Récupérer le total de la facture
                        string queryTotalFacture = @"SELECT total FROM Facture WHERE id_facture = @id_facture";
                        SqlCommand cmdTotalFacture = new SqlCommand(queryTotalFacture, conn);
                        cmdTotalFacture.Parameters.AddWithValue("@id_facture", idFacture);
                        decimal totalFacture = Convert.ToDecimal(cmdTotalFacture.ExecuteScalar());
                        decimal montantRestant = totalFacture - totalPaye;

                        // Mettre à jour le statut de la facture
                        string nouveauStatut = totalPaye >= totalFacture ? "Payée" : "Partiellement payée";
                        string queryUpdate = @"UPDATE Facture SET statut = @statut WHERE id_facture = @id_facture";
                        SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@statut", nouveauStatut);
                        cmdUpdate.Parameters.AddWithValue("@id_facture", idFacture);
                        cmdUpdate.ExecuteNonQuery();

                        string message = $"Paiement de {montant:C2} enregistré !\n\n" +
                                       $"Total payé: {totalPaye:C2} / {totalFacture:C2}\n" +
                                       $"Montant restant: {montantRestant:C2}";
                        MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtMontantPaiement.Clear();
                        if (selectedClientId > 0)
                            ChargerFactures(selectedClientId);
                    }
                    else
                    {
                        // Si la table n'existe pas, créer un message informatif
                        MessageBox.Show("La table Paiement n'existe pas dans la base de données.\n" +
                            "Veuillez créer la table avec les colonnes: id_paiement, id_facture, montant, date_paiement, mode_paiement",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur enregistrement paiement: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
