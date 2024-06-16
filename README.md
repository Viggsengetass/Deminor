
Démineur
Bienvenue au jeu de Démineur ! Ce projet est une implémentation console du célèbre jeu de démineur. Vous pouvez jouer à différents niveaux de difficulté, sauvegarder votre progression et charger des parties sauvegardées.

Fonctionnalités
Nouvelle Partie : Commencez une nouvelle partie en choisissant votre niveau de difficulté.
Charger une Partie Sauvegardée : Reprenez une partie sauvegardée en entrant le nom de la sauvegarde.
Voir le Leaderboard : Consultez les scores des meilleurs joueurs classés par niveau de difficulté.
Quitter : Quittez le jeu.
Niveaux de Difficulté
Facile : Grille de 10x10
Moyen : Grille de 20x20
Difficile : Grille de 26x26
Comment Jouer
Lancer une Nouvelle Partie
Choisissez l'option "Nouvelle partie" dans le menu principal.
Sélectionnez le niveau de difficulté : Facile, Moyen ou Difficile.
Une grille de jeu s'affiche avec toutes les cases masquées.
Sélectionner une Case
Entrez une case dans le format A1 pour révéler la case à la ligne A et colonne 1.
Si la case contient une mine, vous perdez et toutes les mines sont révélées.
Si la case ne contient pas de mine, elle affiche le nombre de mines environnantes.
Sauvegarder et Charger une Partie
Sauvegarder : Tapez SAVE et entrez un nom pour sauvegarder votre progression.
Charger : Choisissez l'option "Charger une partie sauvegardée" dans le menu principal et entrez le nom de la sauvegarde que vous souhaitez charger.
Gagner ou Perdre
Gagner : Vous gagnez si vous révélez toutes les cases qui ne contiennent pas de mines.
Perdre : Vous perdez si vous révélez une mine.
Options Supplémentaires
Déclarer Victoire : Tapez WIN pour déclarer une victoire (pour tester le leaderboard).
Retourner au Menu Principal : Tapez MENU pour retourner au menu principal.
Perdre la Partie : Tapez LOOSE pour perdre la partie volontairement et voir l'emplacement des mines.
Leaderboard
Le leaderboard affiche les meilleurs scores des joueurs triés par niveau de difficulté. Les scores sont enregistrés avec le nom du joueur et la durée de la partie.

Installation et Exécution
Clonez le dépôt : git clone <URL_du_dépôt>
Ouvrez le projet dans votre IDE préféré (par exemple, JetBrains Rider, Visual Studio).
Assurez-vous que vous avez le SDK .NET installé.
Exécutez le projet en démarrant le fichier Program.cs.
Structure du Projet
Models : Contient les modèles de données utilisés dans le jeu (ex: GameSaveDataModel.cs, LeaderboardEntryModel.cs).
Services : Contient les services qui gèrent les différentes fonctionnalités du jeu (ex: GameService.cs, GridService.cs, InputService.cs, MenuService.cs, MineService.cs, SaveService.cs).
assets : Contient des fichiers textes pour la configuration (ex: difficulty.txt, instructions.txt, tailleGrille.txt).

Merci d'avoir joué au Démineur ! Amusez-vous bien et n'oubliez pas de partager vos scores !

@Made by Paul Antoine.
