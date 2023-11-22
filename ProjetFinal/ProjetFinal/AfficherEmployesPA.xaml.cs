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
    public sealed partial class AfficherEmployes : Page
    {
        public AfficherEmployes()
        {
            this.InitializeComponent();
            SingletonEmploye.getInstance().getListeEmployes().Clear();
            lvListeEmployes.ItemsSource = SingletonEmploye.getInstance().getListeEmployes();
        }

        private void btnAjouterEmplo_Click(object sender, RoutedEventArgs e)
        {

        }


        private void cbxPerm_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox b = (CheckBox)sender;
            var c = b.Tag.ToString();
            SingletonEmploye.getInstance().changeStatusFromEmploye(c);
            if (b.IsChecked == true)
            {
                b.IsEnabled = false;
                b.Content = ("Permanent");
                SingletonEmploye.getInstance().getListeEmployes().Clear();
                lvListeEmployes.ItemsSource = SingletonEmploye.getInstance().getListeEmployes();
            }
        }
    }
}
