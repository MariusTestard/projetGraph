using Microsoft.UI.Xaml.Controls;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class EmployeProjet : Employe /*: INotifyPropertyChanged*/
    {
        //string matricule;
        //string nom;
        //string prenom;
        //double tauxHoraire;
        //int nbrHeureTravail;
        //double totSalaireAPay;
        //string photo;

        int nbrHeureTravail;
        decimal totSalaireAPay;

        public EmployeProjet() { }

        public EmployeProjet(string matricule, string nom, string prenom, string dateNaissance,
                             string email, string adresse, string dateEmbauche, decimal tauxHoraire, string photo,
                             bool statut, int _nbrHeureTravail, decimal _totSalaireAPay) : base(matricule, nom, prenom, dateNaissance, email, adresse,
                                                                                               dateEmbauche, tauxHoraire, photo, statut)
        {
            nbrHeureTravail = _nbrHeureTravail;
            totSalaireAPay = _totSalaireAPay;
        }

        public int NbrHeureTravail
        {
            get => nbrHeureTravail;
            set
            {
                nbrHeureTravail = value;
                this.OnPropertyChanged();
            }
        }
        public decimal TotSalaireAPay
        {
            get => totSalaireAPay;
            set
            {
                totSalaireAPay = value;
                this.OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
          this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override string ToString()
        {
            return base.ToString() + $" - nbrHeureTravail: {NbrHeureTravail} - totSalaireApay: {TotSalaireAPay}";
        }
    }
}
