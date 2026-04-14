namespace M2L_Services.Models;

/// <summary>Représente une salle réservable (réunion, amphi, convivialité, multimédia).</summary>
public class Salle
{
    public int    IdSalle   { get; set; }
    public string Nom       { get; set; } = string.Empty;
    public int    Capacite  { get; set; }
    public string TypeSalle { get; set; } = "REUNION"; // REUNION | AMPHI | CONVIVIALITE | MULTIMEDIA

    /// <summary>Affiche le nom dans les ComboBox et DataGridView.</summary>
    public override string ToString() => Nom;
}
