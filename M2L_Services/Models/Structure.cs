namespace M2L_Services.Models;

/// <summary>Représente une structure (ligue, club, association, entreprise...).</summary>
public class Structure
{
    public int    IdStructure   { get; set; }
    public string Nom           { get; set; } = string.Empty;
    public string TypeStructure { get; set; } = "LIGUE"; // LIGUE | CLUB | ASSOCIATION | LYCEE_COLLEGE | ENTREPRISE | AUTRE

    /// <summary>Affiche le nom dans les ComboBox et DataGridView.</summary>
    public override string ToString() => Nom;
}
