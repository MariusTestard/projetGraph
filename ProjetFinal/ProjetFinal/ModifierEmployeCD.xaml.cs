using Microsoft.UI;
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
    public sealed partial class ModifierEmployeCD : ContentDialog
    {

        string matricule;//dadw
        string nom;
        string prenom;
        string dateNaissance;//dwada
        string email;
        string adresse;
        string dateEmbauche;//dawda
        double tauxHoraire;
        string photo;
        bool statut;

        public ModifierEmployeCD()
        {
            this.InitializeComponent();
            tbxNom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxAdresse.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxPrenom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxTauxHoraire.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxPhoto.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        public void setEmploye(Employe e)
        {
            matricule = e.Matricule;
            dateNaissance = e.DateNaissance;
            dateEmbauche = e.DateEmbauche;
            tbxNom.Text = e.Nom.ToString();
            tbxAdresse.Text = e.Adresse.ToString();
            tbxPrenom.Text = e.Prenom.ToString();
            tbxEmail.Text = e.Email.ToString();
            tbxTauxHoraire.Text = e.TauxHoraire.ToString();
            tbxPhoto.Text = e.Photo.ToString();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxNom.Text))
            {
                tbxNom.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNom.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxNom.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxNom.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxAdresse.Text))
            {
                tbxAdresse.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxAdresse.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxAdresse.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxAdresse.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxPrenom.Text))
            {
                tbxPrenom.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPrenom.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxPrenom.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxPrenom.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxEmail.Text))
            {
                tbxEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxEmail.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxEmail.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxTauxHoraire.Text))
            {
                tbxTauxHoraire.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxTauxHoraire.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxTauxHoraire.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxTauxHoraire.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxPhoto.Text))
            {
                tbxPhoto.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPhoto.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxPhoto.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxPhoto.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (tbxNom.Text != String.Empty && tbxAdresse.Text != String.Empty && tbxPrenom.Text != String.Empty && tbxEmail.Text != String.Empty && tbxTauxHoraire.Text != String.Empty && tbxPhoto.Text != String.Empty)
            {
                args.Cancel = false;
                SingletonEmploye.getInstance().modifierEmploye(matricule, tbxNom.Text, tbxPrenom.Text, dateNaissance, tbxEmail.Text, tbxAdresse.Text, dateEmbauche, Double.Parse(tbxTauxHoraire.Text), tbxPhoto.Text);
            }
        }
    }
}
