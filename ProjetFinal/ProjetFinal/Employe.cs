using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Employe
    {
        public Employe(string matricule, string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, double tauxHoraire, string photo, bool statut)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            Email = email;
            Adresse = adresse;
            DateEmbauche = dateEmbauche;
            TauxHoraire = tauxHoraire;
            Photo = photo;
            Statut = statut;
        }

        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string DateNaissance { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string DateEmbauche { get; set; }
        public double TauxHoraire { get; set; }
        public string Photo { get; set; }
        public bool Statut { get; set; }


        public override string ToString()
        {
            return $"Matricule: {Matricule} - Nom: {Nom} - Prénom: {Prenom} - Date de naissance: {DateNaissance} - Email: {Email} - Adresse: {Adresse} - DateEmbauche: {DateEmbauche} - TauxHoraire: {TauxHoraire} - Photo: {Photo} - Statut: {Statut}";
        }
    }
}
