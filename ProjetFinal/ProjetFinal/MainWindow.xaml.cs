using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            if (SingletonAdmin.getInstance().firstTimeAdmin())
            {
                SingletonAdmin.getInstance().NavView = navView;
                navView.OpenPaneLength = 50;
                mainFrame.Navigate(typeof(CreerAdminPA));
            }  
            else
            {
                mainFrame.Navigate(typeof(AfficherProjetsPA));
            } 
        }

        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedNavItem = args.SelectedItem as NavigationViewItem;
            if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "listeClients" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {
                mainFrame.Navigate(typeof(AfficherClientsPA));
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "listeEmployes" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {
                mainFrame.Navigate(typeof(AfficherEmployesPA));
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "listeProjets" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {
                mainFrame.Navigate(typeof(AfficherProjetsPA));
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "SaveFile" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();

                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

                picker.SuggestedFileName = "Projets";
                picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

                //crée le fichier
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                if (monFichier != null)
                {
                    List<Projet> liste = new List<Projet>(SingletonProjet.getInstance().getListeProjets());

                    await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToStringWrite()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
                }

                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "Admin" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {
                LoginAdminCD dialog = new LoginAdminCD();
                dialog.XamlRoot = testgrid.XamlRoot;
                dialog.Title = "Authentification";
                dialog.PrimaryButtonText = "Se connecter";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;
                ContentDialogResult resultat = await dialog.ShowAsync();
                if (resultat == ContentDialogResult.Primary)
                {
                    selectedNavItem.Tag = "Connecté";
                    selectedNavItem.Content = "Déconnexion";
                }
                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "Connecté" && !SingletonAdmin.getInstance().firstTimeAdmin())
            {

                SingletonAdmin.getInstance().deconnexionAdmin();
                selectedNavItem.Tag = "Admin";
                selectedNavItem.Content = "Compte Administrateur";
                navView.SelectedItem = null;
                mainFrame.Navigate(mainFrame.CurrentSourcePageType);
            }
        }

    }
}
