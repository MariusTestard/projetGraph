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

                //btnSuppr.Visibility = Visibility.Visible;
                
            }
            else
            {

                //btnSuppr.Visibility = Visibility.Collapsed;
            }
        }

        private void btnTogglePermanent_Click(object sender, RoutedEventArgs e)
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
            
            SingletonEmploye.getInstance().changeStatusFromEmploye(emp, pos);
            //SingletonEmploye.getInstance().swap(emp, pos);
            //b.Visibility = Visibility.Collapsed;
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterClientCD dialog = new AjouterClientCD();
            dialog.XamlRoot = afficherEmployePA.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.SecondaryButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            var result = await dialog.ShowAsync();
        }
    }
}
