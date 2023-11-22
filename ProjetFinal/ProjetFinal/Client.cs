using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Client
    {
        public Client(int idClient, string nom, string adresse, string numTel, string email)
        {
            IdClient = idClient;
            Nom = nom;
            Adresse = adresse;
            NumTel = numTel;
            Email = email;
        }

        public int IdClient { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string NumTel { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"IdClient: {IdClient} - Nom: {Nom} - Adresse: {Adresse} - Numéro de Téléphone: {NumTel} - Email: {Email}";
        }
    }
}
