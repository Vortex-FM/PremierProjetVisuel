// =====================================================================
//  MainWindow.axaml.cs : le CODE de la fenêtre (ce qui se passe)
// ---------------------------------------------------------------------
//  On appelle ce fichier le "code-behind" : c'est le code "derrière"
//  l'interface décrite dans MainWindow.axaml.
//
//  "ChampPrenom" et "TexteMessage" sont les éléments qui ont un
//  x:Name dans le .axaml. Grâce à ce nom, on peut les utiliser
//  directement ici, comme des variables.
// =====================================================================

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using Avalonia.Controls;
using Avalonia.DesignerSupport.Remote;
using Avalonia.Interactivity;
using Avalonia.Metadata;

namespace Bonjour;

public partial class MainWindow : Window
{
    // LE CONSTRUCTEUR : appelé une seule fois, à l'ouverture de la fenêtre.
    public MainWindow()
    {
        // OBLIGATOIRE : crée les éléments décrits dans le .axaml.
        // Sans cette ligne, la fenêtre reste vide !
        InitializeComponent();
    }
    public void ConversionCeliusFarenheit()
    {
        
    }
    
    private void BoutonSaluer_Click(object sender, RoutedEventArgs e)
    {
        // 1. On LIT le texte écrit dans la zone de saisie.
        string? prenom = ChampPrenom.Text;
        string? Nom = ChampNom.Text;
        DateTimeOffset? naissance = ChampDateNaissance.SelectedDate;
        
        
        string message;
        if (string.IsNullOrEmpty(prenom) || prenom.Length <2 || prenom.Length > 30 || string.IsNullOrEmpty(Nom) || Nom.Length < 2 || Nom.Length > 30 || int.TryParse(prenom,out int i) || int.TryParse(Nom,out int o))
        {
            Foreground = Avalonia.Media.Brushes.Red;
            message = $"Bonjour, votre nom ou Prénom est invalide.(entre 3 et 30 caractères)";
        }
        else if (naissance is null || naissance.Value.Date > DateTime.Today)
        {
            Foreground = Avalonia.Media.Brushes.Red;
            message = $"Bonjour, votre année de naissance n'est pas valide.";
        }
        else
        {
            Foreground = Avalonia.Media.Brushes.Black;
            DateTime datenaissance = naissance.Value.Date;
            DateTime today = DateTime.Today;
            int age = today.Year - datenaissance.Year;
            if(datenaissance.AddYears(age) > today)
            {
                age--;
            }
            message = $"Bonjour, {prenom} {Nom}, vous avez {age} ans.";
        }
        TexteMessage.Text = message;
    }
}
