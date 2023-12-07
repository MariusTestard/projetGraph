using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ZoomProjetPA : Page
    {
        Projet leProjet;
        int pos;
        Employe contexte;
        ObservableCollection<Employe> memoryListAjoutRetir = new ObservableCollection<Employe>();

        public ZoomProjetPA()
        {
            this.InitializeComponent();
            lvListeEmployes.ItemsSource = SingletonEmploye.getInstance().getListeEmployes();
        }
        //(, double budget, int nbrEmplo, double totSalaireApay, int client, bool statut)
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                leProjet = e.Parameter as Projet;
                lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().getEmployeFromAProject(leProjet.numProjet);
                tblNumProjet.Text = leProjet.numProjet.ToString();
                tblDescription.Text = leProjet.description.ToString();
                tblTitre.Text = leProjet.titre.ToString();
                tblDateDeb.Text = leProjet.dateDeb;
                tblBudget.Text = leProjet.budget.ToString();
                tblNbrEmplo.Text = leProjet.nbrEmplo.ToString();
                tblTotSalaireApay.Text = leProjet.totSalaireApay.ToString();
                tblClient.Text = leProjet.client.ToString();
                if (leProjet.statut)
                    tblStatut.Text = "Le statut: Termin�";
                else
                    tblStatut.Text = "Le statut: En cours";
            }
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var c = b.Tag.ToString();
            contexte = b.DataContext as Employe;
            pos = lvListeEmployes.Items.IndexOf(contexte);
            AjouterEmploAProjetCD dialog = new AjouterEmploAProjetCD();
                dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
                dialog.Title = "Pr�cision";
                dialog.PrimaryButtonText = "Confirmer";
                dialog.SecondaryButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Secondary;
                var result = await dialog.ShowAsync();
            SingletonEmploye.getInstance().ajoutEmpProjet(leProjet.numProjet, c, SingletonEmploye.getInstance().NbHeure);
            lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().ListeEmployeProjet;
            tblNbrEmplo.Text = int.Parse(tblNbrEmplo.Text) + 1 + "";
            SingletonEmploye.getInstance().ListeEmploye.RemoveAt(pos);
            memoryListAjoutRetir.Add(contexte);
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var c = b.Tag.ToString();
            SingletonEmploye.getInstance().retirerEmpProjet(leProjet.numProjet, c);
            lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().ListeEmployeProjet;
            tblNbrEmplo.Text = int.Parse(tblNbrEmplo.Text) - 1 + "";
           // SingletonEmploye.getInstance().ListeEmploye.Add(memoryListAjoutRetir.IndexOf(contexte);
            // RETIRER �GALEMENT LA PARTIE DU TOTAL SALAIRE � PAYER PUISQUE L'EMPLOY� N'EST PLUS SUR LE PROJET
        }
    }
}
