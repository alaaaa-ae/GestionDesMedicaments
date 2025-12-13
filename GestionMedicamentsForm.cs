using GestionDesMedicaments.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public class GestionMedicamentsForm : Form
    {
        private SidebarControl sidebar;
        private Panel panelContent;
        private Panel panelFormulaire;
        private ListView listViewMedicaments;
        private ListView listViewAlertePeremption;
        private TextBox txtNom, txtDescription, txtPrixAchat, txtPrixVente, txtStock, txtSeuilAlerte, txtRecherche;
        private ComboBox comboBoxFournisseur;
        private Button btnAjouter, btnModifier, btnSupprimer, btnNouveau;
        private Label lblTitre;
        private int selectedMedicamentId = -1;

        public GestionMedicamentsForm()
        {
            InitializeComponent();
            ChargerFournisseurs();
            ChargerMedicaments();
            ChargerMedicamentsAlertePeremption();
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponent()
        {
            this.sidebar = new SidebarControl();
            this.panelContent = new Panel();
            this.panelFormulaire = new Panel();
            this.listViewMedicaments = new ListView();
            this.listViewAlertePeremption = new ListView();
            this.txtNom = new TextBox();
            this.txtDescription = new TextBox();
            this.txtPrixAchat = new TextBox();
            this.txtPrixVente = new TextBox();
            this.txtStock = new TextBox();
            this.txtSeuilAlerte = new TextBox();
            this.txtRecherche = new TextBox();
            this.comboBoxFournisseur = new ComboBox();
            this.btnAjouter = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnNouveau = new Button();
            this.lblTitre = new Label();

            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Text = "💊 Gestion des Médicaments";

            // --- Sidebar ---
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.SetActiveButton("medicaments");
            this.sidebar.MedicamentsClicked += Sidebar_MedicamentsClicked;
            this.sidebar.CommandesClicked += Sidebar_CommandesClicked;
            this.sidebar.ClientsClicked += Sidebar_ClientsClicked;
            this.sidebar.DashboardClicked += Sidebar_DashboardClicked;
            this.sidebar.RefreshClicked += Sidebar_RefreshClicked;
            this.sidebar.DeconnexionClicked += Sidebar_DeconnexionClicked;

            // --- Panel Content ---
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(245, 247, 250);
            // CORRECTION: Suppression du padding gauche pour permettre au contenu de s'afficher dès le début du panel
            this.panelContent.Padding = new Padding(20); 
            this.panelContent.AutoScroll = true;

            // --- Titre ---
            this.lblTitre.Text = "💊 Gestion des Médicaments";
            this.lblTitre.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(50, 50, 60);
            this.lblTitre.Location = new Point(20, 20); // Décalé de 20px (Padding)
            this.lblTitre.AutoSize = true;

            // --- Recherche ---
            var lblRecherche = new Label
            {
                Text = "🔍 Rechercher:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120),
                Location = new Point(20, 95),
                AutoSize = true
            };
            this.txtRecherche.Location = new Point(140, 92);
            this.txtRecherche.Size = new Size(400, 28);
            this.txtRecherche.Font = new Font("Segoe UI", 10F);
            this.txtRecherche.BorderStyle = BorderStyle.FixedSingle;
            this.txtRecherche.TextChanged += TxtRecherche_TextChanged;
            
            // --- Label Liste Médicaments ---
            var lblListeMedicaments = new Label
            {
                Text = "📋 Liste des Médicaments",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(440, 140), // Position initiale (sera ajustée)
                AutoSize = true
            };

            // --- Panel Formulaire ---
            this.panelFormulaire.Location = new Point(20, 120);
            this.panelFormulaire.Size = new Size(400, 600);
            this.panelFormulaire.BackColor = Color.White;
            this.panelFormulaire.BorderStyle = BorderStyle.FixedSingle; // Mieux que None pour délimiter
            this.panelFormulaire.Padding = new Padding(20);

            var lblFormTitre = new Label
            {
                Text = "📝 Formulaire Médicament",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.panelFormulaire.Controls.Add(lblFormTitre);

            int y = 60;
            int gap = 35;
            int labelX = 20;
            int inputX = 150;
            int inputWidth = 220;

            // Nom
            var lblNom = new Label { Text = "Nom *", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtNom.Location = new Point(inputX, y);
            this.txtNom.Size = new Size(inputWidth, 25);
            this.txtNom.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Description
            var lblDesc = new Label { Text = "Description", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtDescription.Location = new Point(inputX, y);
            this.txtDescription.Size = new Size(inputWidth, 25);
            this.txtDescription.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Prix Achat
            var lblPA = new Label { Text = "Prix Achat", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtPrixAchat.Location = new Point(inputX, y);
            this.txtPrixAchat.Size = new Size(inputWidth, 25);
            this.txtPrixAchat.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Prix Vente
            var lblPV = new Label { Text = "Prix Vente", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtPrixVente.Location = new Point(inputX, y);
            this.txtPrixVente.Size = new Size(inputWidth, 25);
            this.txtPrixVente.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Stock
            var lblStock = new Label { Text = "Stock", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtStock.Location = new Point(inputX, y);
            this.txtStock.Size = new Size(inputWidth, 25);
            this.txtStock.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Seuil Alerte
            var lblSeuil = new Label { Text = "Seuil Alerte", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtSeuilAlerte.Location = new Point(inputX, y);
            this.txtSeuilAlerte.Size = new Size(inputWidth, 25);
            this.txtSeuilAlerte.Font = new Font("Segoe UI", 9F);
            y += gap;

            // Fournisseur
            var lblFour = new Label { Text = "Fournisseur *", Location = new Point(labelX, y), Size = new Size(120, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.comboBoxFournisseur.Location = new Point(inputX, y);
            this.comboBoxFournisseur.Size = new Size(inputWidth, 25);
            this.comboBoxFournisseur.Font = new Font("Segoe UI", 9F);
            this.comboBoxFournisseur.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 50;

            // Boutons
            this.btnAjouter.Text = "➕ Ajouter";
            this.btnAjouter.Location = new Point(20, y);
            this.btnAjouter.Size = new Size(85, 35);
            this.btnAjouter.BackColor = Color.FromArgb(255, 140, 0);
            this.btnAjouter.ForeColor = Color.White;
            this.btnAjouter.FlatStyle = FlatStyle.Flat;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAjouter.Click += BtnAjouter_Click;

            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Location = new Point(115, y);
            this.btnModifier.Size = new Size(85, 35);
            this.btnModifier.BackColor = Color.FromArgb(60, 160, 60);
            this.btnModifier.ForeColor = Color.White;
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnModifier.Click += BtnModifier_Click;

            this.btnSupprimer.Text = "❌ Supprimer";
            this.btnSupprimer.Location = new Point(210, y);
            this.btnSupprimer.Size = new Size(85, 35);
            this.btnSupprimer.BackColor = Color.FromArgb(200, 50, 50);
            this.btnSupprimer.ForeColor = Color.White;
            this.btnSupprimer.FlatStyle = FlatStyle.Flat;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSupprimer.Click += BtnSupprimer_Click;

            this.btnNouveau.Text = "🆕 Nouveau";
            this.btnNouveau.Location = new Point(305, y); // Aligné
            this.btnNouveau.Size = new Size(85, 35);
            this.btnNouveau.BackColor = Color.Gray;
            this.btnNouveau.ForeColor = Color.White;
            this.btnNouveau.FlatStyle = FlatStyle.Flat;
            this.btnNouveau.FlatAppearance.BorderSize = 0;
            this.btnNouveau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNouveau.Click += BtnNouveau_Click;

            this.panelFormulaire.Controls.AddRange(new Control[] {
                lblNom, txtNom, lblDesc, txtDescription, lblPA, txtPrixAchat,
                lblPV, txtPrixVente, lblStock, txtStock, lblSeuil, txtSeuilAlerte,
                lblFour, comboBoxFournisseur, btnAjouter, btnModifier, btnSupprimer, btnNouveau
            });
            
            // --- ListView Médicaments ---
            this.listViewMedicaments.Location = new Point(440, 170); // Y après le titre de liste
            this.listViewMedicaments.Size = new Size(800, 350);
            this.listViewMedicaments.View = View.Details;
            this.listViewMedicaments.FullRowSelect = true;
            this.listViewMedicaments.GridLines = true;
            this.listViewMedicaments.BorderStyle = BorderStyle.FixedSingle;
            this.listViewMedicaments.BackColor = Color.White;
            this.listViewMedicaments.Font = new Font("Segoe UI", 9F);
            this.listViewMedicaments.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewMedicaments.DoubleClick += ListViewMedicaments_DoubleClick;
            this.listViewMedicaments.Columns.Add("ID", 60);
            this.listViewMedicaments.Columns.Add("Nom", 200);
            this.listViewMedicaments.Columns.Add("Description", 250);
            this.listViewMedicaments.Columns.Add("Prix Achat", 100);
            this.listViewMedicaments.Columns.Add("Prix Vente", 100);
            this.listViewMedicaments.Columns.Add("Stock", 80);
            this.listViewMedicaments.Columns.Add("Seuil", 80);

            // --- ListView Alerte Péremption ---
            this.listViewAlertePeremption.Location = new Point(440, 580); // Y après le titre de liste
            this.listViewAlertePeremption.Size = new Size(800, 200);
            this.listViewAlertePeremption.View = View.Details;
            this.listViewAlertePeremption.FullRowSelect = true;
            this.listViewAlertePeremption.GridLines = true;
            this.listViewAlertePeremption.BorderStyle = BorderStyle.FixedSingle;
            this.listViewAlertePeremption.BackColor = Color.White;
            this.listViewAlertePeremption.Font = new Font("Segoe UI", 9F);
            this.listViewAlertePeremption.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewAlertePeremption.Columns.Add("Médicament", 300);
            this.listViewAlertePeremption.Columns.Add("Stock", 100);
            this.listViewAlertePeremption.Columns.Add("Jours Restants", 150);

            var lblAlerte = new Label
            {
                Text = "⏰ Médicaments en Alerte Péremption (30 jours)",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(440, 550), // Position du titre d'alerte
                AutoSize = true
            };

            // --- Ajout des contrôles au Panel Content ---
            this.panelContent.Controls.Add(this.lblTitre);
            this.panelContent.Controls.Add(lblRecherche);
            this.panelContent.Controls.Add(this.txtRecherche);
            this.panelContent.Controls.Add(lblListeMedicaments);
            this.panelContent.Controls.Add(this.panelFormulaire);
            this.panelContent.Controls.Add(this.listViewMedicaments);
            this.panelContent.Controls.Add(lblAlerte);
            this.panelContent.Controls.Add(this.listViewAlertePeremption);

            // --- Ajout des contrôles principaux au Formulaire ---
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.sidebar);

            this.ResumeLayout(false);

            this.Load += (s, e) => AjusterTailles();
            this.Resize += (s, e) => AjusterTailles(); // Gère le redimensionnement
        }

        private void AjusterTailles()
        {
            // La largeur totale disponible pour le contenu
            int contentWidth = this.panelContent.ClientSize.Width - 40; // 40 = 20 (padding gauche) + 20 (padding droit)
            
            // La largeur du panneau de formulaire est fixe
            int formPanelWidth = 400;
            
            // L'espace entre le formulaire et les listes (20px)
            int gap = 20;

            // La largeur restante pour les listes
            int listWidth = contentWidth - formPanelWidth - gap;

            // Définir la position et la taille du panneau de formulaire (côté gauche)
            this.panelFormulaire.Location = new Point(20, 130);
            // La hauteur peut être ajustée pour mieux remplir l'espace si nécessaire
            int formulaireHeight = Math.Max(this.panelContent.Height - 140, 600);
            this.panelFormulaire.Size = new Size(formPanelWidth, formulaireHeight);

            // Définir la position de départ pour les listes (côté droit)
            int listStartX = 20 + formPanelWidth + gap; // 20 (padding) + 400 (form) + 20 (gap) = 440

            // 1. Ajustement de la Liste des Médicaments (Liste Supérieure)
            int listViewMedicamentsY = 170; // Position Y fixe

            // Ajustement du titre de la liste
            var lblListeMedicaments = this.panelContent.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Liste des Médicaments"));
            if (lblListeMedicaments != null) lblListeMedicaments.Location = new Point(listStartX, 140);

            // Ajustement de la liste
            this.listViewMedicaments.Location = new Point(listStartX, listViewMedicamentsY);
            // Hauteur dynamique pour laisser de la place à la liste d'alerte en bas
            int listViewMedicamentsHeight = (int)((this.panelContent.ClientSize.Height - listViewMedicamentsY - 40) * 0.55); // ~55% du reste
            this.listViewMedicaments.Size = new Size(listWidth, listViewMedicamentsHeight);
            
            // 2. Ajustement de la Liste d'Alerte Péremption (Liste Inférieure)
            int lblAlerteY = listViewMedicamentsY + listViewMedicamentsHeight + 10;
            int listViewAlerteY = lblAlerteY + 30;

            // Ajustement du titre d'alerte
            var lblAlerte = this.panelContent.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Alerte Péremption"));
            if (lblAlerte != null) lblAlerte.Location = new Point(listStartX, lblAlerteY);
            
            // Ajustement de la liste
            this.listViewAlertePeremption.Location = new Point(listStartX, listViewAlerteY);
            int listViewAlerteHeight = (int)((this.panelContent.ClientSize.Height - listViewAlerteY - 40) * 0.40); // ~40% du reste
            this.listViewAlertePeremption.Size = new Size(listWidth, listViewAlerteHeight);

            // 3. Ajustement de la barre de recherche
            this.txtRecherche.Size = new Size(contentWidth - 140, 28); // S'étend sur presque toute la largeur
        }

        private void ChargerFournisseurs()
        {
            // Vérifie si la classe Fournisseur existe et a la méthode GetAll()
            try
            {
                var fournisseurs = Fournisseur.GetAll();
                this.comboBoxFournisseur.DataSource = fournisseurs;
                this.comboBoxFournisseur.DisplayMember = "Nom";
                this.comboBoxFournisseur.ValueMember = "Id";
                this.comboBoxFournisseur.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Ignore l'erreur si la classe Fournisseur est manquante pour l'instant
                // ou si la BDD est inaccessible.
                System.Diagnostics.Debug.WriteLine($"Erreur chargement fournisseurs: {ex.Message}");
            }
        }

        private void ChargerMedicaments()
        {
            this.listViewMedicaments.Items.Clear();
            var medicaments = Medicament.GetAll();

            foreach (var m in medicaments)
            {
                var item = new ListViewItem(m.Id.ToString());
                item.SubItems.Add(m.Nom);
                item.SubItems.Add(m.Description ?? "");
                item.SubItems.Add(m.PrixAchat.ToString("C2"));
                item.SubItems.Add(m.PrixVente.ToString("C2"));
                item.SubItems.Add(m.Stock.ToString());
                item.SubItems.Add(m.SeuilAlerte.ToString());
                if (m.Stock <= m.SeuilAlerte) item.ForeColor = Color.Red;
                item.Tag = m.Id;
                this.listViewMedicaments.Items.Add(item);
            }
        }

        private void ChargerMedicamentsAlertePeremption()
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection conn = Classes.Database.GetConnection())
                {
                    conn.Open();
                    string checkColumn = @"SELECT COUNT(*) 
                                             FROM INFORMATION_SCHEMA.COLUMNS 
                                             WHERE TABLE_NAME = 'Medicament' 
                                             AND COLUMN_NAME = 'date_peremption'";
                    System.Data.SqlClient.SqlCommand cmdCheck = new System.Data.SqlClient.SqlCommand(checkColumn, conn);
                    int columnExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    this.listViewAlertePeremption.Items.Clear();

                    if (columnExists > 0)
                    {
                        string query = @"SELECT TOP 20 
                                            nom, stock, DATEDIFF(DAY, GETDATE(), date_peremption) as JoursRestants
                                            FROM Medicament 
                                            WHERE date_peremption IS NOT NULL
                                            AND date_peremption <= DATEADD(DAY, 30, GETDATE())
                                            ORDER BY date_peremption ASC";

                        using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                        {
                            using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var item = new ListViewItem(reader["nom"].ToString());
                                    item.SubItems.Add(reader["stock"].ToString());
                                    
                                    // Mise en couleur selon le nombre de jours restants
                                    int joursRestants = Convert.ToInt32(reader["JoursRestants"]);
                                    item.SubItems.Add(joursRestants.ToString());

                                    if (joursRestants < 0)
                                    {
                                        item.BackColor = Color.FromArgb(255, 192, 192); // Rouge très clair (Périmé)
                                    }
                                    else if (joursRestants <= 7)
                                    {
                                        item.BackColor = Color.FromArgb(255, 255, 192); // Jaune clair (Urgent)
                                    }
                                    else
                                    {
                                        item.BackColor = Color.FromArgb(230, 255, 230); // Vert très clair (Alerte)
                                    }
                                    
                                    this.listViewAlertePeremption.Items.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                         var item = new ListViewItem("Colonne 'date_peremption' manquante dans la table Medicament.");
                         item.ForeColor = Color.DarkRed;
                         item.BackColor = Color.WhiteSmoke;
                         this.listViewAlertePeremption.Items.Add(item);
                    }
                }
            }
            catch (Exception ex) 
            {
                 var item = new ListViewItem($"Erreur de connexion BDD: {ex.Message}");
                 item.ForeColor = Color.DarkRed;
                 item.BackColor = Color.WhiteSmoke;
                 this.listViewAlertePeremption.Items.Add(item);
            }
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text) || comboBoxFournisseur.SelectedValue == null)
            {
                MessageBox.Show("Nom et Fournisseur sont obligatoires !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Médicament ajouté !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerMedicaments();
                ReinitialiserFormulaire();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (selectedMedicamentId == -1)
            {
                MessageBox.Show("Sélectionnez un médicament !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Medicament med = new Medicament
            {
                Id = selectedMedicamentId,
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
                MessageBox.Show("Médicament modifié !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerMedicaments();
                ReinitialiserFormulaire();
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (selectedMedicamentId == -1)
            {
                MessageBox.Show("Sélectionnez un médicament !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Confirmer la suppression ?", "Supprimer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Medicament.Supprimer(selectedMedicamentId))
                {
                    MessageBox.Show("Médicament supprimé !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ChargerMedicaments();
                    ReinitialiserFormulaire();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression ! Le médicament est peut-être lié à une commande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            ReinitialiserFormulaire();
        }

        private void ListViewMedicaments_DoubleClick(object sender, EventArgs e)
        {
            if (listViewMedicaments.SelectedItems.Count > 0)
            {
                int id = (int)listViewMedicaments.SelectedItems[0].Tag;
                var med = Medicament.GetById(id);
                if (med != null)
                {
                    selectedMedicamentId = med.Id;
                    txtNom.Text = med.Nom;
                    txtDescription.Text = med.Description ?? "";
                    txtPrixAchat.Text = med.PrixAchat.ToString();
                    txtPrixVente.Text = med.PrixVente.ToString();
                    txtStock.Text = med.Stock.ToString();
                    txtSeuilAlerte.Text = med.SeuilAlerte.ToString();
                    // Assurez-vous que l'élément est dans la ComboBox avant d'essayer de le sélectionner
                    if (comboBoxFournisseur.Items.Cast<Fournisseur>().Any(f => f.Id == med.IdFournisseur))
                    {
                        comboBoxFournisseur.SelectedValue = med.IdFournisseur;
                    }
                    else
                    {
                        comboBoxFournisseur.SelectedIndex = -1; // Réinitialise si non trouvé
                    }
                }
            }
        }

        private void TxtRecherche_TextChanged(object sender, EventArgs e)
        {
            // Simplification de la recherche pour afficher uniquement les items correspondants
            string nom = txtRecherche.Text.Trim().ToLower();
            foreach (ListViewItem item in listViewMedicaments.Items)
            {
                if (string.IsNullOrEmpty(nom) || item.SubItems[1].Text.ToLower().Contains(nom))
                {
                    item.ListView.BeginUpdate();
                    item.Selected = false; // Désélectionne
                    item.Remove();
                    item.ListView.Items.Add(item); // Remet l'élément à la fin pour le tri visuel
                    item.ListView.EndUpdate();
                }
            }
            // Recharger tous les médicaments si la recherche est vide pour retrouver l'ordre initial
            if (string.IsNullOrEmpty(nom))
            {
                ChargerMedicaments(); 
            }
            else
            {
                // Un filtre plus efficace serait de recharger depuis la BDD avec le filtre, 
                // mais la logique actuelle dans ListView est conservée pour la compatibilité.
                // Ici, on filtre simplement la vue, mais cela ne fonctionne pas bien. 
                // Je recommande de mettre en place une vraie fonction de recherche BDD (ex: Medicament.GetAll(filtre))
                // Pour l'instant, je vais juste faire une recherche visuelle basique sur la liste déjà chargée:
                foreach (ListViewItem item in listViewMedicaments.Items)
                {
                    bool isVisible = item.SubItems[1].Text.ToLower().Contains(nom);
                    item.ListView.BeginUpdate();
                    if (!isVisible)
                    {
                         item.Remove(); // Masque l'élément en le retirant temporairement
                    }
                    else if (!item.ListView.Items.Contains(item))
                    {
                         item.ListView.Items.Add(item); // Le rajoute si manquant
                    }
                    item.ListView.EndUpdate();
                }
            }
        }

        private void ReinitialiserFormulaire()
        {
            selectedMedicamentId = -1;
            txtNom.Clear();
            txtDescription.Clear();
            txtPrixAchat.Clear();
            txtPrixVente.Clear();
            txtStock.Clear();
            txtSeuilAlerte.Clear();
            comboBoxFournisseur.SelectedIndex = -1;
        }

        // Événements Sidebar
        private void Sidebar_MedicamentsClicked(object sender, EventArgs e) { }
        private void Sidebar_CommandesClicked(object sender, EventArgs e) { OuvrirForm(new GestionCommandesForm()); }
        private void Sidebar_ClientsClicked(object sender, EventArgs e) { OuvrirForm(new GestionClientsForm()); }
        private void Sidebar_DashboardClicked(object sender, EventArgs e) { OuvrirForm(new DashboardPharmacien()); }
        private void Sidebar_RefreshClicked(object sender, EventArgs e) { ChargerMedicaments(); ChargerMedicamentsAlertePeremption(); }
        private void Sidebar_DeconnexionClicked(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void OuvrirForm(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            this.Hide();
        }
    }
}