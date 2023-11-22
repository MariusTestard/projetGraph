using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
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
    public sealed partial class AfficherClientsPA : Page
    {
        public AfficherClientsPA()
        {
            this.InitializeComponent();
            lvListeClients.ItemsSource = SingletonClient.getInstance().getListeClients();
        }

        private void btnSuppr_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var c = b.Tag.ToString();
            SingletonClient.getInstance().supprimerClients(c);
        }

        private async void lvListeClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModifierClientCD dialog = new ModifierClientCD();
            dialog.XamlRoot = afficherClientsPA.XamlRoot;
            dialog.Title = "Modifier un client";
            dialog.PrimaryButtonText = "Modifier";
            dialog.SecondaryButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            var result = await dialog.ShowAsync();
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterClientCD dialog = new AjouterClientCD();
            dialog.XamlRoot = afficherClientsPA.XamlRoot;
            dialog.Title = "Ajouter un client";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.SecondaryButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Secondary;
            var result = await dialog.ShowAsync();
        }
    }
}
