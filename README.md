# Mes séries préférées

### SVP ne pas pusher un code qui ne compile pas en local

## Api choisie:
http://api.tvmaze.com/

## Modèles
- Pour l'instant: générés depuis le format json des réponses aux requetes vers api.TvMaze (copier json, créer une nouvelle classe .cs, collage spécial (depuis menu edit) -> coller json comme une classe (paste JSON as Class)
- Tous les champs ne sont pas forcéments nécessaires

## Interface graphique:
(Arnaud) mon idée est d'afficher lors du démarrage de l'appli les deux boutons + une liste d'images des séries récentes. Lors du click sur favoris, les icones des séries favorites de l'utilisateur sont displayées, lors du click sur la recherche le champ recherche est déployé. 

## Modèle de branchement avec l'API
à voir

## Problèmes de références System lors d'un pull:
[StackOverFlow](https://stackoverflow.com/questions/32607616/visual-studio-2015-c-sharp-windows-universal-app-missing-assembly-reference/32607617#32607617)
###### en résumé : uploader ou désintall/réinstall la dépendance Microsoft.NETCore.UniversalWindowsPlatform depuis le Nuget manager)

## Problèmes git pull
[StackOverflow](https://stackoverflow.com/questions/1125968/how-do-i-force-git-pull-to-overwrite-local-files)

## Switch de System.Net.Request à HttpClientAPI
- [StackOverFlow](https://stackoverflow.com/a/38871200)
- [Windows.Web.Http.HttpClient vs System.Net.Http.HttpClient](https://stackoverflow.com/questions/31291008/system-net-http-httpclient-vs-windows-web-http-httpclient-what-are-the-main-di)
- [Demystifying HttpClient APIs in the Universal Windows Platform](https://blogs.windows.com/buildingapps/2015/11/23/demystifying-httpclient-apis-in-the-universal-windows-platform/)