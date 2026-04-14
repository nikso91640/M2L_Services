using M2L_Services.Models;

namespace M2L_Services.Utils;

/// <summary>
/// Stocke l'utilisateur connecté pour toute la durée de la session.
/// Accessible depuis n'importe quel formulaire via Session.UtilisateurConnecte.
/// </summary>
public static class Session
{
    /// <summary>L'utilisateur actuellement connecté (null si personne n'est connecté).</summary>
    public static Utilisateur? UtilisateurConnecte { get; set; }

    /// <summary>Vrai si l'utilisateur connecté est administrateur.</summary>
    public static bool EstAdmin => UtilisateurConnecte?.Role == "ADMIN";
}
