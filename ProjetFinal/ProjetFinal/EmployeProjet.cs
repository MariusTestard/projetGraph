using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class EmployeProjet
    {
        public EmployeProjet() { }

        public EmployeProjet(string matricule, string nom, string prenom, double tauxHoraire, int nbrHeureTravail, double totSalaireAPay, string photo)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            TauxHoraire = tauxHoraire;
            NbrHeureTravail = nbrHeureTravail;
            TotSalaireAPay = totSalaireAPay;
            Photo = photo;
        }

        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public double TauxHoraire { get; set; }
        public int NbrHeureTravail { get; set; }
        public double TotSalaireAPay { get; set; }
        public string Photo { get; set; }
    }

}
