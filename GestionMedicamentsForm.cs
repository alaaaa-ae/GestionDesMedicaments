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
    public partial class GestionMedicamentsForm : Form
    {
        private List<Fournisseur> fournisseurs;
        private Medicament medicamentEnCours;

        public GestionMedicamentsForm()
        {
            InitializeComponent();
            ChargerFournisseurs();
            ChargerMedicaments();
            ConfigurerDataGridView();
        }

        private void ChargerFournisseurs()
        {
            try
            {
                fournisseurs = Fournisseur.GetAll();
                comboBoxFournisseur.DataSource = fournisseurs;
                comboBoxFournisseur.DisplayMember = "Nom";
                comboBoxFournisseur.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement fournisseurs: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerMedicaments()
        {
            try
            {
                var medicaments = Medicament.GetAll()
                    .Select(m => new
                    {
                        m.Id,
                        m.Nom,
                        m.Description,
                        PrixAchat = m.PrixAchat.ToString("C2"),
                        PrixVente = m.PrixVente.ToString("C2"),
                        m.Stock,
                        m.SeuilAlerte,
                        Fournisseur = fournisseurs.FirstOrDefault(f => f.Id == m.IdFournisseur)?.Nom,
                        Alerte = m.EstEnAlerteStock() ? "⚠️" : "✅"
                    })
                    .ToList();

                dataGridViewMedicaments.DataSource = medicaments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement médicaments: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurerDataGridView()
        {
            dataGridViewMedicaments.AutoGenerateColumns = false;
            dataGridViewMedicaments.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { Name = "Nom", DataPropertyName = "Nom", HeaderText = "Nom", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Description", DataPropertyName = "Description", HeaderText = "Description", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "PrixAchat", DataPropertyName = "PrixAchat", HeaderText = "Prix Achat", Width = 90 },
                new DataGridViewTextBoxColumn { Name = "PrixVente", DataPropertyName = "PrixVente", HeaderText = "Prix Vente", Width = 90 },
                new DataGridViewTextBoxColumn { Name = "Stock", DataPropertyName = "Stock", HeaderText = "Stock", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "SeuilAlerte", DataPropertyName = "SeuilAlerte", HeaderText = "Seuil", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "Fournisseur", DataPropertyName = "Fournisseur", HeaderText = "Fournisseur", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "Alerte", DataPropertyName = "Alerte", HeaderText = "Statut", Width = 60 }
            };

            dataGridViewMedicaments.Columns.AddRange(columns);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (!ValiderFormulaire())
                return;

            try
            {
                var medicament = new Medicament(
                    txtNom.Text.Trim(),
                    txtDescription.Text.Trim(),
                    decimal.Parse(txtPrixAchat.Text),
                    decimal.Parse(txtPrixVente.Text),
                    int.Parse(txtStock.Text),
                    int.Parse(txtSeuilAlerte.Text),
                    (int)comboBoxFournisseur.SelectedValue
                );

                if (medicament.Ajouter())
                {
                    MessageBox.Show("Médicament ajouté avec succès!", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViderFormulaire();
                    ChargerMedicaments();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du médicament", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (medicamentEnCours == null)
            {
                MessageBox.Show("Veuillez sélectionner un médicament à modifier", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValiderFormulaire())
                return;

            try
            {
                medicamentEnCours.Nom = txtNom.Text.Trim();
                medicamentEnCours.Description = txtDescription.Text.Trim();
                medicamentEnCours.PrixAchat = decimal.Parse(txtPrixAchat.Text);
                medicamentEnCours.PrixVente = decimal.Parse(txtPrixVente.Text);
                medicamentEnCours.Stock = int.Parse(txtStock.Text);
                medicamentEnCours.SeuilAlerte = int.Parse(txtSeuilAlerte.Text);
                medicamentEnCours.IdFournisseur = (int)comboBoxFournisseur.SelectedValue;

                if (medicamentEnCours.Modifier())
                {
                    MessageBox.Show("Médicament modifié avec succès!", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViderFormulaire();
                    ChargerMedicaments();
                    medicamentEnCours = null;
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (medicamentEnCours == null)
            {
                MessageBox.Show("Veuillez sélectionner un médicament à supprimer", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Êtes-vous sûr de vouloir supprimer le médicament '{medicamentEnCours.Nom}' ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (Medicament.Supprimer(medicamentEnCours.Id))
                    {
                        MessageBox.Show("Médicament supprimé avec succès!", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ViderFormulaire();
                        ChargerMedicaments();
                        medicamentEnCours = null;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression", "Erreur",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
        {
            ViderFormulaire();
            medicamentEnCours = null;
        }

        private void dataGridViewMedicaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridViewMedicaments.Rows[e.RowIndex].Cells["Id"].Value);
                ChargerMedicamentPourModification(id);
            }
        }

        private void ChargerMedicamentPourModification(int id)
        {
            try
            {
                medicamentEnCours = Medicament.GetById(id);
                if (medicamentEnCours != null)
                {
                    txtNom.Text = medicamentEnCours.Nom;
                    txtDescription.Text = medicamentEnCours.Description;
                    txtPrixAchat.Text = medicamentEnCours.PrixAchat.ToString();
                    txtPrixVente.Text = medicamentEnCours.PrixVente.ToString();
                    txtStock.Text = medicamentEnCours.Stock.ToString();
                    txtSeuilAlerte.Text = medicamentEnCours.SeuilAlerte.ToString();
                    comboBoxFournisseur.SelectedValue = medicamentEnCours.IdFournisseur;

                    groupBoxFormulaire.Text = "Modifier le Médicament (ID: " + id + ")";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement médicament: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValiderFormulaire()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Le nom est obligatoire", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNom.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrixAchat.Text, out decimal prixAchat) || prixAchat <= 0)
            {
                MessageBox.Show("Prix d'achat invalide", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrixAchat.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrixVente.Text, out decimal prixVente) || prixVente <= 0)
            {
                MessageBox.Show("Prix de vente invalide", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrixVente.Focus();
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Stock invalide", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            if (!int.TryParse(txtSeuilAlerte.Text, out int seuil) || seuil < 0)
            {
                MessageBox.Show("Seuil d'alerte invalide", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSeuilAlerte.Focus();
                return false;
            }

            if (comboBoxFournisseur.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ViderFormulaire()
        {
            txtNom.Clear();
            txtDescription.Clear();
            txtPrixAchat.Clear();
            txtPrixVente.Clear();
            txtStock.Clear();
            txtSeuilAlerte.Clear();
            if (comboBoxFournisseur.Items.Count > 0)
                comboBoxFournisseur.SelectedIndex = 0;

            groupBoxFormulaire.Text = "Ajouter un Nouveau Médicament";
        }

        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            string recherche = txtRecherche.Text.Trim().ToLower();

            try
            {
                var medicaments = Medicament.GetAll()
                    .Where(m => m.Nom.ToLower().Contains(recherche) ||
                               (m.Description != null && m.Description.ToLower().Contains(recherche)))
                    .Select(m => new
                    {
                        m.Id,
                        m.Nom,
                        m.Description,
                        PrixAchat = m.PrixAchat.ToString("C2"),
                        PrixVente = m.PrixVente.ToString("C2"),
                        m.Stock,
                        m.SeuilAlerte,
                        Fournisseur = fournisseurs.FirstOrDefault(f => f.Id == m.IdFournisseur)?.Nom,
                        Alerte = m.EstEnAlerteStock() ? "⚠️" : "✅"
                    })
                    .ToList();

                dataGridViewMedicaments.DataSource = medicaments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur recherche: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}