using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MediaTek86.Modele;

namespace MediaTek86.Vue
{
    public partial class FrmAjouterAbsence : Form
    {
        private int idPersonnel;

        public FrmAjouterAbsence(int idPersonnel)
        {
            InitializeComponent();
            this.idPersonnel = idPersonnel;
            RemplirMotifs();

            
            btnValider.Click += btnValider_Click;
            btnAnnuler.Click += btnAnnuler_Click;
        }

        private void RemplirMotifs()
        {
            try
            {
                cboMotif.Items.Clear();
                BddManager.OpenConnection();

                MySqlCommand cmd = new MySqlCommand("SELECT idmotif, libelle FROM motif", BddManager.GetConnection());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string libelle = reader.GetString("libelle");
                    int id = reader.GetInt32("idmotif");
                    cboMotif.Items.Add(new ComboBoxItem(libelle, id));
                }

                reader.Close();
                BddManager.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des motifs : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (cboMotif.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un motif.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDebut.Value > dtpFin.Value)
            {
                MessageBox.Show("La date de début ne peut pas être après la date de fin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComboBoxItem motifItem = cboMotif.SelectedItem as ComboBoxItem;
            int idMotif = motifItem.Value;
            DateTime dateDebut = dtpDebut.Value.Date;
            DateTime dateFin = dtpFin.Value.Date;

            try
            {
                BddManager.OpenConnection();

                string req = "INSERT INTO absence (idpersonnel, idmotif, dateDebut, dateFin) VALUES (@idpersonnel, @idmotif, @dateDebut, @dateFin)";
                MySqlCommand cmd = new MySqlCommand(req, BddManager.GetConnection());
                cmd.Parameters.AddWithValue("@idpersonnel", idPersonnel);
                cmd.Parameters.AddWithValue("@idmotif", idMotif);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                cmd.ExecuteNonQuery();
                BddManager.CloseConnection();

                MessageBox.Show("Absence ajoutée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
