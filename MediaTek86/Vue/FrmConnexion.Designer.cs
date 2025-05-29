namespace MediaTek86.Vue
{
    partial class FrmConnexion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.Identifiant = new System.Windows.Forms.Label();
            this.MDP = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.btnConnexion = new System.Windows.Forms.Button();
            this.txtMdp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Identifiant
            // 
            this.Identifiant.AutoSize = true;
            this.Identifiant.Location = new System.Drawing.Point(61, 189);
            this.Identifiant.Name = "Identifiant";
            this.Identifiant.Size = new System.Drawing.Size(63, 16);
            this.Identifiant.TabIndex = 0;
            this.Identifiant.Text = "Identifiant";
            // 
            // MDP
            // 
            this.MDP.AutoSize = true;
            this.MDP.Location = new System.Drawing.Point(256, 189);
            this.MDP.Name = "MDP";
            this.MDP.Size = new System.Drawing.Size(89, 16);
            this.MDP.TabIndex = 1;
            this.MDP.Text = "Mot de passe";
            // (❌ supprimé : this.MDP.Click += new System.EventHandler(this.label2_Click);)
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(130, 186);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(100, 22);
            this.txtLogin.TabIndex = 2;
            // 
            // btnConnexion
            // 
            this.btnConnexion.AccessibleName = "";
            this.btnConnexion.Location = new System.Drawing.Point(83, 239);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(116, 23);
            this.btnConnexion.TabIndex = 3;
            this.btnConnexion.Text = "Se connecter";
            this.btnConnexion.UseVisualStyleBackColor = true;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            // 
            // txtMdp
            // 
            this.txtMdp.Location = new System.Drawing.Point(351, 186);
            this.txtMdp.Name = "txtMdp";
            this.txtMdp.Size = new System.Drawing.Size(100, 22);
            this.txtMdp.TabIndex = 4;
            this.txtMdp.UseSystemPasswordChar = true;
            // 
            // FrmConnexion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMdp);
            this.Controls.Add(this.btnConnexion);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.MDP);
            this.Controls.Add(this.Identifiant);
            this.Name = "FrmConnexion";
            this.Text = "FrmConnexion";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Identifiant;
        private System.Windows.Forms.Label MDP;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.TextBox txtMdp;
    }
}
