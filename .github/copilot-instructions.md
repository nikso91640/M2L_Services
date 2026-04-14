# Copilot Instructions — M2L Services

## Préférences utilisateur
- L'application doit rester simple pour des étudiants de BTS SIO, avec beaucoup de commentaires et un code direct, en privilégiant l'ergonomie, la fluidité et la complétude, dans le respect du contexte et des attentes du projet.
- L'interface WinForms doit être professionnelle, incluant un MenuStrip et un tableau de bord sur la page d'accueil, en utilisant le Designer pour le front.

---

# Contexte projet — M2L Services

## Rôle attendu de GitHub Copilot
Tu m'assistes comme un binôme de développement pour un projet de **BTS SIO**.
Tu dois proposer des réponses :
- simples ;
- pédagogiques ;
- progressives ;
- directement exploitables dans **Visual Studio** ;
- adaptées à un niveau **BTS SIO** ;
- sans complexité inutile.

Tu dois éviter :
- les architectures trop avancées ;
- les design patterns surdimensionnés ;
- les frameworks inutiles ;
- le code trop abstrait ;
- les solutions difficiles à expliquer à l'oral.

Quand tu proposes du code :
- donne du code complet et exploitable ;
- précise dans quel fichier le mettre ;
- ajoute des commentaires simples ;
- explique brièvement le rôle du code ;
- reste cohérent avec l'existant du projet.

---

## Nom du projet
**M2L Services**

## Type de projet
Application **Windows Forms en C#** développée dans **Visual Studio**.

## Public visé
Étudiants de **BTS SIO**.

Le projet doit donc rester :
- compréhensible ;
- structuré ;
- démontrable à l'oral ;
- réaliste pour un étudiant.

---

## Contexte métier
La **Maison des Ligues de Lorraine (M2L)** héberge plusieurs ligues sportives dans ses locaux.

Elle propose à ces ligues un ensemble de **services mutualisés** :
- la réservation de salles de réunion, de l'amphithéâtre et de la salle de convivialité ;
- l'affranchissement du courrier ;
- les impressions en reprographie ;
- l'information sur le digicode du jour et la clé Wifi visiteurs.

L'objectif de l'application est de permettre à la M2L de **gérer ces services** : enregistrer les réservations, suivre les consommations des ligues (affranchissement, impressions), et diffuser les informations pratiques du jour.

---

## Périmètre fonctionnel retenu

L'application doit gérer uniquement :

- les **salles** disponibles à la réservation ;
- les **réservations** de salles ;
- les **structures** (ligues, clubs, associations, entreprises) qui réservent ;
- l'**affranchissement** consommé par ligue ;
- les **impressions** (reprographie) consommées par ligue ;
- le **digicode du jour** et la **clé Wifi** en vigueur ;
- les **utilisateurs** de l'application.

Le projet ne doit pas couvrir :
- la gestion des équipements informatiques (c'est le projet du binôme) ;
- la gestion des bulletins de salaire ;
- la comptabilité complète ;
- la gestion des ligues et bureaux (déjà couvert par le binôme) ;
- les fonctionnalités trop complexes hors périmètre.

---

## Entités principales

Le projet repose sur les tables suivantes :

- `utilisateur`
- `salle`
- `structure`
- `reservation`
- `service_reservation` *(services optionnels associés à une réservation)*
- `consommation_affranchissement`
- `consommation_impression`
- `info_jour` *(digicode et clé Wifi du jour)*

---

## Règles métier

### Salles
- une salle possède : un nom, une capacité, un type (`REUNION`, `AMPHI`, `CONVIVIALITE`, `MULTIMEDIA`) ;
- une salle peut être réservée ou non selon le type de structure.

### Structures
- une structure possède : un nom, un type (`LIGUE`, `CLUB`, `ASSOCIATION`, `LYCEE_COLLEGE`, `ENTREPRISE`, `AUTRE`) ;
- le type de structure détermine le **niveau de tarification** applicable.

### Niveaux de tarification
Il existe **4 niveaux de tarif** pour les réservations :

| Niveau | Structures concernées | Conditions |
|---|---|---|
| **Gratuit** | Ligues hébergées | 6 réservations/an max (hors amphi) + 1 fois amphi/an |
| **Tarif 1** | Clubs sportifs, comités départementaux | Toujours payant |
| **Tarif 2** | Associations, lycées, collèges | Toujours payant |
| **Tarif 3** | Entreprises et autres | Tarif le plus élevé |

> Une ligue ayant dépassé son quota gratuit passe automatiquement au **Tarif 1**.

### Réservations
- une réservation concerne **une salle**, **une structure**, pour **un créneau** (date + heure début + heure fin) ;
- une réservation possède un **statut** : `EN_ATTENTE`, `CONFIRMEE`, `ANNULEE` ;
- une réservation peut avoir des **services optionnels** associés (eau, café, aménagement particulier, vidéoprojecteur) ;
- certains services optionnels sont payants.

### Conformité d'une réservation
Une réservation est **valide** si :
- le créneau demandé est disponible (pas de chevauchement) ;
- la structure a le droit de réserver la salle (ex : une entreprise ne peut pas réserver une salle de réunion d'étage) ;
- pour une ligue : le quota gratuit annuel n'est pas dépassé (sinon elle est prévenue qu'elle sera facturée).

### Affranchissement
- chaque mois, les consommations d'affranchissement sont enregistrées par ligue ;
- une consommation contient : la ligue, le mois, l'année, la quantité, le type d'affranchissement, le montant ;
- on peut éditer un récapitulatif mensuel par ligue.

### Impressions (reprographie)
- chaque mois, les consommations d'impression sont enregistrées par ligue ;
- une consommation contient : la ligue, le mois, l'année, le type d'impression (`NB`, `COULEUR`, `TRACEUR`), le nombre de pages, le montant calculé ;
- on peut éditer un récapitulatif mensuel par ligue.

### Digicode & Wifi
- chaque jour, un digicode et une clé Wifi peuvent être renseignés par l'administration ;
- on peut consulter l'information du jour ;
- on peut aussi consulter l'historique des codes passés.

### Statut d'une réservation
Le statut peut être :
- `EN_ATTENTE`
- `CONFIRMEE`
- `ANNULEE`

---

## Base de données
Le projet utilise :
- **MySQL**
- **WAMP**
- **phpMyAdmin**

Ne pas proposer :
- SQL Server ;
- Entity Framework ;
- migrations complexes.

Privilégier :
- **ADO.NET**
- **MySql.Data**
- requêtes SQL simples ;
- code clair et pédagogique.

---

## Architecture attendue
Utiliser une architecture simple, adaptée à des BTS SIO.

Organisation recommandée :
```
M2LServices/
├── Forms/
├── Models/
├── DataAccess/
├── Services/
└── Utils/
```

### Rôle des dossiers
- `Forms` : fenêtres WinForms
- `Models` : classes métier simples (Salle, Structure, Reservation, etc.)
- `DataAccess` : accès base de données (un fichier par entité)
- `Services` : logique métier (calcul tarif, vérification quota, calcul montant impressions)
- `Utils` : connexion MySQL, helpers, validations

Ne pas proposer d'architecture trop lourde.

---

## Style de code attendu
Le code doit être :
- lisible ;
- simple ;
- bien nommé ;
- commenté sans excès ;
- cohérent dans tout le projet.

### Bonnes pratiques
- noms de classes clairs ;
- une responsabilité principale par classe ;
- méthodes courtes ;
- validation des champs avant insertion ;
- gestion simple des erreurs avec messages compréhensibles ;
- requêtes SQL paramétrées ;
- éviter les duplications de code.

### À éviter
- code trop "expert" ;
- lambdas complexes inutiles ;
- génériques avancés ;
- reflection ;
- surcouches inutiles ;
- injection de dépendances compliquée ;
- async/await partout sans besoin.

---

## Attentes sur les formulaires WinForms
Les formulaires doivent être :
- simples ;
- lisibles ;
- professionnels ;
- faciles à prendre en main.

### Contrôles souvent attendus
- `DataGridView`
- `TextBox`
- `ComboBox`
- `Label`
- `Button`
- `CheckBox`
- `DateTimePicker`
- `NumericUpDown`
- `GroupBox`

### Logique d'interface
Favoriser des écrans de gestion classiques :
- liste ;
- ajout ;
- modification ;
- suppression ;
- recherche / filtrage ;
- affichage du récapitulatif.

---

## Fonctionnalités minimales attendues

### Salles
- ajouter une salle ;
- modifier une salle ;
- supprimer une salle ;
- afficher la liste des salles avec leur type et capacité.

### Structures
- ajouter une structure ;
- modifier une structure ;
- supprimer une structure ;
- afficher la liste des structures avec leur type.

### Réservations
- créer une réservation (salle + structure + créneau) ;
- vérifier la disponibilité du créneau avant enregistrement ;
- ajouter des services optionnels à une réservation ;
- modifier ou annuler une réservation ;
- afficher la liste des réservations (filtrées par salle, structure, date, statut) ;
- afficher le **tarif applicable** et indiquer si la réservation est **gratuite ou payante** pour une ligue ;
- afficher le **quota restant** d'une ligue pour l'année en cours.

### Affranchissement
- enregistrer une consommation mensuelle d'affranchissement pour une ligue ;
- modifier ou supprimer une consommation ;
- afficher la liste des consommations filtrées par ligue et par mois/année ;
- afficher un récapitulatif mensuel (ligue + quantité + montant).

### Impressions
- enregistrer une consommation mensuelle d'impression pour une ligue ;
- modifier ou supprimer une consommation ;
- afficher la liste des consommations filtrées par ligue, type et mois/année ;
- calculer et afficher le montant selon le type d'impression ;
- afficher un récapitulatif mensuel par ligue.

### Digicode & Wifi
- saisir le digicode et la clé Wifi du jour ;
- afficher l'information du jour en évidence sur l'écran d'accueil ;
- consulter l'historique des codes passés.

### Utilisateurs
- connexion simple (login / mot de passe) ;
- rôle administrateur ou agent d'accueil ;
- seul l'administrateur peut modifier les tarifs et supprimer des données.

---

## Règles de gestion à implémenter dans le code

### Calcul du tarif d'une réservation
```
Si structure.type == LIGUE :
    compter les réservations gratuites utilisées cette année
    Si quota non atteint (< 6 pour salles, < 1 pour amphi) :
        tarif = GRATUIT
    Sinon :
        tarif = TARIF_1
Si structure.type == CLUB ou COMITE_DEPARTEMENTAL :
    tarif = TARIF_1
Si structure.type == ASSOCIATION ou LYCEE ou COLLEGE :
    tarif = TARIF_2
Si structure.type == ENTREPRISE ou AUTRE :
    tarif = TARIF_3
```

### Vérification de disponibilité d'un créneau
```
Vérifier qu'il n'existe pas de réservation CONFIRMEE
pour la même salle, le même jour,
dont les horaires se chevauchent avec le créneau demandé.
```

### Calcul du montant d'une impression
```
NB         : prix unitaire × nombre de pages
COULEUR    : prix unitaire × nombre de pages
TRACEUR    : prix unitaire × nombre de pages
(les prix unitaires sont configurables par l'administrateur)
```

---

## Attentes quand tu proposes du code
Quand je te demande une fonctionnalité :

1. explique d'abord ce que tu fais ;
2. précise les fichiers à créer ou modifier ;
3. donne le code complet ;
4. commente le code de façon pédagogique ;
5. indique comment tester ;
6. reste cohérent avec l'architecture du projet.

---

## Attentes sur le SQL
Quand tu proposes du SQL :
- écris du SQL compatible **MySQL / phpMyAdmin** ;
- utilise une syntaxe propre ;
- évite les spécificités SQL Server ;
- propose des clés primaires, étrangères et contraintes utiles ;
- reste cohérent avec les règles métier du projet.

### Exemple de structure SQL attendue
```sql
-- Table des salles
CREATE TABLE salle (
    id_salle INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    capacite INT NOT NULL,
    type_salle ENUM('REUNION', 'AMPHI', 'CONVIVIALITE', 'MULTIMEDIA') NOT NULL
);

-- Table des structures (ligues, clubs, etc.)
CREATE TABLE structure (
    id_structure INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(150) NOT NULL,
    type_structure ENUM('LIGUE', 'CLUB', 'ASSOCIATION', 'LYCEE_COLLEGE', 'ENTREPRISE', 'AUTRE') NOT NULL
);

-- Table des réservations
CREATE TABLE reservation (
    id_reservation INT AUTO_INCREMENT PRIMARY KEY,
    id_salle INT NOT NULL,
    id_structure INT NOT NULL,
    date_reservation DATE NOT NULL,
    heure_debut TIME NOT NULL,
    heure_fin TIME NOT NULL,
    statut ENUM('EN_ATTENTE', 'CONFIRMEE', 'ANNULEE') NOT NULL DEFAULT 'EN_ATTENTE',
    niveau_tarif ENUM('GRATUIT', 'TARIF_1', 'TARIF_2', 'TARIF_3') NOT NULL,
    commentaire TEXT,
    FOREIGN KEY (id_salle) REFERENCES salle(id_salle),
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Services optionnels d'une réservation
CREATE TABLE service_reservation (
    id_service INT AUTO_INCREMENT PRIMARY KEY,
    id_reservation INT NOT NULL,
    libelle VARCHAR(100) NOT NULL,
    payant TINYINT(1) NOT NULL DEFAULT 0,
    FOREIGN KEY (id_reservation) REFERENCES reservation(id_reservation)
);

-- Consommations d'affranchissement
CREATE TABLE consommation_affranchissement (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_structure INT NOT NULL,
    mois INT NOT NULL,
    annee INT NOT NULL,
    quantite INT NOT NULL,
    type_affranchissement VARCHAR(50),
    montant DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Consommations d'impression
CREATE TABLE consommation_impression (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_structure INT NOT NULL,
    mois INT NOT NULL,
    annee INT NOT NULL,
    type_impression ENUM('NB', 'COULEUR', 'TRACEUR') NOT NULL,
    nb_pages INT NOT NULL,
    montant DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Digicode et clé Wifi du jour
CREATE TABLE info_jour (
    id INT AUTO_INCREMENT PRIMARY KEY,
    date_info DATE NOT NULL UNIQUE,
    digicode VARCHAR(20) NOT NULL,
    cle_wifi VARCHAR(100) NOT NULL
);

-- Utilisateurs de l'application
CREATE TABLE utilisateur (
    id_utilisateur INT AUTO_INCREMENT PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    mot_de_passe VARCHAR(255) NOT NULL,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    role ENUM('ADMIN', 'AGENT') NOT NULL DEFAULT 'AGENT'
);
```

---

## Attentes sur les réponses
Quand tu réponds :
- reste concret ;
- évite les longs discours théoriques ;
- propose des étapes de développement progressives ;
- garde un niveau BTS SIO ;
- aide à produire une application fonctionnelle et présentable à l'examen.

Si plusieurs solutions sont possibles, privilégie :
1. la plus simple ;
2. la plus pédagogique ;
3. la plus réaliste pour un étudiant de BTS SIO.

---

## Priorité absolue
L'objectif n'est pas de produire une application "parfaite" au sens industriel.
L'objectif est de produire une application :
- cohérente ;
- fonctionnelle ;
- bien structurée ;
- expliquable ;
- soutenable à l'oral ;
- adaptée aux compétences d'un étudiant de BTS SIO.

---

## Ordre de développement recommandé

Pour avancer progressivement, voici l'ordre conseillé :

1. **Connexion** — formulaire de login, vérification en base
2. **Salles** — CRUD complet avec DataGridView
3. **Structures** — CRUD complet
4. **Réservations** — création avec vérification de disponibilité + calcul du tarif
5. **Réservations** — affichage liste, filtres, modification, annulation
6. **Affranchissement** — enregistrement et récapitulatif mensuel
7. **Impressions** — enregistrement, calcul montant, récapitulatif mensuel
8. **Digicode & Wifi** — saisie du jour + affichage sur l'écran d'accueil
9. **Finitions** — validation des champs, messages d'erreur, cohérence générale9. **Finitions** — validation des champs, messages d'erreur, cohérence générale