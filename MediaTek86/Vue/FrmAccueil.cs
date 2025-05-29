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

        
        private void btnPersonnel_Click(object sender, EventArgs e)
        {
            FrmPersonnel frm = new FrmPersonnel();
            frm.ShowDialog();
        }

        
        private void btnAbsences_Click(object sender, EventArgs e)
        {
            FrmAbsences frm = new FrmAbsences();
            frm.ShowDialog();
        }

        
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
