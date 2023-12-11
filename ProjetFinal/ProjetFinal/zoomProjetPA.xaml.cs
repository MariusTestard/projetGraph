using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;
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
            SingletonAdmin.getInstance().pageInstance = this.GetType();
            SingletonAdmin.getInstance().codeReference = 1;
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
                    tblStatut.Text = "Termin�";
                else
                    tblStatut.Text = "En cours";
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
                dialog.Title = "Pr�cision";
                dialog.PrimaryButtonText = "Confirmer";
                dialog.SecondaryButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Secondary;
                var result = await dialog.ShowAsync();
                Debug.WriteLine(result);
                if (result == ContentDialogResult.Primary)
                {
                    try
                    {
                        SingletonEmploye.getInstance().ajoutEmpProjet(leProjet.numProjet, c, SingletonEmploye.getInstance().NbHeure);
                        lvEmployesProjet.ItemsSource = SingletonEmploye.getInstance().ListeEmployeProjet;
                        SingletonEmploye.getInstance().ListeEmploye.Remove(contexte);
                        tblNbrEmplo.Text = int.Parse(tblNbrEmplo.Text) + 1 + "";
                        tblTotSalaireApay.Text = SingletonProjet.getInstance().getTotSalaire(leProjet.numProjet);
                        ajoutSuccess();
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Message.Equals("En ajoutant cet employ�, le budget se retrouve d�pass�."))
                        {
                            excedingBudget();
                        }
                        else
                        {
                            alreadyAffiliated();
                        }
                    }
                }
            }
            else
                notAdmin();
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
                tblTotSalaireApay.Text = SingletonProjet.getInstance().getTotSalaire(leProjet.numProjet);
                supressSuccess();
            }
            else
                notAdmin();
        }

        private async void ajoutSuccess()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("ajoutEmpProjSucc");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Succ�s";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void supressSuccess()
        {
            SuccessCD dialog = new SuccessCD();
            dialog.SetIndex("retirEmpProjSucc");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Succ�s";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AfficherProjetsPA));
        }

        private async void notAdmin()
        {
            ErreurCD dialog = new ErreurCD();
            dialog.SetIndex("notAdmin");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Erreur";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void alreadyAffiliated()
        {
            ErreurCD dialog = new ErreurCD();
            dialog.SetIndex("empDejAffili");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Erreur";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

        private async void excedingBudget()
        {
            ErreurCD dialog = new ErreurCD();
            dialog.SetIndex("excedeBudget");
            dialog.XamlRoot = ajouterEmploProjet.XamlRoot;
            dialog.Title = "Erreur";
            dialog.PrimaryButtonText = "OK";
            dialog.DefaultButton = ContentDialogButton.Primary;
            var result = await dialog.ShowAsync();
        }

    }
}
