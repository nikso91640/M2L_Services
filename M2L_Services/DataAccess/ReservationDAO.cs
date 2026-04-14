using MySql.Data.MySqlClient;
using M2L_Services.Models;
using M2L_Services.Utils;

namespace M2L_Services.DataAccess;

/// <summary>
/// Gère les accès base de données pour les réservations.
/// </summary>
public class ReservationDAO
{
    /// <summary>
    /// Retourne les réservations avec filtres optionnels.
    /// Passer 0 ou null pour ignorer un filtre.
    /// </summary>
    public List<Reservation> GetTout(int idSalle = 0, int idStructure = 0, DateTime? date = null, string statut = "")
    {
        var liste = new List<Reservation>();
        using var conn = ConnexionBDD.GetConnexion();

        string sql = @"SELECT r.id_reservation, r.id_salle, r.id_structure,
                              r.date_reservation, r.heure_debut, r.heure_fin,
                              r.statut, r.commentaire,
                              s.nom  AS nom_salle,
                              st.nom AS nom_structure
                       FROM reservation r
                       JOIN salle     s  ON r.id_salle     = s.id_salle
                       JOIN structure st ON r.id_structure = st.id_structure
                       WHERE 1=1";

        if (idSalle     > 0)               sql += " AND r.id_salle = @idSalle";
        if (idStructure > 0)               sql += " AND r.id_structure = @idStructure";
        if (date.HasValue)                 sql += " AND r.date_reservation = @date";
        if (!string.IsNullOrEmpty(statut)) sql += " AND r.statut = @statut";
        sql += " ORDER BY r.date_reservation DESC, r.heure_debut";

        var cmd = new MySqlCommand(sql, conn);
        if (idSalle     > 0)               cmd.Parameters.AddWithValue("@idSalle",     idSalle);
        if (idStructure > 0)               cmd.Parameters.AddWithValue("@idStructure", idStructure);
        if (date.HasValue)                 cmd.Parameters.AddWithValue("@date",        date.Value.ToString("yyyy-MM-dd"));
        if (!string.IsNullOrEmpty(statut)) cmd.Parameters.AddWithValue("@statut",      statut);

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            liste.Add(new Reservation
            {
                IdReservation   = reader.GetInt32("id_reservation"),
                IdSalle         = reader.GetInt32("id_salle"),
                IdStructure     = reader.GetInt32("id_structure"),
                DateReservation = reader.GetDateTime("date_reservation"),
                HeureDebut      = reader.GetTimeSpan("heure_debut"),
                HeureFin        = reader.GetTimeSpan("heure_fin"),
                Statut          = reader.GetString("statut"),
                Commentaire     = reader.IsDBNull(reader.GetOrdinal("commentaire")) ? "" : reader.GetString("commentaire"),
                NomSalle        = reader.GetString("nom_salle"),
                NomStructure    = reader.GetString("nom_structure")
            });
        }
        return liste;
    }

    /// <summary>Insère une réservation et retourne son nouvel identifiant.</summary>
    public int Ajouter(Reservation r)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"INSERT INTO reservation
                           (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire)
                       VALUES
                           (@idSalle, @idStructure, @date, @debut, @fin, @statut, @commentaire);
                       SELECT LAST_INSERT_ID();";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@idSalle",     r.IdSalle);
        cmd.Parameters.AddWithValue("@idStructure", r.IdStructure);
        cmd.Parameters.AddWithValue("@date",        r.DateReservation.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@debut",       r.HeureDebut.ToString(@"hh\:mm\:ss"));
        cmd.Parameters.AddWithValue("@fin",         r.HeureFin.ToString(@"hh\:mm\:ss"));
        cmd.Parameters.AddWithValue("@statut",      r.Statut);
        cmd.Parameters.AddWithValue("@commentaire", r.Commentaire);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>Met à jour une réservation existante.</summary>
    public void Modifier(Reservation r)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"UPDATE reservation
                       SET id_salle=@idSalle, id_structure=@idStructure,
                           date_reservation=@date, heure_debut=@debut, heure_fin=@fin,
                           statut=@statut, commentaire=@commentaire
                       WHERE id_reservation=@id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@idSalle",     r.IdSalle);
        cmd.Parameters.AddWithValue("@idStructure", r.IdStructure);
        cmd.Parameters.AddWithValue("@date",        r.DateReservation.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@debut",       r.HeureDebut.ToString(@"hh\:mm\:ss"));
        cmd.Parameters.AddWithValue("@fin",         r.HeureFin.ToString(@"hh\:mm\:ss"));
        cmd.Parameters.AddWithValue("@statut",      r.Statut);
        cmd.Parameters.AddWithValue("@commentaire", r.Commentaire);
        cmd.Parameters.AddWithValue("@id",          r.IdReservation);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Annule une réservation (passe le statut à ANNULEE).</summary>
    public void Annuler(int id)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "UPDATE reservation SET statut = 'ANNULEE' WHERE id_reservation = @id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Supprime définitivement une réservation.</summary>
    public void Supprimer(int id)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = "DELETE FROM reservation WHERE id_reservation = @id";
        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Vérifie qu'aucune réservation CONFIRMEE ne chevauche le créneau demandé.
    /// Retourne true si le créneau est libre.
    /// </summary>
    public bool EstDisponible(int idSalle, DateTime date, TimeSpan heureDebut, TimeSpan heureFin, int excludeId = 0)
    {
        using var conn = ConnexionBDD.GetConnexion();
        string sql = @"SELECT COUNT(*) FROM reservation
                       WHERE id_salle         = @idSalle
                         AND date_reservation = @date
                         AND statut           = 'CONFIRMEE'
                         AND heure_debut      < @heureFin
                         AND heure_fin        > @heureDebut";
        if (excludeId > 0) sql += " AND id_reservation != @excludeId";

        var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@idSalle",    idSalle);
        cmd.Parameters.AddWithValue("@date",       date.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("@heureDebut", heureDebut.ToString(@"hh\:mm\:ss"));
        cmd.Parameters.AddWithValue("@heureFin",   heureFin.ToString(@"hh\:mm\:ss"));
        if (excludeId > 0) cmd.Parameters.AddWithValue("@excludeId", excludeId);

        return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
    }
}
