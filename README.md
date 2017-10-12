# Mes séries préférées

### SVP ne pas pusher un code qui ne compile pas en local

## Api choisie:
http://api.tvmaze.com/

## Modèles
- Pour l'instant: générés depuis le format json des réponses aux requetes vers api.TvMaze (copier json, créer une nouvelle classe .cs, collage spécial (depuis menu edit) -> coller json comme une classe (paste JSON as Class)
- Tous les champs ne sont pas forcéments nécessaires

## Interface graphique:
- chosir structure objets (POO)
-- lister objet : Réalisateurs, Acteurs .. -> Utiliser les modèles de API Maze
--relations entre objets
- interface graphique : mon idée est d'afficher lors du démarrage de l'appli les deux boutons + une liste d'images des séries récentes. Lors du click sur favoris, les icones des séries favorites de l'utilisateur sont displayées, lors du click sur la recherche le champ recherche est déployé. 
- relation avec l'api: à voir

## Problèmes de références System lors d'un pull:
[StackOverFlow](https://stackoverflow.com/questions/32607616/visual-studio-2015-c-sharp-windows-universal-app-missing-assembly-reference/32607617#32607617)
###### en résumé : uploader ou désintall/réinstall la dépendance Microsoft.NETCore.UniversalWindowsPlatform depuis le Nuget manager)