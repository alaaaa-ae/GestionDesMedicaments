using GestionDesMedicaments.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace GestionDesMedicaments
{
    public partial class DashboardClient : Form
    {
        private List<Medicament> medicamentsDisponibles = new List<Medicament>();
        private List<LignePanier> panier = new List<LignePanier>();
        private string clientNom;

        public DashboardClient(string username)
        {
            InitializeComponent();
            clientNom = username;
            lblWelcome.Text = $"Bienvenue, {username} 👋";


        }

        // ============================
        // ⚙️ Événement Load du Form
        // ============================
        private void CommanderForm_Load(object sender, EventArgs e)
        {
            ChargerMedicaments();
            ConfigurerColonnesDataGridViews();
        }

        // ============================
        // 🔄 Rafraîchir la liste
        // ============================
        private void btnRafraichir_Click(object sender, EventArgs e)
        {
            txtRecherche.Text = string.Empty;
            ChargerMedicaments();
        }

        // ============================
        // 🔍 Recherche automatique
        // ============================
        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            string recherche = txtRecherche.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(recherche))
            {
                ChargerMedicaments();
                return;
            }

            var medicamentsFiltres = medicamentsDisponibles
                .Where(m => m.Nom.ToLower().Contains(recherche) ||
                           (m.Description != null && m.Description.ToLower().Contains(recherche)))
                .Select(m => new
                {
                    m.Id,
                    m.Nom,
                    m.Description,
                    Prix_vente = m.PrixVente,
                    m.Stock,
                    QuantiteCommandee = 0
                }).ToList();

            DataGridViewMedicaments.DataSource = medicamentsFiltres;
        }

        // ============================
        // 🔍 Recherche via bouton
        // ============================
        private void btnRechercher_Click(object sender, EventArgs e)
        {
            txtRecherche_TextChanged(sender, e);
        }

        // ============================
        // 📦 Charger les médicaments
        // ============================
        private void ChargerMedicaments()
        {
            try
            {
                medicamentsDisponibles = Medicament.GetAll()
                    .Where(m => m.Stock > 0) // Filtrer seulement les médicaments en stock
                    .ToList();

                DataGridViewMedicaments.DataSource = medicamentsDisponibles.Select(m => new
                {
                    m.Id,
                    m.Nom,
                    m.Description,
                    Prix_vente = m.PrixVente,
                    m.Stock,
                    QuantiteCommandee = 0
                }).ToList();

                DataGridViewMedicaments.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médicaments : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================
        // ⚙️ Configurer les colonnes des DataGridViews
        // ============================
        private void ConfigurerColonnesDataGridViews()
        {
            // Configuration du DataGridView des médicaments
            DataGridViewMedicaments.AutoGenerateColumns = false;
            DataGridViewMedicaments.Columns.Clear();

            var columnsMedicaments = new[]
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
                    Name = "Prix_vente",
                    DataPropertyName = "Prix_vente",
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
                    Name = "QuantiteCommandee",
                    DataPropertyName = "QuantiteCommandee",
                    HeaderText = "Quantité",
                    Width = 80,
                    ReadOnly = false
                }
            };

            DataGridViewMedicaments.Columns.AddRange(columnsMedicaments);

            // Configuration du DataGridView du panier
            DataGridViewPanier.AutoGenerateColumns = false;
            DataGridViewPanier.Columns.Clear();

            var columnsPanier = new[]
            {
                new DataGridViewTextBoxColumn {
                    Name = "MedicamentId",
                    DataPropertyName = "MedicamentId",
                    HeaderText = "ID",
                    Width = 50,
                    ReadOnly = true,
                    Visible = false // Caché mais nécessaire pour les données
                },
                new DataGridViewTextBoxColumn {
                    Name = "NomMedicament",
                    DataPropertyName = "NomMedicament",
                    HeaderText = "Médicament",
                    Width = 200,
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

            DataGridViewPanier.Columns.AddRange(columnsPanier);
        }

        // ============================
        // ➕ Ajouter au panier
        // ============================
        private void btnAjouterPanier_Click(object sender, EventArgs e)
        {
            if (DataGridViewMedicaments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un médicament.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = DataGridViewMedicaments.SelectedRows[0];
            int idMedicament = Convert.ToInt32(row.Cells["Id"].Value);
            var medicament = medicamentsDisponibles.FirstOrDefault(m => m.Id == idMedicament);

            if (medicament == null)
            {
                MessageBox.Show("Médicament introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Quantité souhaitée pour {medicament.Nom} :", "Quantité", "1", -1, -1);

            if (string.IsNullOrEmpty(input)) return; // Annulation

            if (!int.TryParse(input, out int quantite) || quantite <= 0)
            {
                MessageBox.Show("Quantité invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (quantite > medicament.Stock)
            {
                MessageBox.Show($"Stock insuffisant. Disponible : {medicament.Stock}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ajouter ou mettre à jour le panier
            var ligneExistante = panier.FirstOrDefault(p => p.MedicamentId == medicament.Id);
            if (ligneExistante != null)
            {
                ligneExistante.Quantite += quantite;
            }
            else
            {
                panier.Add(new LignePanier
                {
                    MedicamentId = medicament.Id,
                    NomMedicament = medicament.Nom,
                    PrixUnitaire = medicament.PrixVente,
                    Quantite = quantite
                });
            }

            ActualiserPanier();
            MessageBox.Show($"{quantite} x {medicament.Nom} ajouté au panier !", "Succès");
        }

        // ============================
        // 🗑️ Supprimer du panier
        // ============================
        private void btnSupprimerPanier_Click(object sender, EventArgs e)
        {
            if (DataGridViewPanier.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un élément du panier.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Supprimer cet article du panier ?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int id = Convert.ToInt32(DataGridViewPanier.SelectedRows[0].Cells["MedicamentId"].Value);
                panier.RemoveAll(p => p.MedicamentId == id);
                ActualiserPanier();
            }
        }

        // ============================
        // 🧹 Vider le panier
        // ============================
        private void btnViderPanier_Click(object sender, EventArgs e)
        {
            if (!panier.Any())
            {
                MessageBox.Show("Le panier est déjà vide.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Vider tout le panier ?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                panier.Clear();
                ActualiserPanier();
            }
        }

        // ============================
        // ✅ Valider la commande
        // ============================
        private void btnValiderCommande_Click(object sender, EventArgs e)
        {
            if (!panier.Any())
            {
                MessageBox.Show("Votre panier est vide.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vérifier les stocks une dernière fois
            foreach (var item in panier)
            {
                var medicament = medicamentsDisponibles.FirstOrDefault(m => m.Id == item.MedicamentId);
                if (medicament == null || medicament.Stock < item.Quantite)
                {
                    MessageBox.Show($"Stock insuffisant pour {item.NomMedicament}", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                // Ici vous pouvez ajouter la logique de sauvegarde en base
                // Exemple : CreerCommandeEnBase(panier);
                
                MessageBox.Show("Commande validée avec succès !", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panier.Clear();
                ActualiserPanier();
                ChargerMedicaments(); // Recharger pour mettre à jour les stocks
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la validation : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================
        // 🧾 Actualiser l'affichage panier
        // ============================
        private void ActualiserPanier()
        {
            DataGridViewPanier.DataSource = null;
            DataGridViewPanier.DataSource = panier.Select(p => new
            {
                p.MedicamentId,
                p.NomMedicament,
                PrixUnitaire = p.PrixUnitaire,
                p.Quantite,
                Total = p.PrixUnitaire * p.Quantite
            }).ToList();

            lblTotal.Text = $"Total : {panier.Sum(p => p.PrixUnitaire * p.Quantite):0.00} €";
        }

        // ============================
        // 🔄 Double-clic pour ajouter rapidement
        // ============================
        private void DataGridViewMedicaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnAjouterPanier_Click(sender, e);
            }
        }
    }
}