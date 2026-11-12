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
using System.Linq;
using System.Xml.Serialization;
using Avalonia.Controls;
using Avalonia.DesignerSupport.Remote;
using Avalonia.Interactivity;
using Avalonia.Metadata;
using Avalonia.Input;
using Avalonia.Media;


namespace Bonjour;
public class TacheItem : INotifyPropertyChanged
{
    private bool _estFaite;

    public string Texte { get; set; } = "";

    public bool EstFaite
    {
        get => _estFaite;
        set
        {
            if (_estFaite == value) return;
            _estFaite = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EstFaite)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

public partial class MainWindow : Window
{
    private bool _MiseAJoursEnCours = false;
    private readonly ObservableCollection<TacheItem> _taches = new();
    public MainWindow()
    {
        InitializeComponent();
        ToDoList.ItemsSource = _taches;
        degreeC.PropertyChanged += DegreeCPropertyChanged;
        degreeF.PropertyChanged += DegreeFPropertyChanged;
        Nombre1.PropertyChanged += CalculPropertyChanged;
        Nombre2.PropertyChanged += CalculPropertyChanged;
        Operateur.PropertyChanged += CalculPropertyChanged;

    }
    public void ConversionCeliusFarenheit()
    {
        string? CelsiusValue = degreeC.Text;
        if (double.TryParse(CelsiusValue, out double c))
        {
            double FarenheitValue = c * 9 / 5 + 32;
            degreeF.Text = FarenheitValue.ToString("F2");
        }
        else
        {
            degreeF.Text = "";
        }
    }
    public void ConversionFarenheitCelsius()
    {
        string? FarenheitValue = degreeF.Text;
        if (double.TryParse(FarenheitValue, out double f))
        {
            double CelsiusValue = (f - 32) / 9 * 5;
            degreeC.Text = CelsiusValue.ToString("F2");
        }
        else
        {
            degreeC.Text = "";
        }
    }
    private void DegreeCPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != TextBox.TextProperty) return;
        if (_MiseAJoursEnCours) return;
        _MiseAJoursEnCours = true;
        try
        {
            ConversionCeliusFarenheit();
        }
        finally
        {
            _MiseAJoursEnCours = false;
        }

    }
    private void DegreeFPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != TextBox.TextProperty) return;
        if (_MiseAJoursEnCours) return;
        _MiseAJoursEnCours = true;
        try
        {
            ConversionFarenheitCelsius();
        }
        finally
        {
            _MiseAJoursEnCours = false;
        }
    }
    private void BoutonSaluer_Click(object sender, RoutedEventArgs e)
    {
        string? prenom = ChampPrenom.Text;
        string? Nom = ChampNom.Text;
        DateTimeOffset? naissance = ChampDateNaissance.SelectedDate;


        string message;
        if (string.IsNullOrEmpty(prenom) || prenom.Length < 3 || prenom.Length > 30 || string.IsNullOrEmpty(Nom) || Nom.Length < 2 || Nom.Length > 30 || int.TryParse(prenom, out int i) || int.TryParse(Nom, out int o))
        {
            TexteMessage.Foreground = Avalonia.Media.Brushes.Red;
            message = $"Bonjour, votre nom ou Prénom est invalide.(entre 3 et 30 caractères)";
        }
        else if (naissance is null || naissance.Value.Date > DateTime.Today)
        {
            TexteMessage.Foreground = Avalonia.Media.Brushes.Red;
            message = $"Bonjour, votre année de naissance n'est pas valide.";
        }
        else
        {
            TexteMessage.Foreground = Avalonia.Media.Brushes.Black;
            DateTime datenaissance = naissance.Value.Date;
            DateTime today = DateTime.Today;
            int age = today.Year - datenaissance.Year;
            if (datenaissance.AddYears(age) > today)
            {
                age--;
            }

            if (age <= 5 || age >= 125)
            {
                TexteMessage.Foreground = Avalonia.Media.Brushes.Red;
                message = $"Bonjour, votre âge n'est pas valide (<5 ou >125)";
            }
            else
            {
                message = $"Bonjour, {prenom} {Nom}, vous avez {age} ans.";
            }
        }
        TexteMessage.Text = message;


    }
    private void CalculPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property != TextBox.TextProperty) return;
        Calculatrice();
    }
    public void Calculatrice()
    {
        if (Nombre1 == null || Nombre2 == null || Operateur == null) return;
        if (double.TryParse(Nombre1.Text, out double nb1) && double.TryParse(Nombre2.Text, out double nb2))
        {
            var op = Operateur.Text;
            switch (op)
            {
                case "+":
                    Resultat.Text = (nb1 + nb2).ToString();
                    break;
                case "-":
                    Resultat.Text = (nb1 - nb2).ToString();
                    break;
                case "*":
                    Resultat.Text = (nb1 * nb2).ToString();
                    break;
                case "/":
                    Resultat.Text = Math.Abs(nb2) < double.Epsilon ? "Erreur" : (nb1 / nb2).ToString();
                    break;
                default:
                    Resultat.Text = "";
                    break;
            }
        }
    }
    private void ToDoAdd_Click(object? sender, RoutedEventArgs e) => ToDoAdd();
    private void ToDo_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) ToDoAdd();
    }
    public void ToDoAdd()
    {
        string texte = ToDo.Text?.Trim() ?? "";
        if (texte.Length == 0) return;

        _taches.Add(new TacheItem { Texte = texte });
        ToDo.Text = "";
    }
    private void ToDoDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: TacheItem tache })
            _taches.Remove(tache);
    }
    private void BoutonAffiche_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: string cible }) return;
        
        PanelSalutations.IsVisible = cible == "Salutations" ? true : false;
        PanelConversion.IsVisible = cible == "Conversion"? true : false;
        PanelCalculatrice.IsVisible = cible == "Calc"? true : false;
        PanelToDo.IsVisible = cible == "ToDo" ? true : false;
        
    }
}

