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
    internal class SingletonClient
    {
       
        ObservableCollection<Client> listeClients;
        MySqlConnection conn;
        static SingletonClient instance = null;
        string idClient;

        public SingletonClient()
        {
            listeClients = new ObservableCollection<Client>();
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        // RÉCUPÈRE L'INSTANCE DE L'OBJET 
        public static SingletonClient getInstance()
        {
            if (instance == null)
                instance = new SingletonClient();
            return instance;
        }

        // RÉCUPÈRE TOUS LES CLIENTS DE LA BASE DE DONNÉES
        public ObservableCollection<Client> getListeClients()
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

        // AJOUTE UN CLIENT DANS LA BASE DE DONNÉES 
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
                getListeClients();
                getListeClients().Clear();
                getListeClients();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        // MODIFIER UN CLIENT DANS LA BASE DE DONNÉES
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
                getListeClients();
                getListeClients().Clear();
                getListeClients();
            }
            catch (MySqlException ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return conn;
        }

        // SUPPRIMER UN CLIENT DANS LA BASE DE DONNÉES
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
                getListeClients();
                getListeClients().Clear();
                getListeClients();
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
