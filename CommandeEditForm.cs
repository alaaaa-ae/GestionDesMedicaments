using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments.Clients
{
    public partial class CommandeEditForm : Form
    {
        public enum EditMode { Create, Edit }
        public EditMode Mode { get; set; } = EditMode.Create;
        public int IdCommande { get; set; } = -1;

        private List<LigneCommandeItem> lignesCommande = new List<LigneCommandeItem>();
        private List<Medicament> medicamentsDisponibles = new List<Medicament>();
        private string commandeUserColumn = "id_utilisateur";
        private string factureUserColumn = "id_utilisateur";

        public CommandeEditForm()
        {
            InitializeComponent();
            LoadClients();
            LoadMedicaments();
            ConfigurerDataGridView();
            InitialiserColonnes();
            cbStatut.Items.AddRange(new string[] { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" });
        }

        private class LigneCommandeItem
        {
            public int IdMedicament { get; set; }
            public string NomMedicament { get; set; }
            public int Quantite { get; set; }
            public decimal PrixUnitaire { get; set; }
            public decimal SousTotal => Quantite * PrixUnitaire;
        }

        private void CommandeEditForm_Load(object sender, EventArgs e)
        {
            if (Mode == EditMode.Edit && IdCommande > 0)
                LoadCommande();
            else
            {
                dtpDate.Value = DateTime.Now;
                cbStatut.SelectedIndex = 0;
            }
            ActualiserAffichage();
        }

        private void InitialiserColonnes()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    commandeUserColumn = Database.GetExistingColumn(conn, "Commande", "id_utilisateur", "id_client");
                    factureUserColumn = Database.GetExistingColumn(conn, "Facture", "id_utilisateur", "id_client");
                }
            }
            catch
            {
                commandeUserColumn = "id_utilisateur";
                factureUserColumn = "id_utilisateur";
            }
        }

        private void ConfigurerDataGridView()
        {
            dgvMedicamentsCommande.AutoGenerateColumns = false;
            dgvMedicamentsCommande.Columns.Clear();

            dgvMedicamentsCommande.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "NomMedicament", DataPropertyName = "NomMedicament", HeaderText = "Médicament", Width = 250, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "Quantite", DataPropertyName = "Quantite", HeaderText = "Quantité", Width = 80, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "PrixUnitaire", DataPropertyName = "PrixUnitaire", HeaderText = "Prix Unitaire", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } },
                new DataGridViewTextBoxColumn { Name = "SousTotal", DataPropertyName = "SousTotal", HeaderText = "Sous-Total", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } }
            });
        }

        private void LoadMedicaments()
        {
            cbMedicaments.Items.Clear();
            try
            {
                medicamentsDisponibles = Medicament.GetAll()
                    .Where(m => m.Stock > 0)
                    .OrderBy(m => m.Nom)
                    .ToList();

                foreach (var med in medicamentsDisponibles)
                {
                    cbMedicaments.Items.Add(new ComboboxItem
                    {
                        Text = $"{med.Nom} - Stock: {med.Stock} - Prix: {med.PrixVente:C2}",
                        Value = med
                    });
                }

                if (cbMedicaments.Items.Count > 0)
                    cbMedicaments.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------
        // Implémentation des méthodes manquantes
        // -----------------------------
        private void LoadClients()
        {
            cbClients.Items.Clear();
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_utilisateur, nom + ' ' + prenom as NomComplet FROM Utilisateur WHERE role='client' ORDER BY nom, prenom", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cbClients.Items.Add(new ComboboxItem
                        {
                            Text = reader["NomComplet"].ToString(),
                            Value = reader["id_utilisateur"]
                        });
                    }
                    if (cbClients.Items.Count > 0)
                        cbClients.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des clients : " + ex.Message);
            }
        }

        private void LoadCommande()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT id_commande, {commandeUserColumn} as ClientId, date_commande, statut FROM Commande WHERE id_commande=@id", conn);
                    cmd.Parameters.AddWithValue("@id", IdCommande);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dtpDate.Value = Convert.ToDateTime(reader["date_commande"]);

                        string statut = reader["statut"].ToString();
                        int index = cbStatut.Items.IndexOf(statut);
                        if (index >= 0)
                            cbStatut.SelectedIndex = index;
                        else
                            cbStatut.SelectedIndex = 0;

                        int clientId = Convert.ToInt32(reader["ClientId"]);
                        for (int i = 0; i < cbClients.Items.Count; i++)
                        {
                            if (((ComboboxItem)cbClients.Items[i]).Value.Equals(clientId))
                            {
                                cbClients.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    reader.Close();

                    // Charger les lignes de commande
                    lignesCommande.Clear();
                    SqlCommand cmdLignes = new SqlCommand(@"
                        SELECT 
                            lc.id_medicament,
                            m.nom as NomMedicament,
                            lc.quantite,
                            COALESCE(lc.prix_unitaire, m.prix_vente) as prix_unitaire
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @id", conn);
                    cmdLignes.Parameters.AddWithValue("@id", IdCommande);

                    SqlDataReader readerLignes = cmdLignes.ExecuteReader();
                    while (readerLignes.Read())
                    {
                        lignesCommande.Add(new LigneCommandeItem
                        {
                            IdMedicament = Convert.ToInt32(readerLignes["id_medicament"]),
                            NomMedicament = readerLignes["NomMedicament"].ToString(),
                            Quantite = Convert.ToInt32(readerLignes["quantite"]),
                            PrixUnitaire = Convert.ToDecimal(readerLignes["prix_unitaire"])
                        });
                    }
                }
                ActualiserAffichage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de la commande : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAjouterMedicament_Click(object sender, EventArgs e)
        {
            if (cbMedicaments.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un médicament.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var item = (ComboboxItem)cbMedicaments.SelectedItem;
            var medicament = (Medicament)item.Value;
            int quantite = (int)nudQuantite.Value;

            if (quantite <= 0)
            {
                MessageBox.Show("La quantité doit être supérieure à 0.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantite > medicament.Stock)
            {
                MessageBox.Show($"Stock insuffisant. Stock disponible: {medicament.Stock}", "Stock insuffisant",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier si le médicament est déjà dans la commande
            var ligneExistante = lignesCommande.FirstOrDefault(l => l.IdMedicament == medicament.Id);
            if (ligneExistante != null)
            {
                if (ligneExistante.Quantite + quantite > medicament.Stock)
                {
                    MessageBox.Show($"Quantité totale dépasse le stock disponible. Stock restant: {medicament.Stock - ligneExistante.Quantite}",
                        "Stock insuffisant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ligneExistante.Quantite += quantite;
            }
            else
            {
                lignesCommande.Add(new LigneCommandeItem
                {
                    IdMedicament = medicament.Id,
                    NomMedicament = medicament.Nom,
                    Quantite = quantite,
                    PrixUnitaire = medicament.PrixVente
                });
            }

            ActualiserAffichage();
            nudQuantite.Value = 1;
        }

        private void BtnSupprimerMedicament_Click(object sender, EventArgs e)
        {
            if (dgvMedicamentsCommande.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un médicament à supprimer.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvMedicamentsCommande.SelectedRows[0];
            string nomMedicament = selectedRow.Cells["NomMedicament"].Value.ToString();
            lignesCommande.RemoveAll(l => l.NomMedicament == nomMedicament);
            ActualiserAffichage();
        }

        private void ActualiserAffichage()
        {
            dgvMedicamentsCommande.DataSource = null;
            dgvMedicamentsCommande.DataSource = lignesCommande;

            decimal total = lignesCommande.Sum(l => l.SousTotal);
            lblTotalValue.Text = total.ToString("C2");
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (cbClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbStatut.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un statut.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lignesCommande.Count == 0)
            {
                MessageBox.Show("Veuillez ajouter au moins un médicament à la commande.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        SqlCommand cmd;
                        int commandeId = IdCommande;
                        string ancienStatut = "";
                        string nouveauStatut = cbStatut.SelectedItem.ToString();

                        // Si modification, récupérer l'ancien statut et restituer le stock si nécessaire
                        if (Mode == EditMode.Edit)
                        {
                            // Récupérer l'ancien statut
                            SqlCommand cmdOldStatus = new SqlCommand("SELECT statut FROM Commande WHERE id_commande=@id", conn, transaction);
                            cmdOldStatus.Parameters.AddWithValue("@id", IdCommande);
                            object oldStatusObj = cmdOldStatus.ExecuteScalar();
                            ancienStatut = oldStatusObj?.ToString() ?? "";

                            // Si la commande était confirmée, restituer le stock des anciennes lignes
                            if (ancienStatut == "Confirmée")
                            {
                                SqlCommand cmdOldLines = new SqlCommand(
                                    "SELECT id_medicament, quantite FROM LigneCommande WHERE id_commande=@id", 
                                    conn, transaction);
                                cmdOldLines.Parameters.AddWithValue("@id", IdCommande);
                                using (SqlDataReader reader = cmdOldLines.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int idMedicament = Convert.ToInt32(reader["id_medicament"]);
                                        int quantite = Convert.ToInt32(reader["quantite"]);
                                        
                                        // Restituer le stock
                                        SqlCommand cmdRestore = new SqlCommand(
                                            "UPDATE Medicament SET stock = stock + @qte WHERE id_medicament = @id", 
                                            conn, transaction);
                                        cmdRestore.Parameters.AddWithValue("@qte", quantite);
                                        cmdRestore.Parameters.AddWithValue("@id", idMedicament);
                                        cmdRestore.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Supprimer les anciennes lignes de commande
                            SqlCommand cmdDelete = new SqlCommand("DELETE FROM LigneCommande WHERE id_commande=@id", conn, transaction);
                            cmdDelete.Parameters.AddWithValue("@id", commandeId);
                            cmdDelete.ExecuteNonQuery();
                        }

                        string insertSql = $"INSERT INTO Commande ({commandeUserColumn}, date_commande, statut) OUTPUT INSERTED.id_commande VALUES (@client, @date, @statut)";
                        string updateSql = $"UPDATE Commande SET {commandeUserColumn}=@client, date_commande=@date, statut=@statut WHERE id_commande=@id";

                        if (Mode == EditMode.Create)
                        {
                            cmd = new SqlCommand(insertSql, conn, transaction);
                            cmd.Parameters.AddWithValue("@client", ((ComboboxItem)cbClients.SelectedItem).Value);
                            cmd.Parameters.AddWithValue("@date", dtpDate.Value);
                            cmd.Parameters.AddWithValue("@statut", nouveauStatut);
                            commandeId = (int)cmd.ExecuteScalar();
                        }
                        else
                        {
                            cmd = new SqlCommand(updateSql, conn, transaction);
                            cmd.Parameters.AddWithValue("@id", IdCommande);
                            cmd.Parameters.AddWithValue("@client", ((ComboboxItem)cbClients.SelectedItem).Value);
                            cmd.Parameters.AddWithValue("@date", dtpDate.Value);
                            cmd.Parameters.AddWithValue("@statut", nouveauStatut);
                            cmd.ExecuteNonQuery();
                            commandeId = IdCommande;
                        }

                        // Insérer les nouvelles lignes de commande
                        foreach (var ligne in lignesCommande)
                        {
                            SqlCommand cmdLigne = new SqlCommand(
                                "INSERT INTO LigneCommande (id_commande, id_medicament, quantite, prix_unitaire) VALUES (@id_commande, @id_medicament, @quantite, @prix_unitaire)",
                                conn, transaction);
                            cmdLigne.Parameters.AddWithValue("@id_commande", commandeId);
                            cmdLigne.Parameters.AddWithValue("@id_medicament", ligne.IdMedicament);
                            cmdLigne.Parameters.AddWithValue("@quantite", ligne.Quantite);
                            cmdLigne.Parameters.AddWithValue("@prix_unitaire", ligne.PrixUnitaire);
                            cmdLigne.ExecuteNonQuery();
                        }

                        // Si le statut est "Confirmée", diminuer le stock
                        if (nouveauStatut == "Confirmée")
                        {
                            foreach (var ligne in lignesCommande)
                            {
                                // Vérifier le stock disponible
                                SqlCommand cmdCheckStock = new SqlCommand(
                                    "SELECT stock FROM Medicament WHERE id_medicament = @id", 
                                    conn, transaction);
                                cmdCheckStock.Parameters.AddWithValue("@id", ligne.IdMedicament);
                                object stockObj = cmdCheckStock.ExecuteScalar();
                                
                                if (stockObj != null && stockObj != DBNull.Value)
                                {
                                    int stockActuel = Convert.ToInt32(stockObj);
                                    if (stockActuel < ligne.Quantite)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show(
                                            $"Stock insuffisant pour {ligne.NomMedicament}. Stock disponible: {stockActuel}, Quantité demandée: {ligne.Quantite}",
                                            "Erreur Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    // Diminuer le stock
                                    SqlCommand cmdUpdateStock = new SqlCommand(
                                        "UPDATE Medicament SET stock = stock - @qte WHERE id_medicament = @id", 
                                        conn, transaction);
                                    cmdUpdateStock.Parameters.AddWithValue("@qte", ligne.Quantite);
                                    cmdUpdateStock.Parameters.AddWithValue("@id", ligne.IdMedicament);
                                    cmdUpdateStock.ExecuteNonQuery();
                                }
                            }
                        }

                        // Mettre à jour ou créer la facture
                        decimal total = lignesCommande.Sum(l => l.SousTotal);
                        int idUtilisateur = Convert.ToInt32(((ComboboxItem)cbClients.SelectedItem).Value);
                        SqlCommand cmdFacture = new SqlCommand($@"
                            IF EXISTS (SELECT 1 FROM Facture WHERE id_commande = @id_commande)
                                UPDATE Facture SET total = @total WHERE id_commande = @id_commande
                            ELSE
                                INSERT INTO Facture ({factureUserColumn}, id_commande, date_facture, total, statut) 
                                VALUES (@userId, @id_commande, GETDATE(), @total, 'Impayée')",
                            conn, transaction);
                        cmdFacture.Parameters.AddWithValue("@id_commande", commandeId);
                        cmdFacture.Parameters.AddWithValue("@userId", idUtilisateur);
                        cmdFacture.Parameters.AddWithValue("@total", total);
                        cmdFacture.ExecuteNonQuery();

                        transaction.Commit();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Classe utilitaire pour stocker la valeur et le texte dans ComboBox
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public override string ToString() { return Text; }
    }
}