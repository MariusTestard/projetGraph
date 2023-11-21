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
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherProjets : Page
    {
        String strSelectedItem = "";

        public AfficherProjets()
        {
            this.InitializeComponent();
            Singleton.getInstance().GetListeProjetsEnCours().Clear();
            lvListeProjets.ItemsSource = Singleton.getInstance().GetListeProjetsEnCours();
        }

        private void btnEnCours_Click(object sender, RoutedEventArgs e)
        {
            Singleton.getInstance().GetListeProjetsEnCours().Clear();
            lvListeProjets.ItemsSource = Singleton.getInstance().GetListeProjetsEnCours();
        }

        private void btnTerminer_Click(object sender, RoutedEventArgs e)
        {
            Singleton.getInstance().GetListeProjetsTermine().Clear();
            lvListeProjets.ItemsSource = Singleton.getInstance().GetListeProjetsTermine();
        }

        private void lvListeProjets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projet unProjet = lvListeProjets.SelectedItem as Projet;
            strSelectedItem = unProjet.numProjet;
            Frame.Navigate(typeof(zoomProjet), strSelectedItem);
        }
    }
}
