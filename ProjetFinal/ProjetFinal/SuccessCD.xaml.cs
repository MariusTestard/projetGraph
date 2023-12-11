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
    public sealed partial class SuccessCD : ContentDialog
    {
        public SuccessCD()
        {
            this.InitializeComponent();
            ajoutEmpProjSucc.Visibility = Visibility.Collapsed;
            retirEmpProjSucc.Visibility = Visibility.Collapsed;
            adminCoSucc.Visibility = Visibility.Collapsed;
            ajoutEmpSucc.Visibility = Visibility.Collapsed;
            ajoutCliSucc.Visibility = Visibility.Collapsed; 
            ajoutProjSucc.Visibility = Visibility.Collapsed; 
            modifEmpSucc.Visibility = Visibility.Collapsed; 
            modifCliSucc.Visibility = Visibility.Collapsed; 
        }

        public string indexErr;

        public void SetIndex(string _indexErr)
        {
            indexErr = _indexErr;
            if (indexErr != null)
            {
                if (indexErr == "ajoutEmpProjSucc")
                    ajoutEmpProjSucc.Visibility = Visibility.Visible;
                else if (indexErr == "retirEmpProjSucc")
                    retirEmpProjSucc.Visibility = Visibility.Visible;
                else if (indexErr == "ajoutEmpSucc")
                    ajoutEmpSucc.Visibility = Visibility.Visible;
                else if (indexErr == "ajoutCliSucc")
                    ajoutCliSucc.Visibility = Visibility.Visible;
                else if (indexErr == "ajoutProjSucc")
                    ajoutProjSucc.Visibility = Visibility.Visible;
                else if (indexErr == "modifEmpSucc")
                    modifEmpSucc.Visibility = Visibility.Visible;
                else if (indexErr == "adminCoSucc")
                    adminCoSucc.Visibility = Visibility.Visible;
                else
                    modifCliSucc.Visibility = Visibility.Visible;
            }
        }

    }
}
