using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments
{
    public partial class GestionMedicamentsForm : Form
    {
        public GestionMedicamentsForm()
        {
            InitializeComponent();
            ChargerFournisseurs();
            ChargerMedicaments();
            ChargerMedicamentsAlertePeremption();
        }

        // 🔹 Charger la ComboBox des fournisseurs
        private void ChargerFournisseurs()
        {
            comboBoxFournisseur.DataSource = Fournisseur.GetAll();
            comboBoxFournisseur.DisplayMember = "Nom";
            comboBoxFournisseur.ValueMember = "Id";
            comboBoxFournisseur.SelectedIndex = -1;
        }

        // 🔹 Charger le DataGridView des médicaments
        private void ChargerMedicaments()
        {
            dataGridViewMedicaments.DataSource = null;
            dataGridViewMedicaments.DataSource = Medicament.GetAll();
        }

        // 🔹 Ajouter un médicament
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || comboBoxFournisseur.SelectedIndex == -1)
            {
                MessageBox.Show("Nom et Fournisseur sont obligatoires !");
                return;
            }

            Medicament med = new Medicament
            {
                Nom = txtNom.Text,
                Description = txtDescription.Text,
                PrixAchat = decimal.TryParse(txtPrixAchat.Text, out var pa) ? pa : 0,
                PrixVente = decimal.TryParse(txtPrixVente.Text, out var pv) ? pv : 0,
                Stock = int.TryParse(txtStock.Text, out var s) ? s : 0,
                SeuilAlerte = int.TryParse(txtSeuilAlerte.Text, out var sa) ? sa : 0,
                IdFournisseur = (int)comboBoxFournisseur.SelectedValue
            };

            if (med.Ajouter())
            {
                MessageBox.Show("Médicament ajouté !");
                ChargerMedicaments();
                ReinitialiserFormulaire();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout !");
            }
        }

        // 🔹 Modifier un médicament
        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (dataGridViewMedicaments.CurrentRow == null)
            {
                MessageBox.Show("Sélectionnez un médicament !");
                return;
            }

            Medicament med = new Medicament
            {
                Id = (int)dataGridViewMedicaments.CurrentRow.Cells["Id"].Value,
                Nom = txtNom.Text,
                Description = txtDescription.Text,
                PrixAchat = decimal.TryParse(txtPrixAchat.Text, out var pa) ? pa : 0,
                PrixVente = decimal.TryParse(txtPrixVente.Text, out var pv) ? pv : 0,
                Stock = int.TryParse(txtStock.Text, out var s) ? s : 0,
                SeuilAlerte = int.TryParse(txtSeuilAlerte.Text, out var sa) ? sa : 0,
                IdFournisseur = (int)comboBoxFournisseur.SelectedValue
            };

            if (med.Modifier())
            {
                MessageBox.Show("Médicament modifié !");
                ChargerMedicaments();
                ReinitialiserFormulaire();
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification !");
            }
        }

        // 🔹 Supprimer un médicament
        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridViewMedicaments.CurrentRow == null)
            {
                MessageBox.Show("Sélectionnez un médicament !");
                return;
            }

            int id = (int)dataGridViewMedicaments.CurrentRow.Cells["Id"].Value;
            if (MessageBox.Show("Confirmer la suppression ?", "Supprimer", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Medicament.Supprimer(id))
                {
                    MessageBox.Show("Médicament supprimé !");
                    ChargerMedicaments();
                    ReinitialiserFormulaire();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression !");
                }
            }
        }

        // 🔹 Double-clic sur une ligne pour remplir le formulaire
        private void dataGridViewMedicaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewMedicaments.Rows[e.RowIndex];
                txtNom.Text = row.Cells["Nom"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value?.ToString();
                txtPrixAchat.Text = row.Cells["PrixAchat"].Value.ToString();
                txtPrixVente.Text = row.Cells["PrixVente"].Value.ToString();
                txtStock.Text = row.Cells["Stock"].Value.ToString();
                txtSeuilAlerte.Text = row.Cells["SeuilAlerte"].Value.ToString();
                comboBoxFournisseur.SelectedValue = row.Cells["IdFournisseur"].Value;
            }
        }

        // 🔹 Rechercher en temps réel
        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            string nom = txtRecherche.Text.Trim().ToLower();
            List<Medicament> filtered = Medicament.GetAll()
                .Where(m => m.Nom.ToLower().Contains(nom))
                .ToList();
            dataGridViewMedicaments.DataSource = filtered;
        }

        // 🔹 Réinitialiser le formulaire
        private void ReinitialiserFormulaire()
        {
            txtNom.Clear();
            txtDescription.Clear();
            txtPrixAchat.Clear();
            txtPrixVente.Clear();
            txtStock.Clear();
            txtSeuilAlerte.Clear();
            comboBoxFournisseur.SelectedIndex = -1;
        }

        // 🔹 Nouveau
        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ReinitialiserFormulaire();
        }

        // 🔹 Retour vers Dashboard
        private void btnRetour_Click(object sender, EventArgs e)
        {
            DashboardPharmacien dashboard = new DashboardPharmacien();
            dashboard.Show();
            this.Close();
        }

        // 🔹 Charger les médicaments en alerte péremption
        private void ChargerMedicamentsAlertePeremption()
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = Classes.Database.GetConnection())
                {
                    conn.Open();
                    // Vérifier si la colonne date_peremption existe
                    string checkColumn = @"SELECT COUNT(*) 
                                          FROM INFORMATION_SCHEMA.COLUMNS 
                                          WHERE TABLE_NAME = 'Medicament' 
                                          AND COLUMN_NAME = 'date_peremption'";
                    System.Data.SqlClient.SqlCommand cmdCheck = new System.Data.SqlClient.SqlCommand(checkColumn, conn);
                    int columnExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (columnExists > 0)
                    {
                        string query = @"SELECT TOP 20 
                                       id_medicament, nom, stock, date_peremption,
                                       DATEDIFF(DAY, GETDATE(), date_peremption) as JoursRestants
                                       FROM Medicament 
                                       WHERE date_peremption IS NOT NULL
                                       AND date_peremption <= DATEADD(DAY, 30, GETDATE())
                                       ORDER BY date_peremption ASC";
                        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, conn);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        da.Fill(dt);
                        dataGridViewAlertePeremption.DataSource = dt;
                    }
                    else
                    {
                        // Si la colonne n'existe pas, créer un DataTable vide avec message
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt.Columns.Add("nom", typeof(string));
                        dt.Columns.Add("JoursRestants", typeof(int));
                        dt.Rows.Add("⚠️ Colonne date_peremption non disponible dans la base", 0);
                        dataGridViewAlertePeremption.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement alertes péremption: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
