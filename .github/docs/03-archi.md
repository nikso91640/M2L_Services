# 03 — Architecture et conventions de code

## Structure des dossiers

```
M2LServices/
├── Forms/
│   ├── FormLogin.cs
│   ├── FormAccueil.cs
│   ├── FormSalles.cs
│   ├── FormStructures.cs
│   ├── FormReservations.cs
│   ├── FormAffranchissement.cs
│   ├── FormImpressions.cs
│   └── FormDigicode.cs
│
├── Models/
│   ├── Utilisateur.cs
│   ├── Salle.cs
│   ├── Structure.cs
│   ├── Reservation.cs
│   ├── ServiceReservation.cs
│   ├── ConsommationAffranchissement.cs
│   ├── ConsommationImpression.cs
│   └── InfoJour.cs
│
├── DataAccess/
│   ├── UtilisateurDAO.cs
│   ├── SalleDAO.cs
│   ├── StructureDAO.cs
│   ├── ReservationDAO.cs
│   ├── AffranchissementDAO.cs
│   ├── ImpressionDAO.cs
│   └── InfoJourDAO.cs
│
├── Services/
│   ├── TarifService.cs       ← calcule le niveau de tarif
│   ├── DisponibiliteService.cs ← vérifie les chevauchements
│   └── ImpressionService.cs  ← calcule le montant d'une impression
│
└── Utils/
    └── ConnexionBDD.cs
```

---

## Convention : classe Model

Simple conteneur de données. Pas de logique métier dedans.

```csharp
namespace M2LServices.Models
{
    public class Salle
    {
        public int    IdSalle   { get; set; }
        public string Nom       { get; set; }
        public int    Capacite  { get; set; }
        public string TypeSalle { get; set; } // "REUNION", "AMPHI", etc.

        // Pour afficher proprement dans un ComboBox ou DataGridView
        public override string ToString() => Nom;
    }
}
```

---

## Convention : classe DAO

Toutes les requêtes SQL sont dans les DAO. Les Forms ne font jamais de SQL directement.
Toujours utiliser des requêtes paramétrées (jamais de concaténation de chaînes SQL).

```csharp
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using M2LServices.Models;
using M2LServices.Utils;

namespace M2LServices.DataAccess
{
    public class SalleDAO
    {
        // Récupère toutes les salles
        public List<Salle> GetTout()
        {
            var liste = new List<Salle>();

            using (var conn = ConnexionBDD.GetConnexion())
            {
                string sql = "SELECT id_salle, nom, capacite, type_salle FROM salle ORDER BY nom";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    liste.Add(new Salle
                    {
                        IdSalle   = reader.GetInt32("id_salle"),
                        Nom       = reader.GetString("nom"),
                        Capacite  = reader.GetInt32("capacite"),
                        TypeSalle = reader.GetString("type_salle")
                    });
                }
            }

            return liste;
        }

        // Insère une nouvelle salle — exemple de requête paramétrée
        public void Ajouter(Salle s)
        {
            using (var conn = ConnexionBDD.GetConnexion())
            {
                string sql = @"INSERT INTO salle (nom, capacite, type_salle)
                               VALUES (@nom, @capacite, @type)";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nom",      s.Nom);
                cmd.Parameters.AddWithValue("@capacite", s.Capacite);
                cmd.Parameters.AddWithValue("@type",     s.TypeSalle);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
```

---

## Convention : classe Service

Contient la logique métier. Appelée par les Forms. N'accède pas directement à la BDD (délègue aux DAO).

```csharp
namespace M2LServices.Services
{
    public class TarifService
    {
        private readonly ReservationDAO _reservationDAO = new ReservationDAO();

        /// <summary>
        /// Calcule le niveau de tarif applicable pour une structure et un type de salle donnés.
        /// </summary>
        public string CalculerTarif(Structure structure, string typeSalle)
        {
            if (structure.TypeStructure == "LIGUE")
            {
                int quota = GetQuotaGratuit(typeSalle);
                int utilisees = _reservationDAO.CompterReservationsGratuitesAnnee(structure.IdStructure, typeSalle);

                if (utilisees < quota)
                    return "GRATUIT";
                else
                    return "TARIF_1";
            }

            if (structure.TypeStructure == "CLUB")       return "TARIF_1";
            if (structure.TypeStructure == "ASSOCIATION") return "TARIF_2";
            if (structure.TypeStructure == "LYCEE_COLLEGE") return "TARIF_2";

            return "TARIF_3"; // ENTREPRISE et AUTRE
        }

        private int GetQuotaGratuit(string typeSalle)
        {
            if (typeSalle == "REUNION")      return 6;
            if (typeSalle == "AMPHI")        return 1;
            if (typeSalle == "CONVIVIALITE") return 1;
            return 0;
        }
    }
}
```

---

## Conventions de nommage

| Élément | Convention | Exemple |
|---|---|---|
| Classe | PascalCase | `SalleDAO`, `TarifService` |
| Méthode | PascalCase | `GetTout()`, `Ajouter()` |
| Variable locale | camelCase | `listeSalles`, `maReservation` |
| Paramètre SQL | @nomParam | `@idSalle`, `@date` |
| Fichier | = nom de la classe | `SalleDAO.cs` |

---

## Règles à toujours respecter

- Jamais de SQL dans un Form — toujours passer par un DAO
- Toujours utiliser `using` pour les connexions MySQL
- Toujours paramétrer les requêtes SQL (jamais de concaténation)
- Valider les champs dans le Form avant d'appeler le DAO
- Afficher des messages d'erreur compréhensibles (`MessageBox.Show`)
- Commenter les méthodes avec `///`
