# Bonjour (C# / Avalonia) : pas à pas

Une fenêtre avec une zone de saisie et un bouton **Saluer**.
On écrit son prénom, on clique, et le message `Bonjour, <prénom> !` s'affiche sous le bouton.

Ce dossier contient le **projet terminé et commenté** (le corrigé).
Le guide ci-dessous permet de le refaire soi-même, étape par étape.

---

## Étape 0 : vérifier son installation

Dans un terminal :

```bash
dotnet --version
```

Il faut une version 8 ou plus récente (10 recommandée).
Installez ensuite les modèles de projet Avalonia (une seule fois par ordinateur) :

```bash
dotnet new install Avalonia.Templates
```

## Étape 1 : créer le projet

```bash
dotnet new avalonia.app -o Bonjour
cd Bonjour
dotnet run
```

Une fenêtre « Welcome to Avalonia! » s'ouvre : le projet fonctionne. Fermez-la.

> Le premier `dotnet run` télécharge les paquets Avalonia : cela peut prendre une minute.

## Étape 2 : simplifier le projet

Ouvrez `Bonjour.csproj` et remplacez :

```xml
<Nullable>enable</Nullable>
```

par :

```xml
<Nullable>disable</Nullable>
```

Cela évite d'avoir à écrire des `?` partout dans le code. C'est plus simple pour débuter.

## Étape 3 : dessiner l'interface

Ouvrez `MainWindow.axaml` et remplacez **tout** son contenu par :

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class="Bonjour.MainWindow"
        Title="Bonjour"
        Width="400" Height="260">

    <StackPanel Margin="20" Spacing="12" VerticalAlignment="Center">
        <TextBlock Text="Quel est ton prénom ?" FontSize="16" />
        <TextBox x:Name="ChampPrenom" />
        <Button Content="Saluer" HorizontalAlignment="Center" />
        <TextBlock x:Name="TexteMessage" FontSize="22" HorizontalAlignment="Center" />
    </StackPanel>

</Window>
```

Relancez avec `dotnet run`. La fenêtre affiche le texte, la zone de saisie et le bouton.
Le bouton **ne fait encore rien** : c'est normal.

À retenir :

| Élément | Rôle |
|---|---|
| `StackPanel` | Empile les éléments les uns sous les autres |
| `TextBlock` | Un texte affiché (non modifiable par l'utilisateur) |
| `TextBox` | Une zone où l'utilisateur écrit |
| `Button` | Un bouton cliquable |
| `x:Name="..."` | Donne un nom à l'élément pour l'utiliser dans le code C# |

## Étape 4 : relier le bouton au code

**4a.** Dans `MainWindow.axaml`, ajoutez `Click="BoutonSaluer_Click"` sur le bouton :

```xml
<Button Content="Saluer" Click="BoutonSaluer_Click" HorizontalAlignment="Center" />
```

**4b.** Ouvrez `MainWindow.axaml.cs` et ajoutez cette méthode dans la classe `MainWindow`, sous le constructeur :

```csharp
private void BoutonSaluer_Click(object sender, RoutedEventArgs e)
{
    // 1. On lit le texte écrit dans la zone de saisie
    string prenom = ChampPrenom.Text;

    // 2. On construit le message
    string message = "Bonjour, " + prenom + " !";

    // 3. On l'affiche sous le bouton
    TexteMessage.Text = message;
}
```

**4c.** Tout en haut du fichier, vérifiez que ces deux lignes sont présentes (sinon ajoutez-les) :

```csharp
using Avalonia.Controls;
using Avalonia.Interactivity;
```

`RoutedEventArgs` vient de `Avalonia.Interactivity`.

## Étape 5 : tester

```bash
dotnet run
```

Écrivez un prénom, cliquez sur **Saluer** : `Bonjour, Sofia !` s'affiche sous le bouton. 🎉

---

## Comment ça marche ?

```
MainWindow.axaml                          MainWindow.axaml.cs
─────────────────                         ───────────────────
<TextBox x:Name="ChampPrenom" />    ───►  ChampPrenom.Text      (on lit)
<Button Click="BoutonSaluer_Click"/> ───►  BoutonSaluer_Click()  (appelée au clic)
<TextBlock x:Name="TexteMessage" /> ◄───  TexteMessage.Text = … (on écrit)
```

- Le `.axaml` décrit **ce qu'on voit**, le `.cs` décrit **ce qui se passe**.
- `x:Name` crée un pont : l'élément devient une variable utilisable en C#.
- `Click` crée l'autre pont : le clic appelle la méthode du même nom.

## Erreurs fréquentes

| Message ou symptôme | Cause probable |
|---|---|
| `The name 'ChampPrenom' does not exist` | Le `x:Name` est mal écrit, ou différent dans le `.axaml` et le `.cs` (majuscules comprises) |
| Erreur de compilation qui cite `BoutonSaluer_Click` | Le nom dans `Click="..."` ne correspond pas exactement au nom de la méthode dans le `.cs` |
| `The type or namespace name 'RoutedEventArgs' could not be found` | Il manque `using Avalonia.Interactivity;` en haut du fichier |
| La fenêtre est vide | La ligne `InitializeComponent();` a été supprimée du constructeur |
| `x:Class` introuvable | Le `namespace` du `.cs` ne correspond pas au début de `x:Class` (ici `Bonjour`) |

## Pour aller plus loin

1. **Champ vide** : si rien n'est écrit, afficher « Écris d'abord ton prénom » au lieu de `Bonjour,  !`.
   Indice : `if (string.IsNullOrWhiteSpace(prenom)) { ... } else { ... }`
2. **Espaces en trop** : `"  Sofia  "` devrait donner `Bonjour, Sofia !`. Indice : `.Trim()`
3. **Touche Entrée** : saluer aussi quand on appuie sur Entrée dans la zone de saisie.
   Indice : `<Button IsDefault="True" ... />`
4. **Bouton Effacer** : ajouter un second bouton qui vide la zone de saisie et le message.

## Les fichiers du corrigé

| Fichier | Rôle |
|---|---|
| `MainWindow.axaml` | **L'interface** : texte, zone de saisie, bouton, message |
| `MainWindow.axaml.cs` | **La logique** : ce qui se passe au clic |
| `Program.cs` | Point d'entrée : démarre Avalonia |
| `App.axaml` / `App.axaml.cs` | L'application : le thème et la fenêtre à ouvrir |
| `Bonjour.csproj` | Le projet : la version de .NET et les paquets utilisés |

Pour lancer le corrigé : `cd Bonjour` puis `dotnet run`.
