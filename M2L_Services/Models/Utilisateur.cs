namespace M2L_Services.Models;

/// <summary>Représente un utilisateur de l'application (admin ou agent).</summary>
public class Utilisateur
{
    public int    IdUtilisateur { get; set; }
    public string Login         { get; set; } = string.Empty;
    public string Nom           { get; set; } = string.Empty;
    public string Prenom        { get; set; } = string.Empty;
    public string Role          { get; set; } = "AGENT"; // "ADMIN" ou "AGENT"

    public override string ToString() => $"{Prenom} {Nom}";
}
