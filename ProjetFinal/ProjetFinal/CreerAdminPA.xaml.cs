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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreerAdminPA : Page
    {
        public CreerAdminPA()
        {
            this.InitializeComponent();
            tbxuser.BorderBrush = new SolidColorBrush(Colors.LightGray);
            mdp.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxuser.Text))
            {
                tbxuser.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxuser.PlaceholderText = "Nom d'utilisateur requis !";
            }
            else
            {
                tbxuser.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxuser.PlaceholderText = String.Empty;
            }
            if (String.IsNullOrEmpty(mdp.Password))
            {
                mdp.BorderBrush = new SolidColorBrush(Colors.Red);
                mdp.PlaceholderText = "Mot de passe requis !";
            }
            else
            {
                mdp.BorderBrush = new SolidColorBrush(Colors.LightGray);
                mdp.PlaceholderText = String.Empty;
            }
            if (String.IsNullOrEmpty(mdpConf.Password))
            {
                mdpConf.BorderBrush = new SolidColorBrush(Colors.Red);
                mdpConf.PlaceholderText = "Confirmation de mot de passe requis !";
            }
            else
            {
                mdpConf.BorderBrush = new SolidColorBrush(Colors.LightGray);
                mdpConf.PlaceholderText = String.Empty;
            }
            if (tbxuser.Text != String.Empty && mdp.Password != String.Empty && mdpConf.Password != String.Empty)
            {
                if (mdp.Password.Equals(mdpConf.Password))
                {
                    SingletonAdmin.getInstance().createAdmin(tbxuser.Text, mdp.Password);
                    Frame.Navigate(typeof(AfficherProjetsPA));
                }
                else
                {
                    mdpConf.BorderBrush = new SolidColorBrush(Colors.Red);
                    mdpConf.PlaceholderText = "Ne concorde pas !";
                    mdpConf.Password = String.Empty;
                    mdp.BorderBrush = new SolidColorBrush(Colors.Red);
                    mdp.PlaceholderText = "Ne concorde pas !";
                    mdp.Password = String.Empty;
                }
            }
        }

    }
}
