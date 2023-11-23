using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.CompilerServices.RuntimeHelpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    public sealed partial class AjouterClientCD : ContentDialog
    {
        string nom;
        string adresse;
        string numTel;
        string email;

        public AjouterClientCD()
        {
            this.InitializeComponent();
            tbxNom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxAdresse.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxNumTel.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxEmail.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxNom.Text))
            {
                tbxNom.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNom.PlaceholderText = "Requis";
                args.Cancel = true;
            } else
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
            if (String.IsNullOrEmpty(tbxNumTel.Text))
            {
                tbxNumTel.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNumTel.PlaceholderText = "Requis";
                args.Cancel = true;
            }
            else
            {
                tbxNumTel.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxNumTel.PlaceholderText = String.Empty;
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
            if (tbxNom.Text != String.Empty && tbxAdresse.Text != String.Empty && tbxNumTel.Text != String.Empty && tbxEmail.Text != String.Empty)
            {
                args.Cancel = false;
                SingletonClient.getInstance().ajouterClients(tbxNom.Text, tbxAdresse.Text, tbxNumTel.Text, tbxEmail.Text);
            }
        }
    }
}
