namespace M2L_Services.Models;

/// <summary>Digicode et clé Wifi du jour, saisis par l'administration.</summary>
public class InfoJour
{
    public int      Id       { get; set; }
    public DateTime DateInfo { get; set; }
    public string   Digicode { get; set; } = string.Empty;
    public string   CleWifi  { get; set; } = string.Empty;
}
