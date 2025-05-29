using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmModifierPersonnel : Form
    {
        private int idPersonnel;

        public FrmModifierPersonnel(int id, string nom, string prenom, string tel, string mail, int idService)
        {
            InitializeComponent();
            idPersonnel = id;

            btnValider.Click += btnValider_Click;
            btnAnnuler.Click += btnAnnuler_Click;

            RemplirServices();

            txtNom.Text = nom;
            txtPrenom.Text = prenom;
            txtTel.Text = tel;
            txtMail.Text = mail;

            foreach (ComboBoxItem item in cboService.Items)
            {
                if (item.Value == idService)
                {
                    cboService.SelectedItem = item;
                    break;
                }
            }
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

            if (selectedService == null || string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(mail))
            {
                MessageBox.Show("Tous les champs sont obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BddManager.OpenConnection();

                string req = "UPDATE personnel SET nom = @nom, prenom = @prenom, tel = @tel, mail = @mail, idService = @idService WHERE idpersonnel = @id";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@tel", tel);
                cmd.Parameters.AddWithValue("@mail", mail);
                cmd.Parameters.AddWithValue("@idService", selectedService.Value);
                cmd.Parameters.AddWithValue("@id", idPersonnel);

                cmd.ExecuteNonQuery();
                BddManager.CloseConnection();

                MessageBox.Show("Personnel modifié avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
