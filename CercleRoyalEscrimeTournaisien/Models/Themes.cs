using System;

namespace CercleRoyalEscrimeTournaisien.Models
{
    public class clsThemes
    {
        public clsThemes()
        {
            Theme = string.Empty;
            UrlTheme = string.Empty;
            TitreTheme = string.Empty;            
            PhotoAdditionnelle = string.Empty;
        }
        public string Theme { get; set; }
        public string UrlTheme { get; set; }
        public string TitreTheme { get; set; }
        public string Image { get; set; }
        public string PhotoAdditionnelle { get; set; }
        public int Id { get; set; }
    }
}