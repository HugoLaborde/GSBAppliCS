using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using System.Timers;
using System.Threading;

namespace service
{
    class Program
    {
        //Initialisation du timer
        private static System.Timers.Timer aTimer;
        //Initialisation de la variable qui check que le timer ne se lance que une seule fois
        private static bool com = false;

        static void Main(string[] args)
        {
            //Initialisation de la tache
            Thread maTache;

            //On assosie maTache au ThreadLoop (fonction qui va lancé le timer)
            maTache = new Thread(new ThreadStart(ThreadLoop));

            //Lancement de maTache
            maTache.Start();

            //while (true)   Ceci marche aussi bien que l'utilisation du thread
            //{
            //    //Test si le timer c'est lancé une fois au moins (sans ce test il va lancer le timer à l'infinie et faire cracher l'ordi au bout d'un moment)
            //    if (com == false)
            //    {
            //        //Lancement du timer
            //        SetTimer();
            //        com = true;
            //    }  
            //}
        }

        private static void SetTimer()
        {
            //Création du timer avec 6 heures (1 seconde = 1000) d'intervalle.
            aTimer = new System.Timers.Timer(21600000);
            //aTimer = new System.Timers.Timer(10000); //Pour les tests (10s d'intervalles entre le check des requètes)

            //A la fin du timer on effectue la fonction OnTimedEvent
            // ensuite on relance le timer
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("{0:HH:mm:ss}", e.SignalTime);

            //Toutes les 6 heures on check si on est le 1er du mois
            if (GestionDates.entre(1, 1))
            {
                string etat2 = "CR";//Change cet état
                string etat1 = "CL";//en celui là
                DBConnect.Update(etat1, etat2);
                //Console.WriteLine("La requête Update de 1 à 10 a été envoyée");
            }
            else if (GestionDates.entre(20, 31))
            {
                string etat2 = "VA";//Change cet état
                string etat1 = "RB";// en celui là
                DBConnect.Update(etat1, etat2);
                //Console.WriteLine("La requête Update du 20+ a été envoyée");
            }
        }

        public static void ThreadLoop()
        {
            // Tant que le thread n'est pas tué, on effectue la tache
            while (Thread.CurrentThread.IsAlive)
            {
                //Test si le timer c'est lancé une fois au moins (sans ce test il va lancer le timer à l'infinie et faire cracher l'ordi au bout d'un moment)
                if (com == false)
                {
                    //Lancement du timer
                    SetTimer();
                    com = true;
                }

                //Console.WriteLine("\nAppuyer sur une touche pour quitter l'application...\n");
                //Console.WriteLine("Début de l'application à {0:HH:mm:ss}", DateTime.Now);
                //Console.ReadLine();

                //aTimer.Stop();
                //aTimer.Dispose();

                //Console.WriteLine("Fermeture de l'application...");

                {
                    //int jour1 = 0;
                    //int jour2 = 2;
                    //DateTime date = new DateTime(2018, 10, 1, 14, 21, 0);

                    //string ligne = DBConnect.Select();
                    //string moisP = GestionDates.getMoisPrecedent();
                    //string moisCP = GestionDates.getMoisPrecedent(date);
                    //string moisS = GestionDates.getMoisSuivant();
                    //string moisCS = GestionDates.getMoisSuivant(date);



                    //Console.WriteLine("Retour des informations du 1er visiteur : " + ligne);
                    //Console.WriteLine("Mois PRECEDENT au mois ACTUEL : " + moisP);
                    //Console.WriteLine("Mois PRECEDENT au mois CHOISI : " + moisCP);
                    //Console.WriteLine("Mois SUIVANT au mois ACTUEL : " + moisS);
                    //Console.WriteLine("Mois SUIVANT au mois CHOISI : " + moisCS);

                    //if (GestionDates.entre(jour1, jour2))
                    //{
                    //    Console.WriteLine("Le jour ACTUEL se situe BIEN entre les jours 1 et 2 : " + jour1 + " < " + System.DateTime.Now + " < " + jour2);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Le jour ACTUEL ne se situe PAS entre les jours 1 et 2 : " + jour1 + " /< " + System.DateTime.Now + " /< " + jour2);
                    //}

                    //if (GestionDates.entre(jour1, jour2, date))
                    //{
                    //    Console.WriteLine("Le jour CHOISI se situe BIEN entre les jours 1 et 2 : " + jour1 + " < " + date.Day + " < " + jour2);
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Le jour CHOISI ne se situe PAS entre les jours 1 et 2 : " + jour1 + " /< " + date.Day + " /< " + jour2);
                    //}

                    //Console.ReadKey();
                } // test des fonctionnalitées
            }
        }
    }
}
