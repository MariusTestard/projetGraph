using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Common;
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
        List<int> matriculeMatchRM = new List<int>();
        int pos;

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
                tblNumProjet.Text = leProjet.numProjet.ToString();
                tblDescription.Text = leProjet.description.ToString();
                tblTitre.Text = leProjet.titre.ToString();
                tblDateDeb.Text = leProjet.dateDeb;
                tblBudget.Text = leProjet.budget.ToString();
                tblNbrEmplo.Text = leProjet.nbrEmplo.ToString();
                tblTotSalaireApay.Text = leProjet.totSalaireApay.ToString();
                tblClient.Text = SingletonProjet.getInstance().getNomClient(leProjet.numProjet);
                tblIdClient.Text = leProjet.client.ToString();
                if (leProjet.statut)
                    tblStatut.Text = "Le statut: Terminé";
                else
                    tblStatut.Text = "Le statut: En cours";
                lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().getEmployeFromAProject(leProjet.numProjet);
                foreach (EmployeProjet emp in SingletonEmploye.getInstance().ListeEmployeProjet)
                {
                    Employe empCast = new Employe(
                        emp.Matricule,
                        emp.Nom,
                        emp.Prenom,
                        emp.DateNaissance,
                        emp.Email,
                        emp.Adresse,
                        emp.DateEmbauche,
                        emp.TauxHoraire,
                        emp.Photo,
                        emp.Statut);
                    foreach (Employe realEmp in SingletonEmploye.getInstance().ListeEmploye)
                    {
                        if (empCast.Matricule.Equals(realEmp.Matricule))
                        {
                            int pos = SingletonEmploye.getInstance().ListeEmploye.IndexOf(realEmp);
                            matriculeMatchRM.Add(pos);
                            Debug.WriteLine("Index has been saved");
                        }
                    }
                }
                for (int i = 0; i < matriculeMatchRM.Count; i++)
                {
                    SingletonEmploye.getInstance().ListeEmploye.RemoveAt(matriculeMatchRM[i]);
                }
            }
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonAdmin.getInstance().LoginAdmin())
            {
                Button b = (Button)sender;
                var c = b.Tag.ToString();
                var contexte = b.DataContext as Employe;
                pos = lvListeEmployes.Items.IndexOf(contexte);
                AjouterEmploAProjetCD dialog = new AjouterEmploAProjetCD();
                    dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
                    dialog.Title = "Précision";
                    dialog.PrimaryButtonText = "Confirmer";
                    dialog.SecondaryButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Secondary;
                    var result = await dialog.ShowAsync();
                    if (result.Equals("Primary"))
                        ajoutSuccess();
                try
                {
                    SingletonEmploye.getInstance().ajoutEmpProjet(leProjet.numProjet, c, SingletonEmploye.getInstance().NbHeure);
                    lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().ListeEmployeProjet;
                    SingletonEmploye.getInstance().ListeEmploye.Remove(contexte);
                    tblNbrEmplo.Text = int.Parse(tblNbrEmplo.Text) + 1 + "";
                }
                catch (Exception ex)
                {
                    ErreurCD dialog1 = new ErreurCD();
                    dialog1.SetIndex("empDejAffili");
                    dialog1.XamlRoot = ajouterEmploProjet.XamlRoot;
                    dialog1.Title = "Erreur";
                    dialog1.PrimaryButtonText = "OK";
                    dialog1.DefaultButton = ContentDialogButton.Primary;
                    var result1 = await dialog1.ShowAsync();
                }
            }
        }

        private void btnRetirer_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonAdmin.getInstance().LoginAdmin())
            {
                Button b = (Button)sender;
                var c = b.Tag.ToString();
                var contexte = b.DataContext as Employe;
                SingletonEmploye.getInstance().retirerEmpProjet(leProjet.numProjet, c);
                lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().ListeEmployeProjet;
                SingletonEmploye.getInstance().ListeEmploye.Add(contexte);
                tblNbrEmplo.Text = int.Parse(tblNbrEmplo.Text) - 1 + "";
                    supressSuccess();
                // RETIRER ÉGALEMENT LA PARTIE DU TOTAL SALAIRE À PAYER PUISQUE L'EMPLOYÉ N'EST PLUS SUR LE PROJET
            }
        }

        private async void ajoutSuccess()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("ajoutEmpProjSucc");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Succès";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void supressSuccess()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("retirEmpProjSucc");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Succès";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

    }
}
