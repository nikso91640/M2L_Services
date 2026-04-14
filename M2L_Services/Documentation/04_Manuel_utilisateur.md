# Manuel utilisateur — M2L Services

| **Élément**         | **Valeur**                                |
|---------------------|-------------------------------------------|
| Projet              | M2L Services                              |
| Version             | 1.0                                       |
| Date                | 2025                                      |
| Auteur              | *[Votre nom, prénom]*                     |

---

## 1. Présentation de l'application

**M2L Services** est une application de bureau qui permet au personnel de la Maison des Ligues de Lorraine de gérer les services mutualisés :

- 📋 **Gérer les salles** de réunion, l'amphithéâtre, la salle de convivialité et la salle multimédia ;
- 🏢 **Gérer les structures** (ligues, clubs, associations…) qui réservent des salles ;
- 📅 **Gérer les réservations** de salles avec vérification automatique de disponibilité ;
- 🔑 **Consulter et saisir** le digicode du jour et la clé Wifi visiteurs ;
- 📊 **Visualiser un tableau de bord** avec les chiffres clés de la journée.

---

## 2. Connexion à l'application

### 2.1 Écran de connexion

Au lancement de l'application, l'écran de connexion s'affiche.

> **📸 Capture d'écran :** *Insérez ici une capture de l'écran de connexion.*

```
┌──────────────────────────────────────────────────┐
│            M2L Services — Connexion              │
├──────────────────────────────────────────────────┤
│                                                  │
│   Login :           [  admin              ]      │
│                                                  │
│   Mot de passe :    [  ********           ]      │
│                                                  │
│   ☐ Afficher le mot de passe                     │
│                                                  │
│              [  Se connecter  ]                  │
│                                                  │
│   Tentatives restantes : 3                       │
│                                                  │
│   (Les messages d'erreur s'affichent ici)        │
│                                                  │
└──────────────────────────────────────────────────┘
```

### 2.2 Comment se connecter

**Étape par étape :**

1. Saisissez votre **login** dans le premier champ ;
2. Saisissez votre **mot de passe** dans le deuxième champ ;
3. *(Optionnel)* Cochez **« Afficher le mot de passe »** pour vérifier votre saisie ;
4. Cliquez sur **« Se connecter »** ou appuyez sur la touche **Entrée**.

**Comptes disponibles pour tester :**

| Login      | Mot de passe | Rôle                  |
|------------|-------------|-----------------------|
| `admin`    | `admin123`  | Administrateur (ADMIN)|
| `jdupont`  | `agent123`  | Agent d'accueil (AGENT)|

### 2.3 Gestion des erreurs de connexion

| Situation                        | Message affiché                                    | Action à faire                    |
|----------------------------------|----------------------------------------------------|-----------------------------------|
| Champ login vide                 | "Veuillez saisir votre login."                     | Saisir le login                   |
| Champ mot de passe vide          | "Veuillez saisir votre mot de passe."              | Saisir le mot de passe            |
| Login ou mot de passe incorrect  | "Login ou mot de passe incorrect."                 | Vérifier vos identifiants         |
| 3 tentatives échouées            | "Connexion bloquée après 3 tentatives."            | Fermer et relancer l'application  |
| MySQL non démarré                | "Impossible de contacter la base de données."      | Vérifier que WAMP est démarré     |

### 2.4 Les deux rôles utilisateur

```
┌─────────────────────────────────────────────────────────────────┐
│                         ADMINISTRATEUR (ADMIN)                  │
│                                                                 │
│  ✅ Consulter toutes les données                                │
│  ✅ Ajouter, modifier, supprimer des salles                     │
│  ✅ Ajouter, modifier, supprimer des structures                 │
│  ✅ Ajouter, modifier, annuler, supprimer des réservations      │
│  ✅ Saisir le digicode et la clé Wifi du jour                   │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                         AGENT D'ACCUEIL (AGENT)                 │
│                                                                 │
│  ✅ Consulter toutes les données (listes, tableau de bord)      │
│  ✅ Consulter l'historique du digicode et de la clé Wifi        │
│  ❌ Ne peut PAS ajouter, modifier ou supprimer de données       │
│  ❌ Ne peut PAS saisir le digicode/Wifi du jour                 │
│                                                                 │
│  → Les boutons d'action sont grisés (désactivés)               │
└─────────────────────────────────────────────────────────────────┘
```

---

## 3. Écran d'accueil (Tableau de bord)

Après une connexion réussie, l'écran d'accueil s'affiche. C'est le **point central** de l'application.

> **📸 Capture d'écran :** *Insérez ici une capture du tableau de bord avec des données.*

```
┌──────────────────────────────────────────────────────────────────────────┐
│  Fichier  │  Salles  │  Structures  │  Réservations  │  Digicode & Wifi│
├──────────────────────────────────────────────────────────────────────────┤
│                                                                          │
│  👤 Connecté : Jean Dupont — ADMIN                                      │
│                                                                          │
│  ─────────────────────────────────────────────────────────────────────── │
│  🔑 Digicode : 5534                   📶 Wifi : M2L-Wifi-Visiteurs     │
│  ─────────────────────────────────────────────────────────────────────── │
│                                                                          │
│  ┌───────────────┐  ┌───────────────┐  ┌───────────────┐  ┌───────────┐│
│  │               │  │               │  │               │  │           ││
│  │      7        │  │      14       │  │      25       │  │     4     ││
│  │               │  │               │  │               │  │           ││
│  │    Salles     │  │  Structures   │  │ Réservations  │  │Aujourd'hui││
│  │               │  │               │  │               │  │           ││
│  └───────────────┘  └───────────────┘  └───────────────┘  └───────────┘│
│                                                                          │
│  ⚠️ Réservations en attente : 4  │  Dernière MAJ : 10/06/2025 14:32:05 │
│                                                                          │
└──────────────────────────────────────────────────────────────────────────┘
```

### 3.1 La barre de menu

La barre de menu en haut permet d'accéder à tous les modules de l'application :

| Menu                  | Sous-menus / Action                                      |
|-----------------------|----------------------------------------------------------|
| **Fichier**           | Actualiser · Se déconnecter · Quitter                    |
| **Salles**            | Ouvre la gestion des salles                              |
| **Structures**        | Ouvre la gestion des structures                          |
| **Réservations**      | Ouvre la gestion des réservations                        |
| **Digicode & Wifi**   | Ouvre la saisie du digicode/wifi et l'historique         |

### 3.2 Les informations affichées

| Zone                    | Contenu                                                  |
|-------------------------|----------------------------------------------------------|
| **Utilisateur connecté** | Prénom, nom et rôle (ADMIN ou AGENT)                    |
| **Info du jour**         | Digicode et clé Wifi du jour (ou message si non renseigné) |
| **Compteur Salles**      | Nombre total de salles enregistrées                     |
| **Compteur Structures**  | Nombre total de structures enregistrées                 |
| **Compteur Réservations**| Nombre total de réservations (tous statuts)             |
| **Compteur Aujourd'hui** | Nombre de réservations prévues aujourd'hui              |
| **Zone d'alerte**        | Nombre de réservations en attente + heure de dernière mise à jour |

### 3.3 Actualisation

Le tableau de bord se met à jour **automatiquement** :
- À chaque retour sur l'écran d'accueil (après fermeture d'un formulaire de gestion) ;
- Via le menu **Fichier → Actualiser**.

---

## 4. Gestion des salles

### 4.1 Accéder à l'écran

Cliquez sur le menu **Salles** depuis l'écran d'accueil.

> **📸 Capture d'écran :** *Insérez ici une capture de l'écran de gestion des salles avec la barre d'outils et le tableau.*

```
┌──────────────────────────────────────────────────────────────────────┐
│  M2L Services — Gestion des salles                                   │
├──────────────────────────────────────────────────────────────────────┤
│  [Ajouter] [Modifier] │ [Supprimer] │ [Actualiser] │ Recherche:[__]│
├──────────────────────────────────────────────────────────────────────┤
│                                                                      │
│  Nom de la salle                │  Capacité  │  Type                │
│  ───────────────────────────────┼────────────┼──────────────────────│
│▶ Amphithéâtre                   │    200     │  AMPHI               │
│  Salle Baccarat                 │     50     │  REUNION             │
│  Salle convivialité             │     80     │  CONVIVIALITE        │
│  Salle Daum                     │     30     │  REUNION             │
│  Salle Gallé                    │     20     │  REUNION             │
│  Salle Majorelle                │     15     │  REUNION             │
│  Salle multimédia               │     24     │  MULTIMEDIA          │
│                                                                      │
└──────────────────────────────────────────────────────────────────────┘
```

### 4.2 Rechercher une salle

1. Tapez un texte dans le champ **Recherche** de la barre d'outils ;
2. Le tableau se filtre **instantanément** (pas besoin d'appuyer sur Entrée) ;
3. Le filtre recherche dans le **nom** et le **type** de salle ;
4. Effacez le texte pour réafficher toutes les salles.

**Exemple :** Tapez `reunion` → seules les salles de type REUNION s'affichent.

### 4.3 Ajouter une salle (ADMIN uniquement)

1. Cliquez sur le bouton **[Ajouter]** dans la barre d'outils ;
2. Un formulaire de détail s'ouvre :

> **📸 Capture d'écran :** *Insérez ici une capture du formulaire d'ajout de salle.*

```
┌──────────────────────────────────────┐
│       Ajouter une salle              │
├──────────────────────────────────────┤
│                                      │
│  Nom :       [____________________]  │
│                                      │
│  Capacité :  [  20   ]              │
│                                      │
│  Type :      [ REUNION          ▼ ]  │
│                                      │
│      [ Valider ]    [ Annuler ]     │
│                                      │
└──────────────────────────────────────┘
```

3. Remplissez les champs :
   - **Nom** : le nom de la salle (obligatoire, doit être unique) ;
   - **Capacité** : le nombre de places ;
   - **Type** : sélectionnez dans la liste (REUNION, AMPHI, CONVIVIALITE, MULTIMEDIA) ;
4. Cliquez sur **Valider** ;
5. Si tout est correct → message « Salle ajoutée avec succès » et la liste se rafraîchit.

**Contrôles automatiques :**
- Si le nom est vide → message « Veuillez saisir un nom » ;
- Si le nom existe déjà → message « Une salle avec ce nom existe déjà ».

### 4.4 Modifier une salle (ADMIN uniquement)

1. **Sélectionnez** une salle en cliquant sur sa ligne dans le tableau ;
2. Cliquez sur **[Modifier]** OU **double-cliquez** sur la ligne ;
3. Le formulaire de détail s'ouvre avec les **valeurs actuelles** déjà remplies ;
4. Modifiez les champs souhaités → cliquez sur **Valider**.

### 4.5 Supprimer une salle (ADMIN uniquement)

1. Sélectionnez une salle dans le tableau ;
2. Cliquez sur **[Supprimer]** ;
3. Une boîte de dialogue de confirmation s'affiche ;
4. Cliquez sur **Oui** pour confirmer.

> ⚠️ **Attention :** Une salle liée à des réservations existantes **ne peut pas être supprimée**. Un message d'erreur s'affiche dans ce cas. Il faut d'abord supprimer les réservations concernées.

---

## 5. Gestion des structures

### 5.1 Accéder à l'écran

Cliquez sur le menu **Structures** depuis l'écran d'accueil.

> **📸 Capture d'écran :** *Insérez ici une capture de l'écran de gestion des structures.*

```
┌──────────────────────────────────────────────────────────────────────┐
│  M2L Services — Gestion des structures                               │
├──────────────────────────────────────────────────────────────────────┤
│  [Ajouter] [Modifier] │ [Supprimer] │ [Actualiser] │ Recherche:[__]│
├──────────────────────────────────────────────────────────────────────┤
│                                                                      │
│  Nom de la structure                        │  Type                  │
│  ───────────────────────────────────────────┼────────────────────────│
│  Association Lorraine des Arts Martiaux     │  ASSOCIATION           │
│  Club Nautique de Metz                      │  CLUB                  │
│  Comité Départemental de Handball 54        │  CLUB                  │
│  Entreprise LogiSport SAS                   │  ENTREPRISE            │
│  Ligue Lorraine d'Athlétisme               │  LIGUE                 │
│  Ligue Lorraine de Basketball              │  LIGUE                 │
│  Ligue Lorraine de Football                │  LIGUE                 │
│  ...                                        │  ...                   │
└──────────────────────────────────────────────────────────────────────┘
```

### 5.2 Fonctionnement

Le fonctionnement est **identique** à la gestion des salles :

| Action       | Comment faire                                            |
|-------------|----------------------------------------------------------|
| Rechercher  | Tapez dans le champ Recherche (filtre par nom ou type)   |
| Ajouter     | Bouton [Ajouter] → remplir nom + type → Valider         |
| Modifier    | Sélectionner + [Modifier] (ou double-clic) → modifier → Valider |
| Supprimer   | Sélectionner + [Supprimer] → confirmer                   |

**Types de structure disponibles :**
- `LIGUE` — Ligue sportive régionale
- `CLUB` — Club sportif ou comité départemental
- `ASSOCIATION` — Association
- `LYCEE_COLLEGE` — Établissement scolaire
- `ENTREPRISE` — Entreprise
- `AUTRE` — Autre type d'organisme

---

## 6. Gestion des réservations

### 6.1 Accéder à l'écran

Cliquez sur le menu **Réservations** depuis l'écran d'accueil.

> **📸 Capture d'écran :** *Insérez ici une capture de l'écran de gestion des réservations avec les filtres et les couleurs de statut.*

```
┌──────────────────────────────────────────────────────────────────────────┐
│  M2L Services — Gestion des réservations                                 │
├──────────────────────────────────────────────────────────────────────────┤
│ [Ajouter][Modifier]│[Annuler][Supprimer]│[Actualiser]│[TOUS ▼]│Rech:[_]│
├──────────────────────────────────────────────────────────────────────────┤
│                                                                          │
│  Date       │ Salle          │ Structure        │ Début │ Fin  │ Statut │
│  ───────────┼────────────────┼──────────────────┼───────┼──────┼────────│
│  15/06/2025 │ Salle Gallé    │ Ligue Football   │ 09:00 │11:00 │🟢CONF. │
│  15/06/2025 │ Amphithéâtre   │ Ligue Basket     │ 14:00 │16:00 │🟠ATT.  │
│  14/06/2025 │ Salle Daum     │ Club Nautique    │ 10:00 │11:30 │🟢CONF. │
│  13/06/2025 │ Salle Baccarat │ Ligue Athlétisme │ 08:00 │10:00 │🔴ANN.  │
│  ...                                                                     │
└──────────────────────────────────────────────────────────────────────────┘
```

### 6.2 Code couleur des statuts

| Couleur      | Statut         | Signification                                     |
|-------------|----------------|----------------------------------------------------|
| 🟢 **Vert**  | `CONFIRMEE`   | La réservation est validée et le créneau est bloqué |
| 🟠 **Orange**| `EN_ATTENTE`  | La réservation attend une confirmation              |
| 🔴 **Rouge** | `ANNULEE`     | La réservation a été annulée (historique conservé)  |

### 6.3 Filtrer les réservations

**Deux filtres sont disponibles :**

1. **Filtre par statut** — Liste déroulante dans la barre d'outils :
   - `TOUS` (par défaut) — affiche toutes les réservations
   - `EN_ATTENTE` — uniquement les réservations en attente
   - `CONFIRMEE` — uniquement les réservations confirmées
   - `ANNULEE` — uniquement les réservations annulées

2. **Filtre par texte** — Champ Recherche :
   - Filtre par **nom de salle** ou **nom de structure**
   - Exemple : tapez `football` → seules les réservations de la Ligue de Football s'affichent

**Les deux filtres se combinent.** Par exemple :
- Statut = `CONFIRMEE` + Recherche = `gallé` → affiche les réservations confirmées de la Salle Gallé uniquement.

### 6.4 Ajouter une réservation (ADMIN uniquement)

1. Cliquez sur **[Ajouter]** ;
2. Le formulaire de détail s'ouvre :

> **📸 Capture d'écran :** *Insérez ici une capture du formulaire d'ajout de réservation.*

```
┌──────────────────────────────────────────────────┐
│        Ajouter une réservation                   │
├──────────────────────────────────────────────────┤
│                                                  │
│  Salle :       [ Salle Gallé              ▼ ]   │
│                                                  │
│  Structure :   [ Ligue Lorraine Football  ▼ ]   │
│                                                  │
│  Date :        [  15/06/2025               ]    │
│                                                  │
│  Heure début : [  09:00  ▼ ]                    │
│  Heure fin :   [  11:00  ▼ ]                    │
│                                                  │
│  Statut :      [ CONFIRMEE  ▼ ]                 │
│                                                  │
│  Commentaire : [_____________________________]  │
│                [_____________________________]  │
│                                                  │
│         [ Valider ]       [ Annuler ]           │
│                                                  │
└──────────────────────────────────────────────────┘
```

3. Remplissez les champs :
   - **Salle** : choisissez la salle dans la liste déroulante ;
   - **Structure** : choisissez la structure qui réserve ;
   - **Date** : sélectionnez la date ;
   - **Heure début / fin** : créneaux de 30 minutes, de 06:00 à 21:00 ;
   - **Statut** : EN_ATTENTE (par défaut), CONFIRMEE ou ANNULEE ;
   - **Commentaire** : texte libre (facultatif) ;
4. Cliquez sur **Valider**.

**Contrôles automatiques :**

| Contrôle                                    | Quand ?                                     |
|---------------------------------------------|---------------------------------------------|
| Salle obligatoire                           | À la validation                             |
| Structure obligatoire                       | À la validation                             |
| Heure fin > Heure début                    | À la validation                             |
| **Vérification de disponibilité**           | Si le statut est `CONFIRMEE` uniquement     |

> 📌 **Important :** La vérification de disponibilité se fait **uniquement pour les réservations CONFIRMEE**. Une réservation EN_ATTENTE peut chevaucher un créneau existant (elle n'est pas encore validée).

### 6.5 Modifier une réservation (ADMIN uniquement)

1. Sélectionnez la réservation dans le tableau ;
2. Cliquez sur **[Modifier]** ou **double-cliquez** sur la ligne ;
3. Le formulaire s'ouvre avec les valeurs actuelles pré-remplies ;
4. Modifiez les champs souhaités → **Valider**.

### 6.6 Annuler une réservation (ADMIN uniquement)

1. Sélectionnez la réservation ;
2. Cliquez sur **[Annuler la réservation]** ;
3. Confirmez dans la boîte de dialogue ;
4. Le statut passe à **ANNULEE** (la réservation reste visible dans l'historique).

### 6.7 Supprimer une réservation (ADMIN uniquement)

1. Sélectionnez la réservation ;
2. Cliquez sur **[Supprimer]** ;
3. Confirmez la suppression définitive.

### 6.8 Différence entre Annuler et Supprimer

```
            Annuler                           Supprimer
    ┌─────────────────────┐           ┌─────────────────────┐
    │  La réservation     │           │  La réservation     │
    │  reste dans la base │           │  est EFFACÉE de     │
    │  avec le statut     │           │  la base de données │
    │  "ANNULEE"          │           │  définitivement     │
    │                     │           │                     │
    │  ✅ Traçabilité     │           │  ❌ Pas d'historique │
    │  ✅ Historique      │           │  ✅ Base propre      │
    └─────────────────────┘           └─────────────────────┘
```

---

## 7. Digicode et clé Wifi

### 7.1 Accéder à l'écran

Cliquez sur le menu **Digicode & Wifi** depuis l'écran d'accueil.

> **📸 Capture d'écran :** *Insérez ici une capture de l'écran Digicode & Wifi avec l'historique.*

```
┌──────────────────────────────────────────────────────────────────────┐
│  M2L Services — Digicode et Wifi du jour                             │
├──────────────────────────────────────────────────────────────────────┤
│  ┌─ Info du jour ─────────────────────────────────────────────────┐  │
│  │                                                                │  │
│  │  Digicode :  [  5534  ]  [ Copier ]                           │  │
│  │                                                  [Enregistrer] │  │
│  │  Clé Wifi :  [  M2L-Wifi-Visiteurs  ]  [ Copier ]            │  │
│  │                                                                │  │
│  └────────────────────────────────────────────────────────────────┘  │
│                                                                      │
│  ┌─ Historique ───────────────────────────────────────────────────┐  │
│  │                                                                │  │
│  │  Date          │  Digicode    │  Clé Wifi                     │  │
│  │  ──────────────┼──────────────┼───────────────────────────────│  │
│  │  10/06/2025    │  5534        │  M2L-Wifi-Visiteurs           │  │
│  │  09/06/2025    │  1188        │  M2L-Wifi-Samedi              │  │
│  │  08/06/2025    │  6742        │  M2L-Wifi-Vendredi            │  │
│  │  07/06/2025    │  3317        │  M2L-Wifi-Jeudi               │  │
│  │  06/06/2025    │  9921        │  M2L-Wifi-Mercredi            │  │
│  │  05/06/2025    │  4456        │  M2L-Wifi-Mardi               │  │
│  │  04/06/2025    │  7783        │  M2L-Wifi-Lundi               │  │
│  │                                                                │  │
│  └────────────────────────────────────────────────────────────────┘  │
└──────────────────────────────────────────────────────────────────────┘
```

### 7.2 Consulter l'info du jour

- Si le digicode et la clé Wifi ont été saisis pour aujourd'hui, ils s'affichent automatiquement en **gras et grande taille** dans les champs ;
- Si rien n'a été saisi, les champs sont vides.

### 7.3 Saisir ou modifier l'info du jour (ADMIN uniquement)

1. Saisissez le **digicode** dans le premier champ (20 caractères maximum) ;
2. Saisissez la **clé Wifi** dans le deuxième champ (100 caractères maximum) ;
3. Cliquez sur **[Enregistrer]** ;
4. Si une info existait déjà pour aujourd'hui, elle est **mise à jour** automatiquement (pas de doublon) ;
5. L'historique se rafraîchit immédiatement.

### 7.4 Copier dans le presse-papier

| Bouton              | Action                                                    |
|---------------------|-----------------------------------------------------------|
| **[Copier]** (digicode) | Copie le digicode dans le presse-papier               |
| **[Copier]** (wifi)     | Copie la clé Wifi dans le presse-papier               |

Après avoir cliqué sur Copier, vous pouvez **coller** (`Ctrl + V`) le texte dans un e-mail, un document, un message, etc.

### 7.5 Historique

Le tableau en bas de l'écran affiche l'**historique complet** de tous les digicodes et clés Wifi, du plus récent au plus ancien. Tous les utilisateurs (ADMIN et AGENT) peuvent consulter cet historique.

---

## 8. Déconnexion et fermeture

### 8.1 Se déconnecter

```
Menu Fichier → Se déconnecter → Confirmer (Oui)
```

1. L'écran d'accueil se ferme ;
2. L'écran de connexion réapparaît ;
3. Vous pouvez vous reconnecter avec un **autre compte** (ex. : passer d'ADMIN à AGENT).

### 8.2 Quitter l'application

```
Menu Fichier → Quitter
```

Ou cliquez sur le bouton **✕** (fermer) de la fenêtre.

---

## 9. Récapitulatif des messages d'erreur

| Message affiché                                        | Cause                                                | Solution                                        |
|--------------------------------------------------------|------------------------------------------------------|-------------------------------------------------|
| "Veuillez saisir votre login."                         | Le champ login est vide                              | Saisir votre identifiant                        |
| "Login ou mot de passe incorrect."                     | Identifiants erronés                                 | Vérifier login et mot de passe                  |
| "Connexion bloquée après 3 tentatives."                | 3 échecs consécutifs                                 | Fermer et relancer l'application                |
| "Impossible de contacter la base de données."          | MySQL non démarré ou erreur réseau                   | Vérifier que WAMP est démarré (icône verte)     |
| "Aucune donnée trouvée."                               | La base de données est vide                          | Exécuter le script `database.sql`               |
| "Une salle avec ce nom existe déjà."                   | Doublon de nom de salle                              | Choisir un nom différent                        |
| "Une structure avec ce nom existe déjà."               | Doublon de nom de structure                          | Choisir un nom différent                        |
| "La salle est déjà réservée le..."                     | Chevauchement de créneau confirmé                    | Choisir un autre créneau ou une autre salle     |
| "L'heure de fin doit être après l'heure de début."     | Incohérence d'horaires                               | Corriger les heures                             |
| "Veuillez d'abord sélectionner une ligne."             | Aucune ligne sélectionnée dans le tableau            | Cliquer sur une ligne avant de modifier/supprimer |
| "Veuillez saisir un digicode."                         | Le champ digicode est vide                           | Remplir le champ avant d'enregistrer            |

---

## 10. Astuces et raccourcis

| Astuce                                    | Description                                              |
|-------------------------------------------|----------------------------------------------------------|
| **Touche Entrée** sur l'écran de connexion | Valide la connexion automatiquement                     |
| **Double-clic** sur une ligne              | Ouvre directement la modification (ADMIN uniquement)    |
| **Recherche instantanée**                  | Le filtre s'applique dès que vous tapez (pas besoin d'appuyer sur Entrée) |
| **Combinaison de filtres** (réservations)  | Filtrez par statut ET par texte en même temps           |
| **Boutons Copier** (Digicode & Wifi)       | Copie la valeur pour la coller facilement ailleurs      |
| **Retour au tableau de bord**              | Fermez le formulaire en cours → vous revenez à l'accueil avec les données mises à jour |

---

## 11. Insertion des captures d'écran

Pour finaliser ce manuel, **remplacez** chaque encadré `📸 Capture d'écran` par une vraie capture de l'application en fonctionnement.

**Comment faire :**

1. Lancez l'application M2L Services ;
2. Connectez-vous avec le compte `admin` / `admin123` ;
3. Naviguez vers chaque écran ;
4. Appuyez sur `Win + Maj + S` pour ouvrir l'outil de capture Windows ;
5. Sélectionnez la zone à capturer ;
6. Collez l'image dans votre éditeur (Word, Google Docs, LibreOffice…).

**Liste des captures à réaliser :**

| N° | Écran à capturer                                     |
|----|------------------------------------------------------|
| 1  | Formulaire de connexion (vide)                       |
| 2  | Formulaire de connexion (erreur après mauvais login) |
| 3  | Tableau de bord (avec des données)                   |
| 4  | Gestion des salles (liste complète)                  |
| 5  | Formulaire d'ajout de salle                          |
| 6  | Gestion des structures (liste complète)              |
| 7  | Gestion des réservations (avec filtres et couleurs)  |
| 8  | Formulaire d'ajout de réservation                    |
| 9  | Message de créneau indisponible                      |
| 10 | Digicode & Wifi (info du jour + historique)          |
| 11 | Gestion des salles connecté en AGENT (boutons grisés)|
