using Microsoft.UI;
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
    public sealed partial class LoginAdminCD : ContentDialog
    {

        public LoginAdminCD()
        {
            this.InitializeComponent();
            tbxuser.BorderBrush = new SolidColorBrush(Colors.LightGray);
            mdp.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxuser.Text))
            {
                tbxuser.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxuser.PlaceholderText = "Nom d'utilisateur requis";
                args.Cancel = true;
            }
            else
            {
                tbxuser.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxuser.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(mdp.Password))
            {
                mdp.BorderBrush = new SolidColorBrush(Colors.Red);
                mdp.PlaceholderText = "Mot de passe requis";
                args.Cancel = true;
            }
            else
            {
                mdp.BorderBrush = new SolidColorBrush(Colors.LightGray);
                mdp.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (tbxuser.Text != String.Empty && mdp.Password != String.Empty)
            {
                try
                {
                    SingletonAdmin.getInstance().connexionAdmin(tbxuser.Text, mdp.Password);
                    args.Cancel = false;
                }
                catch (MySqlException ex)
                {
                    mdp.BorderBrush = new SolidColorBrush(Colors.Red);
                    mdp.Password = String.Empty;
                    mdp.PlaceholderText = "Cet utilisateur n'existe pas";
                    tbxuser.BorderBrush = new SolidColorBrush(Colors.Red);
                    tbxuser.Text = String.Empty;
                    tbxuser.PlaceholderText = "Cet utilisateur n'existe pas";
                    args.Cancel = true;
                }
            }
        }
    }
}
