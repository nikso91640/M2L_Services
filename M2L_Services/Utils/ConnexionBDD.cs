using MySql.Data.MySqlClient;

namespace M2L_Services.Utils;

/// <summary>
/// Centralise la connexion à la base de données MySQL.
/// Toutes les DAO passent par cette classe — ne pas dupliquer la chaîne de connexion ailleurs.
/// </summary>
public static class ConnexionBDD
{
    // Chaîne de connexion — adaptez Pwd= selon votre configuration WAMP
    private static readonly string _chaineConnexion =
        "Server=localhost;Port=3306;Database=m2l_services;Uid=root;Pwd=;CharSet=utf8mb4;";

    /// <summary>
    /// Retourne une connexion MySQL déjà ouverte.
    /// Toujours l'utiliser dans un bloc using() pour la fermer automatiquement.
    /// </summary>
    public static MySqlConnection GetConnexion()
    {
        var conn = new MySqlConnection(_chaineConnexion);
        conn.Open();
        return conn;
    }
}
