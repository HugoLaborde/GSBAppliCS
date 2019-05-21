using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace service
{
    class DBConnect
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        //Constructeur
        public DBConnect()
        {
            Initialize();
        }

        //Initialisation des valeurs
        private static void Initialize()
        {
            server = "localhost";
            database = "gsb_frais";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //Ouverture de la connection
        public static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //Message d'erreur de la connection :
                //0 : Impossible de se connecter au serveur.
                //1045 : Username ou mot de passe inccorect.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Connection au serveur impossible.");
                        break;

                    case 1045:
                        Console.WriteLine("Username/Password erroné");
                        break;
                }
                return false;
            }
        }

        //Fermeture de la connection
        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Requête Insert
        public void Insert()
        {
            Initialize();
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //Ouverture de la connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execution de la demande
                cmd.ExecuteNonQuery();

                //Fermeture de la connexion
                CloseConnection();
            }
        }

        //Requête Update
        public static void Update(string uIdEtat, string dIdEtat)
        {
            Initialize();
            //Création de variable ou stocker les résultat récupéreré par la requête (innutile sauf pour les test)
            string moisP = GestionDates.getMoisPrecedent();
            DateTime date = System.DateTime.Now;
            string moisPAnnee = (date.Year).ToString() + moisP; //(date.Year).ToString() récupère l'année en string et on y concaténe moisP (qui est le mois précédent)

            //Change l'état des fiche créer a l'état cloturé
            string query = "UPDATE fichefrais SET idetat = @unIdEtat WHERE idetat = @deuxIdEtat AND mois = @unMoisAnnee;";

            //Ouverture de la connection
            if (OpenConnection() == true)
            {
                //Création de la requête Sql
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Assignation des paramètres
                cmd.Parameters.Add(new MySqlParameter("@unIdEtat", uIdEtat));
                cmd.Parameters.Add(new MySqlParameter("@deuxIdEtat", dIdEtat));
                cmd.Parameters.Add(new MySqlParameter("@unMoisAnnee", moisPAnnee));

                //Execution de la demande
                cmd.ExecuteNonQuery();

                //Fermeture de la connexion
                CloseConnection();
            }
        }

        //Requête Delete
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        //Requête Select
        public static string Select(string moisa, string ide)//moisa -> chaine sous cette forme aaaamm tels que (201811) ide -> idetat de la fiche de frasi a récupérer
        {
            Initialize();
            string query = "SELECT idvisiteur, mois, idetat FROM fichefrais where idetat = @unId AND mois = @unMois";

            //Création de variable ou stocker les résultat récupéreré par la requête (innutile sauf pour les test)
            string idvisiteur = "";
            string mois = "";
            string idetat = "";
            

            //Ouverture de la connexion
            if (OpenConnection() == true)
            {
                //Création de la requête
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add(new MySqlParameter("@unId", ide));
                cmd.Parameters.Add(new MySqlParameter("@unMois", moisa));
                //Création d'un dataReader et exécution de la requête
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //On récupère les valeurs du dataReader et les rentres dans les variables
                while (dataReader.Read())
                {
                    idvisiteur = dataReader["idvisiteur"] + "";
                    mois = dataReader["mois"] + "";
                    idetat = dataReader["idetat"] + "";

                }


                //Fermeture du dataReader
                dataReader.Close();

                //Fermeture de la connexion
                CloseConnection();

                string ligne = idvisiteur + " " + mois + " " + idetat;
                //On retourne une ligne avec les valeurs pour les tests
                return ligne;
            }
            else
            {
                return "Erreur, la requete Select ne s'est pas exécuté";
            }
        }
    }
}