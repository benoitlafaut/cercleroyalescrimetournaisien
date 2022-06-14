using System.Collections.Generic;
using System.Linq;

namespace CercleRoyalEscrimeTournaisien
{
    public class Photos
    {
        public static int NumeroAlbumCurrent;
        public static int NumeroPhotoOfAlbumCurrent;
        public static int NombreMaxPhotosOfAlbumCurrent;
        public static string titreDiapo;
        public static List<Album> ListeDesAlbumsStatic;

        public List<Album> ListeDesAlbums = new List<Album>() { };
        public Photos()
        {
            ListeDesAlbums.Add(new Album { NomAlbum = "Challenge Charles Debeur", NumeroAlbum = 1 });
            ListeDesAlbums.Add(new Album { NomAlbum = "A l'entraînement (juin 2017)", NumeroAlbum = 2 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Diverses photos 2017", NumeroAlbum = 3 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Les petits et les grands en fin de saison 2017", NumeroAlbum = 4 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Compétition à Lessines le 21/10/2017", NumeroAlbum = 5 });
            ListeDesAlbums.Add(new Album { NomAlbum = "La saint-Nicolas 2017", NumeroAlbum = 6 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Gembloux février 2018", NumeroAlbum = 7 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Carnaval 2018", NumeroAlbum = 8 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Un petit hockey à Don Bosco", NumeroAlbum = 9 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Fin de saison à Saint André", NumeroAlbum = 10 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Carnaval 2019", NumeroAlbum = 11 });
            ListeDesAlbums.Add(new Album { NomAlbum = "Rencontre Maubeuge-Charleroi-Tournai 2019", NumeroAlbum = 12 });

            ListeDesAlbums = ListeDesAlbums.OrderByDescending(i => i.NumeroAlbum).ToList();
            ListeDesAlbumsStatic = ListeDesAlbums;
        }

        public static int GetMaxPhotosOfAlbumCurrent()
        {
            switch (NumeroAlbumCurrent)
            {
                case 1:
                    return 8;
                case 2:
                    return 12;
                case 3:
                    return 58;
                case 4:
                    return 173;
                case 5:
                    return 46;
                case 6:
                    return 58;
                case 7:
                    return 5;
                case 8:
                    return 48;
                case 9:
                    return 9;
                case 10:
                    return 72;
                case 11:
                    return 58;
                case 12:
                    return 26;
            }

            return 0;
        }
    }
    public class Album
    {
        public string NomAlbum { get; set; }
        public int NumeroAlbum { get; set; }
    }
}
