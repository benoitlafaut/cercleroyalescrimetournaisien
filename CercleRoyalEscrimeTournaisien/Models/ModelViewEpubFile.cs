using System.Collections.Generic;
using System.Linq;

namespace CercleRoyalEscrimeTournaisien.Models
{
    public class ModelViewEpubFile
    {        
        public ModelViewEpubFile()
        {
            RowsEpub = new List<string>() { };
        }
        public List<string> RowsEpub { get; set; }
        public List<string> RowsEpubToShowTranslated { get; set; }
        public List<string> RowsEpubToShow 
        {
            get
            {
                if (RowsEpub.Count == 0) { return new List<string>(); }
                List<string> newRowsEpub =  RowsEpub.GetRange(CurrentLigne,  NombreDeLignesToSelect);

                for (int i = 0; i < newRowsEpub.Count; i++)
                {
                    newRowsEpub[i] = newRowsEpub[i].Replace("«", "").Replace("»", "").Replace("—", "");                    
                }

                //RowsEpubToShowTranslated = new List<string>() { "", "", "", "", "", "", "", "" };
                 RowsEpubToShowTranslated = Enumerable.Repeat("", newRowsEpub.Count).ToList();

                return newRowsEpub;
            }
        }
        public List<ModelViewBooks> ListeBooks { get { return GetListeBooks(); } }
        public int LayoutPosition { get; set; }
        public int CurrentLigne { get; set; }
        public int CountRowsEpub { get { return RowsEpub.Count(); }}
        public bool IsToListen { get; set; }
        public bool IsToListenOnlyOtherLanguage { get; set; }
        public bool IsChangeLanguageEnglish { get; set; }
        public bool IsChangeLanguageAllemand { get; set; }
        public bool IsChangeLanguageEspagnol { get; set; }
        public bool IsChangeLanguageNeerlandais { get; set; }
        public int NombreDeLignesToSelect
        {
            get
            {
                switch (LayoutPosition)
                {
                    case 2:
                        return 10;
                    case 1:
                        return 10;
                    default:
                        return 4;
                }
            }
        }
        private List<ModelViewBooks> GetListeBooks()
        {
            List<ModelViewOneBook> Books_Stendhal = new List<ModelViewOneBook>() 
            {
                new ModelViewOneBook() { Link= "https://www.vousnousils.fr/casden/epub/id00203.epub", Livre="le rouge et le noir"},
            };

            List<ModelViewOneBook> Books_Tolstoi = new List<ModelViewOneBook>()
            {
                new ModelViewOneBook() {Link= "https://www.vousnousils.fr/casden/epub/id00511.epub", Livre="Guerre et paix"},
            };

            List<ModelViewBooks> ListeBooks = new List<ModelViewBooks>
            {
                new ModelViewBooks() {  NomAuteur = "Stendhal",OneBook = Books_Stendhal },
                new ModelViewBooks() { NomAuteur = "Tolstoi", OneBook = Books_Tolstoi }
            };
            return ListeBooks;
        }

        public bool IsVisible_PartChargerFileDIV { get; set; }
        public bool IsVisible_PartButtonsDIV { get; set; }
        public bool IsVisible_PartSliderDIV { get; set; }
        public bool IsVisible_PartFrenchDIV { get; set; }
        public bool IsVisible_PartChangeLanguageDIV { get; set; }
        public bool IsVisible_PartTranslateDIV { get; set; }
    }
}