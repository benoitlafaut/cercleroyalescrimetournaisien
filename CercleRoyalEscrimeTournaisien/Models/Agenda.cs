using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CercleRoyalEscrimeTournaisien
{
    public static class Agenda
    {
        public static DateTime DateCurrent;
        public static string DateCurrentMonth;
        public static string DateCurrentYear;
        public static string TexteEnDessousDuCalendar
        {
            get
            {
                string link = @"<a href=../FileToUpload/AGENDA_SAISON_2021_2022.docx>ici</a>";
                string texte;

                texte = "L'escrime est un sport de combat. 3 armes sont utilisées : le fleuret, l'épée et le sabre. Ces 3 armes sont mixtes; individuelle ou par équipes.";
                texte += @"<br />" + "Les cours au <b><span style='color: cornflowerblue'>fleuret</span></b> sont en bleu, à <b><span style='color: red'>l'épée</span></b> en rouge et au <b><span style='color: forestgreen'>sabre</span></b> en vert.";
                texte += @"<br />" + "Vous trouverez " + link + " le calendrier téléchargeable sous format word.";

                return texte;
            }
        }

        public static string GetNomOfMois(int mois)
        {
            switch (mois)
            {
                case 1:
                    return "Janvier";
                case 2:
                    return "Février";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juillet";
                case 8:
                    return "Août";
                case 9:
                    return "Septembre";
                case 10:
                    return "Octobre";
                case 11:
                    return "Novembre";
                case 12:
                    return "Décembre";
            }

            return string.Empty;
        }
    }


    public class DateListAgenda
    {
        DateArmes JourNull = new DateArmes()
        {
            ArmeManieeOfDay = TypeArme.None,
            dateOfDay = DateTime.MinValue,
            JourneeOfTheDay = DayOfWeek.Monday,
            NummerOfDay = string.Empty,
            ISDateOfDay = false,
            ClassOfCell = string.Empty
        };

        public List<DateArmes> GetAllDaysOfTheCurrentDate()
        {
            List<DateArmes> list = new List<DateArmes>();

            DateTime DateDepart = Agenda.DateCurrent;

            DateTime startDate = new DateTime(DateDepart.Year, DateDepart.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            List<DateTime> calculatedDates = GetAllDatesBetween(startDate, endDate);

            List<DateTime> AllSeancesFleuret = GetAllSeancesForFleuret();
            List<DateTime> AllSeancesEpée = GetAllSeancesForEpee();
            List<DateTime> AllSeancesSabre = GetAllSeancesForSabre();

            foreach (DateTime date in calculatedDates)
            {
                DateArmes dateArmes = new DateArmes();
                dateArmes.dateOfDay = date;
                dateArmes.NummerOfDay = date.Day.ToString();
                dateArmes.JourneeOfTheDay = date.DayOfWeek;
                dateArmes.ArmeManieeOfDay = TypeArme.None;
                dateArmes.ISDateOfDay = date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) == DateTime.Now.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                if (AllSeancesFleuret.Contains(date))
                {
                    dateArmes.ArmeManieeOfDay = TypeArme.Fleuret;
                    dateArmes.ClassOfCell = "CelluleCouleurFleuret";
                }

                if (AllSeancesEpée.Contains(date))
                {
                    dateArmes.ArmeManieeOfDay = TypeArme.Epée;
                    dateArmes.ClassOfCell = "CelluleCouleurEpee";
                }

                if (AllSeancesSabre.Contains(date))
                {
                    dateArmes.ArmeManieeOfDay = TypeArme.Sabre;
                    dateArmes.ClassOfCell = "CelluleCouleurSabre";
                }

                if (dateArmes.ISDateOfDay)
                {
                    dateArmes.ClassOfCell += " CelluleAujourdhui";
                }

                list.Add(dateArmes);
            }



            return list;
        }

        public List<LaSemaine> AllDatePerWeek(List<DateArmes> getAllDays)
        {
            List<LaSemaine> allDatePerWeek = new List<LaSemaine>();
            allDatePerWeek.Add(new LaSemaine()
            {
                PremierJour = new DateArmes() { NummerOfDay = "Lundi" },
                SecondJour = new DateArmes() { NummerOfDay = "Mardi" },
                TroisièmeJour = new DateArmes() { NummerOfDay = "Mercredi" },
                QuatrièmeJour = new DateArmes() { NummerOfDay = "Jeudi" },
                CinquièmeJour = new DateArmes() { NummerOfDay = "Vendredi" },
                SixièmeJour = new DateArmes() { NummerOfDay = "Samedi" },
                SeptièmeJour = new DateArmes() { NummerOfDay = "Dimanche" }
            });

            for (int i = 1; i <= getAllDays.Count - 1; i++)
            {
                int j = i;

                while (j != i + 6 && getAllDays[j].JourneeOfTheDay != DayOfWeek.Sunday && j < getAllDays.Count - 1)
                {
                    j++;

                };

                List<DateArmes> getPartOfAllDays = getAllDays.Skip(i).Take(j - i + 1).ToList();


                LaSemaine laSemaine = new LaSemaine();
                laSemaine.PremierJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Monday).FirstOrDefault();
                laSemaine.SecondJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Tuesday).FirstOrDefault();
                laSemaine.TroisièmeJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Wednesday).FirstOrDefault();
                laSemaine.QuatrièmeJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Thursday).FirstOrDefault();
                laSemaine.CinquièmeJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Friday).FirstOrDefault();
                laSemaine.SixièmeJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Saturday).FirstOrDefault();
                laSemaine.SeptièmeJour = getPartOfAllDays.Where(x => x.JourneeOfTheDay == System.DayOfWeek.Sunday).FirstOrDefault();

                if (laSemaine.PremierJour == null) { laSemaine.PremierJour = JourNull; }
                if (laSemaine.SecondJour == null) { laSemaine.SecondJour = JourNull; }
                if (laSemaine.TroisièmeJour == null) { laSemaine.TroisièmeJour = JourNull; }
                if (laSemaine.QuatrièmeJour == null) { laSemaine.QuatrièmeJour = JourNull; }
                if (laSemaine.CinquièmeJour == null) { laSemaine.CinquièmeJour = JourNull; }
                if (laSemaine.SixièmeJour == null) { laSemaine.SixièmeJour = JourNull; }
                if (laSemaine.SeptièmeJour == null) { laSemaine.SeptièmeJour = JourNull; }

                allDatePerWeek.Add(laSemaine);

                i = i + (j - i);
            }

            return allDatePerWeek;
        }

        private List<DateTime> GetAllDatesBetween(DateTime start, DateTime end)
        {
            List<DateTime> list = new List<DateTime>() { };
            list.Add(DateTime.MinValue);

            for (DateTime day = start.Date; day <= end; day = day.AddDays(1))
            {
                list.Add(day);
            }
            return list;
        }

        private List<DateTime> GetAllSeancesForFleuret()
        {
            List<DateTime> list = new List<DateTime>() { };
            list.Add(new DateTime(2021, 09, 29, 00, 00, 00));

            list.Add(new DateTime(2021, 10, 1, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 3, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 4, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 6, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 8, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 10, 00, 00, 00));


            list.Add(new DateTime(2021, 11, 29, 00, 00, 00));

            list.Add(new DateTime(2021, 12, 1, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 3, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 5, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 6, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 8, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 10, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 12, 00, 00, 00));

            

            list.Add(new DateTime(2022, 02, 14, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 16, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 18, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 20, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 21, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 23, 00, 00, 00));
            list.Add(new DateTime(2022, 02, 25, 00, 00, 00));

            

            list.Add(new DateTime(2022, 5, 2, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 4, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 6, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 8, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 9, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 11, 00, 00, 00));
            return list;
        }

        private List<DateTime> GetAllSeancesForEpee()
        {
            List<DateTime> list = new List<DateTime>() { };
            list.Add(new DateTime(2021, 09, 13, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 15, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 17, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 19, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 20, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 22, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 24, 00, 00, 00));
            list.Add(new DateTime(2021, 09, 26, 00, 00, 00));

            list.Add(new DateTime(2021, 10, 11, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 13, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 15, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 17, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 18, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 20, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 22, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 24, 00, 00, 00));
            
            list.Add(new DateTime(2021, 11, 15, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 17, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 19, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 21, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 22, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 24, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 26, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 28, 00, 00, 00));

            list.Add(new DateTime(2021, 12, 13, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 15, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 17, 00, 00, 00));
            list.Add(new DateTime(2021, 12, 19, 00, 00, 00));

            list.Add(new DateTime(2022, 1, 10, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 12, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 14, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 16, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 31, 00, 00, 00));

            list.Add(new DateTime(2022, 2, 2, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 4, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 6, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 7, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 9, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 11, 00, 00, 00));
            list.Add(new DateTime(2022, 2, 13, 00, 00, 00));

            list.Add(new DateTime(2022, 3, 7, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 9, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 11, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 13, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 14, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 16, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 18, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 20, 00, 00, 00));

            list.Add(new DateTime(2022, 4, 20, 00, 00, 00));
            list.Add(new DateTime(2022, 4, 22, 00, 00, 00));
            list.Add(new DateTime(2022, 4, 24, 00, 00, 00));
            list.Add(new DateTime(2022, 4, 25, 00, 00, 00));
            list.Add(new DateTime(2022, 4, 27, 00, 00, 00));
            list.Add(new DateTime(2022, 4, 29, 00, 00, 00));

            list.Add(new DateTime(2022, 5, 16, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 18, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 20, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 22, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 23, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 25, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 27, 00, 00, 00));
            list.Add(new DateTime(2022, 5, 29, 00, 00, 00));

            return list;
        }
        private List<DateTime> GetAllSeancesForSabre()
        {
            List<DateTime> list = new List<DateTime>() { };
            list.Add(new DateTime(2021, 10, 25, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 27, 00, 00, 00));
            list.Add(new DateTime(2021, 10, 29, 00, 00, 00));
            

            list.Add(new DateTime(2021, 11, 8, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 10, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 12, 00, 00, 00));
            list.Add(new DateTime(2021, 11, 14, 00, 00, 00));

            list.Add(new DateTime(2022, 1, 17, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 19, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 21, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 23, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 24, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 26, 00, 00, 00));
            list.Add(new DateTime(2022, 1, 28, 00, 00, 00));

            list.Add(new DateTime(2022, 3, 21, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 23, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 25, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 27, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 28, 00, 00, 00));
            list.Add(new DateTime(2022, 3, 30, 00, 00, 00));

            list.Add(new DateTime(2022, 4, 1, 00, 00, 00));

            list.Add(new DateTime(2022, 5, 30, 00, 00, 00));

            list.Add(new DateTime(2022, 6, 1, 00, 00, 00));
            list.Add(new DateTime(2022, 6, 3, 00, 00, 00));
            list.Add(new DateTime(2022, 6, 8, 00, 00, 00));
            list.Add(new DateTime(2022, 6, 10, 00, 00, 00));
            list.Add(new DateTime(2022, 6, 12, 00, 00, 00));

            return list;
        }

    }
    public class DateArmes
    {
        public DateTime dateOfDay { get; set; }
        public string NummerOfDay { get; set; }
        public TypeArme ArmeManieeOfDay { get; set; }
        public DayOfWeek JourneeOfTheDay { get; set; }
        public bool ISDateOfDay { get; set; }
        public string ClassOfCell { get; set; }
    }
    public class LaSemaine
    {
        public DateArmes PremierJour { get; set; }
        public DateArmes SecondJour { get; set; }
        public DateArmes TroisièmeJour { get; set; }
        public DateArmes QuatrièmeJour { get; set; }
        public DateArmes CinquièmeJour { get; set; }
        public DateArmes SixièmeJour { get; set; }
        public DateArmes SeptièmeJour { get; set; }
    }

    public enum TypeArme
    {
        None = 0, Fleuret = 1, Epée = 2, Sabre = 3
    }
}
