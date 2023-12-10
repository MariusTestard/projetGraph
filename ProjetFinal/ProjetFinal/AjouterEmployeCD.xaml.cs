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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    public sealed partial class AjouterEmployeCD : ContentDialog
    {
        public AjouterEmployeCD()
        {
            this.InitializeComponent();
            tbxNom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxPrenom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            DPdateEmbauche.BorderBrush = new SolidColorBrush(Colors.LightGray);
            DPdateNais.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxPhoto.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxAdresse.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxTauxH.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxNom.Text))
            {
                tbxNom.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNom.PlaceholderText = "Nom requis !";
                args.Cancel = true;
            }
            else
            {
                tbxNom.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxNom.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxPrenom.Text))
            {
                tbxPrenom.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPrenom.PlaceholderText = "Prénom requis !";
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
                tbxEmail.PlaceholderText = "Email requis !";
                args.Cancel = true;
            }
            else
            {
                tbxEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxEmail.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxAdresse.Text))
            {
                tbxAdresse.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxAdresse.PlaceholderText = "Adresse requise !";
                args.Cancel = true;
            }
            else
            {
                tbxAdresse.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxAdresse.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxPhoto.Text))
            {
                tbxPhoto.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxPhoto.PlaceholderText = "Photo requise !";
                args.Cancel = true;
            }
            else
            {
                tbxPhoto.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxPhoto.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxTauxH.Text))
            {
                tbxTauxH.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxTauxH.PlaceholderText = "Taux horaire $ requis";
                args.Cancel = true;
            }
            else
            {
                try
                {
                    if (Double.Parse(tbxTauxH.Text) < 15)
                    {
                        tbxTauxH.BorderBrush = new SolidColorBrush(Colors.Red);
                        tbxTauxH.PlaceholderText = "? >= 15";
                        tbxTauxH.Text = String.Empty;
                        args.Cancel = true;
                    }
                    else if (Double.Parse(tbxTauxH.Text) > 100)
                    {
                        tbxTauxH.BorderBrush = new SolidColorBrush(Colors.Red);
                        tbxTauxH.Text = String.Empty;
                        tbxTauxH.PlaceholderText = "Taux horaire trop élevé";
                        args.Cancel = true;
                    }
                    else
                    {
                        tbxTauxH.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        tbxTauxH.PlaceholderText = "";
                        args.Cancel = true;
                    }
                }
                catch (Exception e)
                {
                    tbxTauxH.BorderBrush = new SolidColorBrush(Colors.Red);
                    tbxTauxH.Text = String.Empty;
                    tbxTauxH.PlaceholderText = "Mauvais format !";
                    args.Cancel = true;
                }
            }
            if (DPdateNais.SelectedDate == null)
            {
                DPdateNais.BorderBrush = new SolidColorBrush(Colors.Red);
                tblDateNaisLabel.Text = "Requis";
                tblDateNaisLabel.Foreground = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
            else
            {

                if((DateTimeOffset.Now.Year - DPdateNais.SelectedDate.Value.Year)>=18 && (DateTimeOffset.Now.Year - DPdateNais.SelectedDate.Value.Year)<=65) 
                {
                    DPdateNais.BorderBrush = new SolidColorBrush(Colors.LightGray);
                    tblDateNaisLabel.Text = "";
                    tblDateNaisLabel.Foreground = new SolidColorBrush(Colors.LightGray);
                    args.Cancel = true;
                }
                else
                {
                    DPdateNais.BorderBrush = new SolidColorBrush(Colors.Red);
                    tblDateNaisLabel.Text = "Âge minimum 18 | Âge maximum 65";
                    tblDateNaisLabel.Foreground = new SolidColorBrush(Colors.Red);
                    args.Cancel = true;
                }
            }
            if (DPdateEmbauche.SelectedDate == null)
            {
                DPdateEmbauche.BorderBrush = new SolidColorBrush(Colors.Red);
                tblDateEmbaucheLabel.Text = "Requis";
                tblDateEmbaucheLabel.Foreground = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
            else
            {
                    if (DPdateEmbauche.SelectedDate<=DateTimeOffset.Now)
                    { 
                    DPdateEmbauche.BorderBrush = new SolidColorBrush(Colors.Red);
                    tblDateEmbaucheLabel.Text = "La date d’embauche ne doit pas être dans le futur";
                    tblDateEmbaucheLabel.Foreground = new SolidColorBrush(Colors.Red);
                    args.Cancel = true;
                    }
                    else
                    {
                        DPdateEmbauche.BorderBrush = new SolidColorBrush(Colors.LightGray);
                        tblDateEmbaucheLabel.Text = "";
                        tblDateEmbaucheLabel.Foreground = new SolidColorBrush(Colors.LightGray);
                        args.Cancel = true;
                    }
            }
            if (tbxNom.Text != String.Empty && tbxPrenom.Text != String.Empty && tbxAdresse.Text != String.Empty && tbxEmail.Text != String.Empty
                && tbxPhoto.Text != String.Empty && tbxTauxH.Text != String.Empty && DPdateEmbauche.SelectedDate != null && DPdateNais.SelectedDate != null)
            {
                args.Cancel = false;
                SingletonEmploye.getInstance().ajouterEmployes(tbxNom.Text, tbxPrenom.Text, DPdateNais.SelectedDate.Value.ToString("yyyy-MM-dd"), tbxEmail.Text,
                                                                tbxAdresse.Text, DPdateEmbauche.SelectedDate.Value.ToString("yyyy-MM-dd"), Double.Parse(tbxTauxH.Text), tbxPhoto.Text);
            }
        }
    }
}
