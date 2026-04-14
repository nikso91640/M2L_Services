# 01 — Contexte métier et règles de gestion

## Entités principales

| Entité | Rôle |
|---|---|
| `Salle` | Salle réservable (réunion, amphi, convivialité, multimédia) |
| `Structure` | Ligue, club, association, lycée, entreprise... |
| `Reservation` | Réservation d'une salle par une structure |
| `ServiceReservation` | Service optionnel lié à une réservation |
| `ConsommationAffranchissement` | Consommation courrier mensuelle par ligue |
| `ConsommationImpression` | Consommation reprographie mensuelle par ligue |
| `InfoJour` | Digicode et clé Wifi du jour |
| `Utilisateur` | Utilisateur de l'application (admin ou agent) |

---

## Types de salles

- `REUNION` — salles de réunion (6 réservations gratuites/an pour les ligues)
- `AMPHI` — amphithéâtre (1 réservation gratuite/an pour les ligues)
- `CONVIVIALITE` — salle de convivialité (1 réservation gratuite/an pour les ligues)
- `MULTIMEDIA` — salle multimédia

---

## Types de structures

- `LIGUE` — ligue sportive hébergée
- `CLUB` — club sportif ou comité départemental
- `ASSOCIATION` — association, lycée ou collège
- `LYCEE_COLLEGE` — lycée ou collège
- `ENTREPRISE` — société privée
- `AUTRE` — tout autre organisme extérieur

---

## Niveaux de tarification

| Niveau | Structure concernée | Condition |
|---|---|---|
| GRATUIT | Ligue | Quota non atteint sur l'année |
| TARIF_1 | Ligue (quota dépassé), Club, Comité dép. | Toujours payant ou quota épuisé |
| TARIF_2 | Association, Lycée, Collège | Toujours payant |
| TARIF_3 | Entreprise, Autre | Tarif le plus élevé |

### Quotas annuels gratuits pour une ligue

- Salles de réunion : **6 réservations gratuites par an**
- Amphithéâtre : **1 réservation gratuite par an**
- Salle de convivialité : **1 réservation gratuite par an**

> Si le quota est atteint, la ligue passe automatiquement en TARIF_1.

### Algorithme de calcul du tarif (à implémenter dans `TarifService.cs`)

```
Si structure.Type == LIGUE :
    compter les réservations CONFIRMEE de l'année en cours pour cette ligue et ce type de salle
    Si count < quota_pour_ce_type_de_salle :
        tarif = GRATUIT
    Sinon :
        tarif = TARIF_1
Si structure.Type == CLUB ou COMITE_DEPARTEMENTAL :
    tarif = TARIF_1
Si structure.Type == ASSOCIATION ou LYCEE_COLLEGE :
    tarif = TARIF_2
Si structure.Type == ENTREPRISE ou AUTRE :
    tarif = TARIF_3
```

---

## Statuts d'une réservation

- `EN_ATTENTE` — créée, pas encore confirmée
- `CONFIRMEE` — validée par l'administration
- `ANNULEE` — annulée

---

## Règle de disponibilité d'un créneau

Avant d'enregistrer une réservation, vérifier qu'il n'existe pas de réservation `CONFIRMEE`
pour la même salle, le même jour, dont les horaires se chevauchent :

```sql
SELECT COUNT(*) FROM reservation
WHERE id_salle = @idSalle
  AND date_reservation = @date
  AND statut = 'CONFIRMEE'
  AND heure_debut < @heureFin
  AND heure_fin > @heureDebut
```

Si COUNT > 0 : créneau indisponible → afficher un message d'erreur clair.

---

## Types d'impressions

- `NB` — noir & blanc (photocopieuse)
- `COULEUR` — laser couleur A4/A3
- `TRACEUR` — traceur A2 (affiches, banderoles)

Les prix unitaires par type sont configurables par l'administrateur (table `tarif_impression`).

---

## Rôles utilisateurs

- `ADMIN` — accès complet : modification des tarifs, suppression de données
- `AGENT` — saisie et consultation uniquement
