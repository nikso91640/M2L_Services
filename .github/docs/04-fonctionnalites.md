# 04 — Fonctionnalités et ordre de développement

## Principe de développement

Toujours travailler étape par étape :
- Une étape = un écran ou une fonctionnalité
- Valider et tester avant de passer à la suivante
- Ne jamais générer plusieurs étapes d'un coup

---

## Étape 1 — Connexion

**Fichiers à créer :** `Forms/FormLogin.cs`, `DataAccess/UtilisateurDAO.cs`, `Models/Utilisateur.cs`

**Ce que doit faire l'écran :**
- Champs : login (TextBox), mot de passe (TextBox avec PasswordChar)
- Bouton "Se connecter"
- Vérifier login + SHA2(mot_de_passe) en base
- Si OK : ouvrir `FormAccueil` et fermer `FormLogin`
- Si KO : afficher "Identifiants incorrects"

**Règle :** stocker l'utilisateur connecté dans une variable statique (`Session.UtilisateurConnecte`) accessible partout.

---

## Étape 2 — Gestion des Salles

**Fichiers à créer :** `Forms/FormSalles.cs`, `DataAccess/SalleDAO.cs`, `Models/Salle.cs`

**Ce que doit faire l'écran :**
- DataGridView listant toutes les salles (nom, capacité, type)
- Boutons : Ajouter, Modifier, Supprimer
- ComboBox pour le type de salle lors de l'ajout/modification
- Validation : nom non vide, capacité > 0
- Confirmation avant suppression

---

## Étape 3 — Gestion des Structures

**Fichiers à créer :** `Forms/FormStructures.cs`, `DataAccess/StructureDAO.cs`, `Models/Structure.cs`

**Ce que doit faire l'écran :**
- DataGridView listant toutes les structures (nom, type)
- Boutons : Ajouter, Modifier, Supprimer
- ComboBox pour le type de structure
- Validation : nom non vide

---

## Étape 4 — Réservations (création)

**Fichiers à créer :** `Forms/FormReservations.cs`, `DataAccess/ReservationDAO.cs`, `Models/Reservation.cs`, `Services/TarifService.cs`, `Services/DisponibiliteService.cs`

**Ce que doit faire l'écran de création :**
- ComboBox : salle, structure
- DateTimePicker : date
- DateTimePicker (heure) : heure début, heure fin
- TextBox : commentaire
- Label : affiche automatiquement le niveau de tarif calculé
- Label : affiche le quota restant si structure = LIGUE
- Bouton "Vérifier disponibilité" → appelle `DisponibiliteService`
- Bouton "Enregistrer" → appelle `TarifService` puis enregistre en BDD
- Statut par défaut : EN_ATTENTE

---

## Étape 5 — Réservations (liste + gestion)

**Même fichier Form que l'étape 4, onglet ou panel séparé**

**Ce que doit faire l'écran de liste :**
- DataGridView avec filtres : par salle, par structure, par date, par statut
- Boutons : Confirmer (→ CONFIRMEE), Annuler (→ ANNULEE), Modifier, Supprimer
- Affichage coloré selon le statut (vert = confirmée, rouge = annulée, orange = en attente)

---

## Étape 6 — Affranchissement

**Fichiers à créer :** `Forms/FormAffranchissement.cs`, `DataAccess/AffranchissementDAO.cs`, `Models/ConsommationAffranchissement.cs`

**Ce que doit faire l'écran :**
- ComboBox : structure, mois, année
- TextBox : quantité, type d'affranchissement, montant
- DataGridView : liste des consommations filtrées
- Boutons : Ajouter, Modifier, Supprimer
- Bouton "Récapitulatif mensuel" → affiche total par structure pour un mois/année donné

---

## Étape 7 — Impressions

**Fichiers à créer :** `Forms/FormImpressions.cs`, `DataAccess/ImpressionDAO.cs`, `Models/ConsommationImpression.cs`, `Services/ImpressionService.cs`

**Ce que doit faire l'écran :**
- ComboBox : structure, type impression (NB/COULEUR/TRACEUR), mois, année
- NumericUpDown : nombre de pages
- Label : montant calculé automatiquement (nb_pages × prix_unitaire du type)
- DataGridView : liste des consommations filtrées
- Boutons : Ajouter, Modifier, Supprimer
- Bouton "Récapitulatif mensuel" → total par structure et par type

---

## Étape 8 — Digicode & Wifi

**Fichiers à créer :** `Forms/FormDigicode.cs`, `DataAccess/InfoJourDAO.cs`, `Models/InfoJour.cs`

**Ce que doit faire l'écran :**
- Affiche en grand le digicode et la clé Wifi du jour (récupérés automatiquement)
- Si aucune info pour aujourd'hui : message "Aucune information saisie"
- Formulaire de saisie pour admin : date, digicode, clé Wifi
- DataGridView : historique des codes passés

**Sur l'écran d'accueil (`FormAccueil`) :**
- Afficher le digicode et la clé Wifi du jour dans un encadré visible

---

## Étape 9 — Finitions

- Vérifier toutes les validations de champs (aucun champ obligatoire vide)
- Vérifier tous les messages d'erreur (MessageBox clairs et en français)
- Vérifier la cohérence des données affichées
- Tester les cas limites : quota gratuit épuisé, créneau indisponible, suppression avec données liées
- Vérifier que les boutons sont bien activés/désactivés selon le rôle (ADMIN vs AGENT)
