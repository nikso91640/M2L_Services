namespace M2L_Services.Forms
{
    partial class FormReservationDetail
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
            _lblDate = new Label();
            _dtpDate = new DateTimePicker();
            _lblSalle = new Label();
            _cboSalle = new ComboBox();
            _lblStructure = new Label();
            _cboStructure = new ComboBox();
            _lblHeureDebut = new Label();
            _cboHeureDebut = new ComboBox();
            _lblHeureFin = new Label();
            _cboHeureFin = new ComboBox();
            _lblStatut = new Label();
            _cboStatut = new ComboBox();
            _lblCommentaire = new Label();
            _txtCommentaire = new TextBox();
            _flpBoutons = new FlowLayoutPanel();
            _btnAnnuler = new Button();
            _btnValider = new Button();
            _tlpMain.SuspendLayout();
            _flpBoutons.SuspendLayout();
            SuspendLayout();
            // 
            // _tlpMain
            // 
            _tlpMain.ColumnCount = 2;
            _tlpMain.ColumnStyles.Add(new ColumnStyle());
            _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpMain.Controls.Add(_lblDate, 0, 0);
            _tlpMain.Controls.Add(_dtpDate, 1, 0);
            _tlpMain.Controls.Add(_lblSalle, 0, 1);
            _tlpMain.Controls.Add(_cboSalle, 1, 1);
            _tlpMain.Controls.Add(_lblStructure, 0, 2);
            _tlpMain.Controls.Add(_cboStructure, 1, 2);
            _tlpMain.Controls.Add(_lblHeureDebut, 0, 3);
            _tlpMain.Controls.Add(_cboHeureDebut, 1, 3);
            _tlpMain.Controls.Add(_lblHeureFin, 0, 4);
            _tlpMain.Controls.Add(_cboHeureFin, 1, 4);
            _tlpMain.Controls.Add(_lblStatut, 0, 5);
            _tlpMain.Controls.Add(_cboStatut, 1, 5);
            _tlpMain.Controls.Add(_lblCommentaire, 0, 6);
            _tlpMain.Controls.Add(_txtCommentaire, 1, 6);
            _tlpMain.Controls.Add(_flpBoutons, 1, 7);
            _tlpMain.Dock = DockStyle.Fill;
            _tlpMain.Location = new Point(0, 0);
            _tlpMain.Margin = new Padding(6);
            _tlpMain.Name = "_tlpMain";
            _tlpMain.Padding = new Padding(22, 26, 22, 26);
            _tlpMain.RowCount = 8;
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.Size = new Size(891, 896);
            _tlpMain.TabIndex = 0;
            // 
            // _lblDate
            // 
            _lblDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblDate.Location = new Point(22, 37);
            _lblDate.Margin = new Padding(0, 11, 19, 13);
            _lblDate.Name = "_lblDate";
            _lblDate.Size = new Size(186, 49);
            _lblDate.TabIndex = 0;
            _lblDate.Text = "Date :";
            // 
            // _dtpDate
            // 
            _dtpDate.Dock = DockStyle.Fill;
            _dtpDate.Format = DateTimePickerFormat.Short;
            _dtpDate.Location = new Point(227, 32);
            _dtpDate.Margin = new Padding(0, 6, 0, 13);
            _dtpDate.Name = "_dtpDate";
            _dtpDate.Size = new Size(642, 39);
            _dtpDate.TabIndex = 1;
            // 
            // _lblSalle
            // 
            _lblSalle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblSalle.Location = new Point(22, 110);
            _lblSalle.Margin = new Padding(0, 11, 19, 13);
            _lblSalle.Name = "_lblSalle";
            _lblSalle.Size = new Size(186, 49);
            _lblSalle.TabIndex = 2;
            _lblSalle.Text = "Salle :";
            // 
            // _cboSalle
            // 
            _cboSalle.Dock = DockStyle.Fill;
            _cboSalle.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboSalle.Location = new Point(227, 105);
            _cboSalle.Margin = new Padding(0, 6, 0, 13);
            _cboSalle.Name = "_cboSalle";
            _cboSalle.Size = new Size(642, 40);
            _cboSalle.TabIndex = 3;
            // 
            // _lblStructure
            // 
            _lblStructure.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblStructure.Location = new Point(22, 183);
            _lblStructure.Margin = new Padding(0, 11, 19, 13);
            _lblStructure.Name = "_lblStructure";
            _lblStructure.Size = new Size(186, 49);
            _lblStructure.TabIndex = 4;
            _lblStructure.Text = "Structure :";
            // 
            // _cboStructure
            // 
            _cboStructure.Dock = DockStyle.Fill;
            _cboStructure.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboStructure.Location = new Point(227, 178);
            _cboStructure.Margin = new Padding(0, 6, 0, 13);
            _cboStructure.Name = "_cboStructure";
            _cboStructure.Size = new Size(642, 40);
            _cboStructure.TabIndex = 5;
            // 
            // _lblHeureDebut
            // 
            _lblHeureDebut.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblHeureDebut.Location = new Point(22, 256);
            _lblHeureDebut.Margin = new Padding(0, 11, 19, 13);
            _lblHeureDebut.Name = "_lblHeureDebut";
            _lblHeureDebut.Size = new Size(186, 49);
            _lblHeureDebut.TabIndex = 6;
            _lblHeureDebut.Text = "Heure début :";
            // 
            // _cboHeureDebut
            // 
            _cboHeureDebut.Dock = DockStyle.Fill;
            _cboHeureDebut.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboHeureDebut.Location = new Point(227, 251);
            _cboHeureDebut.Margin = new Padding(0, 6, 0, 13);
            _cboHeureDebut.Name = "_cboHeureDebut";
            _cboHeureDebut.Size = new Size(642, 40);
            _cboHeureDebut.TabIndex = 7;
            // 
            // _lblHeureFin
            // 
            _lblHeureFin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblHeureFin.Location = new Point(22, 329);
            _lblHeureFin.Margin = new Padding(0, 11, 19, 13);
            _lblHeureFin.Name = "_lblHeureFin";
            _lblHeureFin.Size = new Size(186, 49);
            _lblHeureFin.TabIndex = 8;
            _lblHeureFin.Text = "Heure fin :";
            // 
            // _cboHeureFin
            // 
            _cboHeureFin.Dock = DockStyle.Fill;
            _cboHeureFin.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboHeureFin.Location = new Point(227, 324);
            _cboHeureFin.Margin = new Padding(0, 6, 0, 13);
            _cboHeureFin.Name = "_cboHeureFin";
            _cboHeureFin.Size = new Size(642, 40);
            _cboHeureFin.TabIndex = 9;
            // 
            // _lblStatut
            // 
            _lblStatut.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblStatut.Location = new Point(22, 402);
            _lblStatut.Margin = new Padding(0, 11, 19, 13);
            _lblStatut.Name = "_lblStatut";
            _lblStatut.Size = new Size(186, 49);
            _lblStatut.TabIndex = 10;
            _lblStatut.Text = "Statut :";
            // 
            // _cboStatut
            // 
            _cboStatut.Dock = DockStyle.Fill;
            _cboStatut.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboStatut.Items.AddRange(new object[] { "EN_ATTENTE", "CONFIRMEE", "ANNULEE" });
            _cboStatut.Location = new Point(227, 397);
            _cboStatut.Margin = new Padding(0, 6, 0, 13);
            _cboStatut.Name = "_cboStatut";
            _cboStatut.Size = new Size(642, 40);
            _cboStatut.TabIndex = 11;
            // 
            // _lblCommentaire
            // 
            _lblCommentaire.Location = new Point(22, 475);
            _lblCommentaire.Margin = new Padding(0, 11, 19, 13);
            _lblCommentaire.Name = "_lblCommentaire";
            _lblCommentaire.Size = new Size(186, 49);
            _lblCommentaire.TabIndex = 12;
            _lblCommentaire.Text = "Commentaire :";
            // 
            // _txtCommentaire
            // 
            _txtCommentaire.Dock = DockStyle.Fill;
            _txtCommentaire.Location = new Point(227, 470);
            _txtCommentaire.Margin = new Padding(0, 6, 0, 13);
            _txtCommentaire.MaxLength = 500;
            _txtCommentaire.Multiline = true;
            _txtCommentaire.Name = "_txtCommentaire";
            _txtCommentaire.ScrollBars = ScrollBars.Vertical;
            _txtCommentaire.Size = new Size(642, 123);
            _txtCommentaire.TabIndex = 13;
            // 
            // _flpBoutons
            // 
            _flpBoutons.AutoSize = true;
            _flpBoutons.Controls.Add(_btnAnnuler);
            _flpBoutons.Controls.Add(_btnValider);
            _flpBoutons.Dock = DockStyle.Fill;
            _flpBoutons.FlowDirection = FlowDirection.RightToLeft;
            _flpBoutons.Location = new Point(227, 627);
            _flpBoutons.Margin = new Padding(0, 21, 0, 0);
            _flpBoutons.Name = "_flpBoutons";
            _flpBoutons.Size = new Size(642, 243);
            _flpBoutons.TabIndex = 14;
            // 
            // _btnAnnuler
            // 
            _btnAnnuler.DialogResult = DialogResult.Cancel;
            _btnAnnuler.Location = new Point(450, 6);
            _btnAnnuler.Margin = new Padding(6);
            _btnAnnuler.Name = "_btnAnnuler";
            _btnAnnuler.Size = new Size(186, 64);
            _btnAnnuler.TabIndex = 0;
            _btnAnnuler.Text = "Annuler";
            // 
            // _btnValider
            // 
            _btnValider.Location = new Point(249, 0);
            _btnValider.Margin = new Padding(0, 0, 9, 0);
            _btnValider.Name = "_btnValider";
            _btnValider.Size = new Size(186, 64);
            _btnValider.TabIndex = 1;
            _btnValider.Text = "Valider";
            _btnValider.Click += BtnValider_Click;
            // 
            // FormReservationDetail
            // 
            AcceptButton = _btnValider;
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = _btnAnnuler;
            ClientSize = new Size(891, 896);
            Controls.Add(_tlpMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(6);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormReservationDetail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Détail d'une réservation";
            _tlpMain.ResumeLayout(false);
            _tlpMain.PerformLayout();
            _flpBoutons.ResumeLayout(false);
            ResumeLayout(false);
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.TableLayoutPanel _tlpMain;
        private System.Windows.Forms.Label            _lblDate;
        private System.Windows.Forms.DateTimePicker   _dtpDate;
        private System.Windows.Forms.Label            _lblSalle;
        private System.Windows.Forms.ComboBox         _cboSalle;
        private System.Windows.Forms.Label            _lblStructure;
        private System.Windows.Forms.ComboBox         _cboStructure;
        private System.Windows.Forms.Label            _lblHeureDebut;
        private System.Windows.Forms.ComboBox         _cboHeureDebut;
        private System.Windows.Forms.Label            _lblHeureFin;
        private System.Windows.Forms.ComboBox         _cboHeureFin;
        private System.Windows.Forms.Label            _lblStatut;
        private System.Windows.Forms.ComboBox         _cboStatut;
        private System.Windows.Forms.Label            _lblCommentaire;
        private System.Windows.Forms.TextBox          _txtCommentaire;
        private System.Windows.Forms.FlowLayoutPanel  _flpBoutons;
        private System.Windows.Forms.Button           _btnValider;
        private System.Windows.Forms.Button           _btnAnnuler;
    }
}
