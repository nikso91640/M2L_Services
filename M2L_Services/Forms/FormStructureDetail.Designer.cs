namespace M2L_Services.Forms
{
    partial class FormStructureDetail
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
            // 1. Instancier les contrôles
            _tlpMain    = new System.Windows.Forms.TableLayoutPanel();
            _lblNom     = new System.Windows.Forms.Label();
            _txtNom     = new System.Windows.Forms.TextBox();
            _lblType    = new System.Windows.Forms.Label();
            _cboType    = new System.Windows.Forms.ComboBox();
            _flpBoutons = new System.Windows.Forms.FlowLayoutPanel();
            _btnValider = new System.Windows.Forms.Button();
            _btnAnnuler = new System.Windows.Forms.Button();

            // 2. Suspendre le rendu
            _tlpMain.SuspendLayout();
            _flpBoutons.SuspendLayout();
            SuspendLayout();

            // _tlpMain — 2 colonnes, 3 lignes
            _tlpMain.ColumnCount = 2;
            _tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            _tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            _tlpMain.RowCount = 3;
            _tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            _tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            _tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            _tlpMain.Dock    = System.Windows.Forms.DockStyle.Fill;
            _tlpMain.Padding = new System.Windows.Forms.Padding(12);
            _tlpMain.Name    = "_tlpMain";
            _tlpMain.Controls.Add(_lblNom,     0, 0);
            _tlpMain.Controls.Add(_txtNom,     1, 0);
            _tlpMain.Controls.Add(_lblType,    0, 1);
            _tlpMain.Controls.Add(_cboType,    1, 1);
            _tlpMain.Controls.Add(_flpBoutons, 1, 2);

            // _lblNom
            _lblNom.Text   = "Nom :";
            _lblNom.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _lblNom.Margin = new System.Windows.Forms.Padding(0, 5, 10, 6);
            _lblNom.Name   = "_lblNom";

            // _txtNom
            _txtNom.Dock      = System.Windows.Forms.DockStyle.Fill;
            _txtNom.MaxLength = 150;
            _txtNom.Margin    = new System.Windows.Forms.Padding(0, 3, 0, 6);
            _txtNom.Name      = "_txtNom";

            // _lblType
            _lblType.Text   = "Type :";
            _lblType.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _lblType.Margin = new System.Windows.Forms.Padding(0, 5, 10, 6);
            _lblType.Name   = "_lblType";

            // _cboType
            _cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _cboType.Dock          = System.Windows.Forms.DockStyle.Fill;
            _cboType.Margin        = new System.Windows.Forms.Padding(0, 3, 0, 6);
            _cboType.Name          = "_cboType";
            _cboType.Items.Add("LIGUE");
            _cboType.Items.Add("CLUB");
            _cboType.Items.Add("ASSOCIATION");
            _cboType.Items.Add("LYCEE_COLLEGE");
            _cboType.Items.Add("ENTREPRISE");
            _cboType.Items.Add("AUTRE");
            _cboType.SelectedIndex = 0;

            // _flpBoutons
            _flpBoutons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            _flpBoutons.Dock          = System.Windows.Forms.DockStyle.Fill;
            _flpBoutons.AutoSize      = true;
            _flpBoutons.AutoSizeMode  = System.Windows.Forms.AutoSizeMode.GrowOnly;
            _flpBoutons.Margin        = new System.Windows.Forms.Padding(0, 10, 0, 0);
            _flpBoutons.Name          = "_flpBoutons";
            _flpBoutons.Controls.Add(_btnAnnuler);
            _flpBoutons.Controls.Add(_btnValider);

            // _btnValider
            _btnValider.Text         = "Valider";
            _btnValider.Size         = new System.Drawing.Size(100, 30);
            _btnValider.Margin       = new System.Windows.Forms.Padding(0, 0, 5, 0);
            _btnValider.DialogResult = System.Windows.Forms.DialogResult.None;
            _btnValider.Name         = "_btnValider";
            _btnValider.Click       += BtnValider_Click;

            // _btnAnnuler
            _btnAnnuler.Text         = "Annuler";
            _btnAnnuler.Size         = new System.Drawing.Size(100, 30);
            _btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            _btnAnnuler.Name         = "_btnAnnuler";

            // 5. Configurer le formulaire
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(420, 180);
            FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox         = false;
            MinimizeBox         = false;
            AcceptButton        = _btnValider;
            CancelButton        = _btnAnnuler;
            Controls.Add(_tlpMain);
            Name          = "FormStructureDetail";
            Text          = "D\u00E9tail d'une structure";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // 6. Reprendre le rendu
            _flpBoutons.ResumeLayout(false);
            _tlpMain.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.TableLayoutPanel _tlpMain;
        private System.Windows.Forms.Label            _lblNom;
        private System.Windows.Forms.TextBox          _txtNom;
        private System.Windows.Forms.Label            _lblType;
        private System.Windows.Forms.ComboBox         _cboType;
        private System.Windows.Forms.FlowLayoutPanel  _flpBoutons;
        private System.Windows.Forms.Button           _btnValider;
        private System.Windows.Forms.Button           _btnAnnuler;
    }
}
