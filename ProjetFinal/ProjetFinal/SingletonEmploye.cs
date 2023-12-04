using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class SingletonEmploye
    {
        ObservableCollection<Employe> listeEmployes;
        ObservableCollection<Employe> empProjet;
        MySqlConnection conn;
        static SingletonEmploye instance = null;
        string matricule;

        public SingletonEmploye() 
        {
            listeEmployes = new ObservableCollection<Employe>();
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        // RÉCUPÈRE L'INSTANCE DE L'OBJET 
        public static SingletonEmploye getInstance()
        {
            if (instance == null)
                instance = new SingletonEmploye();
            return instance;
        }

        public ObservableCollection<Employe> ListeEmployeProjet { get { return empProjet; } }

        public ObservableCollection<Employe> ListeEmploye { get { return listeEmployes; } }

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
            }
            
        }
    }
}
