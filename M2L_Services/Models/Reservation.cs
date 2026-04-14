namespace M2L_Services.Models;

/// <summary>Représente une réservation de salle par une structure.</summary>
public class Reservation
{
    public int      IdReservation   { get; set; }
    public int      IdSalle         { get; set; }
    public int      IdStructure     { get; set; }
    public DateTime DateReservation { get; set; }
    public TimeSpan HeureDebut      { get; set; }
    public TimeSpan HeureFin        { get; set; }
    public string   Statut          { get; set; } = "EN_ATTENTE"; // EN_ATTENTE | CONFIRMEE | ANNULEE
    public string   Commentaire     { get; set; } = string.Empty;

    // Propriétés calculées via JOIN — pour l'affichage dans les DataGridView
    public string NomSalle     { get; set; } = string.Empty;
    public string NomStructure { get; set; } = string.Empty;
}
