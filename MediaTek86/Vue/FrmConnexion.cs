using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmConnexion : Form
    {
        public FrmConnexion()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string mdp = txtMdp.Text.Trim();

            try
            {
                BddManager.OpenConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM utilisateur WHERE login=@login AND mdp=@mdp", BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@mdp", mdp);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    BddManager.CloseConnection();

                    MessageBox.Show("Connexion réussie !");

                    
                    FrmAccueil frmAccueil = new FrmAccueil();
                    frmAccueil.Show();

                    this.Hide(); 
                }
                else
                {
                    reader.Close();
                    BddManager.CloseConnection();

                    MessageBox.Show("Identifiants incorrects.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
    }
}
