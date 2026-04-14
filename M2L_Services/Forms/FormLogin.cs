using System.Windows.Forms;
using M2L_Services.DataAccess;
using M2L_Services.Utils;

namespace M2L_Services.Forms
{
    /// <summary>
    /// Formulaire de connexion.
    /// Permet à l'utilisateur de saisir son login et son mot de passe.
    /// Étape 1 de développement (voir 04-fonctionnalites.md).
    /// </summary>
    public partial class FormLogin : Form
    {
        private const int NbTentativesMax = 3;
        private int _nbTentativesRestantes = NbTentativesMax;

        public FormLogin()
        {
            InitializeComponent();
            MettreAJourLibelleTentatives();
        }

        private void MettreAJourLibelleTentatives()
        {
            _lblTentatives.Text = $"Tentatives restantes : {_nbTentativesRestantes}";
        }

        private void ReinitialiserEtatConnexion()
        {
            _nbTentativesRestantes = NbTentativesMax;
            _btnConnecter.Enabled = true;
            _txtLogin.Enabled = true;
            _txtMotDePasse.Enabled = true;
            MettreAJourLibelleTentatives();
        }

        // Clic sur "Se connecter" (ou touche Entrée grâce à AcceptButton)
        private void BtnConnecter_Click(object? sender, EventArgs e)
        {
            // Réinitialiser le message d'erreur
            _lblErreur.Text = "";

            // Vérifier que les champs ne sont pas vides
            if (string.IsNullOrWhiteSpace(_txtLogin.Text))
            {
                _lblErreur.Text = "Veuillez saisir votre login.";
                _txtLogin.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(_txtMotDePasse.Text))
            {
                _lblErreur.Text = "Veuillez saisir votre mot de passe.";
                _txtMotDePasse.Focus();
                return;
            }

            try
            {
                var dao = new UtilisateurDAO();
                var utilisateur = dao.Authentifier(_txtLogin.Text.Trim(), _txtMotDePasse.Text);

                if (utilisateur == null)
                {
                    _nbTentativesRestantes--;
                    MettreAJourLibelleTentatives();

                    _lblErreur.Text = "Login ou mot de passe incorrect.";
                    _txtMotDePasse.Clear();
                    _txtMotDePasse.Focus();

                    if (_nbTentativesRestantes <= 0)
                    {
                        _lblErreur.Text = "Connexion bloquée après 3 tentatives. Redémarrez l'application.";
                        _btnConnecter.Enabled = false;
                        _txtLogin.Enabled = false;
                        _txtMotDePasse.Enabled = false;
                    }

                    return;
                }

                // Connexion réussie : on stocke l'utilisateur en session
                Session.UtilisateurConnecte = utilisateur;

                // Réinitialiser l'état des tentatives pour une prochaine déconnexion
                ReinitialiserEtatConnexion();

                // On ouvre l'accueil, on écoute sa fermeture, et on se masque
                var accueil = new FormAccueil();
                accueil.FormClosed += Accueil_FormClosed;
                accueil.Show();
                Hide();
            }
            catch (Exception ex)
            {
                _lblErreur.Text = "Impossible de contacter la base de données.";
                MessageBox.Show(
                    "Détail de l'erreur :\n" + ex.Message,
                    "Erreur de connexion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ChkAfficherMotDePasse_CheckedChanged(object? sender, EventArgs e)
        {
            _txtMotDePasse.PasswordChar = _chkAfficherMotDePasse.Checked ? '\0' : '*';
        }

        /// <summary>
        /// Appelé quand FormAccueil se ferme.
        /// Si la session est vide → déconnexion, on réaffiche la connexion.
        /// Sinon → fermeture normale, on quitte l'application.
        /// </summary>
        private void Accueil_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (Session.UtilisateurConnecte == null)
            {
                // L'utilisateur s'est déconnecté → réafficher la connexion
                _txtLogin.Clear();
                _txtMotDePasse.Clear();
                _lblErreur.Text = "";
                _chkAfficherMotDePasse.Checked = false;
                ReinitialiserEtatConnexion();
                Show();
            }
            else
            {
                // Fermeture normale de la fenêtre → quitter l'application
                Application.Exit();
            }
        }

        private void _lblErreur_Click(object sender, EventArgs e)
        {

        }

        private void _txtLogin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
