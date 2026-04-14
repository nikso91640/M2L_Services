using MySql.Data.MySqlClient;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.DataAccess;

/// <summary>Gère les accès base de données pour le digicode et la clé Wifi du jour.</summary>
public class InfoJourDAO
{
    /// <summary>Retourne l'info du jour (aujourd'hui), ou null si aucune saisie.</summary>
    public InfoJour? GetAujourdhui()
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "SELECT id, date_info, digicode, cle_wifi FROM info_jour WHERE date_info = CURDATE()";
        var cmd    = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
            return Mapper(reader);
        return null;
    }

    /// <summary>Retourne l'historique de toutes les infos du jour, du plus récent au plus ancien.</summary>
    public List<InfoJour> GetHistorique()
    {
        var liste = new List<InfoJour>();
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "SELECT id, date_info, digicode, cle_wifi FROM info_jour ORDER BY date_info DESC";
        var cmd    = new MySqlCommand(sql, conn);
        var reader = cmd.ExecuteReader();
        while (reader.Read())
            liste.Add(Mapper(reader));
        return liste;
    }

    /// <summary>
    /// Enregistre ou met à jour l'info du jour.
    /// Si une entrée existe déjà pour cette date, elle est mise à jour (INSERT ... ON DUPLICATE KEY UPDATE).
    /// </summary>
    public void EnregistrerOuModifier(InfoJour info)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"INSERT INTO info_jour (date_info, digicode, cle_wifi)
                       VALUES (@date, @digicode, @wifi)
                       ON DUPLICATE KEY UPDATE digicode=@digicode, cle_wifi=@wifi";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@date",    info.DateInfo.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@digicode", info.Digicode);
        cmd.Parameters.AddWithValue("@wifi",     info.CleWifi);
        cmd.ExecuteNonQuery();
    }

    // Mappe une ligne du reader vers un objet InfoJour
    private InfoJour Mapper(MySqlDataReader reader)
    {
        return new InfoJour
        {
            Id       = reader.GetInt32("id"),
            DateInfo = reader.GetDateTime("date_info"),
            Digicode = reader.GetString("digicode"),
            CleWifi  = reader.GetString("cle_wifi")
        };
    }
}
