namespace M2L_Services.Forms
{
    partial class FormStructures
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
            _tsBoutons     = new System.Windows.Forms.ToolStrip();
            _btnAjouter    = new System.Windows.Forms.ToolStripButton();
            _btnModifier   = new System.Windows.Forms.ToolStripButton();
            _sep1          = new System.Windows.Forms.ToolStripSeparator();
            _btnSupprimer  = new System.Windows.Forms.ToolStripButton();
            _sep2          = new System.Windows.Forms.ToolStripSeparator();
            _btnActualiser = new System.Windows.Forms.ToolStripButton();
            _sepRecherche  = new System.Windows.Forms.ToolStripSeparator();
            _lblRecherche  = new System.Windows.Forms.ToolStripLabel();
            _txtRecherche  = new System.Windows.Forms.ToolStripTextBox();
            _dgvStructures = new System.Windows.Forms.DataGridView();

            // 2. Suspendre le rendu
            _tsBoutons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvStructures).BeginInit();
            SuspendLayout();

            // _tsBoutons
            _tsBoutons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            _tsBoutons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                _btnAjouter,
                _btnModifier,
                _sep1,
                _btnSupprimer,
                _sep2,
                _btnActualiser,
                _sepRecherche,
                _lblRecherche,
                _txtRecherche
            });
            _tsBoutons.Location = new System.Drawing.Point(0, 0);
            _tsBoutons.Name     = "_tsBoutons";
            _tsBoutons.Padding  = new System.Windows.Forms.Padding(4, 2, 4, 2);
            _tsBoutons.Size     = new System.Drawing.Size(750, 31);
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

            // _sepRecherche
            _sepRecherche.Name = "_sepRecherche";

            // _lblRecherche
            _lblRecherche.Name = "_lblRecherche";
            _lblRecherche.Text = "Recherche :";

            // _txtRecherche
            _txtRecherche.Name = "_txtRecherche";
            _txtRecherche.Size = new System.Drawing.Size(180, 27);
            _txtRecherche.ToolTipText = "Filtrer par nom ou type";
            _txtRecherche.TextChanged += TxtRecherche_TextChanged;

            // _dgvStructures
            _dgvStructures.Dock                 = System.Windows.Forms.DockStyle.Fill;
            _dgvStructures.AllowUserToAddRows    = false;
            _dgvStructures.AllowUserToDeleteRows = false;
            _dgvStructures.AllowUserToResizeRows = false;
            _dgvStructures.ReadOnly              = true;
            _dgvStructures.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _dgvStructures.MultiSelect           = false;
            _dgvStructures.RowHeadersVisible     = false;
            _dgvStructures.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            _dgvStructures.Name                  = "_dgvStructures";
            _dgvStructures.CellDoubleClick      += DgvStructures_CellDoubleClick;

            // 5. Configurer le formulaire
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(750, 500);
            MinimumSize         = new System.Drawing.Size(600, 400);
            Controls.Add(_dgvStructures);
            Controls.Add(_tsBoutons);
            Name          = "FormStructures";
            Text          = "M2L Services \u2014 Gestion des structures";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // 6. Reprendre le rendu
            _tsBoutons.ResumeLayout(false);
            _tsBoutons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvStructures).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.ToolStrip          _tsBoutons;
        private System.Windows.Forms.ToolStripButton    _btnAjouter;
        private System.Windows.Forms.ToolStripButton    _btnModifier;
        private System.Windows.Forms.ToolStripSeparator _sep1;
        private System.Windows.Forms.ToolStripButton    _btnSupprimer;
        private System.Windows.Forms.ToolStripSeparator _sep2;
        private System.Windows.Forms.ToolStripButton    _btnActualiser;
        private System.Windows.Forms.ToolStripSeparator _sepRecherche;
        private System.Windows.Forms.ToolStripLabel     _lblRecherche;
        private System.Windows.Forms.ToolStripTextBox   _txtRecherche;
        private System.Windows.Forms.DataGridView       _dgvStructures;
    }
}
