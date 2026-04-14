-- ============================================================
-- Script SQL — M2L Services
-- À exécuter dans phpMyAdmin ou via MySQL CLI
-- Compatible MySQL 5.7+ / MariaDB
-- ============================================================

CREATE DATABASE IF NOT EXISTS m2l_services
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE m2l_services;

-- ------------------------------------------------------------
-- Utilisateurs de l'application
-- ------------------------------------------------------------
CREATE TABLE utilisateur (
    id_utilisateur INT          AUTO_INCREMENT PRIMARY KEY,
    login          VARCHAR(50)  NOT NULL UNIQUE,
    mot_de_passe   VARCHAR(255) NOT NULL,   -- stocké en SHA2-256
    nom            VARCHAR(100) NOT NULL,
    prenom         VARCHAR(100) NOT NULL,
    role           ENUM('ADMIN','AGENT') NOT NULL DEFAULT 'AGENT'
);

-- ------------------------------------------------------------
-- Salles réservables
-- ------------------------------------------------------------
CREATE TABLE salle (
    id_salle   INT          AUTO_INCREMENT PRIMARY KEY,
    nom        VARCHAR(100) NOT NULL,
    capacite   INT          NOT NULL,
    type_salle ENUM('REUNION','AMPHI','CONVIVIALITE','MULTIMEDIA') NOT NULL
);

-- ------------------------------------------------------------
-- Structures (ligues, clubs, associations, etc.)
-- ------------------------------------------------------------
CREATE TABLE structure (
    id_structure   INT          AUTO_INCREMENT PRIMARY KEY,
    nom            VARCHAR(150) NOT NULL,
    type_structure ENUM('LIGUE','CLUB','ASSOCIATION','LYCEE_COLLEGE','ENTREPRISE','AUTRE') NOT NULL
);

-- ------------------------------------------------------------
-- Réservations
-- ------------------------------------------------------------
CREATE TABLE reservation (
    id_reservation   INT  AUTO_INCREMENT PRIMARY KEY,
    id_salle         INT  NOT NULL,
    id_structure     INT  NOT NULL,
    date_reservation DATE NOT NULL,
    heure_debut      TIME NOT NULL,
    heure_fin        TIME NOT NULL,
    statut           ENUM('EN_ATTENTE','CONFIRMEE','ANNULEE') NOT NULL DEFAULT 'EN_ATTENTE',
    commentaire      TEXT,
    FOREIGN KEY (id_salle)     REFERENCES salle(id_salle),
    FOREIGN KEY (id_structure) REFERENCES structure(id_structure)
);

-- ------------------------------------------------------------
-- Digicode et clé Wifi du jour
-- ------------------------------------------------------------
CREATE TABLE info_jour (
    id        INT          AUTO_INCREMENT PRIMARY KEY,
    date_info DATE         NOT NULL UNIQUE,
    digicode  VARCHAR(20)  NOT NULL,
    cle_wifi  VARCHAR(100) NOT NULL
);

-- ============================================================
-- Données initiales
-- ============================================================

-- Compte admin par défaut (mot de passe : admin123)
INSERT INTO utilisateur (login, mot_de_passe, nom, prenom, role)
VALUES ('admin', SHA2('admin123', 256), 'Admin', 'M2L', 'ADMIN');

-- Salles de démonstration
INSERT INTO salle (nom, capacite, type_salle) VALUES
('Salle Gallé',        20,  'REUNION'),
('Salle Daum',         30,  'REUNION'),
('Salle Majorelle',    15,  'REUNION'),
('Salle Baccarat',     50,  'REUNION'),
('Amphithéâtre',      200,  'AMPHI'),
('Salle convivialité', 80,  'CONVIVIALITE'),
('Salle multimédia',   24,  'MULTIMEDIA');

-- ============================================================
-- Jeu de test complet
-- ============================================================

-- ------------------------------------------------------------
-- Utilisateurs supplémentaires
-- Mots de passe : agent123 (agents) / admin123 (admin déjà inséré)
-- ------------------------------------------------------------
INSERT INTO utilisateur (login, mot_de_passe, nom, prenom, role) VALUES
('jdupont',  SHA2('agent123', 256), 'Dupont',   'Jean',    'AGENT'),
('mmartin',  SHA2('agent123', 256), 'Martin',   'Marie',   'AGENT'),
('lbernard', SHA2('agent123', 256), 'Bernard',  'Lucas',   'AGENT'),
('sadmin',   SHA2('admin123', 256), 'Supervisor','Sophie', 'ADMIN');

-- ------------------------------------------------------------
-- Structures (ligues, clubs, associations, entreprises...)
-- Couvre tous les types de l'ENUM pour les tests
-- ------------------------------------------------------------
INSERT INTO structure (nom, type_structure) VALUES
-- Ligues hébergées à la M2L
('Ligue Lorraine de Football',       'LIGUE'),
('Ligue Lorraine de Basketball',     'LIGUE'),
('Ligue Lorraine d''Athlétisme',     'LIGUE'),
('Ligue Lorraine de Tennis',         'LIGUE'),
-- Clubs sportifs
('Club Omnisports de Nancy',         'CLUB'),
('ASPTT Metz',                       'CLUB'),
('Nancy Basket',                     'CLUB'),
-- Associations
('Association Sportive Scolaire',    'ASSOCIATION'),
('Comité Régional Olympique',        'ASSOCIATION'),
-- Lycées / collèges
('Lycée Henri Poincaré Nancy',       'LYCEE_COLLEGE'),
('Collège Charles de Gaulle Metz',   'LYCEE_COLLEGE'),
-- Entreprises
('BNP Paribas Grand Est',            'ENTREPRISE'),
('Société Générale Nancy',           'ENTREPRISE'),
-- Autre
('Fédération des Parents d''Élèves', 'AUTRE');

-- ------------------------------------------------------------
-- Réservations — passées, présentes et futures, tous statuts
-- Adapté aux salles insérées :
--   id 1 Gallé (20), 2 Daum (30), 3 Majorelle (15),
--   id 4 Baccarat (50), 5 Amphi (200), 6 Convivialité (80),
--   id 7 Multimédia (24)
-- Structures insérées dans l'ordre : id 1..14
-- ------------------------------------------------------------

-- --- Réservations passées confirmées ---
INSERT INTO reservation (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire) VALUES
(1, 1, DATE_SUB(CURDATE(), INTERVAL 14 DAY), '09:00', '11:00', 'CONFIRMEE', 'Réunion du bureau directeur'),
(2, 2, DATE_SUB(CURDATE(), INTERVAL 13 DAY), '14:00', '16:30', 'CONFIRMEE', 'Commission technique basketball'),
(5, 3, DATE_SUB(CURDATE(), INTERVAL 10 DAY), '10:00', '12:00', 'CONFIRMEE', 'Assemblée générale athlétisme'),
(4, 4, DATE_SUB(CURDATE(), INTERVAL 9  DAY), '09:30', '11:30', 'CONFIRMEE', 'Formation arbitres tennis'),
(3, 5, DATE_SUB(CURDATE(), INTERVAL 7  DAY), '15:00', '17:00', 'CONFIRMEE', NULL),
(6, 9, DATE_SUB(CURDATE(), INTERVAL 5  DAY), '11:00', '13:00', 'CONFIRMEE', 'Cocktail comité olympique'),
(7, 10,DATE_SUB(CURDATE(), INTERVAL 4  DAY), '08:30', '10:30', 'CONFIRMEE', 'Atelier numérique lycée Poincaré');

-- --- Réservations passées annulées ---
INSERT INTO reservation (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire) VALUES
(1, 6, DATE_SUB(CURDATE(), INTERVAL 12 DAY), '10:00', '12:00', 'ANNULEE', 'Annulée — intervenant indisponible'),
(3, 8, DATE_SUB(CURDATE(), INTERVAL 6  DAY), '14:00', '15:30', 'ANNULEE', 'Annulée — manque de participants'),
(2, 12,DATE_SUB(CURDATE(), INTERVAL 3  DAY), '09:00', '10:30', 'ANNULEE', NULL);

-- --- Réservations d'aujourd'hui ---
INSERT INTO reservation (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire) VALUES
(1, 1, CURDATE(), '08:00', '09:30', 'CONFIRMEE', 'Point hebdomadaire Ligue Football'),
(2, 3, CURDATE(), '10:00', '12:00', 'CONFIRMEE', 'Réunion commission sportive'),
(4, 7, CURDATE(), '14:00', '16:00', 'CONFIRMEE', 'Conférence Nancy Basket'),
(7, 11,CURDATE(), '09:00', '11:00', 'EN_ATTENTE', 'Atelier informatique collège');

-- --- Réservations futures confirmées ---
INSERT INTO reservation (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire) VALUES
(1, 2, DATE_ADD(CURDATE(), INTERVAL 2  DAY), '09:00', '11:00', 'CONFIRMEE', 'Réunion bureau basketball'),
(5, 1, DATE_ADD(CURDATE(), INTERVAL 3  DAY), '10:00', '17:00', 'CONFIRMEE', 'Congrès annuel Ligue Football — Amphi'),
(3, 4, DATE_ADD(CURDATE(), INTERVAL 4  DAY), '14:00', '16:00', 'CONFIRMEE', 'Formation juges tennis'),
(2, 9, DATE_ADD(CURDATE(), INTERVAL 5  DAY), '09:30', '11:30', 'CONFIRMEE', 'AG comité olympique'),
(6, 13,DATE_ADD(CURDATE(), INTERVAL 7  DAY), '12:00', '14:00', 'CONFIRMEE', 'Réunion Société Générale'),
(7, 10,DATE_ADD(CURDATE(), INTERVAL 8  DAY), '08:30', '10:00', 'CONFIRMEE', 'TP multimédia lycée Poincaré'),
(4, 5, DATE_ADD(CURDATE(), INTERVAL 10 DAY), '15:00', '17:30', 'CONFIRMEE', 'Séminaire Club Omnisports');

-- --- Réservations futures en attente ---
INSERT INTO reservation (id_salle, id_structure, date_reservation, heure_debut, heure_fin, statut, commentaire) VALUES
(1, 6, DATE_ADD(CURDATE(), INTERVAL 3  DAY), '14:00', '15:30', 'EN_ATTENTE', NULL),
(3, 8, DATE_ADD(CURDATE(), INTERVAL 6  DAY), '10:00', '12:00', 'EN_ATTENTE', 'En attente de confirmation salle'),
(2, 14,DATE_ADD(CURDATE(), INTERVAL 9  DAY), '09:00', '10:30', 'EN_ATTENTE', 'Association parents d''élèves'),
(5, 3, DATE_ADD(CURDATE(), INTERVAL 14 DAY), '09:00', '18:00', 'EN_ATTENTE', 'Journée régionale athlétisme — à confirmer');

-- ------------------------------------------------------------
-- Digicode et Wifi — historique + jour actuel
-- ------------------------------------------------------------
INSERT INTO info_jour (date_info, digicode, cle_wifi) VALUES
(DATE_SUB(CURDATE(), INTERVAL 6 DAY), '7823', 'M2L-Wifi-Lundi'),
(DATE_SUB(CURDATE(), INTERVAL 5 DAY), '4491', 'M2L-Wifi-Mardi'),
(DATE_SUB(CURDATE(), INTERVAL 4 DAY), '9056', 'M2L-Wifi-Mercredi'),
(DATE_SUB(CURDATE(), INTERVAL 3 DAY), '3317', 'M2L-Wifi-Jeudi'),
(DATE_SUB(CURDATE(), INTERVAL 2 DAY), '6742', 'M2L-Wifi-Vendredi'),
(DATE_SUB(CURDATE(), INTERVAL 1 DAY), '1188', 'M2L-Wifi-Samedi'),
(CURDATE(),                            '5534', 'M2L-Wifi-Visiteurs');
