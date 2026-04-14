using M2L_Services.Forms;

namespace M2L_Services
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée de l'application M2L Services.
        /// Lance le formulaire de connexion au démarrage.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormLogin());
        }
    }
}