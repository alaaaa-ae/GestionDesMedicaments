using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class DashboardPharmacien
    {
        private Button btnDeconnexion;

        private System.ComponentModel.IContainer components = null;
        private Label lblTitre;
        private Panel panelStats;
        private Label lblCAJournalier;
        private Label lblCommandesJour;
        private Label lblAlertesStock;
        private Label lblClientsMois;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dataGridViewStockBas;
        private DataGridView dataGridViewCommandes;
        private DataGridView dataGridViewPopulaires;
        private Label lblStockBas;
        private Label lblCommandesRecentes;
        private Label lblMedicamentsPopulaires;
        private Button btnGestionMedicaments;
        private Button btnGestionCommandes;
        private Button btnGestionClients;
        private Button btnRafraichir;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCAJournalier = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCommandesJour = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAlertesStock = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblClientsMois = new System.Windows.Forms.Label();
            this.dataGridViewStockBas = new System.Windows.Forms.DataGridView();
            this.dataGridViewCommandes = new System.Windows.Forms.DataGridView();
            this.dataGridViewPopulaires = new System.Windows.Forms.DataGridView();
            this.lblStockBas = new System.Windows.Forms.Label();
            this.lblCommandesRecentes = new System.Windows.Forms.Label();
            this.lblMedicamentsPopulaires = new System.Windows.Forms.Label();
            this.btnGestionMedicaments = new System.Windows.Forms.Button();
            this.btnGestionCommandes = new System.Windows.Forms.Button();
            this.btnGestionClients = new System.Windows.Forms.Button();
            this.btnRafraichir = new System.Windows.Forms.Button();
            this.panelStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockBas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPopulaires)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.Orange;
            this.lblTitre.Location = new System.Drawing.Point(300, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(350, 40);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "📊 Tableau de Bord Pharmacien";
            this.lblTitre.Click += new System.EventHandler(this.lblTitre_Click);
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.label1);
            this.panelStats.Controls.Add(this.lblCAJournalier);
            this.panelStats.Controls.Add(this.label2);
            this.panelStats.Controls.Add(this.lblCommandesJour);
            this.panelStats.Controls.Add(this.label3);
            this.panelStats.Controls.Add(this.lblAlertesStock);
            this.panelStats.Controls.Add(this.label4);
            this.panelStats.Controls.Add(this.lblClientsMois);
            this.panelStats.Location = new System.Drawing.Point(30, 80);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(740, 100);
            this.panelStats.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "CA Journalier";
            // 
            // lblCAJournalier
            // 
            this.lblCAJournalier.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCAJournalier.ForeColor = System.Drawing.Color.Green;
            this.lblCAJournalier.Location = new System.Drawing.Point(30, 40);
            this.lblCAJournalier.Name = "lblCAJournalier";
            this.lblCAJournalier.Size = new System.Drawing.Size(100, 23);
            this.lblCAJournalier.TabIndex = 1;
            this.lblCAJournalier.Text = "0,00 €";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Commandes Aujourd\'hui";
            // 
            // lblCommandesJour
            // 
            this.lblCommandesJour.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCommandesJour.Location = new System.Drawing.Point(200, 40);
            this.lblCommandesJour.Name = "lblCommandesJour";
            this.lblCommandesJour.Size = new System.Drawing.Size(100, 23);
            this.lblCommandesJour.TabIndex = 3;
            this.lblCommandesJour.Text = "0";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(400, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Alertes Stock";
            // 
            // lblAlertesStock
            // 
            this.lblAlertesStock.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlertesStock.ForeColor = System.Drawing.Color.Red;
            this.lblAlertesStock.Location = new System.Drawing.Point(400, 40);
            this.lblAlertesStock.Name = "lblAlertesStock";
            this.lblAlertesStock.Size = new System.Drawing.Size(100, 23);
            this.lblAlertesStock.TabIndex = 5;
            this.lblAlertesStock.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(580, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Clients Ce Mois";
            // 
            // lblClientsMois
            // 
            this.lblClientsMois.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblClientsMois.Location = new System.Drawing.Point(580, 40);
            this.lblClientsMois.Name = "lblClientsMois";
            this.lblClientsMois.Size = new System.Drawing.Size(100, 23);
            this.lblClientsMois.TabIndex = 7;
            this.lblClientsMois.Text = "0";
            // 
            // dataGridViewStockBas
            // 
            this.dataGridViewStockBas.ColumnHeadersHeight = 34;
            this.dataGridViewStockBas.Location = new System.Drawing.Point(30, 230);
            this.dataGridViewStockBas.Name = "dataGridViewStockBas";
            this.dataGridViewStockBas.RowHeadersWidth = 62;
            this.dataGridViewStockBas.Size = new System.Drawing.Size(360, 150);
            this.dataGridViewStockBas.TabIndex = 3;
            // 
            // dataGridViewCommandes
            // 
            this.dataGridViewCommandes.ColumnHeadersHeight = 34;
            this.dataGridViewCommandes.Location = new System.Drawing.Point(410, 230);
            this.dataGridViewCommandes.Name = "dataGridViewCommandes";
            this.dataGridViewCommandes.RowHeadersWidth = 62;
            this.dataGridViewCommandes.Size = new System.Drawing.Size(360, 150);
            this.dataGridViewCommandes.TabIndex = 5;
            // 
            // dataGridViewPopulaires
            // 
            this.dataGridViewPopulaires.ColumnHeadersHeight = 34;
            this.dataGridViewPopulaires.Location = new System.Drawing.Point(30, 430);
            this.dataGridViewPopulaires.Name = "dataGridViewPopulaires";
            this.dataGridViewPopulaires.RowHeadersWidth = 62;
            this.dataGridViewPopulaires.Size = new System.Drawing.Size(360, 150);
            this.dataGridViewPopulaires.TabIndex = 7;
            // 
            // lblStockBas
            // 
            this.lblStockBas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStockBas.ForeColor = System.Drawing.Color.Orange;
            this.lblStockBas.Location = new System.Drawing.Point(30, 200);
            this.lblStockBas.Name = "lblStockBas";
            this.lblStockBas.Size = new System.Drawing.Size(100, 23);
            this.lblStockBas.TabIndex = 2;
            this.lblStockBas.Text = "⚠️ Médicaments Stock Bas";
            // 
            // lblCommandesRecentes
            // 
            this.lblCommandesRecentes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCommandesRecentes.ForeColor = System.Drawing.Color.Orange;
            this.lblCommandesRecentes.Location = new System.Drawing.Point(410, 200);
            this.lblCommandesRecentes.Name = "lblCommandesRecentes";
            this.lblCommandesRecentes.Size = new System.Drawing.Size(100, 23);
            this.lblCommandesRecentes.TabIndex = 4;
            this.lblCommandesRecentes.Text = "📦 Commandes Récentes";
            // 
            // lblMedicamentsPopulaires
            // 
            this.lblMedicamentsPopulaires.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMedicamentsPopulaires.ForeColor = System.Drawing.Color.Orange;
            this.lblMedicamentsPopulaires.Location = new System.Drawing.Point(30, 400);
            this.lblMedicamentsPopulaires.Name = "lblMedicamentsPopulaires";
            this.lblMedicamentsPopulaires.Size = new System.Drawing.Size(100, 23);
            this.lblMedicamentsPopulaires.TabIndex = 6;
            this.lblMedicamentsPopulaires.Text = "🔥 Médicaments Populaires (30j)";
            // 
            // btnGestionMedicaments
            // 
            this.btnGestionMedicaments.Location = new System.Drawing.Point(410, 430);
            this.btnGestionMedicaments.Name = "btnGestionMedicaments";
            this.btnGestionMedicaments.Size = new System.Drawing.Size(170, 40);
            this.btnGestionMedicaments.TabIndex = 8;
            this.btnGestionMedicaments.Text = "💊 Gestion Médicaments";
            this.btnGestionMedicaments.Click += new System.EventHandler(this.btnGestionMedicaments_Click);
            // 
            // btnGestionCommandes
            // 
            this.btnGestionCommandes.Location = new System.Drawing.Point(410, 480);
            this.btnGestionCommandes.Name = "btnGestionCommandes";
            this.btnGestionCommandes.Size = new System.Drawing.Size(170, 40);
            this.btnGestionCommandes.TabIndex = 9;
            this.btnGestionCommandes.Text = "📦 Gestion Commandes";
            this.btnGestionCommandes.Click += new System.EventHandler(this.btnGestionCommandes_Click);
            // 
            // btnGestionClients
            // 
            this.btnGestionClients.Location = new System.Drawing.Point(410, 530);
            this.btnGestionClients.Name = "btnGestionClients";
            this.btnGestionClients.Size = new System.Drawing.Size(170, 40);
            this.btnGestionClients.TabIndex = 10;
            this.btnGestionClients.Text = "👥 Gestion Clients";
            this.btnGestionClients.Click += new System.EventHandler(this.btnGestionClients_Click);
            // 
            // btnRafraichir
            // 
            this.btnRafraichir.Location = new System.Drawing.Point(600, 530);
            this.btnRafraichir.Name = "btnRafraichir";
            this.btnRafraichir.Size = new System.Drawing.Size(170, 40);
            this.btnRafraichir.TabIndex = 11;
            this.btnRafraichir.Text = "🔄 Rafraîchir";
            this.btnRafraichir.Click += new System.EventHandler(this.btnRafraichir_Click);
            // 
            // DashboardPharmacien
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.lblStockBas);
            this.Controls.Add(this.dataGridViewStockBas);
            this.Controls.Add(this.lblCommandesRecentes);
            this.Controls.Add(this.dataGridViewCommandes);
            this.Controls.Add(this.lblMedicamentsPopulaires);
            this.Controls.Add(this.dataGridViewPopulaires);
            this.Controls.Add(this.btnGestionMedicaments);
            this.Controls.Add(this.btnGestionCommandes);
            this.Controls.Add(this.btnGestionClients);
            this.Controls.Add(this.btnRafraichir);
            this.Name = "DashboardPharmacien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Pharmacien";
            this.Load += new System.EventHandler(this.DashboardPharmacien_Load);
            this.panelStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockBas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPopulaires)).EndInit();
            // 
            // btnDeconnexion
            // 
            this.btnDeconnexion = new System.Windows.Forms.Button();
            this.btnDeconnexion.Location = new System.Drawing.Point(650, 20); // position en haut à droite
            this.btnDeconnexion.Name = "btnDeconnexion";
            this.btnDeconnexion.Size = new System.Drawing.Size(120, 32);
            this.btnDeconnexion.TabIndex = 12;
            this.btnDeconnexion.Text = "🔒 Déconnexion";
            this.btnDeconnexion.BackColor = Color.DarkRed;
            this.btnDeconnexion.ForeColor = Color.White;
            this.btnDeconnexion.FlatStyle = FlatStyle.Flat;
            this.btnDeconnexion.FlatAppearance.BorderSize = 0;
            this.btnDeconnexion.Click += new System.EventHandler(this.btnDeconnexion_Click);
            this.Controls.Add(this.btnDeconnexion);

            this.ResumeLayout(false);

        }

    }
}