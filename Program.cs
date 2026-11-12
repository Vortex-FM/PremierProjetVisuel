// =====================================================================
//  Program.cs : le POINT D'ENTRÉE du programme
// ---------------------------------------------------------------------
//  Quand on tape "dotnet run", c'est la méthode Main ci-dessous qui
//  s'exécute en premier. Son seul rôle : démarrer Avalonia.
//  En général, on ne modifie jamais ce fichier.
// =====================================================================

// "using" = on indique quelles bibliothèques on utilise dans ce fichier.
using System;
using Avalonia; 

// Le "namespace" est comme un dossier qui regroupe toutes nos classes.
namespace Bonjour;

class Program
{
    // [STAThread] est une obligation technique pour les applications
    // graphiques (surtout sous Windows). Il faut le laisser.
    [STAThread]
    public static void Main(string[] args)
    {
        // On prépare Avalonia, puis on lance l'application de bureau.
        // Le programme reste "bloqué" sur cette ligne tant que la
        // fenêtre est ouverte. Fermer la fenêtre = fin du programme.
        AppBuilder builder = BuildAvaloniaApp();
        builder.StartWithClassicDesktopLifetime(args);
    }

    // Configure Avalonia. Cette méthode doit garder ce nom exact,
    // car l'aperçu (previewer) de l'IDE l'utilise aussi.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>() // notre classe App (voir App.axaml)
            .UsePlatformDetect()           // détecte le système : Windows, macOS ou Linux
            .WithInterFont()               // utilise la police "Inter"
            .LogToTrace();                 // affiche les messages d'erreur d'Avalonia
    }
}
