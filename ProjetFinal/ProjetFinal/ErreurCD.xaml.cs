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
    public sealed partial class ErreurCD : ContentDialog
    {
        public ErreurCD()
        {
            this.InitializeComponent();
            moisAncien3.Visibility = Visibility.Collapsed;
            notAdmin.Visibility = Visibility.Collapsed;
            empDejAffili.Visibility = Visibility.Collapsed;
            excedeBudget.Visibility = Visibility.Collapsed;
            maxProj.Visibility = Visibility.Collapsed;
        }

        public string indexErr;

        public void SetIndex(string _indexErr)
        {
            indexErr = _indexErr;
            if (indexErr != null)
            {
                if (indexErr == "moisAncien3")
                    moisAncien3.Visibility = Visibility.Visible;
                else if (indexErr == "notAdmin")
                    notAdmin.Visibility = Visibility.Visible;
                else if (indexErr == "empDejAffili")
                    empDejAffili.Visibility = Visibility.Visible;
                else if (indexErr == "maxProj")
                    maxProj.Visibility = Visibility.Visible;
                else
                    excedeBudget.Visibility = Visibility.Visible;
            }
        }
    
    }
}
