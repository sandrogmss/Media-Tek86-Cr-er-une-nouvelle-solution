using System;
using System.Windows.Forms;

namespace MediaTek86.Vue
{
    public partial class FrmAccueil : Form
    {
        public FrmAccueil()
        {
            InitializeComponent();
        }

        // Ouvre le formulaire de gestion du personnel
        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            FrmPersonnel frm = new FrmPersonnel();
            frm.ShowDialog();
        }

        // Ouvre le formulaire de gestion des absences
        private void btnAbsences_Click(object sender, EventArgs e)
        {
            FrmAbsences frm = new FrmAbsences();
            frm.ShowDialog();
        }

        // Ferme l'application
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
