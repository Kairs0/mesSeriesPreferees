# Mes séries préférées
Plateforme : Windows 10

## Api choisie:
http://api.tvmaze.com/

## Lors du premier git clone:
- Ouvrir la solution avec Visual Studio (Series > Series.sln)
- Dans l'onglet "Solution Platform" (habituellement situé à gauche de la commande Run), selectionner (x64 ou x86)
- Dans l'explorateur de solution : click droit sur la solution -> Rebuild Solution
- F5 (run)

## Pour tester les notifications
- S'assurer que le mode "Ne pas déranger" n'est pas activé dans l'explorer de notifications de Windows
- Dans le fichier NotificationManager.cs : décommenter la ligne 23, commenter la ligne 28 -> Permet d'enovoyer les notifications toutes les 30 secondes au lieu des 10 minutes pour le mode production
- Depuis l'application en mode run, ajouter une (des) série(s) à l'affiche dans ses favoris 

<!-- ## Notifications
[Docs Microsoft](https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/tiles-and-notifications-send-local-toast) -->


<!-- ## Problèmes de références System lors d'un pull:
[StackOverFlow](https://stackoverflow.com/questions/32607616/visual-studio-2015-c-sharp-windows-universal-app-missing-assembly-reference/32607617#32607617)
###### en résumé : uploader ou désintall/réinstall la dépendance Microsoft.NETCore.UniversalWindowsPlatform depuis le Nuget manager) -->


<!-- ## Switch de System.Net.Request à HttpClientAPI
- [StackOverFlow](https://stackoverflow.com/a/38871200)
- [Windows.Web.Http.HttpClient vs System.Net.Http.HttpClient](https://stackoverflow.com/questions/31291008/system-net-http-httpclient-vs-windows-web-http-httpclient-what-are-the-main-di)
- [Demystifying HttpClient APIs in the Universal Windows Platform](https://blogs.windows.com/buildingapps/2015/11/23/demystifying-httpclient-apis-in-the-universal-windows-platform/) -->