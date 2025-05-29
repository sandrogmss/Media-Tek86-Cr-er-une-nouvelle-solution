using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmPersonnel : Form
    {
        public FrmPersonnel()
        {
            InitializeComponent();
            ChargerPersonnel();

            // Abonnement manuel aux événements (au cas où ce n’est pas fait dans le Designer)
            btnAjouterPersonnel.Click += btnAjouterPersonnel_Click;
            btnModifierPersonnel.Click += btnModifierPersonnel_Click;
            btnSupprimerPersonnel.Click += btnSupprimerPersonnel_Click;
        }

        public void ChargerPersonnel()
        {
            try
            {
                BddManager.OpenConnection();

                string query = "SELECT idpersonnel, nom, prenom, tel, mail, idservice FROM personnel";
                MySqlCommand cmd = new MySqlCommand(query, BddManager.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvPersonnel.DataSource = dt;

                dgvPersonnel.Columns[0].HeaderText = "ID";
                dgvPersonnel.Columns[1].HeaderText = "Nom";
                dgvPersonnel.Columns[2].HeaderText = "Prénom";
                dgvPersonnel.Columns[3].HeaderText = "Téléphone";
                dgvPersonnel.Columns[4].HeaderText = "Email";
                dgvPersonnel.Columns[5].HeaderText = "Service";

                BddManager.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement du personnel : " + ex.Message);
            }
        }

        private void btnAjouterPersonnel_Click(object sender, EventArgs e)
        {
            FrmAjoutPersonnel frmAjout = new FrmAjoutPersonnel();
            frmAjout.FormClosed += (s, args) => ChargerPersonnel();
            frmAjout.ShowDialog();
        }

        private void btnSupprimerPersonnel_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un personnel à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce personnel ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            int idPersonnel = Convert.ToInt32(dgvPersonnel.SelectedRows[0].Cells["idpersonnel"].Value);

            try
            {
                BddManager.OpenConnection();

                string req = "DELETE FROM personnel WHERE idpersonnel = @id";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@id", idPersonnel);
                cmd.ExecuteNonQuery();

                BddManager.CloseConnection();

                MessageBox.Show("Personnel supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ChargerPersonnel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un personnel à modifier.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvPersonnel.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["idpersonnel"].Value);
            string nom = row.Cells["nom"].Value.ToString();
            string prenom = row.Cells["prenom"].Value.ToString();
            string tel = row.Cells["tel"].Value.ToString();
            string mail = row.Cells["mail"].Value.ToString();
            int idService = Convert.ToInt32(row.Cells["idservice"].Value);

            FrmModifierPersonnel frmModif = new FrmModifierPersonnel(id, nom, prenom, tel, mail, idService);
            frmModif.FormClosed += (s, args) => ChargerPersonnel();
            frmModif.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Bouton inutilisé
        }
    }
}
