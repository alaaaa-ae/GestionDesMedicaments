using GestionDesMedicaments.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public partial class GestionCommandesForm : Form
    {
        public GestionCommandesForm()
        {
            InitializeComponent();
            ChargerCommandes();
            ConfigurerDataGridViews();
            ChargerFiltres();
        }

        private void ConfigurerDataGridViews()
        {
            // Configuration du DataGridView des commandes
            DataGridViewCommandes.AutoGenerateColumns = false;
            DataGridViewCommandes.Columns.Clear();

            // ✅ CORRECTION : Spécifier explicitement le type DataGridViewColumn[]
            DataGridViewColumn[] columnsCommandes = new DataGridViewColumn[]
            {
        new DataGridViewTextBoxColumn {
            Name = "id_commande",
            DataPropertyName = "id_commande",
            HeaderText = "N° Commande",
            Width = 100,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "Client",
            DataPropertyName = "Client",
            HeaderText = "Client",
            Width = 150,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "date_commande",
            DataPropertyName = "date_commande",
            HeaderText = "Date",
            Width = 120,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "total",
            DataPropertyName = "total",
            HeaderText = "Total",
            Width = 80,
            ReadOnly = true
        },
        new DataGridViewComboBoxColumn {
            Name = "statut",
            DataPropertyName = "statut",
            HeaderText = "Statut",
            Width = 120,
            DataSource = new List<string> { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" }
        }
            };

            DataGridViewCommandes.Columns.AddRange(columnsCommandes);

            // Configuration du DataGridView des détails
            DataGridViewDetails.AutoGenerateColumns = false;
            DataGridViewDetails.Columns.Clear();

            // ✅ CORRECTION : Spécifier explicitement le type DataGridViewColumn[]
            DataGridViewColumn[] columnsDetails = new DataGridViewColumn[]
            {
        new DataGridViewTextBoxColumn {
            Name = "NomMedicament",
            DataPropertyName = "NomMedicament",
            HeaderText = "Médicament",
            Width = 150,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "quantite",
            DataPropertyName = "quantite",
            HeaderText = "Quantité",
            Width = 80,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "prix_unitaire",
            DataPropertyName = "prix_unitaire",
            HeaderText = "Prix Unitaire",
            Width = 100,
            ReadOnly = true
        },
        new DataGridViewTextBoxColumn {
            Name = "SousTotal",
            DataPropertyName = "SousTotal",
            HeaderText = "Sous-Total",
            Width = 100,
            ReadOnly = true
        }
            };

            DataGridViewDetails.Columns.AddRange(columnsDetails);
        }
        private void ChargerFiltres()
        {
            // Charger les statuts dans le ComboBox
            ComboBoxStatut.Items.AddRange(new string[] { "Tous", "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" });
            ComboBoxStatut.SelectedIndex = 0;

            // Charger les dates
            DatePickerDebut.Value = DateTime.Now.AddDays(-30);
            DatePickerFin.Value = DateTime.Now;
        }

        private void ChargerCommandes()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    c.id_commande,
                    cl.nom + ' ' + cl.prenom as Client,
                    c.date_commande,
                    ISNULL(f.total, 0) as total,
                    c.statut,
                    cl.telephone,
                    cl.email
                FROM Commande c
                INNER JOIN Client cl ON c.id_client = cl.id_client
                LEFT JOIN Facture f ON c.id_commande = f.id_commande
                WHERE c.date_commande BETWEEN @DateDebut AND @DateFin
                AND (@Statut = 'Tous' OR c.statut = @Statut)
                ORDER BY c.date_commande DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DateDebut", DatePickerDebut.Value.Date);
                    cmd.Parameters.AddWithValue("@DateFin", DatePickerFin.Value.Date.AddDays(1));

                    // ✅ CORRECTION : Gérer le cas null
                    string statut = ComboBoxStatut.SelectedItem?.ToString() ?? "Tous";
                    cmd.Parameters.AddWithValue("@Statut", statut);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewCommandes.DataSource = dt;

                    // Mettre à jour les statistiques
                    MettreAJourStatistiques(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des commandes: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MettreAJourStatistiques(DataTable commandes)
        {
            int totalCommandes = commandes.Rows.Count;
            decimal chiffreAffaires = commandes.AsEnumerable()
                .Sum(row => Convert.ToDecimal(row["total"]));
            int commandesEnAttente = commandes.AsEnumerable()
                .Count(row => row["statut"].ToString() == "En attente");

            LabelTotalCommandes.Text = totalCommandes.ToString();
            LabelChiffreAffaires.Text = $"{chiffreAffaires:C2}";
            LabelCommandesAttente.Text = commandesEnAttente.ToString();
        }

        private void ChargerDetailsCommande(int idCommande)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            m.nom as NomMedicament,
                            lc.quantite,
                            lc.prix_unitaire,
                            (lc.quantite * lc.prix_unitaire) as SousTotal
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @IdCommande";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewDetails.DataSource = dt;

                    // Afficher les informations de la commande
                    AfficherInfosCommande(idCommande);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des détails: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherInfosCommande(int idCommande)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            c.id_commande,
                            cl.nom + ' ' + cl.prenom as Client,
                            cl.telephone,
                            cl.email,
                            cl.adresse,
                            c.date_commande,
                            c.statut,
                            f.total,
                            f.statut as statut_facture
                        FROM Commande c
                        INNER JOIN Client cl ON c.id_client = cl.id_client
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE c.id_commande = @IdCommande";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            GroupBoxDetails.Text = $"Détails de la commande #{reader["id_commande"]}";
                            LabelClient.Text = reader["Client"].ToString();
                            LabelTelephone.Text = reader["telephone"]?.ToString() ?? "Non renseigné";
                            LabelEmail.Text = reader["email"]?.ToString() ?? "Non renseigné";
                            LabelAdresse.Text = reader["adresse"]?.ToString() ?? "Non renseigné";
                            LabelDate.Text = Convert.ToDateTime(reader["date_commande"]).ToString("dd/MM/yyyy HH:mm");
                            LabelStatut.Text = reader["statut"].ToString();
                            LabelTotal.Text = $"{reader["total"]:C2}";
                            LabelStatutFacture.Text = reader["statut_facture"]?.ToString() ?? "Non générée";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des informations: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFiltrer_Click(object sender, EventArgs e)
        {
            ChargerCommandes();
        }

        private void DataGridViewCommandes_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count > 0)
            {
                int idCommande = Convert.ToInt32(DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value);
                ChargerDetailsCommande(idCommande);
            }
        }

        private void DataGridViewCommandes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == DataGridViewCommandes.Columns["statut"].Index)
            {
                int idCommande = Convert.ToInt32(DataGridViewCommandes.Rows[e.RowIndex].Cells["id_commande"].Value);
                string nouveauStatut = DataGridViewCommandes.Rows[e.RowIndex].Cells["statut"].Value.ToString();

                MettreAJourStatutCommande(idCommande, nouveauStatut);
            }
        }

        private void MettreAJourStatutCommande(int idCommande, string nouveauStatut)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE Commande SET statut = @Statut WHERE id_commande = @IdCommande";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Statut", nouveauStatut);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Statut de la commande #{idCommande} mis à jour: {nouveauStatut}", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Si la commande est livrée, mettre à jour le statut de la facture
                        if (nouveauStatut == "Livrée")
                        {
                            MettreAJourStatutFacture(idCommande, "Payée");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du statut: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MettreAJourStatutFacture(int idCommande, string statutFacture)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE Facture SET statut = @Statut WHERE id_commande = @IdCommande";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Statut", statutFacture);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // La facture peut ne pas exister encore, c'est normal
                Console.WriteLine($"Note: {ex.Message}");
            }
        }

        private void BtnImprimerFacture_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une commande.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idCommande = Convert.ToInt32(DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value);
            GenererFacture(idCommande);
        }

        private void GenererFacture(int idCommande)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Vérifier si une facture existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM Facture WHERE id_commande = @IdCommande";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@IdCommande", idCommande);
                    int factureExists = (int)checkCmd.ExecuteScalar();

                    if (factureExists == 0)
                    {
                        // Créer la facture
                        string query = @"
                            INSERT INTO Facture (id_client, id_commande, date_facture, total, statut)
                            SELECT 
                                c.id_client,
                                c.id_commande,
                                GETDATE(),
                                ISNULL(SUM(lc.quantite * lc.prix_unitaire), 0),
                                'Impayée'
                            FROM Commande c
                            LEFT JOIN LigneCommande lc ON c.id_commande = lc.id_commande
                            WHERE c.id_commande = @IdCommande
                            GROUP BY c.id_client, c.id_commande";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@IdCommande", idCommande);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Facture générée pour la commande #{idCommande}", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Une facture existe déjà pour la commande #{idCommande}", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Recharger les détails pour afficher le nouveau statut
                    if (DataGridViewCommandes.SelectedRows.Count > 0)
                    {
                        ChargerDetailsCommande(idCommande);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la génération de la facture: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRafraichir_Click(object sender, EventArgs e)
        {
            ChargerCommandes();
            MessageBox.Show("Liste des commandes rafraîchie", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fonctionnalité d'export Excel en développement", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}