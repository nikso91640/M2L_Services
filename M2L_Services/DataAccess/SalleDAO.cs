using MySql.Data.MySqlClient;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.DataAccess;

/// <summary>Gère les accès base de données pour les salles.</summary>
public class SalleDAO
{
    /// <summary>Retourne toutes les salles triées par nom.</summary>
    public List<Salle> GetTout()
    {
        var liste = new List<Salle>();
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "SELECT id_salle, nom, capacite, type_salle FROM salle ORDER BY nom";
        var cmd    = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            liste.Add(new Salle
            {
                IdSalle   = reader.GetInt32("id_salle"),
                Nom       = reader.GetString("nom"),
                Capacite  = reader.GetInt32("capacite"),
                TypeSalle = reader.GetString("type_salle")
            });
        }
        return liste;
    }

    /// <summary>Ajoute une nouvelle salle.</summary>
    public void Ajouter(Salle s)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "INSERT INTO salle (nom, capacite, type_salle) VALUES (@nom, @capacite, @type)";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nom",      s.Nom);
        cmd.Parameters.AddWithValue("@capacite", s.Capacite);
        cmd.Parameters.AddWithValue("@type",     s.TypeSalle);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Modifie une salle existante.</summary>
    public void Modifier(Salle s)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "UPDATE salle SET nom=@nom, capacite=@capacite, type_salle=@type WHERE id_salle=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nom",      s.Nom);
        cmd.Parameters.AddWithValue("@capacite", s.Capacite);
        cmd.Parameters.AddWithValue("@type",     s.TypeSalle);
        cmd.Parameters.AddWithValue("@id",       s.IdSalle);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Supprime une salle par son identifiant.</summary>
    public void Supprimer(int id)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "DELETE FROM salle WHERE id_salle = @id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
