using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmAbsences : Form
    {
        public FrmAbsences()
        {
            InitializeComponent();
            ChargerPersonnel();
        }

        private void ChargerPersonnel()
        {
            try
            {
                cboPersonnel.Items.Clear();
                BddManager.OpenConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT idpersonnel, nom FROM personnel", BddManager.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nom = reader.GetString("nom");
                    int id = reader.GetInt32("idpersonnel");
                    cboPersonnel.Items.Add(new ComboBoxItem(nom, id));
                }

                reader.Close();
                BddManager.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des personnels : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AfficherAbsences(int idPersonnel)
        {
            try
            {
                BddManager.OpenConnection();

                string req = @"SELECT absence.idabsence, motif.libelle AS motif, dateDebut, dateFin 
                               FROM absence 
                               JOIN motif ON absence.idmotif = motif.idmotif 
                               WHERE idpersonnel = @id";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@id", idPersonnel);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAbsences.DataSource = dt;

                dgvAbsences.Columns[0].HeaderText = "ID";
                dgvAbsences.Columns[1].HeaderText = "Motif";
                dgvAbsences.Columns[2].HeaderText = "Début";
                dgvAbsences.Columns[3].HeaderText = "Fin";

                BddManager.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'affichage des absences : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboPersonnel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem selected = cboPersonnel.SelectedItem as ComboBoxItem;
            if (selected != null)
            {
                AfficherAbsences(selected.Value);
            }
        }

        private void btnAjouterAbsence_Click(object sender, EventArgs e)
        {
            ComboBoxItem selected = cboPersonnel.SelectedItem as ComboBoxItem;
            if (selected == null)
            {
                MessageBox.Show("Veuillez sélectionner un personnel avant d'ajouter une absence.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idPersonnel = selected.Value;

            FrmAjouterAbsence frmAjout = new FrmAjouterAbsence(idPersonnel);
            frmAjout.FormClosed += (s, args) => AfficherAbsences(idPersonnel);
            frmAjout.ShowDialog();
        }

        private void btnSupprimerAbsence_Click(object sender, EventArgs e)
        {
            if (dgvAbsences.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une absence à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette absence ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            int idAbsence = Convert.ToInt32(dgvAbsences.SelectedRows[0].Cells["idabsence"].Value);

            try
            {
                BddManager.OpenConnection();

                string req = "DELETE FROM absence WHERE idabsence = @id";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@id", idAbsence);
                cmd.ExecuteNonQuery();

                BddManager.CloseConnection();

                ComboBoxItem selected = cboPersonnel.SelectedItem as ComboBoxItem;
                if (selected != null)
                {
                    AfficherAbsences(selected.Value);
                }

                MessageBox.Show("Absence supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression de l'absence : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAbsences_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Inutilisé
        }

        private void lblPersonnel_Click(object sender, EventArgs e)
        {
            // Inutilisé
        }
    }
}
