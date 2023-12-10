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
    public sealed partial class AjouterEmploAProjetCD : ContentDialog
    {
        public AjouterEmploAProjetCD()
        {
            this.InitializeComponent();
            tbxNbrHeure.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxNbrHeure.Text))
            {
                tbxNbrHeure.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxNbrHeure.PlaceholderText = "Nombre d'heures consacrées requis";
                args.Cancel = true;
            }
            else
            {
                try
                {
                    int test = int.Parse(tbxNbrHeure.Text);
                }
                catch (Exception e)
                {
                    tbxNbrHeure.BorderBrush = new SolidColorBrush(Colors.Red);
                    tbxNbrHeure.Text = String.Empty;
                    tbxNbrHeure.PlaceholderText = "Mauvais format";
                    args.Cancel = true;
                    goto inputValidation;
                }
                tbxNbrHeure.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxNbrHeure.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            inputValidation:
            if (tbxNbrHeure.Text != String.Empty)
            {
                args.Cancel = false;
                SingletonEmploye.getInstance().nbHeureMethod(int.Parse(tbxNbrHeure.Text));
            }
        }
    }
}
