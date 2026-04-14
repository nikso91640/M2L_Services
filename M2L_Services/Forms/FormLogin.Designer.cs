namespace M2L_Services.Forms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            _tlpMain = new TableLayoutPanel();
            _lblTitre = new Label();
            _lblLogin = new Label();
            _txtLogin = new TextBox();
            _lblMotDePasse = new Label();
            _txtMotDePasse = new TextBox();
            _chkAfficherMotDePasse = new CheckBox();
            _btnConnecter = new Button();
            _lblTentatives = new Label();
            _lblErreur = new Label();
            _tlpMain.SuspendLayout();
            SuspendLayout();
            // 
            // _tlpMain
            // 
            _tlpMain.ColumnCount = 2;
            _tlpMain.ColumnStyles.Add(new ColumnStyle());
            _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpMain.Controls.Add(_lblTitre, 0, 0);
            _tlpMain.Controls.Add(_lblLogin, 0, 1);
            _tlpMain.Controls.Add(_txtLogin, 1, 1);
            _tlpMain.Controls.Add(_lblMotDePasse, 0, 2);
            _tlpMain.Controls.Add(_txtMotDePasse, 1, 2);
            _tlpMain.Controls.Add(_chkAfficherMotDePasse, 0, 3);
            _tlpMain.Controls.Add(_btnConnecter, 0, 4);
            _tlpMain.Controls.Add(_lblTentatives, 0, 5);
            _tlpMain.Controls.Add(_lblErreur, 0, 6);
            _tlpMain.Dock = DockStyle.Fill;
            _tlpMain.Location = new Point(0, 0);
            _tlpMain.Margin = new Padding(6);
            _tlpMain.Name = "_tlpMain";
            _tlpMain.Padding = new Padding(37, 43, 37, 21);
            _tlpMain.RowCount = 7;
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.Size = new Size(799, 640);
            _tlpMain.TabIndex = 0;
            // 
            // _lblTitre
            // 
            _tlpMain.SetColumnSpan(_lblTitre, 2);
            _lblTitre.Dock = DockStyle.Fill;
            _lblTitre.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblTitre.Location = new Point(37, 43);
            _lblTitre.Margin = new Padding(0, 0, 0, 43);
            _lblTitre.Name = "_lblTitre";
            _lblTitre.Size = new Size(725, 49);
            _lblTitre.TabIndex = 0;
            _lblTitre.Text = "M2L Services — Connexion";
            _lblTitre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblLogin
            // 
            _lblLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblLogin.Location = new Point(37, 144);
            _lblLogin.Margin = new Padding(0, 9, 19, 17);
            _lblLogin.Name = "_lblLogin";
            _lblLogin.Size = new Size(186, 49);
            _lblLogin.TabIndex = 1;
            _lblLogin.Text = "Login :";
            // 
            // _txtLogin
            // 
            _txtLogin.Dock = DockStyle.Fill;
            _txtLogin.Location = new Point(242, 135);
            _txtLogin.Margin = new Padding(0, 0, 0, 17);
            _txtLogin.Name = "_txtLogin";
            _txtLogin.Size = new Size(520, 39);
            _txtLogin.TabIndex = 2;
            _txtLogin.TextChanged += _txtLogin_TextChanged;
            // 
            // _lblMotDePasse
            // 
            _lblMotDePasse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblMotDePasse.Location = new Point(37, 219);
            _lblMotDePasse.Margin = new Padding(0, 9, 19, 17);
            _lblMotDePasse.Name = "_lblMotDePasse";
            _lblMotDePasse.Size = new Size(186, 49);
            _lblMotDePasse.TabIndex = 3;
            _lblMotDePasse.Text = "Mot de passe :";
            // 
            // _txtMotDePasse
            // 
            _txtMotDePasse.Dock = DockStyle.Fill;
            _txtMotDePasse.Location = new Point(242, 210);
            _txtMotDePasse.Margin = new Padding(0, 0, 0, 17);
            _txtMotDePasse.Name = "_txtMotDePasse";
            _txtMotDePasse.PasswordChar = '*';
            _txtMotDePasse.Size = new Size(520, 39);
            _txtMotDePasse.TabIndex = 4;
            // 
            // _chkAfficherMotDePasse
            // 
            _chkAfficherMotDePasse.AutoSize = true;
            _tlpMain.SetColumnSpan(_chkAfficherMotDePasse, 2);
            _chkAfficherMotDePasse.Location = new Point(37, 285);
            _chkAfficherMotDePasse.Margin = new Padding(0, 0, 0, 17);
            _chkAfficherMotDePasse.Name = "_chkAfficherMotDePasse";
            _chkAfficherMotDePasse.Size = new Size(305, 36);
            _chkAfficherMotDePasse.TabIndex = 5;
            _chkAfficherMotDePasse.Text = "Afficher le mot de passe";
            _chkAfficherMotDePasse.CheckedChanged += ChkAfficherMotDePasse_CheckedChanged;
            // 
            // _btnConnecter
            // 
            _tlpMain.SetColumnSpan(_btnConnecter, 2);
            _btnConnecter.Dock = DockStyle.Fill;
            _btnConnecter.Location = new Point(37, 351);
            _btnConnecter.Margin = new Padding(0, 13, 0, 17);
            _btnConnecter.Name = "_btnConnecter";
            _btnConnecter.Size = new Size(725, 75);
            _btnConnecter.TabIndex = 6;
            _btnConnecter.Text = "Se connecter";
            _btnConnecter.Click += BtnConnecter_Click;
            // 
            // _lblTentatives
            // 
            _tlpMain.SetColumnSpan(_lblTentatives, 2);
            _lblTentatives.Dock = DockStyle.Fill;
            _lblTentatives.ForeColor = Color.DimGray;
            _lblTentatives.Location = new Point(43, 443);
            _lblTentatives.Margin = new Padding(6, 0, 6, 0);
            _lblTentatives.Name = "_lblTentatives";
            _lblTentatives.Size = new Size(713, 49);
            _lblTentatives.TabIndex = 7;
            _lblTentatives.Text = "Tentatives restantes : 3";
            _lblTentatives.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblErreur
            // 
            _tlpMain.SetColumnSpan(_lblErreur, 2);
            _lblErreur.Dock = DockStyle.Fill;
            _lblErreur.ForeColor = Color.Red;
            _lblErreur.Location = new Point(43, 492);
            _lblErreur.Margin = new Padding(6, 0, 6, 0);
            _lblErreur.Name = "_lblErreur";
            _lblErreur.Size = new Size(713, 127);
            _lblErreur.TabIndex = 8;
            _lblErreur.TextAlign = ContentAlignment.MiddleCenter;
            _lblErreur.Click += _lblErreur_Click;
            // 
            // FormLogin
            // 
            AcceptButton = _btnConnecter;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 640);
            Controls.Add(_tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "M2L Services — Connexion";
            _tlpMain.ResumeLayout(false);
            _tlpMain.PerformLayout();
            ResumeLayout(false);
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.TableLayoutPanel _tlpMain;
        private System.Windows.Forms.Label            _lblTitre;
        private System.Windows.Forms.Label            _lblLogin;
        private System.Windows.Forms.TextBox          _txtLogin;
        private System.Windows.Forms.Label            _lblMotDePasse;
        private System.Windows.Forms.TextBox          _txtMotDePasse;
        private System.Windows.Forms.CheckBox         _chkAfficherMotDePasse;
        private System.Windows.Forms.Button           _btnConnecter;
        private System.Windows.Forms.Label            _lblTentatives;
        private System.Windows.Forms.Label            _lblErreur;
    }
}
