using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments
{
    public partial class CommanderForm : Form
    {
        private List<Medicament> medicamentsDisponibles;
        private List<LignePanier> panier;
        private int clientId;

        public CommanderForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            medicamentsDisponibles = new List<Medicament>();
            panier = new List<LignePanier>();
            ChargerMedicaments();
            ConfigurerDataGridViews();
        }

        private void ConfigurerDataGridViews()
        {
            // Configuration du DataGridView des médicaments
            dataGridViewMedicaments.AutoGenerateColumns = false;
            dataGridViewMedicaments.Columns.Clear();

            // Colonnes pour les médicaments
            var columns = new[]
            {
                new DataGridViewTextBoxColumn {
                    Name = "Id",
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Nom",
                    DataPropertyName = "Nom",
                    HeaderText = "Nom",
                    Width = 150,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Description",
                    DataPropertyName = "Description",
                    HeaderText = "Description",
                    Width = 200,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Prix",
                    DataPropertyName = "PrixVente",
                    HeaderText = "Prix",
                    Width = 80,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Stock",
                    DataPropertyName = "Stock",
                    HeaderText = "Stock",
                    Width = 60,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Quantite",
                    DataPropertyName = "QuantiteCommandee",
                    HeaderText = "Quantité",
                    Width = 80
                }
            };

            dataGridViewMedicaments.Columns.AddRange(columns);

            // Configuration du DataGridView du panier
            dataGridViewPanier.AutoGenerateColumns = false;
            dataGridViewPanier.Columns.Clear();

            var columnsPanier = new[]
            {
                new DataGridViewTextBoxColumn {
                    Name = "Nom",
                    DataPropertyName = "NomMedicament",
                    HeaderText = "Médicament",
                    Width = 150,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "PrixUnitaire",
                    DataPropertyName = "PrixUnitaire",
                    HeaderText = "Prix Unitaire",
                    Width = 100,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Quantite",
                    DataPropertyName = "Quantite",
                    HeaderText = "Quantité",
                    Width = 80,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn {
                    Name = "Total",
                    DataPropertyName = "Total",
                    HeaderText = "Total",
                    Width = 100,
                    ReadOnly = true
                }
            };

            dataGridViewPanier.Columns.AddRange(columnsPanier);
        }

        private void ChargerMedicaments()
        {
            try
            {
                medicamentsDisponibles = Medicament.GetAll()
                    .Where(m => m.Stock > 0)
                    .ToList();

                // Créer une liste avec une propriété supplémentaire pour la quantité commandée
                var medicamentsAvecQuantite = medicamentsDisponibles.Select(m => new MedicamentAvecQuantite
                {
                    Id = m.Id,
                    Nom = m.Nom,
                    Description = m.Description,
                    PrixVente = m.PrixVente,
                    Stock = m.Stock,
                    QuantiteCommandee = 0
                }).ToList();

                dataGridViewMedicaments.DataSource = medicamentsAvecQuantite;
                ActualiserTotalPanier();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des médicaments: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAjouterPanier_Click(object sender, EventArgs e)
        {
            if (dataGridViewMedicaments.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un médicament.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var selectedRow = dataGridViewMedicaments.CurrentRow;
                int idMedicament = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                var medicament = medicamentsDisponibles.FirstOrDefault(m => m.Id == idMedicament);

                if (medicament == null)
                {
                    MessageBox.Show("Médicament non trouvé.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Récupérer la quantité depuis la colonne Quantité
                int quantite = 0;
                var quantiteCell = selectedRow.Cells["Quantite"];
                if (quantiteCell.Value != null && int.TryParse(quantiteCell.Value.ToString(), out int qte))
                {
                    quantite = qte;
                }

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

                // Vérifier si le médicament est déjà dans le panier
                var ligneExistante = panier.FirstOrDefault(p => p.MedicamentId == idMedicament);
                if (ligneExistante != null)
                {
                    // Mettre à jour la quantité
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
                    // Ajouter une nouvelle ligne au panier
                    panier.Add(new LignePanier
                    {
                        MedicamentId = idMedicament,
                        NomMedicament = medicament.Nom,
                        PrixUnitaire = medicament.PrixVente,
                        Quantite = quantite
                    });
                }

                ActualiserPanier();

                // Réinitialiser la quantité dans le grid
                selectedRow.Cells["Quantite"].Value = 0;

                MessageBox.Show($"Médicament ajouté au panier: {medicament.Nom} x{quantite}", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout au panier: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualiserPanier()
        {
            dataGridViewPanier.DataSource = null;
            dataGridViewPanier.DataSource = panier.Select(p => new
            {
                p.NomMedicament,
                PrixUnitaire = p.PrixUnitaire.ToString("C2"),
                p.Quantite,
                Total = (p.PrixUnitaire * p.Quantite).ToString("C2")
            }).ToList();

            ActualiserTotalPanier();
        }

        private void ActualiserTotalPanier()
        {
            decimal total = panier.Sum(p => p.PrixUnitaire * p.Quantite);
            lblTotal.Text = $"Total: {total:C2}";
        }

        private void btnValiderCommande_Click(object sender, EventArgs e)
        {
            if (!panier.Any())
            {
                MessageBox.Show("Votre panier est vide.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Vérifier les stocks une dernière fois
                foreach (var ligne in panier)
                {
                    var medicament = medicamentsDisponibles.FirstOrDefault(m => m.Id == ligne.MedicamentId);
                    if (medicament == null || medicament.Stock < ligne.Quantite)
                    {
                        MessageBox.Show($"Stock insuffisant pour {ligne.NomMedicament}. Commande annulée.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Créer la commande
                int commandeId = CreerCommande();

                // Créer les lignes de commande
                CreerLignesCommande(commandeId);

                // Mettre à jour les stocks
                MettreAJourStocks();

                // Créer la facture
                CreerFacture(commandeId);

                MessageBox.Show("Commande validée avec succès! Une facture a été générée.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Réinitialiser le panier
                panier.Clear();
                ActualiserPanier();
                ChargerMedicaments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la validation de la commande: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CreerCommande()
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Commande (id_client, date_commande, statut) 
                               OUTPUT INSERTED.id_commande 
                               VALUES (@ClientId, GETDATE(), 'Confirmée')";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);

                return (int)command.ExecuteScalar();
            }
        }

        private void CreerLignesCommande(int commandeId)
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();

                foreach (var ligne in panier)
                {
                    string query = @"INSERT INTO LigneCommande (id_commande, id_medicament, quantite, prix_unitaire) 
                                   VALUES (@CommandeId, @MedicamentId, @Quantite, @PrixUnitaire)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CommandeId", commandeId);
                    command.Parameters.AddWithValue("@MedicamentId", ligne.MedicamentId);
                    command.Parameters.AddWithValue("@Quantite", ligne.Quantite);
                    command.Parameters.AddWithValue("@PrixUnitaire", ligne.PrixUnitaire);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void MettreAJourStocks()
        {
            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();

                foreach (var ligne in panier)
                {
                    string query = "UPDATE Medicament SET stock = stock - @Quantite WHERE id_medicament = @MedicamentId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Quantite", ligne.Quantite);
                    command.Parameters.AddWithValue("@MedicamentId", ligne.MedicamentId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreerFacture(int commandeId)
        {
            decimal total = panier.Sum(p => p.PrixUnitaire * p.Quantite);

            using (SqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Facture (id_client, id_commande, date_facture, total, statut) 
                               VALUES (@ClientId, @CommandeId, GETDATE(), @Total, 'Impayée')";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                command.Parameters.AddWithValue("@CommandeId", commandeId);
                command.Parameters.AddWithValue("@Total", total);

                command.ExecuteNonQuery();
            }
        }

        private void CommanderForm_Load(object sender, EventArgs e)
        {
            ChargerMedicaments();
        }

        private void btnSupprimerPanier_Click(object sender, EventArgs e)
        {
            if (dataGridViewPanier.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un article à supprimer.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string nomMedicament = dataGridViewPanier.CurrentRow.Cells["Nom"].Value.ToString();
                panier.RemoveAll(p => p.NomMedicament == nomMedicament);
                ActualiserPanier();

                MessageBox.Show("Article supprimé du panier.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViderPanier_Click(object sender, EventArgs e)
        {
            if (!panier.Any())
            {
                MessageBox.Show("Le panier est déjà vide.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Voulez-vous vraiment vider tout le panier ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                panier.Clear();
                ActualiserPanier();
                MessageBox.Show("Panier vidé.", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            string recherche = txtRecherche.Text.Trim();
            if (string.IsNullOrEmpty(recherche))
            {
                ChargerMedicaments();
                return;
            }

            try
            {
                var medicamentsFiltres = medicamentsDisponibles
                    .Where(m => m.Nom.IndexOf(recherche, StringComparison.OrdinalIgnoreCase) >= 0 ||
                               (m.Description != null && m.Description.IndexOf(recherche, StringComparison.OrdinalIgnoreCase) >= 0))
                    .Select(m => new MedicamentAvecQuantite
                    {
                        Id = m.Id,
                        Nom = m.Nom,
                        Description = m.Description,
                        PrixVente = m.PrixVente,
                        Stock = m.Stock,
                        QuantiteCommandee = 0
                    }).ToList();

                dataGridViewMedicaments.DataSource = medicamentsFiltres;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewMedicaments_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Valider la quantité saisie
            if (e.ColumnIndex == dataGridViewMedicaments.Columns["Quantite"].Index)
            {
                var cell = dataGridViewMedicaments.Rows[e.RowIndex].Cells["Quantite"];
                if (cell.Value != null)
                {
                    if (!int.TryParse(cell.Value.ToString(), out int quantite) || quantite < 0)
                    {
                        MessageBox.Show("La quantité doit être un nombre positif.", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cell.Value = 0;
                    }
                }
            }
        }
    }

    // Classes auxiliaires
    
    public class MedicamentAvecQuantite
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal PrixVente { get; set; }
        public int Stock { get; set; }
        public int QuantiteCommandee { get; set; }
    }
}