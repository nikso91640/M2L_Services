using MySql.Data.MySqlClient;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.DataAccess;

/// <summary>
/// Gère les accès base de données pour les utilisateurs.
/// Le mot de passe est toujours haché en SHA2-256 par MySQL.
/// </summary>
public class UtilisateurDAO
{
    /// <summary>
    /// Vérifie login + mot de passe. Retourne l'utilisateur si trouvé, sinon null.
    /// </summary>
    public Utilisateur? Authentifier(string login, string motDePasse)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"SELECT id_utilisateur, login, nom, prenom, role
                       FROM utilisateur
                       WHERE login = @login AND mot_de_passe = SHA2(@mdp, 256)";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@mdp",   motDePasse);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Utilisateur
            {
                IdUtilisateur = reader.GetInt32("id_utilisateur"),
                Login         = reader.GetString("login"),
                Nom           = reader.GetString("nom"),
                Prenom        = reader.GetString("prenom"),
                Role          = reader.GetString("role")
            };
        }
        return null;
    }

    /// <summary>Retourne tous les utilisateurs triés par nom.</summary>
    public List<Utilisateur> GetTout()
    {
        var liste = new List<Utilisateur>();
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "SELECT id_utilisateur, login, nom, prenom, role FROM utilisateur ORDER BY nom";
        var cmd    = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            liste.Add(new Utilisateur
            {
                IdUtilisateur = reader.GetInt32("id_utilisateur"),
                Login         = reader.GetString("login"),
                Nom           = reader.GetString("nom"),
                Prenom        = reader.GetString("prenom"),
                Role          = reader.GetString("role")
            });
        }
        return liste;
    }

    /// <summary>Ajoute un nouvel utilisateur (mot de passe haché par MySQL).</summary>
    public void Ajouter(Utilisateur u, string motDePasse)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"INSERT INTO utilisateur (login, mot_de_passe, nom, prenom, role)
                       VALUES (@login, SHA2(@mdp, 256), @nom, @prenom, @role)";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@login",  u.Login);
        cmd.Parameters.AddWithValue("@mdp",    motDePasse);
        cmd.Parameters.AddWithValue("@nom",    u.Nom);
        cmd.Parameters.AddWithValue("@prenom", u.Prenom);
        cmd.Parameters.AddWithValue("@role",   u.Role);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Modifie les informations d'un utilisateur (sans changer son mot de passe).</summary>
    public void Modifier(Utilisateur u)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"UPDATE utilisateur
                       SET login=@login, nom=@nom, prenom=@prenom, role=@role
                       WHERE id_utilisateur=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@login",  u.Login);
        cmd.Parameters.AddWithValue("@nom",    u.Nom);
        cmd.Parameters.AddWithValue("@prenom", u.Prenom);
        cmd.Parameters.AddWithValue("@role",   u.Role);
        cmd.Parameters.AddWithValue("@id",     u.IdUtilisateur);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Met à jour le mot de passe d'un utilisateur.</summary>
    public void ModifierMotDePasse(int id, string nouveauMotDePasse)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "UPDATE utilisateur SET mot_de_passe=SHA2(@mdp, 256) WHERE id_utilisateur=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@mdp", nouveauMotDePasse);
        cmd.Parameters.AddWithValue("@id",  id);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Supprime un utilisateur par son identifiant.</summary>
    public void Supprimer(int id)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "DELETE FROM utilisateur WHERE id_utilisateur = @id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
