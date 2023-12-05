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
    public sealed partial class AjouterProjetCD : ContentDialog
    {
        public AjouterProjetCD()
        {
            this.InitializeComponent();
            tbxTitre.BorderBrush = new SolidColorBrush(Colors.LightGray);
            DPDateDeb.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxDesc.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxBudget.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxNbrEmplo.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxClient.BorderBrush = new SolidColorBrush(Colors.LightGray);

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxTitre.Text))
            {
                tbxTitre.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxTitre.PlaceholderText = "Nom requis !";
                args.Cancel = true;
            }
            else
            {
                tbxTitre.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxTitre.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxDesc.Text))
            {
                tbxDesc.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxDesc.PlaceholderText = "Prénom requis !";
                args.Cancel = true;
            }
            else
            {
                tbxDesc.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxDesc.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxBudget.Text))
            {
                tbxBudget.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxBudget.PlaceholderText = "Email requis !";
                args.Cancel = true;
            }
            else
            {
                tbxBudget.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxBudget.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxNbrEmplo.Text))
            {
                tbxNbrEmplo.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNbrEmplo.PlaceholderText = "Adresse requise !";
                args.Cancel = true;
            }
            else
            {
                tbxNbrEmplo.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxNbrEmplo.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxClient.Text))
            {
                tbxClient.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxClient.PlaceholderText = "Photo requise !";
                args.Cancel = true;
            }
            else
            {
                tbxClient.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxClient.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (DPDateDeb.SelectedDate == null)
            {
                DPDateDeb.BorderBrush = new SolidColorBrush(Colors.Red);
                tblDateDebLabel.Text = "Requis";
                tblDateDebLabel.Foreground = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
            else
            {
                DPDateDeb.BorderBrush = new SolidColorBrush(Colors.LightGray);
                args.Cancel = true;
            }
            if (tbxTitre.Text != String.Empty && tbxDesc.Text != String.Empty && tbxBudget.Text != String.Empty && tbxNbrEmplo.Text != String.Empty
                && tbxClient.Text != String.Empty && DPDateDeb.SelectedDate != null)
            {
                args.Cancel = false;
                SingletonProjet.getInstance().ajouterProjets(tbxTitre.Text, DPDateDeb.SelectedDate.Value.ToString("yyyy-MM-dd"),tbxDesc.Text,double.Parse(tbxBudget.Text),int.Parse(tbxNbrEmplo.Text),0,tbxClient.Text);

                //j'ai mis la valeur de totSalaireAPay a zéro, pcq je sais pas si c'est la BD qui la calcule automatiquement
            }
        }
    }
}
