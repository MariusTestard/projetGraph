using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MySql.Data.MySqlClient;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherEmployesPA : Page
    {
        public AfficherEmployesPA()
        {
            this.InitializeComponent();
            SingletonEmploye.getInstance().getListeEmployes().Clear();
            lvListeEmployes.ItemsSource = SingletonEmploye.getInstance().getListeEmployes();

            if (SingletonAdmin.getInstance().LoginAdmin())
            {
                btnAjouter.Visibility = Visibility.Visible;
            }
            else
            {
                btnAjouter.Visibility = Visibility.Collapsed;
            }
            SingletonAdmin.getInstance().Bt = btnAjouter;
        }

        private async void btnTogglePermanent_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonAdmin.getInstance().LoginAdmin())
            {
                Button b = (Button)sender;
                var contexte = b.DataContext as Employe;
                int pos = lvListeEmployes.Items.IndexOf(contexte);

            
                Employe emp = new Employe { 
                    Nom = SingletonEmploye.getInstance().ListeEmploye[pos].Nom,
                    Prenom = SingletonEmploye.getInstance().ListeEmploye[pos].Prenom,
                    DateEmbauche = SingletonEmploye.getInstance().ListeEmploye[pos].DateEmbauche,
                    DateNaissance = SingletonEmploye.getInstance().ListeEmploye[pos].DateNaissance,
                    Matricule = SingletonEmploye.getInstance().ListeEmploye[pos].Matricule,
                    Email = SingletonEmploye.getInstance().ListeEmploye[pos].Email,
                    Photo = SingletonEmploye.getInstance().ListeEmploye[pos].Photo,
                    TauxHoraire = SingletonEmploye.getInstance().ListeEmploye[pos].TauxHoraire,
                    Adresse = SingletonEmploye.getInstance().ListeEmploye[pos].Adresse,
                    Statut = true
                };
                emp.Statut = true;
                try
                {
                    SingletonEmploye.getInstance().changeStatusFromEmploye(emp, pos);
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine(ex);
                    ErreurCD dialog = new ErreurCD();
                    dialog.SetIndex("moisAncien3");
                    dialog.XamlRoot = afficherEmployePA.XamlRoot;
                    dialog.Title = "Erreur";
                    dialog.PrimaryButtonText = "OK";
                    dialog.DefaultButton = ContentDialogButton.Primary;
                    var result = await dialog.ShowAsync();
                }
            }
            else
                notAdmin();
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterEmployeCD dialog = new AjouterEmployeCD();
            dialog.XamlRoot = afficherEmployePA.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.SecondaryButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                ajoutSucces();
        }

        private async void ajoutSucces()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("ajoutEmpSucc");
            dialog.XamlRoot = afficherEmployePA.XamlRoot;
            dialog.Title = "Succès";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void lvListeEmployes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvListeEmployes.SelectedIndex != -1 && SingletonAdmin.getInstance().LoginAdmin()) {
                ModifierEmployeCD dialog = new ModifierEmployeCD();
                dialog.setEmploye(lvListeEmployes.SelectedItem as Employe);
                dialog.XamlRoot = afficherEmployePA.XamlRoot;
                dialog.Title = "Modifier un employé";
                dialog.PrimaryButtonText = "Modifier";
                dialog.SecondaryButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Secondary;
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                    modifSucces();
            }
        }

        private async void modifSucces()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("modifEmpSucc");
            dialog.XamlRoot = afficherEmployePA.XamlRoot;
            dialog.Title = "Succès";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void notAdmin()
        {
            ErreurCD dialog = new ErreurCD();
            dialog.SetIndex("notAdmin");
            dialog.XamlRoot = afficherEmployePA.XamlRoot;
            dialog.Title = "Erreur";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

    }
}
