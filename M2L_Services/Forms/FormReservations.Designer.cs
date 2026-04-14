namespace M2L_Services.Forms
{
    partial class FormReservations
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
            _tsBoutons         = new System.Windows.Forms.ToolStrip();
            _btnAjouter        = new System.Windows.Forms.ToolStripButton();
            _btnModifier       = new System.Windows.Forms.ToolStripButton();
            _sep1              = new System.Windows.Forms.ToolStripSeparator();
            _btnAnnulerRes     = new System.Windows.Forms.ToolStripButton();
            _btnSupprimer      = new System.Windows.Forms.ToolStripButton();
            _sep2              = new System.Windows.Forms.ToolStripSeparator();
            _btnActualiser     = new System.Windows.Forms.ToolStripButton();
            _sepFiltre1        = new System.Windows.Forms.ToolStripSeparator();
            _lblFiltreStatut   = new System.Windows.Forms.ToolStripLabel();
            _cboFiltreStatut   = new System.Windows.Forms.ToolStripComboBox();
            _lblRecherche      = new System.Windows.Forms.ToolStripLabel();
            _txtRecherche      = new System.Windows.Forms.ToolStripTextBox();
            _dgvReservations   = new System.Windows.Forms.DataGridView();

            // 2. Suspendre le rendu
            _tsBoutons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvReservations).BeginInit();
            SuspendLayout();

            // _tsBoutons
            _tsBoutons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            _tsBoutons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                _btnAjouter,
                _btnModifier,
                _sep1,
                _btnAnnulerRes,
                _btnSupprimer,
                _sep2,
                _btnActualiser,
                _sepFiltre1,
                _lblFiltreStatut,
                _cboFiltreStatut,
                _lblRecherche,
                _txtRecherche
            });
            _tsBoutons.Location = new System.Drawing.Point(0, 0);
            _tsBoutons.Name     = "_tsBoutons";
            _tsBoutons.Padding  = new System.Windows.Forms.Padding(4, 2, 4, 2);
            _tsBoutons.Size     = new System.Drawing.Size(920, 31);
            _tsBoutons.TabIndex = 0;

            // _btnAjouter
            _btnAjouter.Name         = "_btnAjouter";
            _btnAjouter.Text         = "Ajouter";
            _btnAjouter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _btnAjouter.Click       += BtnAjouter_Click;

            // _btnModifier
            _btnModifier.Name         = "_btnModifier";
            _btnModifier.Text         = "Modifier";
            _btnModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _btnModifier.Click       += BtnModifier_Click;

            // _sep1
            _sep1.Name = "_sep1";

            // _btnAnnulerRes
            _btnAnnulerRes.Name         = "_btnAnnulerRes";
            _btnAnnulerRes.Text         = "Annuler r\u00E9serv.";
            _btnAnnulerRes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _btnAnnulerRes.ForeColor    = System.Drawing.Color.DarkOrange;
            _btnAnnulerRes.Click       += BtnAnnulerRes_Click;

            // _btnSupprimer
            _btnSupprimer.Name         = "_btnSupprimer";
            _btnSupprimer.Text         = "Supprimer";
            _btnSupprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _btnSupprimer.ForeColor    = System.Drawing.Color.DarkRed;
            _btnSupprimer.Click       += BtnSupprimer_Click;

            // _sep2
            _sep2.Name = "_sep2";

            // _btnActualiser
            _btnActualiser.Name         = "_btnActualiser";
            _btnActualiser.Text         = "Actualiser";
            _btnActualiser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _btnActualiser.Click       += BtnActualiser_Click;

            // _sepFiltre1
            _sepFiltre1.Name = "_sepFiltre1";

            // _lblFiltreStatut
            _lblFiltreStatut.Name = "_lblFiltreStatut";
            _lblFiltreStatut.Text = "Statut :";

            // _cboFiltreStatut
            _cboFiltreStatut.Name = "_cboFiltreStatut";
            _cboFiltreStatut.Size = new System.Drawing.Size(110, 27);
            _cboFiltreStatut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _cboFiltreStatut.Items.AddRange(new object[] { "TOUS", "EN_ATTENTE", "CONFIRMEE", "ANNULEE" });
            _cboFiltreStatut.SelectedIndex = 0;
            _cboFiltreStatut.SelectedIndexChanged += CboFiltreStatut_SelectedIndexChanged;

            // _lblRecherche
            _lblRecherche.Name = "_lblRecherche";
            _lblRecherche.Text = "Recherche :";

            // _txtRecherche
            _txtRecherche.Name = "_txtRecherche";
            _txtRecherche.Size = new System.Drawing.Size(180, 27);
            _txtRecherche.ToolTipText = "Filtrer par salle ou structure";
            _txtRecherche.TextChanged += TxtRecherche_TextChanged;

            // _dgvReservations
            _dgvReservations.Dock                 = System.Windows.Forms.DockStyle.Fill;
            _dgvReservations.AllowUserToAddRows    = false;
            _dgvReservations.AllowUserToDeleteRows = false;
            _dgvReservations.AllowUserToResizeRows = false;
            _dgvReservations.ReadOnly              = true;
            _dgvReservations.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _dgvReservations.MultiSelect           = false;
            _dgvReservations.RowHeadersVisible     = false;
            _dgvReservations.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _dgvReservations.Name                  = "_dgvReservations";
            _dgvReservations.CellDoubleClick      += DgvReservations_CellDoubleClick;

            // 5. Configurer le formulaire
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(920, 580);
            MinimumSize         = new System.Drawing.Size(720, 450);
            Controls.Add(_dgvReservations);
            Controls.Add(_tsBoutons);
            Name          = "FormReservations";
            Text          = "M2L Services \u2014 Gestion des r\u00E9servations";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // 6. Reprendre le rendu
            _tsBoutons.ResumeLayout(false);
            _tsBoutons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvReservations).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.ToolStrip          _tsBoutons;
        private System.Windows.Forms.ToolStripButton    _btnAjouter;
        private System.Windows.Forms.ToolStripButton    _btnModifier;
        private System.Windows.Forms.ToolStripSeparator _sep1;
        private System.Windows.Forms.ToolStripButton    _btnAnnulerRes;
        private System.Windows.Forms.ToolStripButton    _btnSupprimer;
        private System.Windows.Forms.ToolStripSeparator _sep2;
        private System.Windows.Forms.ToolStripButton    _btnActualiser;
        private System.Windows.Forms.ToolStripSeparator _sepFiltre1;
        private System.Windows.Forms.ToolStripLabel     _lblFiltreStatut;
        private System.Windows.Forms.ToolStripComboBox  _cboFiltreStatut;
        private System.Windows.Forms.ToolStripLabel     _lblRecherche;
        private System.Windows.Forms.ToolStripTextBox   _txtRecherche;
        private System.Windows.Forms.DataGridView       _dgvReservations;
    }
}
