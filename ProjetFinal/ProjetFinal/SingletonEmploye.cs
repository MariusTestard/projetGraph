using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class SingletonEmploye
    {
        ObservableCollection<Employe> listeEmployes;
        ObservableCollection<EmployeProjet> empProjet;
        MySqlConnection conn;
        static SingletonEmploye instance = null;
        string matricule;
        int nbHeure;

        public SingletonEmploye() 
        {
            listeEmployes = new ObservableCollection<Employe>();
            empProjet = new ObservableCollection<EmployeProjet>();
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        // RÉCUPÈRE L'INSTANCE DE L'OBJET 
        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();
            return instance;
        }

        public ObservableCollection<EmployeProjet> ListeEmployeProjet { get { return empProjet; } }

        public ObservableCollection<Employe> ListeEmploye { get { return listeEmployes; } }

        public int NbHeure { get { return nbHeure; } }

        public void nbHeureMethod(int _nbHeure)
        {
            nbHeure = _nbHeure;
        }

        // RÉCUPÈRE TOUS LES EMPLOYÉS DE LA BASE DE DONNÉES
        public ObservableCollection<Employe> getListeEmployes()
        {
            listeEmployes.Clear();
            try
            {
                MySqlCommand cmd = new MySqlCommand("affiche_listeEmploye");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    string[] splitDateNais = result["dateNais"].ToString().Split(" ");
                    string[] splitDateEmb = result["dateEmbauche"].ToString().Split(" ");
                    Employe employe = new Employe(matricule: result["matricule"].ToString(),
                        nom: result["nom"].ToString(),
                        prenom: result["prenom"].ToString(),
                        dateNaissance: splitDateNais[0].Replace('/', '-'),
                        email: result["email"].ToString(),
                        adresse: result["adresse"].ToString(),
                        dateEmbauche: splitDateEmb[0].Replace('/', '-'),
                        tauxHoraire: (decimal)result["tauxHoraire"],
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
                Debug.WriteLine(ex);
            }
            return listeEmployes;
        }

        public ObservableCollection<EmployeProjet> getEmployeFromAProject(String idProjet)
        {
            empProjet.Clear();
            try
            {
                MySqlCommand cmd = new MySqlCommand("employe_dans_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idProjet", idProjet);
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    string resultBool;
                    if (int.Parse(result["statut"].ToString()) == 1)
                        resultBool = "true";
                    else
                        resultBool = "false";
                    EmployeProjet employe = new EmployeProjet(
                        result["matricule"].ToString(),
                        result["nom"].ToString(),
                        result["prenom"].ToString(),
                        result["dateNais"].ToString(),
                        result["email"].ToString(),
                        result["adresse"].ToString(),
                        result["dateEmbauche"].ToString(),
                        (decimal)result["tauxHoraire"],
                        result["photo"].ToString(),
                        Boolean.Parse(resultBool),
                        (int)result["nbrHeureTravail"],
                        (decimal)result["totSalaireAPay"]);
                    empProjet.Add(employe);
                }
                result.Close();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
            }
            return empProjet;
        }

        // AJOUTE UN EMPLOYÉ DANS LA BASE DE DONNÉES
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
                getListeEmployes();
                getListeEmployes().Clear();
                getListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
            }
            return conn;
        }

        // MODIFIE LES INFORMATIONS D'UN EMPLOYÉ DANS LA BASE DE DONNÉES
        public MySqlConnection modifierEmploye(string matricule, string nom, string prenom, string email, string adresse, double tauxHoraire, string photo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("modifier_employe");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_matricule", matricule);
                cmd.Parameters.AddWithValue("_nom", nom);
                cmd.Parameters.AddWithValue("_prenom", prenom);
                cmd.Parameters.AddWithValue("_email", email);
                cmd.Parameters.AddWithValue("_adresse", adresse);
                cmd.Parameters.AddWithValue("_tauxHoraire", tauxHoraire);
                cmd.Parameters.AddWithValue("_photo", photo);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                //Employe e = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                //listeEmployes[position] = (e);
                getListeEmployes();
                getListeEmployes().Clear();
                getListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
            }
            return conn;
        }

        public void swap(Employe employe, int position)
        {
            listeEmployes[position] = employe;
        }


        // SUPPRIME UN EMPLOYÉ DANS LA BASE DE DONNÉES
        public MySqlConnection supprimerEmploye(Employe employe)
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
                getListeEmployes();
                getListeEmployes().Clear();
                getListeEmployes();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
            }
            return conn;
        }

        // CHANGE LE STATUT D'UN EMPLOYÉ DE JOURNALIER À PERMANENT DANS LA BASE DE DONNÉES
        public void changeStatusFromEmploye(Employe employe, int pos)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("update_statut_employePermanent");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("_matricule", employe.Matricule);
                cmd.ExecuteNonQuery();
                conn.Close();
                swap(employe, pos);
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
                throw ex;
            }
        }

        // AJOUTE UN EMPLOYÉ À UN PROJET
        public void ajoutEmpProjet(String idProjet, String idEmplo, int nbHeure)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("ajout_emplo_sur_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idProjet", idProjet);
                cmd.Parameters.AddWithValue("_idEmplo", idEmplo);
                cmd.Parameters.AddWithValue("_nbHeure", nbHeure);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
                getEmployeFromAProject(idProjet);
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                throw ex;
            }
        }

        public void retirerEmpProjet(String idProjet, String idEmplo)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("retirer_emplo_sur_projet");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idProjet", idProjet);
                cmd.Parameters.AddWithValue("_idEmplo", idEmplo);
                conn.Open();
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                Debug.WriteLine(ex);
            }
            getEmployeFromAProject(idProjet);
        }

    }
}
