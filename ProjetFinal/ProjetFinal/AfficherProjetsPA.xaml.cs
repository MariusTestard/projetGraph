using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherProjetsPA : Page
    {

        public AfficherProjetsPA()
        {
            this.InitializeComponent();
            SingletonProjet.getInstance().getListeProjets().Clear();
            lvListeProjets.ItemsSource = SingletonProjet.getInstance().getListeProjetsEnCours();
            SingletonAdmin.getInstance().NavView.OpenPaneLength = 220;
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

        private void btnEnCours_Click(object sender, RoutedEventArgs e)
        {
            SingletonProjet.getInstance().getListeProjets().Clear();
            lvListeProjets.ItemsSource = SingletonProjet.getInstance().getListeProjetsEnCours();
        }

        private void btnTerminer_Click(object sender, RoutedEventArgs e)
        {
            SingletonProjet.getInstance().getListeProjets().Clear();
            lvListeProjets.ItemsSource = SingletonProjet.getInstance().getListeProjetsTermine();
        }

        private void lvListeProjets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projet unProjet = lvListeProjets.SelectedItem as Projet;
            Frame.Navigate(typeof(ZoomProjetPA), unProjet);
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterProjetCD dialog = new AjouterProjetCD();
            dialog.XamlRoot = afficherProjetPA.XamlRoot;
            dialog.Title = "Ajouter un projet";
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
            dialog.SetIndex("ajoutProjSucc");
            dialog.XamlRoot = afficherProjetPA.XamlRoot;
            dialog.Title = "Succès";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

    }
}
