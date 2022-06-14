using System;

namespace CercleRoyalEscrimeTournaisien
{
    public class Accueil
    {
        public Accueil()
        {
            Horaires = "Le Cercle Royal d'Escrime de Tournai";
            Horaires += Environment.NewLine + "vous accueille à l'école de Don Bosco";
            Horaires += Environment.NewLine + "dès le 13 septembre 2021. ";
            Horaires += Environment.NewLine + Environment.NewLine + "Pour chaque groupe, l'entrée se fait ";
            Horaires += Environment.NewLine + "par la rue des Augustins.";
            Horaires += Environment.NewLine + Environment.NewLine + "Les entraînements se font";
            Horaires += Environment.NewLine + Environment.NewLine + "<u>"+ "Pour les moins de 12 ans" + "</u>";
            Horaires += Environment.NewLine + "\u25C9  " + " les lundis    de 18h à 19h30";
            Horaires += Environment.NewLine + "\u25C9  " + " les mercredis de 18h à 19h30";
            Horaires += Environment.NewLine + Environment.NewLine + "<u>" + "Pour les plus de 12 ans" + "</u>";
            Horaires += Environment.NewLine + "\u25C9  " + " les vendredis de 18h à 20h";
            Horaires += Environment.NewLine + "\u25C9  " + " les dimanches de 10h à 12h";

            Contacts = "Pour tout renseignement,";
            Contacts += Environment.NewLine + "vous pouvez nous contacter:";
            Contacts += Environment.NewLine + Environment.NewLine + "Via notre secrétaire";
            Contacts += Environment.NewLine + "Shirley Vandevoorde : 0497/38.55.07";
            Contacts += Environment.NewLine + Environment.NewLine + "Via notre maître d'Armes";
            Contacts += Environment.NewLine + "Benoît Lafaut: 0478/34.78.95";
            Contacts += Environment.NewLine + Environment.NewLine + "Via mail";
            Contacts += Environment.NewLine + "escrime.tournai@gmail.com";
        }
        public string Horaires { get; set; }
        public string Contacts { get; set; }


    }
}
