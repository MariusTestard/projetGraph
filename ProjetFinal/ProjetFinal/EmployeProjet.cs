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
    internal class EmployeProjet : INotifyPropertyChanged
    {
        string matricule;
        string nom;
        string prenom;
        double tauxHoraire;
        int nbrHeureTravail;
        double totSalaireAPay;
        string photo;

        public EmployeProjet() { }

        public EmployeProjet(string _matricule, string _nom, string _prenom, double _tauxHoraire, int _nbrHeureTravail, double _totSalaireAPay, string _photo)
        {
            matricule = _matricule;
            nom = _nom;
            prenom = _prenom;
            tauxHoraire = _tauxHoraire;
            nbrHeureTravail = _nbrHeureTravail;
            totSalaireAPay = _totSalaireAPay;
            photo = _photo;
        }

        public string Matricule
        {
            get => matricule;
            set
            {
                matricule = value;
                this.OnPropertyChanged();
            }
        }

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                this.OnPropertyChanged();
            }
        }
        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
                this.OnPropertyChanged();
            }
        }
        public double TauxHoraire
        {
            get => tauxHoraire;
            set
            {
                tauxHoraire = value;
                this.OnPropertyChanged();
            }
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
        public double TotSalaireAPay
        {
            get => totSalaireAPay;
            set
            {
                totSalaireAPay = value;
                this.OnPropertyChanged();
            }
        }
        public string Photo
        {
            get => photo;
            set
            {
                photo = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
          this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
