// =====================================================================
//  App.axaml.cs : le code C# associé à App.axaml
// ---------------------------------------------------------------------
//  Son rôle principal : dire quelle fenêtre ouvrir au démarrage.
// =====================================================================

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Bonjour;

// "partial" veut dire que la classe est écrite en deux morceaux :
// une partie ici, et une autre générée automatiquement à partir de
// App.axaml. "App : Application" = App est une sorte d'Application.
public partial class App : Application
{
    // Appelée au tout début : charge le contenu de App.axaml (le thème).
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // Appelée quand Avalonia est prêt : on crée la fenêtre principale.
    public override void OnFrameworkInitializationCompleted()
    {
        // On vérifie qu'on est bien dans une application de bureau
        // (Avalonia peut aussi tourner sur mobile ou dans un navigateur).
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // La fenêtre qui s'ouvre au démarrage : notre fenêtre "Bonjour".
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
