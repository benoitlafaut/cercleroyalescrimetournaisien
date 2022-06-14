using System.Collections.Generic;

namespace CercleRoyalEscrimeTournaisien.Models
{
    public class ModelViewBooks
    {
        public ModelViewBooks()
        {            
            OneBook = new List<ModelViewOneBook>();
            NomAuteur = string.Empty;
        }
        
        public List<ModelViewOneBook> OneBook { get; set; }
        public string NomAuteur { get; set; }
    }
    public class ModelViewOneBook
    {
        
        public string Livre { get; set; }
        public string Link { get; set; }
        public ModelViewOneBook()
        {
            Link = string.Empty;
            Livre = string.Empty;
           
        }
    }
}