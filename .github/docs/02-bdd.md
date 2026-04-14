# 02 — Base de données MySQL

## Connexion

- Serveur : `localhost`
- Port : `3306`
- Base : `m2l_services`
- Utilisateur : `root` (ou compte dédié)
- Mot de passe : *(celui de WAMP)*

La chaîne de connexion est centralisée dans `Utils/ConnexionBDD.cs`.

---

## Script SQL complet

```sql
CREATE DATABASE IF NOT EXISTS m2l_services CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE m2l_services;

-- Utilisateurs de l'application
CREATE TABLE utilisateur (
    id_utilisateur INT AUTO_INCREMENT PRIMARY KEY,
    login          VARCHAR(50)  NOT NULL UNIQUE,
    mot_de_passe   VARCHAR(255) NOT NULL,
    nom            VARCHAR(100) NOT NULL,
    prenom         VARCHAR(100) NOT NULL,
    role           ENUM('ADMIN','AGENT') NOT NULL DEFAULT 'AGENT'
);

-- Salles réservables
CREATE TABLE salle (
    id_salle   INT AUTO_INCREMENT PRIMARY KEY,
    nom        VARCHAR(100) NOT NULL,
    capacite   INT          NOT NULL,
    type_salle ENUM('REUNION','AMPHI','CONVIVIALITE','MULTIMEDIA') NOT NULL
);

-- Structures (ligues, clubs, associations, etc.)
CREATE TABLE structure (
    id_structure   INT AUTO_INCREMENT PRIMARY KEY,
    nom            VARCHAR(150) NOT NULL,
    type_structure ENUM('LIGUE','CLUB','ASSOCIATION','LYCEE_COLLEGE','ENTREPRISE','AUTRE') NOT NULL
);

-- Réservations
CREATE TABLE reservation (
    id_reservation   INT AUTO_INCREMENT PRIMARY KEY,
    id_salle         INT  NOT NULL,
    id_structure     INT  NOT NULL,
    date_reservation DATE NOT NULL,
    heure_debut      TIME NOT NULL,
    heure_fin        TIME NOT NULL,
    statut           ENUM('EN_ATTENTE','CONFIRMEE','ANNULEE') NOT NULL DEFAULT 'EN_ATTENTE',
    niveau_tarif     ENUM('GRATUIT','TARIF_1','TARIF_2','TARIF_3') NOT NULL,
    commentaire      TEXT,
    FOREIGN KEY (id_salle)     REFERENCES salle(id_salle),
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Services optionnels d'une réservation
CREATE TABLE service_reservation (
    id_service     INT AUTO_INCREMENT PRIMARY KEY,
    id_reservation INT          NOT NULL,
    libelle        VARCHAR(100) NOT NULL,
    payant         TINYINT(1)   NOT NULL DEFAULT 0,
    FOREIGN KEY (id_reservation) REFERENCES reservation(id_reservation)
);

-- Consommations d'affranchissement mensuelles
CREATE TABLE consommation_affranchissement (
    id               INT AUTO_INCREMENT PRIMARY KEY,
    id_structure     INT          NOT NULL,
    mois             INT          NOT NULL CHECK (mois BETWEEN 1 AND 12),
    annee            INT          NOT NULL,
    quantite         INT          NOT NULL,
    type_affranchissement VARCHAR(50),
    montant          DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Consommations d'impression mensuelles
CREATE TABLE consommation_impression (
    id               INT AUTO_INCREMENT PRIMARY KEY,
    id_structure     INT          NOT NULL,
    mois             INT          NOT NULL CHECK (mois BETWEEN 1 AND 12),
    annee            INT          NOT NULL,
    type_impression  ENUM('NB','COULEUR','TRACEUR') NOT NULL,
    nb_pages         INT          NOT NULL,
    montant          DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- Tarifs unitaires des impressions (configurables par l'admin)
CREATE TABLE tarif_impression (
    id              INT AUTO_INCREMENT PRIMARY KEY,
    type_impression ENUM('NB','COULEUR','TRACEUR') NOT NULL UNIQUE,
    prix_unitaire   DECIMAL(6,4) NOT NULL
);

-- Digicode et clé Wifi du jour
CREATE TABLE info_jour (
    id        INT AUTO_INCREMENT PRIMARY KEY,
    date_info DATE        NOT NULL UNIQUE,
    digicode  VARCHAR(20) NOT NULL,
    cle_wifi  VARCHAR(100) NOT NULL
);

-- Données initiales
INSERT INTO utilisateur (login, mot_de_passe, nom, prenom, role)
VALUES ('admin', SHA2('admin123', 256), 'Admin', 'M2L', 'ADMIN');

INSERT INTO tarif_impression (type_impression, prix_unitaire) VALUES
('NB',      0.05),
('COULEUR', 0.20),
('TRACEUR', 1.50);

INSERT INTO salle (nom, capacite, type_salle) VALUES
('Salle Gallé',       20, 'REUNION'),
('Salle Daum',        30, 'REUNION'),
('Salle Majorelle',   15, 'REUNION'),
('Salle Baccarat',    50, 'REUNION'),
('Amphithéâtre',     200, 'AMPHI'),
('Salle convivialité', 80, 'CONVIVIALITE'),
('Salle multimédia',   24, 'MULTIMEDIA');
```

---

## Classe de connexion (`Utils/ConnexionBDD.cs`)

```csharp
using MySql.Data.MySqlClient;

namespace M2LServices.Utils
{
    /// <summary>
    /// Fournit la connexion à la base de données MySQL.
    /// Centraliser ici évite de dupliquer la chaîne de connexion partout.
    /// </summary>
    public static class ConnexionBDD
    {
        private static readonly string _chaineConnexion =
            "Server=localhost;Port=3306;Database=m2l_services;Uid=root;Pwd=;CharSet=utf8mb4;";

        /// <summary>
        /// Retourne une nouvelle connexion MySQL ouverte.
        /// Penser à la fermer avec using() ou conn.Close().
        /// </summary>
        public static MySqlConnection GetConnexion()
        {
            var conn = new MySqlConnection(_chaineConnexion);
            conn.Open();
            return conn;
        }
    }
}
```
