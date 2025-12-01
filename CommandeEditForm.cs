using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments.Clients
{
    public partial class CommandeEditForm : Form
    {
        public enum EditMode { Create, Edit }
        public EditMode Mode { get; set; } = EditMode.Create;
        public int IdCommande { get; set; } = -1;

        public CommandeEditForm()
        {
            InitializeComponent();
            LoadClients();
            cbStatut.Items.AddRange(new string[] { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" });
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
                    SqlCommand cmd = new SqlCommand("SELECT id_commande, id_utilisateur, date_commande, statut FROM Commande WHERE id_commande=@id", conn);
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

                        int clientId = Convert.ToInt32(reader["id_utilisateur"]);
                        for (int i = 0; i < cbClients.Items.Count; i++)
                        {
                            if (((ComboboxItem)cbClients.Items[i]).Value.Equals(clientId))
                            {
                                cbClients.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de la commande : " + ex.Message);
            }
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

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd;
                    if (Mode == EditMode.Create)
                    {
                        cmd = new SqlCommand("INSERT INTO Commande (id_utilisateur, date_commande, statut) VALUES (@client, @date, @statut)", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("UPDATE Commande SET id_utilisateur=@client, date_commande=@date, statut=@statut WHERE id_commande=@id", conn);
                        cmd.Parameters.AddWithValue("@id", IdCommande);
                    }

                    cmd.Parameters.AddWithValue("@client", ((ComboboxItem)cbClients.SelectedItem).Value);
                    cmd.Parameters.AddWithValue("@date", dtpDate.Value);
                    cmd.Parameters.AddWithValue("@statut", cbStatut.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
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