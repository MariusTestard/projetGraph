using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Projet
    {
        public Projet(string numProjet, string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaire, int client, bool statut)
        {
            this.numProjet = numProjet;
            this.titre = titre;
            this.dateDeb = dateDeb;
            this.description = description;
            this.budget = budget;
            this.nbrEmplo = nbrEmplo;
            this.totSalaire = totSalaire;
            this.client = client;
            this.statut = statut;
        }

        public string numProjet { get; set; }
        public string titre { get; set; }
        public string dateDeb { get; set; }
        public string description { get; set; }
        public double budget { get; set; }
        public int nbrEmplo { get; set; }
        public double totSalaire { get; set; }
        public int client { get; set; }
        public bool statut { get; set; }

        public override string ToString()
        {
            return $"numProjet: {numProjet} - titre: {titre} - dateDeb: {dateDeb} - description: {description} - budget: {budget} - nbrEmplo: {nbrEmplo} - totSalaire: {totSalaire} - client: {client}, - statut: {statut}";
        }
    }
}
