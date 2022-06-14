using System.Collections.Generic;

namespace CercleRoyalEscrimeTournaisien
{
    public class QuelquesSites
    {
       
        public static string ListeDesQuelquesSites
        {
            get {
                string ListeDesQuelquesSites = string.Empty;

                List<string> ListeOfLink = new List<string>();

                ListeOfLink.Add(@"<a target=""_blank"" href=https://ffceb.org/>La Ligue d'escrime belge</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/>Académie Royale d'Armes de Belgique (ARAB)</a>");
                ListeOfLink.Add(@"</br><a target=""_blank"">Théorie sur le fleuret</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/fleuretElem.pdf>Fleuret élémentaire</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/fleuret1.pdf>Fleuret 1er degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/fleuret2.pdf>Fleuret 2ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/fleuret3.pdf>Fleuret 3ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/fleuret4.pdf>Fleuret 4ème degré</a>");
                ListeOfLink.Add(@"</br><a target=""_blank"">Théorie sur l'épée</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/epeeElem.pdf>Epée élémentaire</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/epee1.pdf>Epée 1er degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/epee2.pdf>Epée 2ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/epee3.pdf>Epée 3ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/epee4.pdf>Epée 4ème degré</a>");
                ListeOfLink.Add(@"</br><a target=""_blank"">Théorie sur le sabre</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/sabreElem.pdf>Sabre élémentaire</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/sabre1.pdf>Sabre 1er degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/sabre2.pdf>Sabre 2ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/sabre3.pdf>Sabre 3ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=http://www.arab-ksab.be/Documents/fr/sabre4.pdf>Sabre 4ème degré</a>");
                ListeOfLink.Add(@"<a target=""_blank""></a>");
                ListeOfLink.Add(@"<a target=""_blank"" href=https://ffceb.org/les-clubs/liste-des-clubs/>Les Clubs en Wallonie</a>");



                foreach (string link in ListeOfLink)
                {
                    if (link.IndexOf(@"href") == -1)
                    {
                        ListeDesQuelquesSites += link;
                    }
                    else
                    {
                        ListeDesQuelquesSites += "<li>" + link + "</li>";
                    }
                    
                }
                return ListeDesQuelquesSites;
            }           
        }
    }
    
}