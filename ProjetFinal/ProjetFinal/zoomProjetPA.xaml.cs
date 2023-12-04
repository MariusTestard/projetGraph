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
    public sealed partial class ZoomProjetPA : Page
    {
        public ZoomProjetPA()
        {
            this.InitializeComponent();
        }
        //(, double budget, int nbrEmplo, double totSalaireApay, int client, bool statut)
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                Projet leProjet = e.Parameter as Projet;
                
                tblNumProjet.Text = leProjet.numProjet.ToString();
                tblDescription.Text = leProjet.description.ToString();
                tblTitre.Text = leProjet.titre.ToString();
                tblDateDeb.Text = leProjet.dateDeb;
                tblBudget.Text = leProjet.budget.ToString();
                tblNbrEmplo.Text = leProjet.nbrEmplo.ToString();
                tblTotSalaireApay.Text = leProjet.totSalaireApay.ToString();
                tblClient.Text = leProjet.client.ToString();
                tblStatut.Text = leProjet.statut.ToString();


                SingletonProjet.getInstance().getEmployesDansProjet(leProjet.numProjet.ToString());
                tbltest1.Text = SingletonEmploye.getInstance().ListeEmployeProjet[0].Matricule;
                tbltest2.Text = SingletonEmploye.getInstance().ListeEmployeProjet[0].Nom;
                tbltest3.Text = SingletonEmploye.getInstance().ListeEmployeProjet[0].Prenom;
            }

        }
    }
}
