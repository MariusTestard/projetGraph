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

        // FAIRE UNE PROCÉDURE STOCKÉE PROJETS
        public ObservableCollection<Projet> GetListeProjets()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM projet";
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

        // PROJETS EN COURS
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


        // PROJET TERMINÉ
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

        // FAIRE UNE PROCÉDURE STOCKÉE CLIENTS
        public ObservableCollection<Client> GetListeClients()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM client";
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


        // FAIRE UNE PROCÉDURE STOCKÉE EMPLOYES
        public ObservableCollection<Employe> GetListeEmployes()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM employe";
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

        public ObservableCollection<Projet>ChangeStatusFromEmploye(String _matricule)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("update_statut_employePermanent");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@_matricule", _matricule);
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

        public void firstTimeAdmin()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM admin WHERE firstTime = 1";

                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();


                if (result.HasRows)
                {
                    //Création d'un admin
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
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM admin WHERE username = @username AND password = SHA1(@password)";


                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
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
        
        public MySqlConnection modifierProjets(string numProjet, string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaireApay, string client, bool statut)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = " UPDATE projet SET numProjet = @numProjet, titre = @titre, dateDeb = @dateDeb, description = @description, budget = @budget, nbrEmplo = @nbrEmplo, totSalaireApay = @totSalaireApay, client = @client, statut = @statut";

                cmd.Parameters.AddWithValue("@numProjet", numProjet);
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@dateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@budget", budget);
                cmd.Parameters.AddWithValue("@nbrEmplo", nbrEmplo);
                cmd.Parameters.AddWithValue("@totSalaireApay", totSalaireApay);
                cmd.Parameters.AddWithValue("@client", client);
                cmd.Parameters.AddWithValue("@statut", statut);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM projet WHERE numProjet = @numProjet";

                cmd.Parameters.AddWithValue("@numProjet", numProjet);

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

        public MySqlConnection modifierClients(int idClient, string nom, string adresse, string numTel, string email)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = " UPDATE client SET idClient = @idClient, nom = @nom, adresse = @adresse, numTel = @numTel, email = @email";

                cmd.Parameters.AddWithValue("@idClient", idClient);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@numTel", numTel);
                cmd.Parameters.AddWithValue("@email", email);


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
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM client WHERE idClient = @idClient";

                cmd.Parameters.AddWithValue("@idClient", idClient);

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
        public MySqlConnection modifierEmployes(string matricule, string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, double tauxHoraire, string photo, bool statut)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = " UPDATE employe SET matricule = @matricule, nom = @nom, prenom = @prenom, dateNais = @dateNaissance, email = @email, adresse = @adresse, dateEmbauche = @dateEmbauche, tauxHoraire = @tauxHoraire, photo = @photo, statut = @statut";

            cmd.Parameters.AddWithValue("@matricule", matricule);
            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@prenom", prenom);
            cmd.Parameters.AddWithValue("@dateNaissance", dateNaissance);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@adresse", adresse);
            cmd.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
            cmd.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
            cmd.Parameters.AddWithValue("@photo", photo);
            cmd.Parameters.AddWithValue("@statut", statut);

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
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM employe WHERE matricule = @matricule";

                cmd.Parameters.AddWithValue("@matricule", matricule);

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
        
        public MySqlConnection ajouterProjets(string numProjet, string titre, string dateDeb, string description, double budget, int nbrEmplo, double totSalaireApay, string client, bool statut)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO projet VALUES(@numProjet, @titre, @dateDeb, @description, @budget, @nbrEmplo, @totSalaireApay, @client, @statut)";

                cmd.Parameters.AddWithValue("@numProjet", numProjet);
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@dateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@budget", budget);
                cmd.Parameters.AddWithValue("@nbrEmplo", nbrEmplo);
                cmd.Parameters.AddWithValue("@totSalaireApay", totSalaireApay);
                cmd.Parameters.AddWithValue("@client", client);
                cmd.Parameters.AddWithValue("@statut", statut);

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
        
        public MySqlConnection ajouterClients(int idClient, string nom, string adresse, string numTel, string email)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO client VALUES(@idClient, @nom, @adresse, @numTel, @budget, @nbrEmplo, @totSalaireApay, @client, @statut)";

                cmd.Parameters.AddWithValue("@idClient", idClient);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@numTel", numTel);
                cmd.Parameters.AddWithValue("@budget", email);


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
        
        public MySqlConnection ajouterEmployes(string matricule, string nom, string prenom, string dateNaissance, string email, string adresse, string dateEmbauche, double tauxHoraire, string photo, bool statut)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO employe VALUES(@matricule, @nom, @prenom, @dateNais, @email, @adresse, @dateEmbauche, @tauxHoraire, @photo, @statut)";

                cmd.Parameters.AddWithValue("@matricule", matricule);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@dateNais", dateNaissance);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                cmd.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                cmd.Parameters.AddWithValue("@photo", photo);
                cmd.Parameters.AddWithValue("@statut", statut);

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
