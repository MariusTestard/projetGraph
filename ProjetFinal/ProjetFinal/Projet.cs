using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Projet
    {
        public Projet() { }

        public Projet(string numProjet, string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaireApay, int client, bool statut)
        {
            this.numProjet = numProjet;
            this.titre = titre;
            this.dateDeb = dateDeb;
            this.description = description;
            this.budget = budget;
            this.nbrEmplo = nbrEmplo;
            this.totSalaireApay = totSalaireApay;
            this.client = client;
            this.statut = statut;
        }

        public string numProjet { get; set; }
        public string titre { get; set; }
        public string dateDeb { get; set; }
        public string description { get; set; }
        public double budget { get; set; }
        public int nbrEmplo { get; set; }
        public double totSalaireApay { get; set; }
        public int client { get; set; }
        public bool statut { get; set; }

        public override string ToString()
        {
            return $"numProjet: {numProjet} - titre: {titre} - dateDeb: {dateDeb} - description: {description} - budget: {budget} - nbrEmplo: {nbrEmplo} - totSalaireApay: {totSalaireApay} - client: {client}, - statut: {statut}";
        }

        public string ToStringWrite()
        {
            return $"{numProjet};{titre};{dateDeb};{description};{budget};{nbrEmplo};{totSalaireApay};{client};{statut}";
        }

    }
}
