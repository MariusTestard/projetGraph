using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Singleton
    {
        ObservableCollection<Projet> listeProjets;
        ObservableCollection<Client> listeClients;
        ObservableCollection<Employe> listeEmployes;
        MySqlConnection conn;
        static Singleton instance = null;

        public Singleton()
        {
            listeProjets = new ObservableCollection<Projet>();
            listeClients = new ObservableCollection<Client>();
            listeEmployes = new ObservableCollection<Employe>();
            conn = new MySqlConnection("Server=localhost;Database=demo;Uid=root;Pwd=root;");
        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }

        // FAIRE UNE PROCÉDURE STOCKÉE PROJETS
        public ObservableCollection<Projet> GetListeProjets()
        {
            return listeProjets;
        }

        // FAIRE UNE PROCÉDURE STOCKÉE CLIENTS
        public ObservableCollection<Client> GetListeClients()
        {
            return listeClients;
        }

        // FAIRE UNE PROCÉDURE STOCKÉE EMPLOYES
        public ObservableCollection<Employe> GetListeEmployes()
        {
            return listeEmployes;
        }
    }
}
