using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherEmployesPA : Page
    {
        public AfficherEmployesPA()
        {
            this.InitializeComponent();
            SingletonEmploye.getInstance().getListeEmployes().Clear();
            lvListeEmployes.ItemsSource = SingletonEmploye.getInstance().getListeEmployes();
            
            
            if (SingletonAdmin.getInstance().LoginAdmin())
            {

                //btnSuppr.Visibility = Visibility.Visible;
                
            }
            else
            {

                //btnSuppr.Visibility = Visibility.Collapsed;
            }
        }

        private void btnTogglePermanent_Click(object sender, RoutedEventArgs e)
        {
            int pos = 0;
            ToggleButton b = (ToggleButton)sender;
            var c = b.Tag.ToString();
            for(int i = 0; i < lvListeEmployes.Items.Count(); i++)
            {
                Employe unEmploye = lvListeEmployes.Items[i] as Employe;
                if (c.Equals(unEmploye.Matricule))
                {
                    pos = i;
                    break;
                }
            }
            Employe emp = lvListeEmployes.Items[pos] as Employe;
            emp.Statut = true;
            SingletonEmploye.getInstance().getListeEmployes()[pos] = emp;
            SingletonEmploye.getInstance().changeStatusFromEmploye(c);
            SingletonEmploye.getInstance().getListeEmployes().Clear();
            SingletonEmploye.getInstance().getListeEmployes();
        }
    }
}
