using Microsoft.UI.Xaml.Controls;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
        string matricule;
        string numProjet;
        string idClient;

        public Singleton()
        {
            listeProjets = new ObservableCollection<Projet>();
            listeClients = new ObservableCollection<Client>();
            listeEmployes = new ObservableCollection<Employe>();
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }

        public void firstTimeAdmin()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("see_admin");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    // call controller to create admin
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

        public void ConnexionAdmin(string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("see_admin");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_username", username);
                cmd.Parameters.AddWithValue("_password", password);
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                if (result.HasRows)
                {
                    Debug.WriteLine("Connecté");
                }
                else { Debug.WriteLine("Champ invalide"); }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public ObservableCollection<Projet> GetListeProjets()
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

        public ObservableCollection<Projet> GetListeProjetsEnCours()
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

        public ObservableCollection<Projet> GetListeProjetsTermine()
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
                GetListeProjets();
                GetListeProjets().Clear();
                GetListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

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
                GetListeProjets();
                GetListeProjets().Clear();
                GetListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

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
                GetListeProjets();
                GetListeProjets().Clear();
                GetListeProjets();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        public ObservableCollection<Client> GetListeClients()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_listeClients");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Client client = new Client(idClient: int.Parse(result["idClient"].ToString()),
                                        nom: result["nom"].ToString(),
                                        adresse: result["adresse"].ToString(),
                                        numTel: result["numTel"].ToString(),
                                        email: result["email"].ToString());
                    listeClients.Add(client);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeClients;
        }

        public MySqlConnection modifierClients(int idClient, string nom, string adresse, string numTel, string email)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("modifier_client");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idClient", idClient);
                cmd.Parameters.AddWithValue("_nom", nom);
                cmd.Parameters.AddWithValue("_adresse", adresse);
                cmd.Parameters.AddWithValue("_numTel", numTel);
                cmd.Parameters.AddWithValue("_email", email);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                //Employe e = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                //listeEmployes[position] = (e);
                GetListeClients();
                GetListeClients().Clear();
                GetListeClients();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        public MySqlConnection supprimerClients(Client client)
        {
            idClient = client.IdClient.ToString();
            try
            {
                MySqlCommand cmd = new MySqlCommand("modifier_client");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idClient", idClient);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                conn.Close();
                GetListeClients();
                GetListeClients().Clear();
                GetListeClients();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        public MySqlConnection ajouterClients(string nom, string adresse, string numTel, string email)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("ajouter_client");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_nom", nom);
                cmd.Parameters.AddWithValue("_adresse", adresse);
                cmd.Parameters.AddWithValue("_numTel", numTel);
                cmd.Parameters.AddWithValue("_email", email);

                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                GetListeClients();
                GetListeClients().Clear();
                GetListeClients();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        public ObservableCollection<Employe> GetListeEmployes()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_listeEmploye");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Employe employe = new Employe(matricule: result["matricule"].ToString(),
                                                  nom: result["nom"].ToString(),
                                                  prenom: result["prenom"].ToString(),
                                                  dateNaissance: result["dateNais"].ToString(),
                                                  email: result["email"].ToString(),
                                                  adresse: result["adresse"].ToString(),
                                                  dateEmbauche: result["dateEmbauche"].ToString(),
                                                  tauxHoraire: (double)result["tauxHoraire"],
                                                  photo: result["photo"].ToString(),
                                                  statut: Boolean.Parse(result["statut"].ToString()));
                    listeEmployes.Add(employe);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeEmployes;
        }

        public ObservableCollection<Projet> ChangeStatusFromEmploye(String _matricule)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("update_statut_employePermanent");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("_matricule", _matricule);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return listeProjets;
        }

        public MySqlConnection modifierEmployes(string matricule, string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, double tauxHoraire, string photo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("modifier_employe");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_matricule", matricule);
                cmd.Parameters.AddWithValue("_nom", nom);
                cmd.Parameters.AddWithValue("_prenom", prenom);
                cmd.Parameters.AddWithValue("_dateNaissance", dateNaissance);
                cmd.Parameters.AddWithValue("_email", email);
                cmd.Parameters.AddWithValue("_adresse", adresse);
                cmd.Parameters.AddWithValue("_dateEmbauche", dateEmbauche);
                cmd.Parameters.AddWithValue("_tauxHoraire", tauxHoraire);
                cmd.Parameters.AddWithValue("_photo", photo);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                //Employe e = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                //listeEmployes[position] = (e);
                GetListeEmployes();
                GetListeEmployes().Clear();
                GetListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }
        public MySqlConnection supprimerEmployes(Employe employe)
        {
            matricule = employe.Matricule.ToString();
            try
            {
                MySqlCommand cmd = new MySqlCommand("supprimer_employe");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_matricule", matricule);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                GetListeEmployes();
                GetListeEmployes().Clear();
                GetListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        public MySqlConnection ajouterEmployes(string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, double tauxHoraire, string photo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("ajouter_employe");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_nom", nom);
                cmd.Parameters.AddWithValue("_prenom", prenom);
                cmd.Parameters.AddWithValue("_dateNais", dateNaissance);
                cmd.Parameters.AddWithValue("_email", email);
                cmd.Parameters.AddWithValue("_adresse", adresse);
                cmd.Parameters.AddWithValue("_dateEmbauche", dateEmbauche);
                cmd.Parameters.AddWithValue("_tauxHoraire", tauxHoraire);
                cmd.Parameters.AddWithValue("_photo", photo);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                GetListeEmployes();
                GetListeEmployes().Clear();
                GetListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }
    }
}
