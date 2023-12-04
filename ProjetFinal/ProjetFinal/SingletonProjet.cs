using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class SingletonProjet
    {
        ObservableCollection<Projet> listeProjets;
        static SingletonProjet instance = null;
        MySqlConnection conn;
        string numProjet;

        public SingletonProjet()
        {
            listeProjets = new ObservableCollection<Projet>();
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        // RÉCUPÈRE L'INSTANCE DE L'OBJET 
        public static SingletonProjet getInstance()
        {
            if (instance == null)
                instance = new SingletonProjet();
            return instance;
        }

        // RÉCUPÈRE TOUS LES PROJETS DANS LA BASE DE DONNÉES
        public ObservableCollection<Projet> getListeProjets()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_listeProjets");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Projet projet = new Projet(numProjet: result["numProjet"].ToString(),
                        titre: result["titre"].ToString(),
                        dateDeb: result["dateDeb"].ToString(),
                        description: result["description"].ToString(),
                        budget: (double)result["budget"],
                        nbrEmplo: int.Parse(result["nbrEmplo"].ToString()),
                        totSalaireApay: (double)result["totSalaireApay"],
                        client: int.Parse(result["client"].ToString()),
                        statut: Boolean.Parse(result["statut"].ToString()));
                    listeProjets.Add(projet);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeProjets;
        }

        // AJOUTER UN PROJET DANS LA BASE DE DONNÉES
        public MySqlConnection ajouterProjets(string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaireApay, string client)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("ajouter_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_titre", titre);
                cmd.Parameters.AddWithValue("_dateDeb", dateDeb);
                cmd.Parameters.AddWithValue("_description", description);
                cmd.Parameters.AddWithValue("_budget", budget);
                cmd.Parameters.AddWithValue("_nbrEmplo", nbrEmplo);
                cmd.Parameters.AddWithValue("_totSalaireApay", totSalaireApay);
                cmd.Parameters.AddWithValue("_client", client);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                getListeProjets();
                getListeProjets().Clear();
                getListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        // MODIFIER UN PROJET DANS LA BASE DE DONNÉES
        public MySqlConnection modifierProjets(string numProjet, string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaireApay, string client, bool statut)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("modifier_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_numProjet", numProjet);
                cmd.Parameters.AddWithValue("_titre", titre);
                cmd.Parameters.AddWithValue("_dateDeb", dateDeb);
                cmd.Parameters.AddWithValue("_description", description);
                cmd.Parameters.AddWithValue("_budget", budget);
                cmd.Parameters.AddWithValue("_nbrEmplo", nbrEmplo);
                cmd.Parameters.AddWithValue("_totSalaireApay", totSalaireApay);
                cmd.Parameters.AddWithValue("_client", client);
                cmd.Parameters.AddWithValue("_statut", statut);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                //Employe e = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                //listeEmployes[position] = (e);
                getListeProjets();
                getListeProjets().Clear();
                getListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        // SUPPRIMER UN PROJET DANS LA BASE DE DONNÉES
        public MySqlConnection supprimerProjets(Projet projet)
        {
            numProjet = projet.numProjet.ToString();
            try
            {
                MySqlCommand cmd = new MySqlCommand("supprimer_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_numProjet", numProjet);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                getListeProjets();
                getListeProjets().Clear();
                getListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        // RÉCUPÈRE TOUS LES PROJETS EN COURS DANS LA BASE DE DONNÉES
        public ObservableCollection<Projet> getListeProjetsEnCours()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_projet_enCours");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Projet projet = new Projet(numProjet: result["numProjet"].ToString(),
                        titre: result["titre"].ToString(),
                        dateDeb: result["dateDeb"].ToString(),
                        description: result["description"].ToString(),
                        budget: (double)result["budget"],
                        nbrEmplo: int.Parse(result["nbrEmplo"].ToString()),
                        totSalaireApay: (double)result["totSalaireApay"],
                        client: int.Parse(result["client"].ToString()),
                        statut: Boolean.Parse(result["statut"].ToString()));
                    listeProjets.Add(projet);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeProjets;
        }

        // RÉCUPÈRE TOUS LES PROJETS TERMINÉ DANS LA BASE DE DONNÉES
        public ObservableCollection<Projet> getListeProjetsTermine()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_projet_termine");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Projet projet = new Projet(numProjet: result["numProjet"].ToString(),
                        titre: result["titre"].ToString(),
                        dateDeb: result["dateDeb"].ToString(),
                        description: result["description"].ToString(),
                        budget: (double)result["budget"],
                        nbrEmplo: int.Parse(result["nbrEmplo"].ToString()),
                        totSalaireApay: (double)result["totSalaireApay"],
                        client: int.Parse(result["client"].ToString()),
                        statut: Boolean.Parse(result["statut"].ToString()));
                    listeProjets.Add(projet);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeProjets;
        }

        public void getEmployesDansProjet(String idProjet)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("employe_dans_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idProjet", idProjet);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
   
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Employe employe = new Employe(matricule: result["matricule"].ToString(),
                        nom: result["nom"].ToString(),
                        prenom: result["prenom"].ToString(),


                        tauxHoraire: (double)result["tauxHoraire"]);
                    SingletonEmploye.getInstance().ListeEmployeProjet.Add(employe);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

    }


}
