using MySql.Data.MySqlClient;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.DataAccess;

/// <summary>Gère les accès base de données pour les structures.</summary>
public class StructureDAO
{
    /// <summary>Retourne toutes les structures triées par nom.</summary>
    public List<Structure> GetTout()
    {
        var liste = new List<Structure>();
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "SELECT id_structure, nom, type_structure FROM structure ORDER BY nom";
        var cmd    = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            liste.Add(new Structure
            {
                IdStructure   = reader.GetInt32("id_structure"),
                Nom           = reader.GetString("nom"),
                TypeStructure = reader.GetString("type_structure")
            });
        }
        return liste;
    }

    /// <summary>Ajoute une nouvelle structure.</summary>
    public void Ajouter(Structure s)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "INSERT INTO structure (nom, type_structure) VALUES (@nom, @type)";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nom",  s.Nom);
        cmd.Parameters.AddWithValue("@type", s.TypeStructure);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Modifie une structure existante.</summary>
    public void Modifier(Structure s)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "UPDATE structure SET nom=@nom, type_structure=@type WHERE id_structure=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nom",  s.Nom);
        cmd.Parameters.AddWithValue("@type", s.TypeStructure);
        cmd.Parameters.AddWithValue("@id",   s.IdStructure);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Supprime une structure par son identifiant.</summary>
    public void Supprimer(int id)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "DELETE FROM structure WHERE id_structure = @id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
