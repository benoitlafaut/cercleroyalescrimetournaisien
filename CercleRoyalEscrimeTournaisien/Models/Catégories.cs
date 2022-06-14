
namespace CercleRoyalEscrimeTournaisien
{
    public class Catégories
    {
        public Catégories()
        {
            ListeDesCatégories = @"<p class=""cssForTitreCategories"">Les catégories pour la saison 2021-2022</p>";
            ListeDesCatégories += @"<pre><p class=""cssForCategories"">";
            ListeDesCatégories += @"<br />" + "Catégorie - Année de naissance";
            ListeDesCatégories += @"<br />" + "        U9 - 2013 & 2014";
            ListeDesCatégories += @"<br />" + "       U11 - 2011 & 2012";
            ListeDesCatégories += @"<br />" + "       U13 - 2009 & 2010";
            ListeDesCatégories += @"<br />" + "       U15 - 2007 & 2008";
            ListeDesCatégories += @"<br />" + "       U17 - 2005 & 2006";
            ListeDesCatégories += @"<br />" + "       U20 - 2002, 2003 & 2004";
            ListeDesCatégories += @"<br />" + "   Seniors - 2001 à 1981";
            ListeDesCatégories += @"<br />" + "Vétéran 40 - De 1973 à 1982";
            ListeDesCatégories += @"<br />" + "Vétéran 50 - De 1963 à 1972";
            ListeDesCatégories += @"<br />" + "Vétéran 60 - De 1953 à 1962";
            ListeDesCatégories += @"<br />" + "Vétéran 70 - De 1952";
            ListeDesCatégories += "</P></pre>";
        }
        public static string ListeDesCatégories { get; set; }
    }
    
}