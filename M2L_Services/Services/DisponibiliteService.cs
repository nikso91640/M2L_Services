using System;
using M2L_Services.DataAccess;

namespace M2L_Services.Services
{
    /// <summary>
    /// Service de vérification de disponibilité d'une salle.
    /// Vérifie qu'il n'y a pas de chevauchement avec une réservation existante.
    /// </summary>
    public class DisponibiliteService
    {
        private readonly ReservationDAO _reservationDAO;

        public DisponibiliteService()
        {
            _reservationDAO = new ReservationDAO();
        }

        /// <summary>
        /// Vérifie si une salle est disponible pour le créneau demandé.
        /// Une salle est indisponible si une réservation CONFIRMEE existe
        /// pour la même salle, le même jour, avec un créneau qui se chevauche.
        /// </summary>
        /// <param name="idSalle">Identifiant de la salle à vérifier.</param>
        /// <param name="date">Date de la réservation souhaitée.</param>
        /// <param name="heureDebut">Heure de début du créneau souhaité.</param>
        /// <param name="heureFin">Heure de fin du créneau souhaité.</param>
        /// <param name="idReservationExclure">
        /// En cas de modification, l'ID de la réservation en cours (pour ne pas se bloquer soi-même).
        /// Laisser à 0 pour une nouvelle réservation.
        /// </param>
        /// <returns>True si la salle est disponible, false sinon.</returns>
        public bool EstDisponible(int idSalle, DateTime date, TimeSpan heureDebut, TimeSpan heureFin, int idReservationExclure = 0)
        {
            return _reservationDAO.EstDisponible(idSalle, date, heureDebut, heureFin, idReservationExclure);
        }

        /// <summary>
        /// Retourne un message clair à afficher à l'utilisateur
        /// si la salle n'est pas disponible sur le créneau demandé.
        /// </summary>
        public string GetMessageIndisponibilite(DateTime date, TimeSpan heureDebut, TimeSpan heureFin)
        {
            return $"La salle est déjà réservée le {date:dd/MM/yyyy} " +
                   $"entre {heureDebut:hh\\:mm} et {heureFin:hh\\:mm}.\n" +
                   "Veuillez choisir un autre créneau.";
        }
    }
}
