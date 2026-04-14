# Manuel d'installation et de déploiement — M2L Services

| **Élément**         | **Valeur**                                |
|---------------------|-------------------------------------------|
| Projet              | M2L Services                              |
| Version             | 1.0                                       |
| Date                | 2025                                      |
| Auteur              | *[Votre nom, prénom]*                     |

> **Contexte :** Ce document décrit comment déployer l'application M2L Services sur votre infrastructure afin que le **jury BTS SIO** puisse y accéder à distance lors de l'épreuve E6. Il couvre aussi la génération de l'exécutable et la configuration réseau nécessaire.

---

## 1. Architecture de déploiement

L'application M2L Services nécessite deux composants pour fonctionner :

```
┌──────────────────────────────────────────────────────────────────────┐
│                     Votre infrastructure                             │
│                                                                      │
│  ┌─────────────────────┐          ┌─────────────────────────────┐   │
│  │  Serveur / VM       │          │  Poste client               │   │
│  │  (Windows Server    │          │  (Bureau à distance - RDP)  │   │
│  │   ou Windows 10/11) │◄────────►│                             │   │
│  │                     │  Réseau  │  Le jury se connecte ici    │   │
│  │  ┌───────────────┐  │          │  via Bureau à distance      │   │
│  │  │ MySQL (WAMP)  │  │          └─────────────────────────────┘   │
│  │  │ Port 3306     │  │                                            │
│  │  └───────────────┘  │          ┌─────────────────────────────┐   │
│  │                     │          │  Poste développeur          │   │
│  │  ┌───────────────┐  │          │  (Visual Studio)            │   │
│  │  │ M2L_Services  │  │          │  Code source + compilation  │   │
│  │  │ .exe          │  │          └─────────────────────────────┘   │
│  │  └───────────────┘  │                                            │
│  └─────────────────────┘                                            │
└──────────────────────────────────────────────────────────────────────┘
```

**Deux scénarios possibles :**

| Scénario | Description | Recommandé pour |
|----------|-------------|-----------------|
| **A — Tout sur une seule machine** | MySQL + application sur le même poste. Le jury s'y connecte en Bureau à distance (RDP). | Infrastructure simple |
| **B — Séparé** | MySQL sur un serveur dédié ; l'application sur le poste client du jury. | Infrastructure avec serveur dédié |

---

## 2. Prérequis

### 2.1 Sur la machine serveur (ou machine unique)

| Élément                   | Version minimale         | Téléchargement                              |
|---------------------------|--------------------------|---------------------------------------------|
| Windows                   | 10 Pro / 11 Pro / Server 2019+ | —                                      |
| WAMP Server               | 3.2.0+                   | https://www.wampserver.com/                 |
| .NET Desktop Runtime      | 10.0                     | https://dotnet.microsoft.com/download       |
| Bureau à distance activé  | —                        | Paramètres Windows → Système → Bureau à distance |

### 2.2 Pour compiler (poste développeur)

| Élément                   | Version minimale         |
|---------------------------|--------------------------|
| Visual Studio             | 2022 (v17.8+) ou 2026   |
| .NET SDK                  | 10.0                     |

---

## 3. Étape 1 — Installer et configurer MySQL (WAMP)

### 3.1 Installer WAMP

1. Téléchargez WAMP depuis https://www.wampserver.com/ ;
2. Lancez l'installateur → suivez les étapes par défaut ;
3. Lancez **WAMP** ;
4. Attendez que l'icône dans la barre des tâches passe au **vert** :

```
  Icône WAMP :
    🔴 Rouge  = services arrêtés
    🟠 Orange = certains services en erreur
    🟢 Vert   = tous les services démarrés ✅
```

### 3.2 Vérifier que MySQL fonctionne

1. Clic gauche sur l'icône WAMP → **phpMyAdmin** ;
2. Connectez-vous avec :
   - **Utilisateur** : `root`
   - **Mot de passe** : *(vide par défaut)*
3. Si phpMyAdmin s'affiche → MySQL est opérationnel ✅

### 3.3 Configurer MySQL pour l'accès réseau (scénario B uniquement)

Si l'application sera lancée depuis un **autre poste** que le serveur MySQL :

1. Clic gauche sur l'icône WAMP → **MySQL** → **my.ini** ;
2. Trouvez la ligne `bind-address = 127.0.0.1` ;
3. Remplacez par `bind-address = 0.0.0.0` (écoute sur toutes les interfaces) ;
4. Redémarrez les services WAMP ;
5. Dans phpMyAdmin, créez un utilisateur autorisé depuis le réseau :

```sql
-- Remplacez 192.168.x.% par votre plage réseau
CREATE USER 'root'@'192.168.%.%' IDENTIFIED BY '';
GRANT ALL PRIVILEGES ON m2l_services.* TO 'root'@'192.168.%.%';
FLUSH PRIVILEGES;
```

6. Ouvrez le port **3306** dans le pare-feu Windows du serveur :

```
Panneau de configuration → Pare-feu Windows
→ Paramètres avancés → Règles de trafic entrant
→ Nouvelle règle → Port → TCP 3306 → Autoriser
```

---

## 4. Étape 2 — Créer la base de données

### 4.1 Exécuter le script SQL

1. Ouvrez **phpMyAdmin** dans votre navigateur : `http://localhost/phpmyadmin` ;
2. Cliquez sur l'onglet **SQL** en haut ;
3. Ouvrez le fichier **`database.sql`** situé à la racine du projet M2L_Services :

```
📂 M2L_Services/
   📄 database.sql    ◄── Ce fichier
   📂 Forms/
   📂 Models/
   ...
```

4. **Copiez tout le contenu** du fichier ;
5. **Collez-le** dans la zone de saisie SQL de phpMyAdmin ;
6. Cliquez sur **Exécuter** ;
7. Attendez le message de confirmation (toutes les requêtes en vert).

### 4.2 Vérifier que la base est correcte

Dans le panneau de gauche de phpMyAdmin, vous devez voir :

```
📂 m2l_services
   📋 utilisateur      (5 enregistrements)
   📋 salle            (7 enregistrements)
   📋 structure        (14 enregistrements)
   📋 reservation      (25 enregistrements)
   📋 info_jour        (7 enregistrements)
```

> **📸 Capture recommandée :** Faites une capture d'écran de phpMyAdmin montrant les tables créées et le nombre d'enregistrements. Insérez-la ici lors de la mise en forme finale.

### 4.3 Comptes utilisateurs disponibles

| Login      | Mot de passe | Rôle   | Description                    |
|------------|-------------|--------|--------------------------------|
| `admin`    | `admin123`  | ADMIN  | Compte administrateur principal|
| `sadmin`   | `admin123`  | ADMIN  | Deuxième compte admin          |
| `jdupont`  | `agent123`  | AGENT  | Agent d'accueil (consultation) |
| `mmartin`  | `agent123`  | AGENT  | Agent d'accueil                |
| `lbernard` | `agent123`  | AGENT  | Agent d'accueil                |

> **Pour le jury :** Utilisez le compte `admin` / `admin123` pour tester toutes les fonctionnalités.

---

## 5. Étape 3 — Générer l'exécutable

### 5.1 Méthode 1 — Depuis Visual Studio (recommandé)

1. Ouvrez le projet dans Visual Studio (fichier `M2L_Services.sln`) ;
2. Dans la barre d'outils, passez de **Debug** à **Release** :

```
  ┌──────────────┐
  │  Debug   ▼   │  ←── Cliquez et sélectionnez "Release"
  └──────────────┘
```

3. Menu **Générer** → **Publier M2L_Services** ;
4. Choisissez **Dossier** comme cible ;
5. Configurez :
   - Chemin de sortie : `bin\Publish\` ;
   - Mode : **Release** ;
   - Framework cible : `net10.0-windows` ;
6. Cliquez sur **Publier**.

Le dossier de sortie contiendra tous les fichiers nécessaires, dont `M2L_Services.exe`.

### 5.2 Méthode 2 — Depuis le terminal (ligne de commande)

Ouvrez un terminal PowerShell dans le dossier du projet et exécutez :

```powershell
dotnet publish -c Release -o bin\Publish
```

### 5.3 Méthode 3 — Exécutable autonome (sans .NET installé sur le poste cible)

Si le poste cible **n'a pas le .NET Runtime 10 installé**, créez un exécutable autonome :

```powershell
dotnet publish -c Release -r win-x64 --self-contained -o bin\PublishAutonome
```

> **Attention** : cette méthode génère un dossier plus volumineux (~80 Mo) car il embarque le runtime .NET.

### 5.4 Contenu du dossier publié

```
📂 bin\Publish\
   📄 M2L_Services.exe          ◄── L'exécutable principal
   📄 M2L_Services.dll
   📄 M2L_Services.deps.json
   📄 M2L_Services.runtimeconfig.json
   📄 MySql.Data.dll             ◄── Connecteur MySQL
   📄 ... (autres dépendances)
```

---

## 6. Étape 4 — Configurer la connexion à la base

### 6.1 Configuration par défaut

La chaîne de connexion est définie dans le fichier `Utils/ConnexionBDD.cs` :

```csharp
private static readonly string _chaineConnexion =
    "Server=localhost;Port=3306;Database=m2l_services;Uid=root;Pwd=;CharSet=utf8mb4;";
```

**Valeurs par défaut :**

| Paramètre | Valeur        | Signification                    |
|-----------|---------------|----------------------------------|
| Server    | `localhost`   | MySQL sur la même machine        |
| Port      | `3306`        | Port MySQL standard              |
| Database  | `m2l_services`| Nom de la base de données        |
| Uid       | `root`        | Utilisateur MySQL                |
| Pwd       | *(vide)*      | Mot de passe MySQL (vide par défaut dans WAMP) |

### 6.2 Si MySQL est sur un autre serveur

Modifiez la chaîne de connexion **avant de compiler** :

```csharp
// Exemple : MySQL sur le serveur 192.168.1.100
private static readonly string _chaineConnexion =
    "Server=192.168.1.100;Port=3306;Database=m2l_services;Uid=root;Pwd=;CharSet=utf8mb4;";
```

### 6.3 Si le mot de passe root est défini

```csharp
// Exemple : mot de passe root = "monmotdepasse"
private static readonly string _chaineConnexion =
    "Server=localhost;Port=3306;Database=m2l_services;Uid=root;Pwd=monmotdepasse;CharSet=utf8mb4;";
```

> **Important :** Après toute modification de `ConnexionBDD.cs`, il faut **recompiler** et **republier** l'exécutable.

---

## 7. Étape 5 — Déployer sur l'infrastructure pour le jury

### 7.1 Préparer la machine pour l'accès à distance

1. **Activer le Bureau à distance (RDP)** sur la machine cible :

```
Paramètres Windows → Système → Bureau à distance → Activer
```

2. **Vérifier l'adresse IP** de la machine :

```powershell
ipconfig
```

Notez l'adresse IPv4 (ex. : `192.168.1.50`).

3. **Ouvrir le port RDP (3389)** dans le pare-feu si ce n'est pas déjà fait :

```
Pare-feu Windows → Règles de trafic entrant → Autoriser le port 3389 (TCP)
```

4. **Créer un compte utilisateur Windows** pour le jury (recommandé) :

```
Paramètres → Comptes → Autres utilisateurs → Ajouter un utilisateur
   Nom : jury
   Mot de passe : jury2025
```

### 7.2 Installer l'application sur la machine

1. Copiez le dossier **`bin\Publish\`** (ou `bin\PublishAutonome\`) sur le bureau de la machine cible :

```
📂 Bureau\M2L_Services\
   📄 M2L_Services.exe
   📄 MySql.Data.dll
   📄 ...
```

2. Si vous n'avez **pas** utilisé `--self-contained`, installez le **.NET Desktop Runtime 10** sur la machine :
   - Téléchargez-le depuis https://dotnet.microsoft.com/download
   - Exécutez l'installateur

3. Vérifiez que **WAMP est démarré** (icône verte) ;

4. **Testez** en double-cliquant sur `M2L_Services.exe` → l'écran de connexion doit s'afficher.

### 7.3 Créer un raccourci sur le bureau

1. Clic droit sur `M2L_Services.exe` → **Créer un raccourci** ;
2. Déplacez le raccourci sur le **Bureau** ;
3. Renommez-le en **« M2L Services »**.

### 7.4 Démarrage automatique de WAMP (recommandé)

Pour que MySQL soit disponible dès le démarrage de la machine :

1. Appuyez sur `Win + R` → tapez `shell:startup` → Entrée ;
2. Placez un raccourci vers `wampmanager.exe` dans ce dossier ;
3. WAMP démarrera automatiquement à chaque ouverture de session.

### 7.5 Fiche de connexion pour le jury

Préparez un document (imprimé ou numérique) à remettre au jury :

```
╔══════════════════════════════════════════════════════════╗
║              FICHE DE CONNEXION — M2L Services          ║
╠══════════════════════════════════════════════════════════╣
║                                                          ║
║  Connexion Bureau à distance :                           ║
║  ─────────────────────────────                           ║
║  Adresse IP  : 192.168.x.x  (à compléter)              ║
║  Utilisateur : jury                                      ║
║  Mot de passe: jury2025                                  ║
║                                                          ║
║  Lancement de l'application :                            ║
║  ─────────────────────────────                           ║
║  Double-cliquer sur "M2L Services" sur le Bureau         ║
║                                                          ║
║  Compte application ADMIN :                              ║
║  ──────────────────────────                              ║
║  Login       : admin                                     ║
║  Mot de passe: admin123                                  ║
║                                                          ║
║  Compte application AGENT (consultation seule) :         ║
║  ──────────────────────────                              ║
║  Login       : jdupont                                   ║
║  Mot de passe: agent123                                  ║
║                                                          ║
╚══════════════════════════════════════════════════════════╝
```

---

## 8. Vérifications avant la soutenance (checklist)

Avant le jour J, vérifiez **chaque point** de cette liste :

| ✅ | Vérification                                                |
|----|-------------------------------------------------------------|
| ☐  | WAMP est installé et démarre automatiquement                |
| ☐  | La base `m2l_services` est créée et contient les données de test |
| ☐  | L'exécutable `M2L_Services.exe` est sur le bureau de la machine |
| ☐  | L'application se lance et l'écran de connexion s'affiche    |
| ☐  | La connexion avec `admin` / `admin123` fonctionne           |
| ☐  | Le tableau de bord affiche les compteurs (pas des « ? »)    |
| ☐  | Les modules Salles, Structures, Réservations, Digicode fonctionnent |
| ☐  | Le Bureau à distance (RDP) est activé                       |
| ☐  | Le compte Windows `jury` est créé et peut se connecter en RDP |
| ☐  | Le pare-feu autorise les ports 3389 (RDP) et 3306 (MySQL si nécessaire) |
| ☐  | La fiche de connexion est prête (imprimée ou numérique)     |
| ☐  | Un test complet a été fait depuis un autre poste en RDP     |

---

## 9. Dépannage

### Problème : "Impossible de contacter la base de données"

| Vérification                                  | Solution                                       |
|----------------------------------------------|------------------------------------------------|
| WAMP est-il démarré ?                        | Lancer WAMP, attendre l'icône verte            |
| La base `m2l_services` existe ?              | Exécuter `database.sql` dans phpMyAdmin        |
| Le mot de passe root a changé ?              | Adapter `ConnexionBDD.cs` et recompiler        |
| MySQL écoute sur le bon port ?               | Vérifier dans WAMP → MySQL → Port utilisé      |
| L'appli est sur un autre poste que MySQL ?   | Vérifier `bind-address` et le pare-feu (§3.3)  |

### Problème : "Aucune donnée dans les tableaux"

- Le script `database.sql` n'a pas été exécuté **en entier** ;
- Vérifiez dans phpMyAdmin que les tables contiennent bien des données.

### Problème : "Le Bureau à distance ne se connecte pas"

| Vérification                              | Solution                                    |
|-------------------------------------------|---------------------------------------------|
| RDP est-il activé ?                       | Paramètres → Système → Bureau à distance    |
| Port 3389 ouvert ?                        | Ajouter une règle entrante dans le pare-feu |
| Les deux machines sont sur le même réseau ?| Vérifier avec `ping 192.168.x.x`           |
| Le compte jury existe ?                   | Créer un utilisateur local                  |

### Problème : "L'exécutable ne se lance pas"

| Vérification                                  | Solution                                    |
|----------------------------------------------|---------------------------------------------|
| .NET Desktop Runtime 10 installé ?           | Télécharger depuis https://dotnet.microsoft.com |
| Vous avez utilisé `--self-contained` ?       | Alors le runtime n'est pas nécessaire       |
| Antivirus bloque l'exe ?                     | Ajouter une exception                       |

### Problème : "Tentatives de connexion bloquées"

- Après 3 tentatives échouées, l'application bloque la connexion ;
- **Solution** : fermez et relancez `M2L_Services.exe`.
