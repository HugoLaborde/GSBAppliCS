using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    abstract class GestionDates
    {
        //Récupère le mois actuelle pour l'utiliser dans les méthodes suivantes
        static DateTime date = System.DateTime.Now;

        //Instanciation des variables moisP et moisS, qui représente les valeurs à retourné
        //du mois précédent pour moisP et du mois suivant pour moisS
        static string moisP;
        static string moisS;

        /// <summary>
        /// Méthode qui retourne le numéro du mois précédent par rapport au mois actuelle
        /// </summary>
        /// <returns>moisP -> (numéro du mois précédent)</returns>
        public static string getMoisPrecedent()
        {
            //Mois Actuel
            int moisA = date.Month;

            moisP = moisPrecedent(moisA);

            return moisP;
        }

        /// <summary>
        /// Méthode qui retourne le numéro du mois précédent celui qu'on lui passe en paramètre
        /// </summary>
        /// <param name="date">date que l'on lui passe en paramètres</param>
        /// <returns>moisP -> (numéro du mois précédent)</returns>
        public static string getMoisPrecedent(DateTime dateC)
        {
            //Mois de la date passé en paramètre (mois Choisi)
            int moisC = dateC.Month;

            moisP = moisPrecedent(moisC);

            return moisP;
        }

        /// <summary>
        /// Méthode qui retourne le numéro du mois suivant par rapport au mois actuelle
        /// </summary>
        /// <returns>moisS -> (numéro du mois suivant)</returns>
        public static string getMoisSuivant()
        {
            //Mois Actuel
            int moisA = date.Month;

            moisS = moisSuivant(moisA);

            return moisS;
        }

        /// <summary>
        /// Méthode qui retourne le numéro du mois suivant celui qu'on lui passe en paramètre
        /// </summary>
        /// <param name="dateC"></param>
        /// <returns>moisS -> (numéro du mois suivant)</returns>
        public static string getMoisSuivant(DateTime dateC)
        {
            //Mois de la date passé en paramètre (mois Choisi)
            int moisC = dateC.Month;

            moisS = moisSuivant(moisC);

            return moisS;
        }

        /// <summary>
        /// Méthode qui test si le numéro du jour actuel se trouve entre les numéros de jours que l'on lui passe
        /// </summary>
        /// <param name="jour1">1er jour passé</param>
        /// <param name="jour2">2ème jour passé</param>
        /// <returns>true si oui sinon false</returns>
        public static bool entre(int jour1, int jour2)
        {
            //Jour Actuel
            int jourA = date.Day;

            return (jourA >= jour1 & jourA <= jour2);
        }

        /// <summary>
        /// Méthode qui test si le numéro du jour de la date que l'on lui passe se trouve entre les numéros de jours que l'on lui passe
        /// </summary>
        /// <param name="jour1">1er jour passé</param>
        /// <param name="jour2">2ème jour passé</param>
        /// <param name="dateC">date passé</param>
        /// <returns>true si oui sinon false</returns>
        public static bool entre(int jour1, int jour2, DateTime dateC)
        {
            //Jour de la date passé en paramètre (jour Choisi)
            int jourC = dateC.Day;

            return (jourC >= jour1 & jourC <= jour2);
        }

        /// <summary>
        /// Méthode qui convertit le numéro du mois en numéro du mois suivant
        /// </summary>
        /// <param name="mois"></param>
        /// <returns>un string du mois précédent au mois passé en paramètre</returns>
        public static string moisPrecedent(int mois)
        {
            //Le mois précédent Janvier est Décembre
            if (mois == 1)
            {
                moisP = "12";
            }
            else
            {
                moisP = (mois - 1).ToString();
            }

            moisP = ajouteZero(mois, moisP);

            return moisP;
        }

        /// <summary>
        /// Méthode qui convertit le numéro du mois en numéro du mois suivant
        /// </summary>
        /// <param name="mois"></param>
        /// <returns>un string du mois suivant au mois passé en paramètre</returns>
        public static string moisSuivant(int mois)
        {
            //Le mois suivant Décembre est Janvier
            if (mois == 12)
            {
                moisS = "01";
            }
            else
            {
                moisS = (mois + 1).ToString();
            }

            moisS = ajouteZero(mois, moisS);

            return moisS;
        }

        /// <summary>
        /// Rajoute un zéro au mois passé si il se trouve entre janvier et septembre
        /// </summary>
        /// <param name="mois">numéro du mois en int</param>
        /// <param name="Lmois">numéro du mois en string</param>
        /// <returns></returns>
        public static string ajouteZero(int mois, string Lmois)
        {
            //Entre 2 et 10 car on fais le check sur le mois actuel et non le mois précédent
            if (mois >= 2 & mois <= 10)
            {
                Lmois = "0" + Lmois;
            }

            return Lmois;
        }
    }
}
