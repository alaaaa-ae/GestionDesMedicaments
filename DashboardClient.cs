using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;
using Microsoft.VisualBasic;
using System.ComponentModel;


namespace GestionDesMedicaments
{
    public partial class DashboardClient : Form
    {
        // Données en mémoire
        private List<Medicament> medicamentsDisponibles = new List<Medicament>();
        private List<LignePanier> panier = new List<LignePanier>();
        private string clientNom;

        // <-- ADAPTE TA CHAÎNE DE CONNEXION ICI -->
        private string connectionString = @"Server=.;Database=PharmacieDB;Integrated Security=true;"; // <-- remplace par ta chaîne

        public DashboardClient(string username)
        {
            InitializeComponent(); // appelle le designer
            clientNom = username;
            lblWelcome.Text = $"Bienvenue, {username} 👋";
        }

        private void CommanderForm_Load(object sender, EventArgs e)
        {
            ChargerMedicaments();
            ConfigurerColonnesDataGridViews();
        }

        private void btnRafraichir_Click(object sender, EventArgs e)
        {
            txtRecherche.Text = string.Empty;
            ChargerMedicaments();
        }

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

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            txtRecherche_TextChanged(sender, e);
        }

        private void ChargerMedicaments()
        {
            try
            {
                medicamentsDisponibles = Medicament.GetAll()
                    .Where(m => m.Stock > 0)
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

        private void ConfigurerColonnesDataGridViews()
        {
            // DataGridView Medicaments
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

            // DataGridView Panier
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
                    Visible = false
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

            string input = Interaction.InputBox(
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
            MessageBox.Show($"{quantite} x {medicament.Nom} ajouté au panier !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

        // DB interaction adapted for Utilisateur (id_utilisateur, role='client')
        /// <summary>
        /// Recherche dynamiquement quelle(s) colonne(s) de "login" existent dans la table Utilisateur
        /// et construit une requête qui teste ces colonnes pour retrouver id_utilisateur où role = 'client'.
        /// </summary>
        private int? GetClientIdByUsername(SqlConnection conn, string username, SqlTransaction tx = null)
        {
            // Colonnes potentielles où le login/identifiant pourrait se trouver — on testera celles qui existent réellement.
            var candidateCols = new[] { "username", "login", "email", "nom_utilisateur", "nom", "user_name", "identifiant" };

            // Récupère les colonnes existantes de la table Utilisateur
            var existingCols = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var cmdCols = new SqlCommand(
                "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Utilisateur'", conn, tx))
            {
                using (var rdr = cmdCols.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        existingCols.Add(rdr.GetString(0));
                    }
                }
            }

            // Garde uniquement les colonnes candidates qui existent réellement
            var colsToUse = candidateCols.Where(c => existingCols.Contains(c, StringComparer.OrdinalIgnoreCase)).ToList();

            if (!colsToUse.Any())
            {
                // Aucune colonne standard trouvée : avertir pour corriger le schéma ou indiquer la colonne réelle
                throw new Exception("Aucune colonne de type 'login' trouvée dans la table Utilisateur. " +
                    "Vérifie le schéma (nom exact des colonnes) ou indique-moi la colonne qui contient le nom d'utilisateur.");
            }

            // Construire la requête sécurisée en utilisant des paramètres et en entourant les noms de colonnes avec des crochets
            var whereParts = colsToUse
                .Select((col, idx) => $"[{col}] = @u{idx}")
                .ToArray();

            string sql = $"SELECT TOP 1 id_utilisateur FROM Utilisateur WHERE role = 'client' AND ({string.Join(" OR ", whereParts)})";

            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                for (int i = 0; i < colsToUse.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@u{i}", username ?? string.Empty);
                }

                var obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value) return null;
                return Convert.ToInt32(obj);
            }
        }


        private int GetClientIdFromUsernameOrThrow()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var id = GetClientIdByUsername(conn, clientNom);
                if (!id.HasValue) throw new Exception("Client introuvable (username -> id_utilisateur). Vérifie le nom d'utilisateur et le rôle 'client' en base.");
                return id.Value;
            }
        }

        /// <summary>
        /// Crée la commande, insère les lignes (table LigneCommande) et met à jour les stocks.
        /// Adapté aux colonnes typiques :
        /// - Medicament : id_medicament, stock
        /// - Commande : id_utilisateur
        /// - LigneCommande : id_commande, id_medicament, quantite, prix_unitaire
        /// </summary>
        private void CreerCommandeEnBase(int id_utilisateur, List<LignePanier> panierLocal)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) Insérer la commande et récupérer id_commande
                        int newCommandeId;
                        using (var cmd = new SqlCommand(
                            "INSERT INTO Commande (id_utilisateur, date_commande, statut) VALUES (@id_utilisateur, @date_commande, @statut); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                            conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_utilisateur", id_utilisateur);
                            cmd.Parameters.AddWithValue("@date_commande", DateTime.Now);
                            cmd.Parameters.AddWithValue("@statut", "En attente");
                        newCommissionId:; // placeholder to keep compiler parity removed below
                            newCommandeId = (int)cmd.ExecuteScalar();
                        }

                        // 2) Traiter chaque ligne du panier
                        foreach (var item in panierLocal)
                        {
                            // a) Vérifier stock actuel (prévention)
                            using (var cmdVerif = new SqlCommand("SELECT stock FROM Medicament WHERE id_medicament = @id", conn, tx))
                            {
                                cmdVerif.Parameters.AddWithValue("@id", item.MedicamentId);
                                var stockObj = cmdVerif.ExecuteScalar();
                                if (stockObj == null || stockObj == DBNull.Value)
                                    throw new Exception($"Médicament id_medicament={item.MedicamentId} introuvable en base.");
                                int stockActuel = Convert.ToInt32(stockObj);
                                if (stockActuel < item.Quantite)
                                    throw new Exception($"Stock insuffisant pour {item.NomMedicament} (disponible {stockActuel}, demandé {item.Quantite}).");
                            }

                            // b) Insérer la ligne (table LigneCommande)
                            using (var cmdLigne = new SqlCommand(
                                "INSERT INTO LigneCommande (id_commande, id_medicament, quantite, prix_unitaire) VALUES (@id_commande, @id_medicament, @quantite, @prix_unitaire)",
                                conn, tx))
                            {
                                cmdLigne.Parameters.AddWithValue("@id_commande", newCommandeId);
                                cmdLigne.Parameters.AddWithValue("@id_medicament", item.MedicamentId);
                                cmdLigne.Parameters.AddWithValue("@quantite", item.Quantite);
                                cmdLigne.Parameters.AddWithValue("@prix_unitaire", item.PrixUnitaire);
                                cmdLigne.ExecuteNonQuery();
                            }

                            // c) Mettre à jour le stock
                            using (var cmdMaj = new SqlCommand(
                                "UPDATE Medicament SET stock = stock - @qte WHERE id_medicament = @id",
                                conn, tx))
                            {
                                cmdMaj.Parameters.AddWithValue("@qte", item.Quantite);
                                cmdMaj.Parameters.AddWithValue("@id", item.MedicamentId);
                                cmdMaj.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { /* ignore */ }
                        throw;
                    }
                }
            }
        }


        private void btnValiderCommande_Click(object sender, EventArgs e)
        {
            if (!panier.Any())
            {
                MessageBox.Show("Votre panier est vide.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                int clientId = GetClientIdFromUsernameOrThrow();
                CreerCommandeEnBase(clientId, new List<LignePanier>(panier));

                MessageBox.Show("Commande validée et enregistrée en base de données !", "Succès",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panier.Clear();
                ActualiserPanier();
                ChargerMedicaments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la validation : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void DataGridViewMedicaments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnAjouterPanier_Click(sender, e);
            }
        }

        private void DataGridViewPanier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optionnel
        }

        // -----------------------------
        // Gestion des commandes client
        // -----------------------------

        // stocke les détails actuellement affichés : mapping id_medicament -> quantite originale
        private Dictionary<int, int> currentOrderOriginalQuantities = new Dictionary<int, int>();

        // Charge les commandes du client dans DataGridViewCommandes
        private void LoadClientOrders()
        {
            try
            {
                int id_utilisateur = GetClientIdFromUsernameOrThrow();

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    "SELECT id_commande, date_commande, statut FROM Commande WHERE id_utilisateur = @id_utilisateur ORDER BY date_commande DESC",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id_utilisateur", id_utilisateur);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var list = new List<object>();
                        while (rdr.Read())
                        {
                            list.Add(new
                            {
                                id_commande = rdr.GetInt32(0),
                                date_commande = rdr.IsDBNull(1) ? (DateTime?)null : rdr.GetDateTime(1),
                                statut = rdr.IsDBNull(2) ? "" : rdr.GetString(2)
                            });
                        }
                        DataGridViewCommandes.DataSource = list;
                    }
                }

                // Configurer colonnes si nécessaire (une fois)
                if (DataGridViewCommandes.Columns.Count > 0)
                {
                    DataGridViewCommandes.Columns["id_commande"].HeaderText = "N° commande";
                    DataGridViewCommandes.Columns["date_commande"].HeaderText = "Date";
                    DataGridViewCommandes.Columns["statut"].HeaderText = "Statut";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des commandes : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // champ de classe (déjà présent mais utile de rappeler)
        private BindingList<LigneCommandeView> currentOrderDetails = new BindingList<LigneCommandeView>();

        private void LoadOrderDetails(int id_commande)
        {
            try
            {
                currentOrderOriginalQuantities.Clear();
                currentOrderDetails = new BindingList<LigneCommandeView>();

                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(
                    "SELECT lc.id_medicament, m.nom AS Nom, lc.quantite, lc.prix_unitaire " +
                    "FROM LigneCommande lc " +
                    "JOIN Medicament m ON m.id_medicament = lc.id_medicament " +
                    "WHERE lc.id_commande = @id_commande",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@id_commande", id_commande);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int idMed = rdr.GetInt32(0);
                            int quant = rdr.GetInt32(2);
                            decimal prix = rdr.IsDBNull(3) ? 0m : rdr.GetDecimal(3);

                            currentOrderOriginalQuantities[idMed] = quant;

                            currentOrderDetails.Add(new LigneCommandeView
                            {
                                id_medicament = idMed,
                                Nom = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                                Quantite = quant,
                                PrixUnitaire = prix
                            });
                        }
                    }
                }

                // Bind the BindingList so changes are editable
                DataGridViewCommandeDetails.DataSource = currentOrderDetails;

                // Configure columns: make Quantite editable, others readonly
                // If columns are autogenerated we can address them by name:
                if (DataGridViewCommandeDetails.Columns["id_medicament"] != null)
                    DataGridViewCommandeDetails.Columns["id_medicament"].ReadOnly = true;
                if (DataGridViewCommandeDetails.Columns["Nom"] != null)
                    DataGridViewCommandeDetails.Columns["Nom"].ReadOnly = true;
                if (DataGridViewCommandeDetails.Columns["PrixUnitaire"] != null)
                    DataGridViewCommandeDetails.Columns["PrixUnitaire"].ReadOnly = true;
                if (DataGridViewCommandeDetails.Columns["Total"] != null)
                    DataGridViewCommandeDetails.Columns["Total"].ReadOnly = true;

                if (DataGridViewCommandeDetails.Columns["Quantite"] != null)
                {
                    DataGridViewCommandeDetails.Columns["Quantite"].ReadOnly = false;
                    DataGridViewCommandeDetails.Columns["Quantite"].ValueType = typeof(int);
                }

                // Hook events for updating Totals and validating input
                DataGridViewCommandeDetails.CellValueChanged -= DataGridViewCommandeDetails_CellValueChanged;
                DataGridViewCommandeDetails.CellValueChanged += DataGridViewCommandeDetails_CellValueChanged;

                DataGridViewCommandeDetails.EditingControlShowing -= DataGridViewCommandeDetails_EditingControlShowing;
                DataGridViewCommandeDetails.EditingControlShowing += DataGridViewCommandeDetails_EditingControlShowing;

                // Refresh to show computed Total column values
                DataGridViewCommandeDetails.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du détail : {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Mettre à jour visuellement la colonne Total quand Quantite change
        private void DataGridViewCommandeDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var dgv = DataGridViewCommandeDetails;
            var row = dgv.Rows[e.RowIndex];

            // Si la colonne modifiée est Quantite, recalculer la cellule Total
            if (dgv.Columns[e.ColumnIndex].Name == "Quantite")
            {
                // Mettre à jour le BindingList item (déjà fait automatiquement),
                // puis forcer le rafraîchissement de la cellule Total
                dgv.InvalidateRow(e.RowIndex);
            }
        }

        // Validation d'entrée : n'autoriser que des entiers positifs dans la colonne Quantite
        private void DataGridViewCommandeDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DataGridViewCommandeDetails.CurrentCell?.ColumnIndex >= 0 &&
                DataGridViewCommandeDetails.Columns[DataGridViewCommandeDetails.CurrentCell.ColumnIndex].Name == "Quantite")
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= QuantiteColumn_KeyPress;
                    tb.KeyPress += QuantiteColumn_KeyPress;
                }
            }
            else
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= QuantiteColumn_KeyPress;
                }
            }
        }

        private void QuantiteColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // autoriser chiffres, backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        // Bouton "Mes commandes"
        private void btnVoirCommandes_Click(object sender, EventArgs e)
        {
            LoadClientOrders();
        }

        // Quand la sélection change dans la liste des commandes -> charger détail
        private void DataGridViewCommandes_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0) return;
            var row = DataGridViewCommandes.SelectedRows[0];
            int id_commande = Convert.ToInt32(row.Cells["id_commande"].Value);
            LoadOrderDetails(id_commande);
        }

        // Appliquer les modifications des quantités (btnModifierCommande)
        private void btnModifierCommande_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Sélectionne une commande à modifier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = DataGridViewCommandes.SelectedRows[0];
            int id_commande = Convert.ToInt32(selectedRow.Cells["id_commande"].Value);

            // Récupérer les nouvelles quantités depuis le DataGridViewCommandeDetails
            var modifications = new List<(int id_medicament, int newQuant, int oldQuant)>();
            foreach (DataGridViewRow r in DataGridViewCommandeDetails.Rows)
            {
                int idMed = Convert.ToInt32(r.Cells["id_medicament"].Value);
                int newQ = Convert.ToInt32(r.Cells["Quantite"].Value);
                int oldQ = currentOrderOriginalQuantities.ContainsKey(idMed) ? currentOrderOriginalQuantities[idMed] : 0;
                if (newQ != oldQ)
                    modifications.Add((idMed, newQ, oldQ));
            }

            if (!modifications.Any())
            {
                MessageBox.Show("Aucune modification détectée.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirmation
            var conf = MessageBox.Show("Valider les modifications de la commande ? Les stocks seront ajustés en conséquence.", "Confirmer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (conf != DialogResult.Yes) return;

            // Appliquer en transaction : vérifier les stocks, mettre à jour LigneCommande et Medicament
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var (id_medicament, newQuant, oldQuant) in modifications)
                            {
                                int delta = newQuant - oldQuant;
                                if (delta > 0)
                                {
                                    // Vérifier stock disponible
                                    using (var cmdCheck = new SqlCommand("SELECT stock FROM Medicament WHERE id_medicament = @id", conn, tx))
                                    {
                                        cmdCheck.Parameters.AddWithValue("@id", id_medicament);
                                        var val = cmdCheck.ExecuteScalar();
                                        if (val == null || val == DBNull.Value)
                                            throw new Exception($"Médicament id_medicament={id_medicament} introuvable.");
                                        int stock = Convert.ToInt32(val);
                                        if (stock < delta)
                                            throw new Exception($"Stock insuffisant pour id_medicament={id_medicament} (disponible {stock}, supplémentaire demandée {delta}).");
                                    }

                                    // soustraire stock
                                    using (var cmdUpdStock = new SqlCommand("UPDATE Medicament SET stock = stock - @qte WHERE id_medicament = @id", conn, tx))
                                    {
                                        cmdUpdStock.Parameters.AddWithValue("@qte", delta);
                                        cmdUpdStock.Parameters.AddWithValue("@id", id_medicament);
                                        cmdUpdStock.ExecuteNonQuery();
                                    }
                                }
                                else if (delta < 0)
                                {
                                    // restituer stock (delta négatif -> enlever le signe)
                                    using (var cmdUpdStock = new SqlCommand("UPDATE Medicament SET stock = stock + @qte WHERE id_medicament = @id", conn, tx))
                                    {
                                        cmdUpdStock.Parameters.AddWithValue("@qte", -delta);
                                        cmdUpdStock.Parameters.AddWithValue("@id", id_medicament);
                                        cmdUpdStock.ExecuteNonQuery();
                                    }
                                }

                                // Mettre à jour la quantité dans LigneCommande
                                using (var cmdUpdLine = new SqlCommand("UPDATE LigneCommande SET quantite = @quantite WHERE id_commande = @id_commande AND id_medicament = @id_medicament", conn, tx))
                                {
                                    cmdUpdLine.Parameters.AddWithValue("@quantite", newQuant);
                                    cmdUpdLine.Parameters.AddWithValue("@id_commande", id_commande);
                                    cmdUpdLine.Parameters.AddWithValue("@id_medicament", id_medicament);
                                    cmdUpdLine.ExecuteNonQuery();
                                }
                            }

                            tx.Commit();
                        }
                        catch
                        {
                            try { tx.Rollback(); } catch { /* ignore */ }
                            throw;
                        }
                    }
                }

                MessageBox.Show("Modifications appliquées avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // recharger vues
                LoadClientOrders();
                // reselectionner la même commande (si encore présente) et recharger details
                foreach (DataGridViewRow r in DataGridViewCommandes.Rows)
                {
                    if (Convert.ToInt32(r.Cells["id_commande"].Value) == id_commande)
                    {
                        r.Selected = true;
                        DataGridViewCommandes.CurrentCell = r.Cells[0];
                        break;
                    }
                }
                LoadOrderDetails(id_commande);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'application des modifications : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Annuler la commande (btnAnnulerCommande) -> statut = 'Annulée' et restituer les stocks
        private void btnAnnulerCommande_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Sélectionne une commande à annuler.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sel = DataGridViewCommandes.SelectedRows[0];
            int id_commande = Convert.ToInt32(sel.Cells["id_commande"].Value);

            var conf = MessageBox.Show("Confirmer l'annulation de cette commande ? Les quantités seront restituées au stock.", "Confirmer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (conf != DialogResult.Yes) return;

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1) récupérer toutes les lignes et restituer le stock
                            using (var cmdLines = new SqlCommand("SELECT id_medicament, quantite FROM LigneCommande WHERE id_commande = @id_commande", conn, tx))
                            {
                                cmdLines.Parameters.AddWithValue("@id_commande", id_commande);
                                using (var rdr = cmdLines.ExecuteReader())
                                {
                                    var restitutions = new List<(int id_medicament, int q)>();
                                    while (rdr.Read())
                                    {
                                        restitutions.Add((rdr.GetInt32(0), rdr.GetInt32(1)));
                                    }
                                    rdr.Close();

                                    foreach (var (id_medicament, q) in restitutions)
                                    {
                                        using (var cmdUpdStock = new SqlCommand("UPDATE Medicament SET stock = stock + @qte WHERE id_medicament = @id", conn, tx))
                                        {
                                            cmdUpdStock.Parameters.AddWithValue("@qte", q);
                                            cmdUpdStock.Parameters.AddWithValue("@id", id_medicament);
                                            cmdUpdStock.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            // 2) Mettre à jour le statut de la commande
                            using (var cmdUpd = new SqlCommand("UPDATE Commande SET statut = @statut WHERE id_commande = @id_commande", conn, tx))
                            {
                                cmdUpd.Parameters.AddWithValue("@statut", "Annulée");
                                cmdUpd.Parameters.AddWithValue("@id_commande", id_commande);
                                cmdUpd.ExecuteNonQuery();
                            }

                            tx.Commit();
                        }
                        catch
                        {
                            try { tx.Rollback(); } catch { /* ignore */ }
                            throw;
                        }
                    }
                }

                MessageBox.Show("Commande annulée et stocks restitués.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadClientOrders();
                DataGridViewCommandeDetails.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'annulation : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Déconnexion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Ouvre le formulaire de login
                LoginForm loginForm = new LoginForm();
                loginForm.Show();

                // Ferme le dashboard
                this.Close();
            }
        }


    }
}
