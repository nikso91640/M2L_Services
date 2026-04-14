# Cahier des charges — M2L Services

| **Élément**         | **Valeur**                                              |
|---------------------|---------------------------------------------------------|
| Projet              | M2L Services                                            |
| Client              | Maison des Ligues de Lorraine (M2L)                     |
| Type d'application  | Application Windows (WinForms, C#)                      |
| Version             | 1.0                                                     |
| Date                | 2025                                                    |
| Auteur              | *[Votre nom, prénom]*                                   |
| Formation           | BTS SIO — Option SLAM                                   |
| Épreuve             | E6 — Conception et développement d'applications         |

---

## 1. Présentation du contexte

### 1.1 Présentation de l'entreprise

La **Maison des Ligues de Lorraine (M2L)** est un établissement public qui héberge plusieurs **ligues sportives régionales** dans ses locaux situés en Lorraine.

La M2L joue un rôle central dans l'organisation du sport régional : elle met à disposition des ligues des bureaux, des salles de réunion, un amphithéâtre, ainsi que des services partagés (accueil, reprographie, Wifi, etc.).

Les ligues hébergées sont par exemple :
- La Ligue Lorraine de Football ;
- La Ligue Lorraine de Basketball ;
- La Ligue Lorraine d'Athlétisme ;
- La Ligue Lorraine de Tennis ;
- etc.

D'autres structures externes (clubs sportifs, associations, lycées, entreprises) utilisent également ponctuellement les salles de la M2L.

### 1.2 Problématique actuelle

Actuellement, la gestion des services de la M2L se fait de manière **manuelle** :
- Les réservations de salles sont notées sur un **cahier papier** à l'accueil ;
- Le digicode d'accès et la clé Wifi sont communiqués **par téléphone ou par e-mail** chaque matin ;
- Il n'existe **aucun outil centralisé** pour vérifier la disponibilité d'une salle avant de la réserver.

Cette situation engendre plusieurs problèmes :

| Problème                          | Conséquence                                              |
|-----------------------------------|----------------------------------------------------------|
| Doubles réservations              | Deux structures arrivent pour la même salle au même moment |
| Manque de traçabilité             | Impossible de savoir qui a réservé quoi le mois dernier   |
| Perte de temps                    | L'agent d'accueil doit feuilleter le cahier pour chaque demande |
| Communication inefficace          | Le digicode du jour est oublié ou mal transmis            |
| Aucune vision globale             | Pas de tableau de bord pour savoir combien de réservations sont prévues |

### 1.3 Objectif du projet

L'objectif est de développer une **application de bureau** (Windows Forms en C#) qui permettra à la M2L de :

1. **Centraliser** la gestion des salles et des réservations ;
2. **Vérifier automatiquement** la disponibilité des créneaux ;
3. **Diffuser** le digicode et la clé Wifi du jour ;
4. **Offrir un tableau de bord** synthétique à l'agent d'accueil ;
5. **Sécuriser l'accès** avec une authentification par login/mot de passe.

### 1.4 Bénéfices attendus

| Bénéfice                          | Description                                              |
|-----------------------------------|----------------------------------------------------------|
| Fiabilité                         | Plus de doubles réservations grâce au contrôle automatique |
| Gain de temps                     | Recherche et filtrage instantanés                        |
| Traçabilité                       | Historique complet des réservations et des codes du jour  |
| Professionnalisme                 | Interface claire et moderne pour l'accueil                |
| Sécurité                          | Accès contrôlé par rôle (admin / agent)                  |

---

## 2. Périmètre fonctionnel

### 2.1 Fonctionnalités incluses

```
┌─────────────────────────────────────────────────────────┐
│                    M2L Services                         │
│                                                         │
│  ┌──────────┐  ┌──────────┐  ┌──────────────────────┐  │
│  │ Connexion│  │ Tableau  │  │   Gestion des        │  │
│  │  Login   │  │ de bord  │  │   Salles (CRUD)      │  │
│  └──────────┘  └──────────┘  └──────────────────────┘  │
│                                                         │
│  ┌──────────────────┐  ┌────────────────────────────┐  │
│  │ Gestion des      │  │  Gestion des               │  │
│  │ Structures (CRUD)│  │  Réservations (CRUD)        │  │
│  └──────────────────┘  │  + vérif. disponibilité     │  │
│                         └────────────────────────────┘  │
│  ┌──────────────────────────────────────────────────┐   │
│  │  Digicode & Wifi du jour + historique            │   │
│  └──────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
```

#### Détail par module

| Module              | Fonctionnalités détaillées                                                      |
|---------------------|---------------------------------------------------------------------------------|
| **Connexion**       | Authentification login/mot de passe ; hachage SHA2-256 ; limitation à 3 tentatives ; rôles ADMIN et AGENT |
| **Tableau de bord** | Compteurs (salles, structures, réservations, réservations du jour) ; info du jour (digicode/wifi) ; alertes (réservations en attente) |
| **Salles**          | Ajouter, modifier, supprimer, lister ; recherche en temps réel ; contrôle des doublons de nom |
| **Structures**      | Ajouter, modifier, supprimer, lister ; recherche en temps réel ; contrôle des doublons de nom |
| **Réservations**    | Ajouter, modifier, annuler, supprimer ; filtrer par statut et par texte ; vérification automatique de disponibilité ; code couleur par statut |
| **Digicode/Wifi**   | Saisir l'info du jour ; consulter l'historique ; copier dans le presse-papier   |

### 2.2 Fonctionnalités exclues (hors périmètre)

Les fonctionnalités suivantes **ne sont pas couvertes** par ce projet :

- Gestion des équipements informatiques (projet du binôme — M2L Intégration) ;
- Gestion de la facturation et de la comptabilité ;
- Gestion des bulletins de salaire ;
- Gestion des consommations (affranchissement, impressions) — envisageable en évolution future ;
- Accès via navigateur web (l'application est uniquement de type bureau Windows) ;
- Gestion avancée des utilisateurs (pas de création de compte depuis l'application).

---

## 3. Acteurs et rôles

### 3.1 Identification des acteurs

```
                    ┌─────────────────────┐
                    │   M2L Services      │
                    │   (Application)     │
                    └──────┬──────────────┘
                           │
              ┌────────────┴────────────┐
              │                         │
    ┌─────────▼─────────┐    ┌─────────▼─────────┐
    │   Administrateur  │    │  Agent d'accueil   │
    │     (ADMIN)       │    │     (AGENT)        │
    └───────────────────┘    └────────────────────┘
```

### 3.2 Droits par rôle

| Action                              | ADMIN | AGENT |
|-------------------------------------|:-----:|:-----:|
| Se connecter                        |  ✅   |  ✅   |
| Voir le tableau de bord             |  ✅   |  ✅   |
| Consulter la liste des salles       |  ✅   |  ✅   |
| Ajouter / Modifier / Supprimer une salle |  ✅   |  ❌   |
| Consulter la liste des structures   |  ✅   |  ✅   |
| Ajouter / Modifier / Supprimer une structure |  ✅   |  ❌   |
| Consulter les réservations          |  ✅   |  ✅   |
| Ajouter / Modifier / Annuler / Supprimer une réservation |  ✅   |  ❌   |
| Consulter l'historique digicode/wifi|  ✅   |  ✅   |
| Saisir le digicode/wifi du jour     |  ✅   |  ❌   |

---

## 4. Règles de gestion

### 4.1 Salles

| Code    | Règle de gestion                                                                |
|---------|---------------------------------------------------------------------------------|
| RG-S01  | Une salle possède un **nom** (obligatoire), une **capacité** (nombre entier) et un **type**. |
| RG-S02  | Les types de salle autorisés sont : `REUNION`, `AMPHI`, `CONVIVIALITE`, `MULTIMEDIA`. |
| RG-S03  | Le nom d'une salle doit être **unique** dans la base de données. L'application contrôle les doublons avant insertion. |
| RG-S04  | Seul un utilisateur ayant le rôle **ADMIN** peut créer, modifier ou supprimer une salle. |
| RG-S05  | Une salle liée à des réservations ne peut pas être supprimée (contrainte de clé étrangère en base). |

### 4.2 Structures

| Code    | Règle de gestion                                                                |
|---------|---------------------------------------------------------------------------------|
| RG-ST01 | Une structure possède un **nom** (obligatoire) et un **type**.                  |
| RG-ST02 | Les types de structure autorisés sont : `LIGUE`, `CLUB`, `ASSOCIATION`, `LYCEE_COLLEGE`, `ENTREPRISE`, `AUTRE`. |
| RG-ST03 | Le nom d'une structure doit être **unique**. L'application contrôle les doublons avant insertion. |
| RG-ST04 | Seul un **ADMIN** peut créer, modifier ou supprimer une structure.              |
| RG-ST05 | Une structure liée à des réservations ne peut pas être supprimée.               |

### 4.3 Réservations

| Code    | Règle de gestion                                                                |
|---------|---------------------------------------------------------------------------------|
| RG-R01  | Une réservation concerne **une salle** et **une structure** pour un **créneau** donné (date + heure début + heure fin). |
| RG-R02  | Les statuts possibles sont : `EN_ATTENTE`, `CONFIRMEE`, `ANNULEE`.             |
| RG-R03  | L'heure de fin doit être **strictement supérieure** à l'heure de début.        |
| RG-R04  | Les créneaux disponibles vont de **06:00 à 21:00** par pas de 30 minutes.      |
| RG-R05  | Un créneau **CONFIRMÉ** ne peut pas chevaucher un autre créneau confirmé sur la **même salle** et la **même date**. |
| RG-R06  | La vérification de disponibilité est effectuée **automatiquement** lors de la validation du formulaire de détail. |
| RG-R07  | Seul un **ADMIN** peut créer, modifier, annuler ou supprimer une réservation.  |
| RG-R08  | **Annuler** change le statut en `ANNULEE` (historique conservé). **Supprimer** retire définitivement l'enregistrement. |

**Algorithme de détection de chevauchement :**

```
Pour vérifier si un créneau [D, F] est disponible sur une salle S à une date J :

    Compter les réservations CONFIRMEES où :
        - même salle S
        - même date J
        - heure_debut < F   (la réservation existante commence avant la fin demandée)
        - heure_fin   > D   (la réservation existante finit après le début demandé)

    Si le compte est > 0 → CRÉNEAU INDISPONIBLE
    Sinon               → CRÉNEAU DISPONIBLE
```

**Illustration du chevauchement :**

```
Cas 1 — Pas de chevauchement (OK) :
    Existante :  |=====|
    Demandée  :              |=====|

Cas 2 — Chevauchement partiel (BLOQUÉ) :
    Existante :  |=========|
    Demandée  :       |=========|

Cas 3 — Chevauchement total (BLOQUÉ) :
    Existante :  |===============|
    Demandée  :     |=======|

Cas 4 — Englobement (BLOQUÉ) :
    Existante :      |=====|
    Demandée  :  |===============|
```

### 4.4 Digicode et Wifi

| Code    | Règle de gestion                                                                |
|---------|---------------------------------------------------------------------------------|
| RG-D01  | Chaque jour, un digicode (20 car. max) et une clé Wifi (100 car. max) peuvent être saisis. |
| RG-D02  | Il ne peut y avoir qu'**une seule entrée par date** (UNIQUE). Si une entrée existe, elle est mise à jour. |
| RG-D03  | Seul un **ADMIN** peut saisir ou modifier les informations du jour.            |
| RG-D04  | Tout utilisateur peut consulter l'historique des codes passés.                 |
| RG-D05  | L'info du jour est affichée en évidence sur le tableau de bord de l'accueil.   |

### 4.5 Connexion et sécurité

| Code    | Règle de gestion                                                                |
|---------|---------------------------------------------------------------------------------|
| RG-C01  | L'utilisateur doit saisir un **login** et un **mot de passe** pour accéder à l'application. |
| RG-C02  | Le mot de passe est stocké en **SHA2-256** en base de données (jamais en clair).|
| RG-C03  | Après **3 tentatives échouées**, la connexion est **bloquée** jusqu'au redémarrage. |
| RG-C04  | Le rôle (`ADMIN` ou `AGENT`) détermine les droits dans toute l'application.    |
| RG-C05  | La **déconnexion** ramène à l'écran de connexion. Le compteur de tentatives est remis à zéro. |

---

## 5. Exigences techniques

### 5.1 Environnement de développement

| Élément              | Choix retenu                    | Justification                                 |
|----------------------|---------------------------------|-----------------------------------------------|
| IDE                  | Visual Studio 2022 / 2026       | IDE standard pour le développement C#         |
| Langage              | C# (.NET 10, Windows Forms)     | Technologie enseignée en BTS SIO SLAM         |
| Base de données      | MySQL 5.7+ / MariaDB            | SGBD open source, simple à administrer        |
| Serveur local        | WAMP                            | Environnement intégré Windows/Apache/MySQL/PHP|
| Administration BDD   | phpMyAdmin                      | Interface web pour MySQL, incluse dans WAMP   |
| Connecteur MySQL     | MySql.Data 9.6.0 (NuGet)       | Connecteur officiel Oracle pour MySQL / .NET  |
| Accès aux données    | ADO.NET                         | Accès simple et direct, sans ORM complexe     |

### 5.2 Architecture logicielle

```
┌─────────────────────────────────────────────────────────┐
│                    Interface utilisateur                 │
│                    (Forms/)                              │
│  FormLogin, FormAccueil, FormSalles, FormStructures,    │
│  FormReservations, FormDigicode, + formulaires détail   │
├─────────────────────────────────────────────────────────┤
│                    Logique métier                        │
│                    (Services/)                           │
│  DisponibiliteService (vérification des créneaux)       │
├─────────────────────────────────────────────────────────┤
│                    Accès aux données                    │
│                    (DataAccess/)                         │
│  SalleDAO, StructureDAO, ReservationDAO, InfoJourDAO,   │
│  UtilisateurDAO                                         │
├─────────────────────────────────────────────────────────┤
│                    Modèles de données                   │
│                    (Models/)                             │
│  Salle, Structure, Reservation, InfoJour, Utilisateur   │
├─────────────────────────────────────────────────────────┤
│                    Utilitaires (Utils/)                  │
│  ConnexionBDD — Session                                 │
├─────────────────────────────────────────────────────────┤
│                    Base de données MySQL                 │
│                    (m2l_services)                        │
└─────────────────────────────────────────────────────────┘
```

### 5.3 Contraintes techniques

| Contrainte                                     | Description                                    |
|------------------------------------------------|------------------------------------------------|
| Système d'exploitation                         | Windows 10 ou supérieur (64 bits)              |
| Serveur MySQL                                  | Doit être démarré pour que l'application fonctionne |
| Réseau                                         | L'app et MySQL doivent être sur le même réseau |
| Code pédagogique                               | Simple, commenté, lisible, adapté BTS SIO      |

---

## 6. Modèle de données

### 6.1 Schéma entités-relations

```
┌──────────────────┐          ┌──────────────────┐
│   utilisateur    │          │      salle       │
├──────────────────┤          ├──────────────────┤
│ PK id_utilisateur│          │ PK id_salle      │
│    login (UNIQUE)│          │    nom           │
│    mot_de_passe  │          │    capacite      │
│    nom           │          │    type_salle    │
│    prenom        │          └────────┬─────────┘
│    role          │                   │ 1
└──────────────────┘                   │
                                       │
                              ┌────────▼─────────────────┐
                              │      reservation         │
                              ├──────────────────────────┤
                              │ PK id_reservation        │
                              │ FK id_salle          ────┤── 1 salle → N réservations
                              │ FK id_structure      ────┤── 1 structure → N réservations
                              │    date_reservation      │
                              │    heure_debut           │
                              │    heure_fin             │
                              │    statut                │
                              │    commentaire           │
                              └────────▲─────────────────┘
                                       │ 1
┌──────────────────┐                   │
│    structure     ├───────────────────┘
├──────────────────┤
│ PK id_structure  │
│    nom           │
│    type_structure│
└──────────────────┘

┌──────────────────┐
│    info_jour     │
├──────────────────┤
│ PK id            │
│    date_info (UQ)│
│    digicode      │
│    cle_wifi      │
└──────────────────┘
```

### 6.2 Dictionnaire de données

*(Voir les tableaux détaillés par table dans le Manuel technique.)*

---

## 7. Maquettes des écrans

> **📸 Consigne :** Les maquettes ci-dessous sont des représentations textuelles. **Remplacez-les par des captures d'écran réelles** lors de la mise en forme finale (`Win + Maj + S` pour capturer).

### 7.1 Écran de connexion

```
┌──────────────────────────────────────────────┐
│           M2L Services — Connexion           │
├──────────────────────────────────────────────┤
│                                              │
│        Login :     [__________________]      │
│                                              │
│        Mot de passe : [****************]     │
│                                              │
│        ☐ Afficher le mot de passe            │
│                                              │
│              [ Se connecter ]                │
│                                              │
│        Tentatives restantes : 3              │
│        (message d'erreur en rouge ici)       │
└──────────────────────────────────────────────┘
```

### 7.2 Écran d'accueil

```
┌──────────────────────────────────────────────────────────────────┐
│  Fichier │ Salles │ Structures │ Réservations │ Digicode & Wifi │
├──────────────────────────────────────────────────────────────────┤
│  Utilisateur : Jean Dupont — ADMIN                               │
│  Digicode : 5534          Wifi : M2L-Wifi-Visiteurs              │
│  ┌───────────┐ ┌───────────┐ ┌───────────┐ ┌───────────┐       │
│  │  7 Salles │ │ 14 Struct.│ │ 25 Réserv.│ │ 4 Auj.    │       │
│  └───────────┘ └───────────┘ └───────────┘ └───────────┘       │
│  En attente : 4  |  Dernière MAJ : 10/06/2025 14:32             │
└──────────────────────────────────────────────────────────────────┘
```

### 7.3 Écran de gestion (Salles / Structures / Réservations)

```
┌──────────────────────────────────────────────────────────────────┐
│  [Ajouter] [Modifier] | [Supprimer] | [Actualiser] | Rech:[___]│
├──────────────────────────────────────────────────────────────────┤
│  Nom de la salle          │  Capacité  │  Type                  │
│  ─────────────────────────┼────────────┼────────────────────────│
│  Amphithéâtre             │    200     │  AMPHI                 │
│  Salle Baccarat           │     50     │  REUNION               │
│  Salle convivialité       │     80     │  CONVIVIALITE          │
│  ...                      │    ...     │  ...                   │
└──────────────────────────────────────────────────────────────────┘
```

---

## 8. Planning et livrables

| Phase                    | Durée estimée |
|--------------------------|---------------|
| 1. Analyse / CDC         | 1 semaine     |
| 2. Conception BDD        | 2 jours       |
| 3. Couche données        | 1 semaine     |
| 4. Développement IHM     | 2 semaines    |
| 5. Tests / corrections   | 1 semaine     |
| 6. Documentation         | 3 jours       |
| 7. Déploiement / soutenance | 1 jour     |

| N° | Livrable                    | Format                          |
|----|-----------------------------|----------------------------------|
| 1  | Code source complet         | Solution Visual Studio (.sln)    |
| 2  | Script SQL                  | `database.sql`                   |
| 3  | Exécutable                  | `M2L_Services.exe`               |
| 4  | Cahier des charges          | PDF                              |
| 5  | Manuel d'installation       | PDF                              |
| 6  | Manuel technique            | PDF                              |
| 7  | Manuel utilisateur          | PDF                              |

---

## 9. Glossaire

| Terme           | Définition                                                              |
|-----------------|-------------------------------------------------------------------------|
| **M2L**         | Maison des Ligues de Lorraine                                           |
| **Structure**   | Tout organisme utilisant les services de la M2L                         |
| **Créneau**     | Période définie par une date + heure début + heure fin                  |
| **DAO**         | Data Access Object — classe encapsulant les requêtes SQL                |
| **CRUD**        | Create, Read, Update, Delete                                            |
| **SHA2-256**    | Algorithme de hachage pour sécuriser les mots de passe                  |
| **WinForms**    | Windows Forms — technologie bureau Microsoft                            |
| **ADO.NET**     | Technologie d'accès aux données Microsoft                               |
