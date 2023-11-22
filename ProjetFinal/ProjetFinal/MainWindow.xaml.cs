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
            mainFrame.Navigate(typeof(AfficherProjetsPA));
        }

        private async void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedNavItem = args.SelectedItem as NavigationViewItem;

            if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "Options")
            {
                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ListeProjet")
            {
                mainFrame.Navigate(typeof(AfficherProjetsPA));
                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "AddProjet")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ModifyProjet")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "RemoveProjet")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ListeClient")
            {
                mainFrame.Navigate(typeof(AfficherClientsPA));
                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "AddClient")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ModifyClient")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "RemoveClient")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ListeEmplo")
            {
                mainFrame.Navigate(typeof(AfficherEmployesPA));
                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "AddEmplo")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "ModifyEmplo")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "RemoveEmplo")
            {

            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "SaveFile")
            {
                var picker = new Windows.Storage.Pickers.FileSavePicker();

                var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
                WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

                picker.SuggestedFileName = "Projets";
                picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

                //cr�e le fichier
                Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

                if (monFichier != null)
                {
                    List<Projet> liste = new List<Projet>(SingletonProjet.getInstance().getListeProjets());

                    await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToStringWrite()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
                }

                navView.SelectedItem = null;
            }
            else if (selectedNavItem != null && selectedNavItem.Tag.ToString() == "Admin")
            {
                LoginAdminCD dialog = new LoginAdminCD();
                dialog.XamlRoot = testgrid.XamlRoot;
                dialog.Title = "Authentification";
                dialog.PrimaryButtonText = "Se connecter";
                dialog.CloseButtonText = "Annuler";


                ContentDialogResult resultat = await dialog.ShowAsync();
                selectedNavItem = null;
                if (resultat == ContentDialogResult.Primary)
                    SingletonAdmin.getInstance().connexionAdmin(dialog.Username, dialog.Password);
                navView.SelectedItem = null;
            }
        }

    }
}
