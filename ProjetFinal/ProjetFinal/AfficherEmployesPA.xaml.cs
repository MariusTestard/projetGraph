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
            /*
            if (tblStatut.Content == "False")
            {
                tblStatut.Content == "Journalier";
            }
            else
            {
                tblStatut.Content == "Permanent";
            }
            */
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
            //btnTogglePermanent.Content = "Permanent";
        }
    }
}
