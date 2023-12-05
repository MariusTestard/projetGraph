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
                Projet leProjet = e.Parameter as Projet;
                lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().getEmployeFromAProject(leProjet.numProjet);
                tblNumProjet.Text = leProjet.numProjet.ToString();
                tblDescription.Text = "Description: " + leProjet.description.ToString();
                tblTitre.Text = "Titre: " + leProjet.titre.ToString();
                tblDateDeb.Text = "Date de début: " + leProjet.dateDeb;
                tblBudget.Text = "Budget: " + leProjet.budget.ToString() + "$";
                tblNbrEmplo.Text = "Nombre d'employés maximum: " + leProjet.nbrEmplo.ToString();
                tblTotSalaireApay.Text = "Salaire à payer total: " + leProjet.totSalaireApay.ToString();
                tblClient.Text = "Le client: " + leProjet.client.ToString();
                if (leProjet.statut)
                    tblStatut.Text = "Le statut: Terminé";
                else
                    tblStatut.Text = "Le statut: En cours";
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var contexte = b.DataContext as Employe;
            int pos = lvListeEmployes.Items.IndexOf(contexte);
        }
    }
}
