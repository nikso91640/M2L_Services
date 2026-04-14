namespace M2L_Services.Forms
{
    partial class FormDigicode
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
            _grpInfoJour = new GroupBox();
            _tlpInfoJour = new TableLayoutPanel();
            _lblDigicode = new Label();
            _txtDigicode = new TextBox();
            _btnCopierDigicode = new Button();
            _lblWifi = new Label();
            _txtWifi = new TextBox();
            _btnCopierWifi = new Button();
            _btnEnregistrer = new Button();
            _grpHistorique = new GroupBox();
            _dgvHistorique = new DataGridView();
            _tlpMain.SuspendLayout();
            _grpInfoJour.SuspendLayout();
            _tlpInfoJour.SuspendLayout();
            _grpHistorique.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHistorique).BeginInit();
            SuspendLayout();
            // 
            // _tlpMain
            // 
            _tlpMain.ColumnCount = 1;
            _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpMain.Controls.Add(_grpInfoJour, 0, 0);
            _tlpMain.Controls.Add(_grpHistorique, 0, 1);
            _tlpMain.Dock = DockStyle.Fill;
            _tlpMain.Location = new Point(0, 0);
            _tlpMain.Margin = new Padding(6);
            _tlpMain.Name = "_tlpMain";
            _tlpMain.Padding = new Padding(19, 21, 19, 21);
            _tlpMain.RowCount = 2;
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _tlpMain.Size = new Size(1300, 1067);
            _tlpMain.TabIndex = 0;
            // 
            // _grpInfoJour
            // 
            _grpInfoJour.AutoSize = true;
            _grpInfoJour.Controls.Add(_tlpInfoJour);
            _grpInfoJour.Dock = DockStyle.Fill;
            _grpInfoJour.Location = new Point(19, 21);
            _grpInfoJour.Margin = new Padding(0, 0, 0, 17);
            _grpInfoJour.Name = "_grpInfoJour";
            _grpInfoJour.Padding = new Padding(15, 11, 15, 17);
            _grpInfoJour.Size = new Size(1262, 60);
            _grpInfoJour.TabIndex = 0;
            _grpInfoJour.TabStop = false;
            _grpInfoJour.Text = "Info du jour";
            // 
            // _tlpInfoJour
            // 
            _tlpInfoJour.ColumnCount = 4;
            _tlpInfoJour.ColumnStyles.Add(new ColumnStyle());
            _tlpInfoJour.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpInfoJour.ColumnStyles.Add(new ColumnStyle());
            _tlpInfoJour.ColumnStyles.Add(new ColumnStyle());
            _tlpInfoJour.Controls.Add(_lblDigicode, 0, 0);
            _tlpInfoJour.Controls.Add(_txtDigicode, 1, 0);
            _tlpInfoJour.Controls.Add(_btnCopierDigicode, 2, 0);
            _tlpInfoJour.Controls.Add(_lblWifi, 0, 1);
            _tlpInfoJour.Controls.Add(_txtWifi, 1, 1);
            _tlpInfoJour.Controls.Add(_btnCopierWifi, 2, 1);
            _tlpInfoJour.Controls.Add(_btnEnregistrer, 3, 0);
            _tlpInfoJour.Dock = DockStyle.Fill;
            _tlpInfoJour.Location = new Point(15, 43);
            _tlpInfoJour.Margin = new Padding(6);
            _tlpInfoJour.Name = "_tlpInfoJour";
            _tlpInfoJour.RowCount = 2;
            _tlpInfoJour.RowStyles.Add(new RowStyle());
            _tlpInfoJour.RowStyles.Add(new RowStyle());
            _tlpInfoJour.Size = new Size(1232, 0);
            _tlpInfoJour.TabIndex = 0;
            // 
            // _lblDigicode
            // 
            _lblDigicode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblDigicode.Location = new Point(0, 16);
            _lblDigicode.Margin = new Padding(0, 11, 19, 13);
            _lblDigicode.Name = "_lblDigicode";
            _lblDigicode.Size = new Size(186, 49);
            _lblDigicode.TabIndex = 0;
            _lblDigicode.Text = "Digicode :";
            // 
            // _txtDigicode
            // 
            _txtDigicode.Dock = DockStyle.Fill;
            _txtDigicode.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _txtDigicode.Location = new Point(205, 6);
            _txtDigicode.Margin = new Padding(0, 6, 15, 13);
            _txtDigicode.MaxLength = 20;
            _txtDigicode.Name = "_txtDigicode";
            _txtDigicode.Size = new Size(654, 57);
            _txtDigicode.TabIndex = 1;
            // 
            // _btnCopierDigicode
            // 
            _btnCopierDigicode.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _btnCopierDigicode.Location = new Point(874, 6);
            _btnCopierDigicode.Margin = new Padding(0, 6, 15, 13);
            _btnCopierDigicode.Name = "_btnCopierDigicode";
            _btnCopierDigicode.Size = new Size(139, 64);
            _btnCopierDigicode.TabIndex = 2;
            _btnCopierDigicode.Text = "Copier";
            _btnCopierDigicode.Click += BtnCopierDigicode_Click;
            // 
            // _lblWifi
            // 
            _lblWifi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _lblWifi.Location = new Point(0, 99);
            _lblWifi.Margin = new Padding(0, 11, 19, 13);
            _lblWifi.Name = "_lblWifi";
            _lblWifi.Size = new Size(186, 49);
            _lblWifi.TabIndex = 3;
            _lblWifi.Text = "Cle Wifi :";
            // 
            // _txtWifi
            // 
            _txtWifi.Dock = DockStyle.Fill;
            _txtWifi.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _txtWifi.Location = new Point(205, 89);
            _txtWifi.Margin = new Padding(0, 6, 15, 13);
            _txtWifi.MaxLength = 100;
            _txtWifi.Name = "_txtWifi";
            _txtWifi.Size = new Size(654, 57);
            _txtWifi.TabIndex = 4;
            // 
            // _btnCopierWifi
            // 
            _btnCopierWifi.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _btnCopierWifi.Location = new Point(874, 89);
            _btnCopierWifi.Margin = new Padding(0, 6, 15, 13);
            _btnCopierWifi.Name = "_btnCopierWifi";
            _btnCopierWifi.Size = new Size(139, 64);
            _btnCopierWifi.TabIndex = 5;
            _btnCopierWifi.Text = "Copier";
            _btnCopierWifi.Click += BtnCopierWifi_Click;
            // 
            // _btnEnregistrer
            // 
            _btnEnregistrer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            _btnEnregistrer.Location = new Point(1028, 24);
            _btnEnregistrer.Margin = new Padding(0, 6, 0, 6);
            _btnEnregistrer.Name = "_btnEnregistrer";
            _tlpInfoJour.SetRowSpan(_btnEnregistrer, 2);
            _btnEnregistrer.Size = new Size(204, 117);
            _btnEnregistrer.TabIndex = 6;
            _btnEnregistrer.Text = "Enregistrer";
            _btnEnregistrer.Click += BtnEnregistrer_Click;
            // 
            // _grpHistorique
            // 
            _grpHistorique.Controls.Add(_dgvHistorique);
            _grpHistorique.Dock = DockStyle.Fill;
            _grpHistorique.Location = new Point(25, 104);
            _grpHistorique.Margin = new Padding(6);
            _grpHistorique.Name = "_grpHistorique";
            _grpHistorique.Padding = new Padding(15, 11, 15, 17);
            _grpHistorique.Size = new Size(1250, 936);
            _grpHistorique.TabIndex = 1;
            _grpHistorique.TabStop = false;
            _grpHistorique.Text = "Historique";
            // 
            // _dgvHistorique
            // 
            _dgvHistorique.AllowUserToAddRows = false;
            _dgvHistorique.AllowUserToDeleteRows = false;
            _dgvHistorique.AllowUserToResizeRows = false;
            _dgvHistorique.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvHistorique.ColumnHeadersHeight = 46;
            _dgvHistorique.Dock = DockStyle.Fill;
            _dgvHistorique.Location = new Point(15, 43);
            _dgvHistorique.Margin = new Padding(6);
            _dgvHistorique.MultiSelect = false;
            _dgvHistorique.Name = "_dgvHistorique";
            _dgvHistorique.ReadOnly = true;
            _dgvHistorique.RowHeadersVisible = false;
            _dgvHistorique.RowHeadersWidth = 82;
            _dgvHistorique.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvHistorique.Size = new Size(1220, 876);
            _dgvHistorique.TabIndex = 0;
            // 
            // FormDigicode
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1300, 1067);
            Controls.Add(_tlpMain);
            Margin = new Padding(6);
            MinimumSize = new Size(999, 730);
            Name = "FormDigicode";
            StartPosition = FormStartPosition.CenterParent;
            Text = "M2L Services - Digicode et Wifi du jour";
            _tlpMain.ResumeLayout(false);
            _tlpMain.PerformLayout();
            _grpInfoJour.ResumeLayout(false);
            _tlpInfoJour.ResumeLayout(false);
            _tlpInfoJour.PerformLayout();
            _grpHistorique.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgvHistorique).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel _tlpMain;
        private System.Windows.Forms.GroupBox         _grpInfoJour;
        private System.Windows.Forms.TableLayoutPanel _tlpInfoJour;
        private System.Windows.Forms.Label            _lblDigicode;
        private System.Windows.Forms.TextBox          _txtDigicode;
        private System.Windows.Forms.Button           _btnCopierDigicode;
        private System.Windows.Forms.Label            _lblWifi;
        private System.Windows.Forms.TextBox          _txtWifi;
        private System.Windows.Forms.Button           _btnCopierWifi;
        private System.Windows.Forms.Button           _btnEnregistrer;
        private System.Windows.Forms.GroupBox         _grpHistorique;
        private System.Windows.Forms.DataGridView     _dgvHistorique;
    }
}

