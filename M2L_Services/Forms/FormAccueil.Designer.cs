namespace M2L_Services.Forms
{
    partial class FormAccueil
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
            _menuPrincipal = new MenuStrip();
            _mnuFichier = new ToolStripMenuItem();
            _mnuActualiser = new ToolStripMenuItem();
            _mnuSepFichier = new ToolStripSeparator();
            _mnuDeconnecter = new ToolStripMenuItem();
            _mnuQuitter = new ToolStripMenuItem();
            _mnuSalles = new ToolStripMenuItem();
            _mnuStructures = new ToolStripMenuItem();
            _mnuReservations = new ToolStripMenuItem();
            _mnuDigicode = new ToolStripMenuItem();
            _tlpMain = new TableLayoutPanel();
            _lbUtilisateur = new Label();
            _grpInfoJour = new GroupBox();
            _lblInfoJour = new Label();
            _grpDashboard = new GroupBox();
            _tlpDashboard = new TableLayoutPanel();
            _grpStatSalles = new GroupBox();
            _lblNbSalles = new Label();
            _grpStatStructures = new GroupBox();
            _lblNbStructures = new Label();
            _grpStatReservations = new GroupBox();
            _lblNbReservations = new Label();
            _grpStatAujourdhui = new GroupBox();
            _lblNbAujourdHui = new Label();
            _lblAlerteDonnees = new Label();
            _menuPrincipal.SuspendLayout();
            _tlpMain.SuspendLayout();
            _grpInfoJour.SuspendLayout();
            _grpDashboard.SuspendLayout();
            _tlpDashboard.SuspendLayout();
            _grpStatSalles.SuspendLayout();
            _grpStatStructures.SuspendLayout();
            _grpStatReservations.SuspendLayout();
            _grpStatAujourdhui.SuspendLayout();
            SuspendLayout();
            // 
            // _menuPrincipal
            // 
            _menuPrincipal.ImageScalingSize = new Size(20, 20);
            _menuPrincipal.Items.AddRange(new ToolStripItem[] { _mnuFichier, _mnuSalles, _mnuStructures, _mnuReservations, _mnuDigicode });
            _menuPrincipal.Location = new Point(0, 0);
            _menuPrincipal.Name = "_menuPrincipal";
            _menuPrincipal.Padding = new Padding(11, 4, 0, 4);
            _menuPrincipal.Size = new Size(1820, 44);
            _menuPrincipal.TabIndex = 0;
            // 
            // _mnuFichier
            // 
            _mnuFichier.DropDownItems.AddRange(new ToolStripItem[] { _mnuActualiser, _mnuSepFichier, _mnuDeconnecter, _mnuQuitter });
            _mnuFichier.Name = "_mnuFichier";
            _mnuFichier.Size = new Size(104, 36);
            _mnuFichier.Text = "Fichier";
            // 
            // _mnuActualiser
            // 
            _mnuActualiser.Name = "_mnuActualiser";
            _mnuActualiser.Size = new Size(362, 44);
            _mnuActualiser.Text = "Actualiser le tableau";
            _mnuActualiser.Click += MnuActualiser_Click;
            // 
            // _mnuSepFichier
            // 
            _mnuSepFichier.Name = "_mnuSepFichier";
            _mnuSepFichier.Size = new Size(359, 6);
            // 
            // _mnuDeconnecter
            // 
            _mnuDeconnecter.Name = "_mnuDeconnecter";
            _mnuDeconnecter.Size = new Size(362, 44);
            _mnuDeconnecter.Text = "Se déconnecter";
            _mnuDeconnecter.Click += MnuDeconnecter_Click;
            // 
            // _mnuQuitter
            // 
            _mnuQuitter.Name = "_mnuQuitter";
            _mnuQuitter.Size = new Size(362, 44);
            _mnuQuitter.Text = "Quitter";
            _mnuQuitter.Click += MnuQuitter_Click;
            // 
            // _mnuSalles
            // 
            _mnuSalles.Name = "_mnuSalles";
            _mnuSalles.Size = new Size(94, 36);
            _mnuSalles.Text = "Salles";
            _mnuSalles.Click += MnuSalles_Click;
            // 
            // _mnuStructures
            // 
            _mnuStructures.Name = "_mnuStructures";
            _mnuStructures.Size = new Size(140, 36);
            _mnuStructures.Text = "Structures";
            _mnuStructures.Click += MnuStructures_Click;
            // 
            // _mnuReservations
            // 
            _mnuReservations.Name = "_mnuReservations";
            _mnuReservations.Size = new Size(167, 36);
            _mnuReservations.Text = "Réservations";
            _mnuReservations.Click += MnuReservations_Click;
            // 
            // _mnuDigicode
            // 
            _mnuDigicode.Name = "_mnuDigicode";
            _mnuDigicode.Size = new Size(204, 36);
            _mnuDigicode.Text = "Digicode && Wifi";
            _mnuDigicode.Click += MnuDigicode_Click;
            // 
            // _tlpMain
            // 
            _tlpMain.ColumnCount = 1;
            _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _tlpMain.Controls.Add(_lbUtilisateur, 0, 0);
            _tlpMain.Controls.Add(_grpInfoJour, 0, 1);
            _tlpMain.Controls.Add(_grpDashboard, 0, 2);
            _tlpMain.Dock = DockStyle.Fill;
            _tlpMain.Location = new Point(0, 44);
            _tlpMain.Margin = new Padding(6);
            _tlpMain.Name = "_tlpMain";
            _tlpMain.Padding = new Padding(26, 21, 26, 21);
            _tlpMain.RowCount = 3;
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle());
            _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _tlpMain.Size = new Size(1820, 1279);
            _tlpMain.TabIndex = 0;
            // 
            // _lbUtilisateur
            // 
            _lbUtilisateur.Dock = DockStyle.Fill;
            _lbUtilisateur.ForeColor = Color.DimGray;
            _lbUtilisateur.Location = new Point(26, 21);
            _lbUtilisateur.Margin = new Padding(0, 0, 0, 21);
            _lbUtilisateur.Name = "_lbUtilisateur";
            _lbUtilisateur.Size = new Size(1768, 32);
            _lbUtilisateur.TabIndex = 0;
            _lbUtilisateur.Text = "Utilisateur inconnu";
            _lbUtilisateur.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _grpInfoJour
            // 
            _grpInfoJour.Controls.Add(_lblInfoJour);
            _grpInfoJour.Dock = DockStyle.Fill;
            _grpInfoJour.Location = new Point(26, 74);
            _grpInfoJour.Margin = new Padding(0, 0, 0, 21);
            _grpInfoJour.MinimumSize = new Size(0, 149);
            _grpInfoJour.Name = "_grpInfoJour";
            _grpInfoJour.Padding = new Padding(15, 11, 15, 17);
            _grpInfoJour.Size = new Size(1768, 213);
            _grpInfoJour.TabIndex = 1;
            _grpInfoJour.TabStop = false;
            _grpInfoJour.Text = "Informations du jour";
            // 
            // _lblInfoJour
            // 
            _lblInfoJour.Dock = DockStyle.Fill;
            _lblInfoJour.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _lblInfoJour.ForeColor = Color.DarkBlue;
            _lblInfoJour.Location = new Point(15, 43);
            _lblInfoJour.Margin = new Padding(6, 0, 6, 0);
            _lblInfoJour.Name = "_lblInfoJour";
            _lblInfoJour.Padding = new Padding(0, 9, 0, 9);
            _lblInfoJour.Size = new Size(1738, 153);
            _lblInfoJour.TabIndex = 0;
            _lblInfoJour.Text = "Chargement...";
            _lblInfoJour.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _grpDashboard
            // 
            _grpDashboard.Controls.Add(_tlpDashboard);
            _grpDashboard.Dock = DockStyle.Fill;
            _grpDashboard.Location = new Point(26, 308);
            _grpDashboard.Margin = new Padding(0, 0, 0, 21);
            _grpDashboard.Name = "_grpDashboard";
            _grpDashboard.Padding = new Padding(15, 11, 15, 17);
            _grpDashboard.Size = new Size(1768, 982);
            _grpDashboard.TabIndex = 2;
            _grpDashboard.TabStop = false;
            _grpDashboard.Text = "Tableau de bord";
            // 
            // _tlpDashboard
            // 
            _tlpDashboard.ColumnCount = 2;
            _tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tlpDashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _tlpDashboard.Controls.Add(_grpStatSalles, 0, 0);
            _tlpDashboard.Controls.Add(_grpStatStructures, 1, 0);
            _tlpDashboard.Controls.Add(_grpStatReservations, 0, 1);
            _tlpDashboard.Controls.Add(_grpStatAujourdhui, 1, 1);
            _tlpDashboard.Controls.Add(_lblAlerteDonnees, 0, 2);
            _tlpDashboard.Dock = DockStyle.Fill;
            _tlpDashboard.Location = new Point(15, 43);
            _tlpDashboard.Margin = new Padding(6);
            _tlpDashboard.Name = "_tlpDashboard";
            _tlpDashboard.RowCount = 3;
            _tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpDashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            _tlpDashboard.RowStyles.Add(new RowStyle());
            _tlpDashboard.Size = new Size(1738, 922);
            _tlpDashboard.TabIndex = 0;
            // 
            // _grpStatSalles
            // 
            _grpStatSalles.Controls.Add(_lblNbSalles);
            _grpStatSalles.Dock = DockStyle.Fill;
            _grpStatSalles.Location = new Point(0, 0);
            _grpStatSalles.Margin = new Padding(0, 0, 11, 17);
            _grpStatSalles.MinimumSize = new Size(0, 192);
            _grpStatSalles.Name = "_grpStatSalles";
            _grpStatSalles.Padding = new Padding(15, 17, 15, 17);
            _grpStatSalles.Size = new Size(858, 415);
            _grpStatSalles.TabIndex = 0;
            _grpStatSalles.TabStop = false;
            _grpStatSalles.Text = "Salles";
            // 
            // _lblNbSalles
            // 
            _lblNbSalles.Dock = DockStyle.Fill;
            _lblNbSalles.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _lblNbSalles.ForeColor = Color.SteelBlue;
            _lblNbSalles.Location = new Point(15, 49);
            _lblNbSalles.Margin = new Padding(6, 0, 6, 0);
            _lblNbSalles.Name = "_lblNbSalles";
            _lblNbSalles.Size = new Size(828, 349);
            _lblNbSalles.TabIndex = 0;
            _lblNbSalles.Text = "0";
            _lblNbSalles.TextAlign = ContentAlignment.MiddleCenter;
            _lblNbSalles.Click += _lblNbSalles_Click;
            // 
            // _grpStatStructures
            // 
            _grpStatStructures.Controls.Add(_lblNbStructures);
            _grpStatStructures.Dock = DockStyle.Fill;
            _grpStatStructures.Location = new Point(880, 0);
            _grpStatStructures.Margin = new Padding(11, 0, 0, 17);
            _grpStatStructures.MinimumSize = new Size(0, 192);
            _grpStatStructures.Name = "_grpStatStructures";
            _grpStatStructures.Padding = new Padding(15, 17, 15, 17);
            _grpStatStructures.Size = new Size(858, 415);
            _grpStatStructures.TabIndex = 1;
            _grpStatStructures.TabStop = false;
            _grpStatStructures.Text = "Structures";
            // 
            // _lblNbStructures
            // 
            _lblNbStructures.Dock = DockStyle.Fill;
            _lblNbStructures.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _lblNbStructures.ForeColor = Color.SteelBlue;
            _lblNbStructures.Location = new Point(15, 49);
            _lblNbStructures.Margin = new Padding(6, 0, 6, 0);
            _lblNbStructures.Name = "_lblNbStructures";
            _lblNbStructures.Size = new Size(828, 349);
            _lblNbStructures.TabIndex = 0;
            _lblNbStructures.Text = "0";
            _lblNbStructures.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _grpStatReservations
            // 
            _grpStatReservations.Controls.Add(_lblNbReservations);
            _grpStatReservations.Dock = DockStyle.Fill;
            _grpStatReservations.Location = new Point(0, 432);
            _grpStatReservations.Margin = new Padding(0, 0, 11, 17);
            _grpStatReservations.MinimumSize = new Size(0, 192);
            _grpStatReservations.Name = "_grpStatReservations";
            _grpStatReservations.Padding = new Padding(15, 17, 15, 17);
            _grpStatReservations.Size = new Size(858, 415);
            _grpStatReservations.TabIndex = 2;
            _grpStatReservations.TabStop = false;
            _grpStatReservations.Text = "Réservations (total)";
            // 
            // _lblNbReservations
            // 
            _lblNbReservations.Dock = DockStyle.Fill;
            _lblNbReservations.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _lblNbReservations.ForeColor = Color.SteelBlue;
            _lblNbReservations.Location = new Point(15, 49);
            _lblNbReservations.Margin = new Padding(6, 0, 6, 0);
            _lblNbReservations.Name = "_lblNbReservations";
            _lblNbReservations.Size = new Size(828, 349);
            _lblNbReservations.TabIndex = 0;
            _lblNbReservations.Text = "0";
            _lblNbReservations.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _grpStatAujourdhui
            // 
            _grpStatAujourdhui.Controls.Add(_lblNbAujourdHui);
            _grpStatAujourdhui.Dock = DockStyle.Fill;
            _grpStatAujourdhui.Location = new Point(880, 432);
            _grpStatAujourdhui.Margin = new Padding(11, 0, 0, 17);
            _grpStatAujourdhui.MinimumSize = new Size(0, 192);
            _grpStatAujourdhui.Name = "_grpStatAujourdhui";
            _grpStatAujourdhui.Padding = new Padding(15, 17, 15, 17);
            _grpStatAujourdhui.Size = new Size(858, 415);
            _grpStatAujourdhui.TabIndex = 3;
            _grpStatAujourdhui.TabStop = false;
            _grpStatAujourdhui.Text = "Réservations (aujourd'hui)";
            // 
            // _lblNbAujourdHui
            // 
            _lblNbAujourdHui.Dock = DockStyle.Fill;
            _lblNbAujourdHui.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _lblNbAujourdHui.ForeColor = Color.SteelBlue;
            _lblNbAujourdHui.Location = new Point(15, 49);
            _lblNbAujourdHui.Margin = new Padding(6, 0, 6, 0);
            _lblNbAujourdHui.Name = "_lblNbAujourdHui";
            _lblNbAujourdHui.Size = new Size(828, 349);
            _lblNbAujourdHui.TabIndex = 0;
            _lblNbAujourdHui.Text = "0";
            _lblNbAujourdHui.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblAlerteDonnees
            // 
            _tlpDashboard.SetColumnSpan(_lblAlerteDonnees, 2);
            _lblAlerteDonnees.Dock = DockStyle.Fill;
            _lblAlerteDonnees.ForeColor = Color.DarkOrange;
            _lblAlerteDonnees.Location = new Point(0, 873);
            _lblAlerteDonnees.Margin = new Padding(0, 9, 0, 0);
            _lblAlerteDonnees.Name = "_lblAlerteDonnees";
            _lblAlerteDonnees.Size = new Size(1738, 49);
            _lblAlerteDonnees.TabIndex = 4;
            // 
            // FormAccueil
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1820, 1323);
            Controls.Add(_tlpMain);
            Controls.Add(_menuPrincipal);
            MainMenuStrip = _menuPrincipal;
            Margin = new Padding(6);
            MinimumSize = new Size(1463, 986);
            Name = "FormAccueil";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "M2L Services — Accueil";
            _menuPrincipal.ResumeLayout(false);
            _menuPrincipal.PerformLayout();
            _tlpMain.ResumeLayout(false);
            _grpInfoJour.ResumeLayout(false);
            _grpDashboard.ResumeLayout(false);
            _tlpDashboard.ResumeLayout(false);
            _grpStatSalles.ResumeLayout(false);
            _grpStatStructures.ResumeLayout(false);
            _grpStatReservations.ResumeLayout(false);
            _grpStatAujourdhui.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        // 7. Champs de contrôles
        private System.Windows.Forms.MenuStrip          _menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem   _mnuFichier;
        private System.Windows.Forms.ToolStripMenuItem   _mnuActualiser;
        private System.Windows.Forms.ToolStripSeparator  _mnuSepFichier;
        private System.Windows.Forms.ToolStripMenuItem   _mnuDeconnecter;
        private System.Windows.Forms.ToolStripMenuItem   _mnuQuitter;
        private System.Windows.Forms.ToolStripMenuItem   _mnuSalles;
        private System.Windows.Forms.ToolStripMenuItem   _mnuStructures;
        private System.Windows.Forms.ToolStripMenuItem   _mnuReservations;
        private System.Windows.Forms.ToolStripMenuItem   _mnuDigicode;
        private System.Windows.Forms.TableLayoutPanel    _tlpMain;
        private System.Windows.Forms.Label               _lbUtilisateur;
        private System.Windows.Forms.GroupBox            _grpInfoJour;
        private System.Windows.Forms.Label               _lblInfoJour;
        private System.Windows.Forms.GroupBox            _grpDashboard;
        private System.Windows.Forms.TableLayoutPanel    _tlpDashboard;
        private System.Windows.Forms.GroupBox            _grpStatSalles;
        private System.Windows.Forms.Label               _lblNbSalles;
        private System.Windows.Forms.GroupBox            _grpStatStructures;
        private System.Windows.Forms.Label               _lblNbStructures;
        private System.Windows.Forms.GroupBox            _grpStatReservations;
        private System.Windows.Forms.Label               _lblNbReservations;
        private System.Windows.Forms.GroupBox            _grpStatAujourdhui;
        private System.Windows.Forms.Label               _lblNbAujourdHui;
        private System.Windows.Forms.Label               _lblAlerteDonnees;
    }
}
