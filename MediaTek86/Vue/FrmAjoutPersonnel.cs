using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmAjoutPersonnel : Form
    {
        public FrmAjoutPersonnel()
        {
            InitializeComponent();
            RemplirServices();
        }

        private void RemplirServices()
        {
            try
            {
                cboService.Items.Clear();
                BddManager.OpenConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM service", BddManager.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nomService = reader.GetString("nom");
                    int idService = reader.GetInt32("idService");
                    cboService.Items.Add(new ComboBoxItem(nomService, idService));
                }

                reader.Close();
                BddManager.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des services : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            string nom = txtNom.Text.Trim();
            string prenom = txtPrenom.Text.Trim();
            string tel = txtTel.Text.Trim();
            string mail = txtMail.Text.Trim();
            ComboBoxItem selectedService = cboService.SelectedItem as ComboBoxItem;
            string idService = selectedService?.Value.ToString();

            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(idService))
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BddManager.OpenConnection();

                string req = "INSERT INTO personnel (nom, prenom, tel, mail, idService) VALUES (@nom, @prenom, @tel, @mail, @idService)";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Parameters.AddWithValue("@mail", mail);
                cmd.Parameters.AddWithValue("@idService", idService);

                cmd.ExecuteNonQuery();
                BddManager.CloseConnection();

                MessageBox.Show("Personnel ajouté avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
