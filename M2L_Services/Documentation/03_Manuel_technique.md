# Manuel technique — M2L Services

| **Élément**         | **Valeur**                                |
|---------------------|-------------------------------------------|
| Projet              | M2L Services                              |
| Version             | 1.0                                       |
| Date                | 2025                                      |
| Auteur              | *[Votre nom, prénom]*                     |
| Formation           | BTS SIO — Option SLAM                     |
| Épreuve             | E6 — Conception et développement d'applications |

---

## 1. Présentation générale

**M2L Services** est une application Windows Forms développée en C# avec le framework .NET 10. Elle permet de gérer les services mutualisés de la Maison des Ligues de Lorraine : salles, structures, réservations, digicode et Wifi.

L'application se connecte à une base de données **MySQL** locale via le connecteur **MySql.Data** et utilise **ADO.NET** pour l'accès aux données (requêtes SQL paramétrées, pas d'ORM).

---

## 2. Stack technique

| Composant          | Technologie / Version                   | Rôle                                         |
|--------------------|-----------------------------------------|----------------------------------------------|
| Langage            | C# 14                                   | Langage de programmation principal            |
| Framework          | .NET 10 (Windows Forms)                 | Interface graphique bureau Windows            |
| IDE                | Visual Studio 2022 / 2026               | Environnement de développement               |
| Base de données    | MySQL 5.7+ / MariaDB (via WAMP)         | Stockage persistant des données              |
| Connecteur BDD     | MySql.Data 9.6.0 (NuGet)               | Permet la communication C# ↔ MySQL           |
| Accès aux données  | ADO.NET                                  | Exécution de requêtes SQL paramétrées        |
| Sécurité           | SHA2-256 (côté MySQL)                   | Hachage des mots de passe                    |

### Fichier projet (.csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="MySql.Data" Version="9.6.0" />
  </ItemGroup>
</Project>
```

**Explications :**
- `WinExe` : l'application est un exécutable Windows (pas une application console) ;
- `net10.0-windows` : cible le framework .NET 10 avec le support Windows Forms ;
- `Nullable` : active les avertissements sur les valeurs nulles en C# ;
- `MySql.Data` : le package NuGet qui fournit les classes `MySqlConnection`, `MySqlCommand`, etc.

---

## 3. Architecture du projet

### 3.1 Organisation des fichiers

```
📂 M2L_Services/
│
├── 📄 Program.cs                         → Point d'entrée de l'application
├── 📄 M2L_Services.csproj                → Configuration du projet .NET
├── 📄 database.sql                       → Script SQL de création de la base
│
├── 📂 Models/                            → Classes métier (données)
│   ├── 📄 Utilisateur.cs                 → Représente un utilisateur
│   ├── 📄 Salle.cs                       → Représente une salle
│   ├── 📄 Structure.cs                   → Représente une structure (ligue, club…)
│   ├── 📄 Reservation.cs                 → Représente une réservation
│   └── 📄 InfoJour.cs                    → Représente le digicode/wifi d'un jour
│
├── 📂 DataAccess/                        → Accès base de données (DAO)
│   ├── 📄 UtilisateurDAO.cs              → Requêtes SQL pour les utilisateurs
│   ├── 📄 SalleDAO.cs                    → Requêtes SQL pour les salles
│   ├── 📄 StructureDAO.cs                → Requêtes SQL pour les structures
│   ├── 📄 ReservationDAO.cs              → Requêtes SQL pour les réservations
│   └── 📄 InfoJourDAO.cs                 → Requêtes SQL pour les infos du jour
│
├── 📂 Services/                          → Logique métier
│   └── 📄 DisponibiliteService.cs        → Vérification de disponibilité d'un créneau
│
├── 📂 Utils/                             → Utilitaires partagés
│   ├── 📄 ConnexionBDD.cs                → Chaîne de connexion MySQL centralisée
│   └── 📄 Session.cs                     → Utilisateur connecté (données de session)
│
├── 📂 Forms/                             → Formulaires WinForms (interface)
│   ├── 📄 FormLogin.cs / .Designer.cs           → Écran de connexion
│   ├── 📄 FormAccueil.cs / .Designer.cs         → Tableau de bord principal
│   ├── 📄 FormSalles.cs / .Designer.cs          → Liste des salles
│   ├── 📄 FormSalleDetail.cs / .Designer.cs     → Détail d'une salle (ajout/modif)
│   ├── 📄 FormStructures.cs / .Designer.cs      → Liste des structures
│   ├── 📄 FormStructureDetail.cs / .Designer.cs → Détail d'une structure
│   ├── 📄 FormReservations.cs / .Designer.cs    → Liste des réservations
│   ├── 📄 FormReservationDetail.cs / .Designer.cs → Détail d'une réservation
│   └── 📄 FormDigicode.cs / .Designer.cs        → Digicode et Wifi du jour
│
└── 📂 Documentation/                    → Documents du projet
    ├── 📄 01_Cahier_des_charges.md
    ├── 📄 02_Manuel_installation.md
    ├── 📄 03_Manuel_technique.md
    └── 📄 04_Manuel_utilisateur.md
```

### 3.2 Rôle de chaque couche

```
┌─────────────────────────────────────────────────┐
│         FORMS (Interface utilisateur)           │
│  Ce que l'utilisateur voit et avec quoi         │
│  il interagit (boutons, grilles, champs…)       │
│  Appelle les DAO pour charger/enregistrer.      │
├─────────────────────────────────────────────────┤
│         SERVICES (Logique métier)               │
│  Contient les règles métier complexes.          │
│  Exemple : "Ce créneau est-il disponible ?"     │
│  Appelé par les Forms, utilise les DAO.         │
├─────────────────────────────────────────────────┤
│         DATAACCESS (DAO)                        │
│  Exécute les requêtes SQL (SELECT, INSERT,      │
│  UPDATE, DELETE). Un fichier par entité.         │
│  Retourne des objets du modèle.                 │
├─────────────────────────────────────────────────┤
│         MODELS (Classes métier)                 │
│  Classes simples avec des propriétés            │
│  (get/set). Pas de logique, juste des données.  │
├─────────────────────────────────────────────────┤
│         UTILS (Utilitaires)                     │
│  ConnexionBDD : ouvre la connexion MySQL.        │
│  Session : stocke l'utilisateur connecté.        │
├─────────────────────────────────────────────────┤
│         BASE DE DONNÉES MYSQL                   │
│  Tables : utilisateur, salle, structure,         │
│  reservation, info_jour                          │
└─────────────────────────────────────────────────┘
```

**Pourquoi cette architecture ?**
- **Séparation des responsabilités** : chaque couche a un rôle unique ;
- **Maintenance facilitée** : pour modifier une requête SQL, on sait qu'il faut aller dans DataAccess ;
- **Réutilisation** : un DAO peut être appelé depuis plusieurs formulaires ;
- **Simplicité** : adaptée au niveau BTS SIO, pas de patterns complexes.

---

## 4. Description détaillée des classes

### 4.1 Couche Models — Les classes métier

Les classes du modèle sont des **classes simples** (appelées POCO — Plain Old CLR Object). Elles ne contiennent que des propriétés `get/set`, sans logique métier.

#### Utilisateur.cs

```csharp
public class Utilisateur
{
    public int    IdUtilisateur { get; set; }
    public string Login         { get; set; } = string.Empty;
    public string Nom           { get; set; } = string.Empty;
    public string Prenom        { get; set; } = string.Empty;
    public string Role          { get; set; } = "AGENT";
}
```

| Propriété       | Type     | Description                                |
|-----------------|----------|--------------------------------------------|
| IdUtilisateur   | `int`    | Identifiant auto-incrémenté (clé primaire) |
| Login           | `string` | Identifiant de connexion (unique en base)  |
| Nom             | `string` | Nom de famille                             |
| Prenom          | `string` | Prénom                                     |
| Role            | `string` | `"ADMIN"` ou `"AGENT"`                     |

> **Note :** Le mot de passe n'est **jamais** stocké dans l'objet C#. Le hachage SHA2-256 est fait directement par MySQL dans la requête SQL.

#### Salle.cs

| Propriété  | Type     | Description                                              |
|------------|----------|----------------------------------------------------------|
| IdSalle    | `int`    | Identifiant auto-incrémenté                              |
| Nom        | `string` | Nom de la salle (ex. : "Salle Gallé")                   |
| Capacite   | `int`    | Nombre de places                                         |
| TypeSalle  | `string` | `REUNION`, `AMPHI`, `CONVIVIALITE` ou `MULTIMEDIA`      |

#### Structure.cs

| Propriété     | Type     | Description                                                |
|---------------|----------|------------------------------------------------------------|
| IdStructure   | `int`    | Identifiant auto-incrémenté                                |
| Nom           | `string` | Nom de la structure (ex. : "Ligue Lorraine de Football")   |
| TypeStructure | `string` | `LIGUE`, `CLUB`, `ASSOCIATION`, `LYCEE_COLLEGE`, `ENTREPRISE`, `AUTRE` |

#### Reservation.cs

| Propriété       | Type       | Description                                       |
|-----------------|------------|---------------------------------------------------|
| IdReservation   | `int`      | Identifiant auto-incrémenté                       |
| IdSalle         | `int`      | Clé étrangère → table `salle`                     |
| IdStructure     | `int`      | Clé étrangère → table `structure`                 |
| DateReservation | `DateTime` | Date de la réservation                            |
| HeureDebut      | `TimeSpan` | Heure de début du créneau                         |
| HeureFin        | `TimeSpan` | Heure de fin du créneau                           |
| Statut          | `string`   | `EN_ATTENTE`, `CONFIRMEE` ou `ANNULEE`            |
| Commentaire     | `string`   | Commentaire libre (facultatif)                    |
| NomSalle        | `string`   | Nom de la salle — chargé via un JOIN en lecture    |
| NomStructure    | `string`   | Nom de la structure — chargé via un JOIN en lecture |

> **Pourquoi NomSalle et NomStructure ?** Pour afficher directement le nom de la salle et de la structure dans le DataGridView sans faire de requête supplémentaire. Ces propriétés sont remplies grâce à un `JOIN` dans la requête SQL du DAO.

#### InfoJour.cs

| Propriété | Type       | Description                              |
|-----------|------------|------------------------------------------|
| Id        | `int`      | Identifiant auto-incrémenté              |
| DateInfo  | `DateTime` | Date du jour concerné (UNIQUE en base)   |
| Digicode  | `string`   | Code d'accès du jour                     |
| CleWifi   | `string`   | Clé Wifi visiteurs du jour               |

---

### 4.2 Couche DataAccess — Les DAO

Chaque DAO (Data Access Object) encapsule toutes les requêtes SQL pour **une seule entité**. Le patron de code est toujours le même :

```
1. Ouvrir une connexion MySQL (via ConnexionBDD.GetConnexion())
2. Écrire la requête SQL avec des paramètres (@param)
3. Associer les valeurs C# aux paramètres (AddWithValue)
4. Exécuter la requête (ExecuteReader / ExecuteNonQuery / ExecuteScalar)
5. Lire les résultats et construire les objets du modèle
6. La connexion se ferme automatiquement grâce au bloc "using"
```

#### Exemple commenté — SalleDAO.GetTout()

```csharp
public List<Salle> GetTout()
{
    // 1. Créer une liste vide pour stocker les résultats
    var liste = new List<Salle>();

    // 2. Ouvrir une connexion MySQL (se ferme automatiquement grâce à "using")
    using var conn = ConnexionBDD.GetConnexion();

    // 3. Écrire la requête SQL
    string sql = "SELECT id_salle, nom, capacite, type_salle FROM salle ORDER BY nom";

    // 4. Créer la commande SQL associée à la connexion
    var cmd = new MySqlCommand(sql, conn);

    // 5. Exécuter la requête et lire les résultats ligne par ligne
    var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        // 6. Pour chaque ligne, créer un objet Salle et l'ajouter à la liste
        liste.Add(new Salle
        {
            IdSalle   = reader.GetInt32("id_salle"),
            Nom       = reader.GetString("nom"),
            Capacite  = reader.GetInt32("capacite"),
            TypeSalle = reader.GetString("type_salle")
        });
    }

    // 7. Retourner la liste complète
    return liste;
}
```

#### Exemple commenté — SalleDAO.Ajouter() (avec paramètres)

```csharp
public void Ajouter(Salle s)
{
    using var conn = ConnexionBDD.GetConnexion();

    // Les @nom, @capacite, @type sont des PARAMÈTRES SQL
    // Ils protègent contre les injections SQL
    string sql = "INSERT INTO salle (nom, capacite, type_salle) VALUES (@nom, @capacite, @type)";

    var cmd = new MySqlCommand(sql, conn);

    // On associe chaque paramètre à sa valeur C#
    cmd.Parameters.AddWithValue("@nom",      s.Nom);
    cmd.Parameters.AddWithValue("@capacite", s.Capacite);
    cmd.Parameters.AddWithValue("@type",     s.TypeSalle);

    // ExecuteNonQuery = exécuter sans lire de résultats (INSERT, UPDATE, DELETE)
    cmd.ExecuteNonQuery();
}
```

> **Pourquoi des paramètres SQL ?** Pour éviter les **injections SQL**. Si on concaténait directement les valeurs dans la requête, un utilisateur malveillant pourrait injecter du code SQL dangereux. Avec `AddWithValue`, MySQL traite toujours la valeur comme une donnée, jamais comme du code.

#### Récapitulatif des méthodes par DAO

| DAO               | Méthodes                                                                        |
|--------------------|--------------------------------------------------------------------------------|
| **UtilisateurDAO** | `Authentifier(login, mdp)` — `GetTout()` — `Ajouter(u, mdp)` — `Modifier(u)` |
| **SalleDAO**       | `GetTout()` — `Ajouter(s)` — `Modifier(s)` — `Supprimer(id)`                 |
| **StructureDAO**   | `GetTout()` — `Ajouter(s)` — `Modifier(s)` — `Supprimer(id)`                 |
| **ReservationDAO** | `GetTout(filtres)` — `Ajouter(r)` — `Modifier(r)` — `Annuler(id)` — `Supprimer(id)` — `EstDisponible(...)` |
| **InfoJourDAO**    | `GetAujourdhui()` — `GetHistorique()` — `EnregistrerOuModifier(info)`         |

---

### 4.3 Couche Services — Logique métier

#### DisponibiliteService.cs

Cette classe isole la **règle métier** de vérification de disponibilité. Elle appelle le DAO et fournit un message d'erreur formaté.

| Méthode                          | Description                                                         |
|----------------------------------|---------------------------------------------------------------------|
| `EstDisponible(...)`             | Retourne `true` si le créneau est libre (pas de chevauchement)     |
| `GetMessageIndisponibilite(...)` | Retourne un message clair pour l'utilisateur si le créneau est pris |

**Requête SQL utilisée (dans ReservationDAO.EstDisponible) :**

```sql
SELECT COUNT(*) FROM reservation
WHERE id_salle         = @idSalle
  AND date_reservation = @date
  AND statut           = 'CONFIRMEE'
  AND heure_debut      < @heureFin      -- commence avant la fin demandée
  AND heure_fin        > @heureDebut    -- finit après le début demandé
```

> **Explication :** Si `COUNT(*) > 0`, il existe au moins une réservation confirmée qui chevauche le créneau demandé → la salle est **indisponible**.

---

### 4.4 Couche Utils — Utilitaires

#### ConnexionBDD.cs

Classe **statique** qui centralise l'accès à MySQL. Toutes les DAO passent par cette classe.

```csharp
public static class ConnexionBDD
{
    // La chaîne de connexion est définie UNE SEULE FOIS ici
    private static readonly string _chaineConnexion =
        "Server=localhost;Port=3306;Database=m2l_services;Uid=root;Pwd=;CharSet=utf8mb4;";

    // Retourne une connexion MySQL déjà ouverte
    public static MySqlConnection GetConnexion()
    {
        var conn = new MySqlConnection(_chaineConnexion);
        conn.Open();
        return conn;
    }
}
```

**Pourquoi une classe statique ?**
- Elle n'a pas besoin d'être instanciée (pas de `new ConnexionBDD()`) ;
- On l'appelle directement : `ConnexionBDD.GetConnexion()` ;
- La chaîne de connexion est définie **à un seul endroit** → facile à modifier.

#### Session.cs

Classe **statique** qui stocke l'utilisateur connecté pendant toute la durée de la session.

```csharp
public static class Session
{
    // L'utilisateur actuellement connecté (null si personne n'est connecté)
    public static Utilisateur? UtilisateurConnecte { get; set; }

    // Raccourci : vrai si l'utilisateur connecté est administrateur
    public static bool EstAdmin => UtilisateurConnecte?.Role == "ADMIN";
}
```

**Comment c'est utilisé :**
- Après une connexion réussie : `Session.UtilisateurConnecte = utilisateur;`
- Pour vérifier le rôle : `if (Session.EstAdmin) { ... }`
- À la déconnexion : `Session.UtilisateurConnecte = null;`

---

### 4.5 Couche Forms — Interface utilisateur

#### Navigation de l'application

```
┌──────────┐     connexion réussie      ┌──────────────┐
│ FormLogin ├──────────────────────────►│ FormAccueil   │
│           │◄──────────────────────────┤ (tableau bord)│
└──────────┘     déconnexion            └──────┬───────┘
                                               │
                          ┌────────────────────┼────────────────────┐
                          │                    │                    │
                    ┌─────▼─────┐       ┌─────▼──────┐      ┌─────▼──────┐
                    │FormSalles │       │FormStructur│      │FormRéserv. │
                    │  (liste)  │       │  (liste)   │      │  (liste)   │
                    └─────┬─────┘       └─────┬──────┘      └─────┬──────┘
                          │                   │                    │
                    ┌─────▼─────┐       ┌─────▼──────┐      ┌─────▼──────┐
                    │FormSalle  │       │FormStructur│      │FormRéserv. │
                    │  Detail   │       │  Detail    │      │  Detail    │
                    │ (dialog)  │       │ (dialog)   │      │ (dialog)   │
                    └───────────┘       └────────────┘      └────────────┘

                    ┌───────────┐
                    │FormDigicod│  (accédé directement depuis le menu)
                    └───────────┘
```

#### Patron commun des formulaires de gestion

Chaque formulaire de gestion (Salles, Structures, Réservations) suit **le même schéma** :

```
┌──────────────────────────────────────────────────────────┐
│  [Ajouter] [Modifier] | [Supprimer] | [Actualiser] | 🔍│  ← ToolStrip
├──────────────────────────────────────────────────────────┤
│                                                          │
│        DataGridView (grille de données)                  │  ← Données en lecture seule
│        - Sélection ligne entière                         │
│        - Double-clic = ouvrir modification               │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

**Cycle de vie d'un formulaire de gestion :**

```
OnLoad()
   │
   ├── ConfigurerGrille()     → Créer les colonnes du DataGridView
   ├── ChargerDonnees()       → Appeler le DAO pour récupérer les données
   │      └── AppliquerFiltre()  → Parcourir les données et remplir la grille
   └── AppliquerDroits()      → Activer/désactiver les boutons selon le rôle

Clic "Ajouter"
   │
   ├── Ouvrir FormDetail(null)    → Mode ajout (champs vides)
   ├── Si OK → DAO.Ajouter(...)   → Insérer en base
   └── ChargerDonnees()           → Rafraîchir la grille

Clic "Modifier" ou Double-clic
   │
   ├── Ouvrir FormDetail(objet)   → Mode modification (champs pré-remplis)
   ├── Si OK → DAO.Modifier(...)  → Mettre à jour en base
   └── ChargerDonnees()           → Rafraîchir la grille
```

#### Gestion des rôles dans les formulaires

```csharp
private void AppliquerDroits()
{
    // On récupère le rôle depuis la Session
    bool estAdmin = Session.EstAdmin;

    // Les boutons CRUD sont activés seulement pour les ADMIN
    _btnAjouter.Enabled   = estAdmin;
    _btnModifier.Enabled  = estAdmin;
    _btnSupprimer.Enabled = estAdmin;

    // Les AGENTS peuvent quand même voir la liste (DataGridView reste actif)
}
```

#### Recherche en temps réel (filtrage)

Le filtrage se fait **en mémoire** (dans la liste C# chargée depuis la base) avec un simple `foreach` :

```csharp
private void AppliquerFiltre()
{
    string filtre = _txtRecherche.Text.Trim().ToLower();
    _dgvSalles.Rows.Clear();

    foreach (Salle s in _salles)
    {
        // Si un filtre est saisi, on vérifie que le nom ou le type le contient
        if (filtre != "" && !s.Nom.ToLower().Contains(filtre)
                         && !s.TypeSalle.ToLower().Contains(filtre))
            continue; // cette salle ne correspond pas → on passe à la suivante

        _dgvSalles.Rows.Add(s.IdSalle, s.Nom, s.Capacite, s.TypeSalle);
    }
}
```

---

## 5. Base de données

### 5.1 Schéma des tables

Le script complet est dans le fichier `database.sql` à la racine du projet.

| Table                  | Description                              | Clé primaire       | Nb colonnes |
|------------------------|------------------------------------------|---------------------|-------------|
| `utilisateur`          | Comptes utilisateurs                     | `id_utilisateur`    | 6           |
| `salle`                | Salles réservables                       | `id_salle`          | 4           |
| `structure`            | Structures (ligues, clubs…)              | `id_structure`      | 3           |
| `reservation`          | Réservations de salles                   | `id_reservation`    | 8           |
| `info_jour`            | Digicode et Wifi du jour                 | `id`                | 4           |

### 5.2 Contraintes d'intégrité

```
reservation.id_salle ──────FK──────► salle.id_salle
reservation.id_structure ──FK──────► structure.id_structure
info_jour.date_info ───────UNIQUE
utilisateur.login ─────────UNIQUE
```

**Conséquence pratique :** si on essaie de supprimer une salle qui est utilisée dans une réservation, MySQL retourne une erreur de clé étrangère. L'application attrape cette erreur et affiche un message compréhensible.

### 5.3 Sécurité des mots de passe

Les mots de passe sont hachés par MySQL avec la fonction `SHA2()` :

```sql
-- À l'insertion :
INSERT INTO utilisateur (..., mot_de_passe) VALUES (..., SHA2('admin123', 256))

-- À la vérification :
SELECT ... FROM utilisateur WHERE login = @login AND mot_de_passe = SHA2(@mdp, 256)
```

**Ce que cela signifie :**
- Le mot de passe `admin123` devient `240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9` en base ;
- Même si la base est compromise, le mot de passe original **ne peut pas être retrouvé** ;
- La comparaison se fait entre deux hachés, jamais en clair.

---

## 6. Sécurité

| Mesure                          | Comment c'est implémenté                                    |
|---------------------------------|-------------------------------------------------------------|
| **Injection SQL**               | Toutes les requêtes utilisent des paramètres (`@param` + `AddWithValue`) |
| **Mot de passe**                | Haché en SHA2-256 par MySQL, jamais transmis en clair       |
| **Contrôle d'accès**            | Rôles ADMIN/AGENT vérifiés dans `AppliquerDroits()` de chaque formulaire |
| **Limitation des tentatives**   | 3 tentatives max dans FormLogin, puis blocage               |
| **Validation des saisies**      | Champs vides, doublons de noms, cohérence des horaires      |

---

## 7. Conventions de code

| Convention                | Exemple                                          | Explication                     |
|---------------------------|--------------------------------------------------|---------------------------------|
| Noms de classes           | `FormSalles`, `SalleDAO`                         | PascalCase (majuscule à chaque mot) |
| Noms de méthodes          | `ChargerSalles()`, `AppliquerFiltre()`           | PascalCase                      |
| Champs privés             | `_dao`, `_salles`, `_dgvSalles`                  | Préfixe `_` (underscore)        |
| Constantes                | `NbTentativesMax`                                | PascalCase                      |
| Contrôles WinForms        | `_btnAjouter`, `_txtRecherche`, `_dgvSalles`     | Préfixe `_` + abréviation du type |
| Commentaires              | En français, simples et courts                   | Adaptés au niveau BTS SIO       |
| Noms de fichiers          | `SalleDAO.cs`, `FormSalles.cs`                   | Même nom que la classe          |

### Abréviations des contrôles

| Abréviation | Contrôle WinForms   | Exemple          |
|-------------|---------------------|------------------|
| `btn`       | Button              | `_btnAjouter`    |
| `txt`       | TextBox             | `_txtRecherche`  |
| `lbl`       | Label               | `_lblErreur`     |
| `dgv`       | DataGridView        | `_dgvSalles`     |
| `cbo`       | ComboBox            | `_cboStatut`     |
| `chk`       | CheckBox            | `_chkAfficher`   |
| `dtp`       | DateTimePicker      | `_dtpDate`       |
| `grp`       | GroupBox            | `_grpInfoJour`   |
| `ts`        | ToolStrip           | `_tsBoutons`     |
| `mnu`       | MenuItem            | `MnuSalles`      |

---

## 8. Diagramme de classes simplifié

```
┌──────────────┐        ┌──────────────┐        ┌──────────────────┐
│  Utilisateur │        │    Salle     │        │    Structure     │
├──────────────┤        ├──────────────┤        ├──────────────────┤
│ IdUtilisateur│        │ IdSalle      │        │ IdStructure      │
│ Login        │        │ Nom          │        │ Nom              │
│ Nom          │        │ Capacite     │        │ TypeStructure    │
│ Prenom       │        │ TypeSalle    │        └────────┬─────────┘
│ Role         │        └──────┬───────┘                 │
└──────────────┘               │                         │
                               │ 1..* ◄──────── 1..* │
                               ▼                         ▼
                       ┌───────────────────────────────────┐
                       │           Reservation             │
                       ├───────────────────────────────────┤
                       │ IdReservation                     │
                       │ IdSalle (FK)                      │
                       │ IdStructure (FK)                  │
                       │ DateReservation                   │
                       │ HeureDebut / HeureFin             │
                       │ Statut                            │
                       │ Commentaire                       │
                       │ NomSalle (via JOIN)               │
                       │ NomStructure (via JOIN)           │
                       └───────────────────────────────────┘

┌──────────────┐
│   InfoJour   │
├──────────────┤
│ Id           │
│ DateInfo     │
│ Digicode     │
│ CleWifi      │
└──────────────┘
```

---

## 9. Évolutions possibles

| Priorité | Évolution                               | Description                                                  |
|----------|-----------------------------------------|--------------------------------------------------------------|
| ⭐⭐⭐   | Gestion des consommations               | Affranchissement et impressions par ligue (avec tarifs)      |
| ⭐⭐⭐   | Calcul automatique des tarifs           | Selon le type de structure et les quotas gratuits             |
| ⭐⭐     | Services optionnels des réservations    | Eau, café, vidéoprojecteur (table `service_reservation`)     |
| ⭐⭐     | Gestion complète des utilisateurs       | CRUD des comptes depuis l'interface d'admin                  |
| ⭐       | Export PDF                              | Récapitulatif mensuel des réservations                       |
| ⭐       | Planning visuel des salles              | Vue calendrier hebdomadaire par salle                        |
