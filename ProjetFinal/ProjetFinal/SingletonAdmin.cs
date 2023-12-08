using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class SingletonAdmin
    {
        MySqlConnection conn;
        static SingletonAdmin instance = null;
        bool AdminLogin = false;
        



        public SingletonAdmin() 
        {
            conn = new MySqlConnection("Server=cours.cegep3r.info;Database=1865294-gabryel-poisson;Uid=1865294;Pwd=1865294;");
        }

        public Button Bt { get; set; }

        // RÉCUPÈRE L'INSTANCE DE L'OBJET 
        public static SingletonAdmin getInstance()
        {
            if (instance == null)
                instance = new SingletonAdmin();
            return instance;
        }

        // CRÉER UN COMPTE ADMINISTRATEUR S'IL N'EN EXISTE PAS DÉJA UN
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

        // CRÉER UNE CONNEXION AVEC LE COMPTE ADMINISTRATEUR SI LES INFORMATIONS ENTRÉES SONT CORRECTES
        public void connexionAdmin(string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("lookup_admin");
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_username", username);
                cmd.Parameters.AddWithValue("_password", password);
                conn.Open();
                MySqlDataReader result = cmd.ExecuteReader();
                if (!result.HasRows)
                    throw new Exception();
                else
                {
                    Debug.WriteLine("Connecté");
                    AdminLogin = true;
                    Bt.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
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

        public void deconnexionAdmin()
        {
            AdminLogin = false;
            Bt.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
        }

        public bool LoginAdmin() 
        { 
            return AdminLogin;
        }
    }
}
