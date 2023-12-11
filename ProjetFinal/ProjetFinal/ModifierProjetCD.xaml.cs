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
    public sealed partial class ModifierProjetCD : ContentDialog
    {
        string numProjet = "";

        public ModifierProjetCD()
        {
            this.InitializeComponent();
            tbxTitre.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbxDescription.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tgStatut.BorderBrush = new SolidColorBrush(Colors.LightGray);
        }

        public void setProjet(string titre, string description, string _numProjet, bool statut)
        {
            tbxTitre.Text = titre;
            tbxDescription.Text = description;
            numProjet = _numProjet;
            Statut = statut;
        }

        public bool Statut { get; set; }

        public bool IsEnable
        {
            get { return !Statut; }
        }

        public string StatutString
        {
            get
            {
                return Statut == false ? "En cours" : "Terminé";
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (String.IsNullOrEmpty(tbxTitre.Text))
            {
                tbxTitre.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxTitre.PlaceholderText = "Titre requis";
                args.Cancel = true;
            }
            else
            {
                tbxTitre.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxTitre.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            if (String.IsNullOrEmpty(tbxDescription.Text))
            {
                tbxDescription.BorderBrush = new SolidColorBrush(Colors.Red);
                tbxDescription.PlaceholderText = "Description requise";
                args.Cancel = true;
            }
            else
            {
                tbxDescription.BorderBrush = new SolidColorBrush(Colors.LightGray);
                tbxDescription.PlaceholderText = String.Empty;
                args.Cancel = true;
            }
            Debug.WriteLine("IM GONNA LOOK IF THE INPUTS ARE GOOD");
            if (tbxTitre.Text != String.Empty && tbxDescription.Text != String.Empty)
            {
                args.Cancel = false;
                Debug.WriteLine("ALL THE INPUTS WERE GOOD");
                SingletonProjet.getInstance().modifierProjets(numProjet, tbxTitre.Text, tbxDescription.Text, IsEnable);
            }
        }

    }
}
