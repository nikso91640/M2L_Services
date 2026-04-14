using System;
using System.Windows.Forms;
using M2L_Services.DataAccess;
using M2L_Services.Utils;
using MySql.Data.MySqlClient;

namespace M2L_Services.Forms
{
    /// <summary>
    /// Formulaire d'accueil principal — tableau de bord.
    /// La navigation se fait uniquement via le MenuStrip :
    ///   Fichier | Salles | Structures | Réservations | Digicode & Wifi
    /// </summary>
    public partial class FormAccueil : Form
    {
        public FormAccueil()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RafraichirAccueil();
        }

        /// <summary>
        /// Recharge tous les blocs de l'accueil (utilisateur, info du jour, statistiques).
        /// </summary>
        private void RafraichirAccueil()
        {
            AfficherUtilisateur();
            ChargerInfoJour();
            ChargerTableauDeBord();
        }

        // Affiche prénom + nom + rôle de l'utilisateur connecté.
        private void AfficherUtilisateur()
        {
            if (Session.UtilisateurConnecte != null)
            {
                _lbUtilisateur.Text =
                    $"{Session.UtilisateurConnecte.Prenom} {Session.UtilisateurConnecte.Nom}  —  {Session.UtilisateurConnecte.Role}";
            }
            else
            {
                _lbUtilisateur.Text = "Utilisateur inconnu";
            }
        }

        // Charge et affiche le digicode / wifi du jour.
        private void ChargerInfoJour()
        {
            try
            {
                InfoJourDAO dao = new InfoJourDAO();
                var info = dao.GetAujourdhui();

                if (info != null)
                {
                    _lblInfoJour.Text = $"Digicode : {info.Digicode}          Wifi : {info.CleWifi}";
                    _lblInfoJour.ForeColor = System.Drawing.Color.DarkBlue;
                }
                else
                {
                    _lblInfoJour.Text = "Digicode et Wifi non renseignés pour aujourd'hui.";
                    _lblInfoJour.ForeColor = System.Drawing.Color.Gray;
                }
            }
            catch (MySqlException)
            {
                _lblInfoJour.Text = "Impossible de charger les informations du jour (connexion MySQL).";
                _lblInfoJour.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Charge les indicateurs du tableau de bord (compteurs).
        /// </summary>
        private void ChargerTableauDeBord()
        {
            try
            {
                SalleDAO salleDao = new SalleDAO();
                StructureDAO structureDao = new StructureDAO();
                ReservationDAO reservationDao = new ReservationDAO();

                int nbSalles = salleDao.GetTout().Count;
                int nbStructures = structureDao.GetTout().Count;
                int nbReservations = reservationDao.GetTout().Count;
                int nbReservationsAujourdhui = reservationDao.GetTout(date: DateTime.Today).Count;
                int nbReservationsEnAttente = reservationDao.GetTout(statut: "EN_ATTENTE").Count;

                _lblNbSalles.Text = nbSalles.ToString();
                _lblNbStructures.Text = nbStructures.ToString();
                _lblNbReservations.Text = nbReservations.ToString();
                _lblNbAujourdHui.Text = nbReservationsAujourdhui.ToString();

                // Message d'aide si la base est vide
                if (nbSalles == 0 && nbStructures == 0 && nbReservations == 0)
                {
                    _lblAlerteDonnees.Text =
                        "Aucune donnée trouvée. Exécutez le script database.sql dans phpMyAdmin (base m2l_services).";
                }
                else if (nbSalles == 0)
                {
                    _lblAlerteDonnees.Text =
                        "Attention : aucune salle trouvée. Les DataGridView de gestion resteront vides.";
                }
                else
                {
                    _lblAlerteDonnees.Text =
                        $"Réservations en attente : {nbReservationsEnAttente}  |  Dernière mise à jour : {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                }
            }
            catch (MySqlException ex)
            {
                _lblNbSalles.Text = "?";
                _lblNbStructures.Text = "?";
                _lblNbReservations.Text = "?";
                _lblNbAujourdHui.Text = "?";
                _lblAlerteDonnees.Text =
                    "Connexion MySQL impossible. Vérifiez WAMP, les identifiants root, et la base m2l_services.";

                MessageBox.Show(
                    "Impossible de charger le tableau de bord.\n\n" +
                    "Cause : connexion MySQL impossible.\n\n" +
                    "Détail technique : " + ex.Message,
                    "Erreur base de données",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ---------------------------------------------------------------------
        // Navigation (menu) — un onglet par famille
        // ---------------------------------------------------------------------

        private void MnuSalles_Click(object? sender, EventArgs e)
        {
            using FormSalles fenetre = new FormSalles();
            fenetre.ShowDialog();
            RafraichirAccueil();
        }

        private void MnuStructures_Click(object? sender, EventArgs e)
        {
            using FormStructures fenetre = new FormStructures();
            fenetre.ShowDialog();
            RafraichirAccueil();
        }

        private void MnuReservations_Click(object? sender, EventArgs e)
        {
            using FormReservations fenetre = new FormReservations();
            fenetre.ShowDialog();
            RafraichirAccueil();
        }

        private void MnuDigicode_Click(object? sender, EventArgs e)
        {
            using FormDigicode fenetre = new FormDigicode();
            fenetre.ShowDialog();
            RafraichirAccueil();
        }

        // ---------------------------------------------------------------------
        // Menu Fichier
        // ---------------------------------------------------------------------

        private void MnuActualiser_Click(object? sender, EventArgs e)
        {
            RafraichirAccueil();
        }

        private void MnuDeconnecter_Click(object? sender, EventArgs e)
        {
            DialogResult choix = MessageBox.Show(
                "Voulez-vous vous déconnecter ?",
                "Déconnexion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (choix == DialogResult.Yes)
            {
                Session.UtilisateurConnecte = null;
                Close();
            }
        }

        private void MnuQuitter_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _lblNbSalles_Click(object sender, EventArgs e)
        {

        }
    }
}
