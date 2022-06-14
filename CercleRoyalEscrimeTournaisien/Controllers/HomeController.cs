using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System;
using VersOne.Epub;
using HtmlAgilityPack;
using System.IO;
using CercleRoyalEscrimeTournaisien.Models;
using System.Web.Script.Serialization;

namespace CercleRoyalEscrimeTournaisien
{
    
    public class HomeController : Controller
    {
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult Accueil()
        {
            Accueil accueil = new Accueil();
            return View(accueil);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerAgenda()
        {
            DateListAgenda dateListAgenda = new DateListAgenda();
            List<DateArmes> getAllDays = dateListAgenda.GetAllDaysOfTheCurrentDate();
            return View(dateListAgenda.AllDatePerWeek(getAllDays).AsEnumerable());
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ChangeMoisMoins1()
        {
            Agenda.DateCurrent = Agenda.DateCurrent.AddMonths(-1);
            Agenda.DateCurrentMonth = Agenda.GetNomOfMois(Agenda.DateCurrent.Month);
            Agenda.DateCurrentYear = Agenda.DateCurrent.Year.ToString();
            return RedirectToAction("ControllerAgenda", "Home");
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ChangeMoisPlus1()
        {
            Agenda.DateCurrent = Agenda.DateCurrent.AddMonths(1);
            Agenda.DateCurrentMonth = Agenda.GetNomOfMois(Agenda.DateCurrent.Month);
            Agenda.DateCurrentYear = Agenda.DateCurrent.Year.ToString();
            return RedirectToAction("ControllerAgenda", "Home");
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerCategories()
        {
            Catégories catégories = new Catégories();
            return View(catégories);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerHistorique()
        {
            Historique historique = new Historique ();
            return View(historique);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerPhotos()
        {
            Photos photos = new Photos();
            Photos.titreDiapo = Photos.ListeDesAlbumsStatic.Where(x=>x.NumeroAlbum == Photos.NumeroAlbumCurrent).First().NomAlbum;
            return View(photos);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ChangePhotos(string  id)
        {
            Photos.NumeroAlbumCurrent = int.Parse(id);
            Photos.NumeroPhotoOfAlbumCurrent = 1;
            Photos.NombreMaxPhotosOfAlbumCurrent = Photos.GetMaxPhotosOfAlbumCurrent();
            //Photos.titreDiapo = "coucou";
            resultChangeImage();            

            return RedirectToAction("ControllerPhotos", "Home");
        }

        private JavaScriptResult resultChangeImage()
        {
            string Js = @"changeImage()";
            return JavaScript(Js);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ChangeImageOfAlbum()
        {
            return RedirectToAction("ControllerPhotos", "Home");
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerActivites()
        {
            Activites activites = new Activites();
            return View(activites);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerQuelquesSites()
        {
            QuelquesSites quelquesSites = new QuelquesSites();
            return View(quelquesSites);
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ControllerNosCours(string newSelectedIndexTheme)
        {
            NosCours nosCours = new NosCours();

            if (newSelectedIndexTheme == null) { newSelectedIndexTheme = "0"; }
            if (newSelectedIndexTheme == "-1") { newSelectedIndexTheme = (nosCours.AllTitresThemeDistinct.Count() - 1).ToString(); }
            if (Convert.ToInt32(newSelectedIndexTheme) == nosCours.AllTitresThemeDistinct.Count()) { newSelectedIndexTheme = "0"; }

            
            nosCours.SelectedIndexTheme = Convert.ToInt32(newSelectedIndexTheme);
            return View(nosCours);      
        }

        public ActionResult Epub()
        {
            ModelViewEpubFile modelViewEpubFile = new ModelViewEpubFile();
            modelViewEpubFile.IsVisible_PartChargerFileDIV = true;
            modelViewEpubFile.IsVisible_PartButtonsDIV = true;
            modelViewEpubFile.IsVisible_PartSliderDIV = true;
            modelViewEpubFile.IsVisible_PartFrenchDIV = true;
            modelViewEpubFile.IsVisible_PartChangeLanguageDIV = true;
            modelViewEpubFile.IsVisible_PartTranslateDIV = true;
            return View(modelViewEpubFile);
        }


        [HttpPost]
        public ActionResult Transform()
        {
            try
            {
                List<string> RowsToCreate = new List<string>() { };
                if (Request.Files.Count > 0)
                {
                    var _file = Request.Files[0];

                    EpubBook book = EpubReader.ReadBook(_file.InputStream);
                    foreach (EpubTextContentFile textContentFile in book.ReadingOrder)
                    {
                        PrintTextContentFile(textContentFile, RowsToCreate);
                    }
                }

                var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 900 };

                return new ContentResult()
                {
                    Content = serializer.Serialize(string.Join("||", RowsToCreate)),
                    ContentType = "application/json"
                };
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ChangeEpub(ModelViewEpubFile modelViewEpubFile, string IsToListenNew, int addLines)
        {
            if (modelViewEpubFile.RowsEpub.Count == 0)
            {
                if (Request.Files.Count > 0)
                {
                    string result = new StreamReader(Request.Files[0].InputStream).ReadToEnd();
                    string[] rowsToRead = result.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    modelViewEpubFile.RowsEpub = rowsToRead.ToList();
                    modelViewEpubFile.CurrentLigne = 1;
                    modelViewEpubFile.IsToListen = false;
                    modelViewEpubFile.IsVisible_PartChargerFileDIV = true;
                    modelViewEpubFile.IsVisible_PartButtonsDIV = true;
                    modelViewEpubFile.IsVisible_PartSliderDIV = true;
                    modelViewEpubFile.IsVisible_PartFrenchDIV = true;
                    modelViewEpubFile.IsVisible_PartChangeLanguageDIV = true;
                    modelViewEpubFile.IsVisible_PartTranslateDIV = true;
                }
            }

            if (IsToListenNew == "1") { modelViewEpubFile.IsToListen = true; }
            if (IsToListenNew == "2") { modelViewEpubFile.IsToListenOnlyOtherLanguage = true; }

            if (addLines == -5) { addLines = -1 * modelViewEpubFile.NombreDeLignesToSelect; }
            if (addLines == 5) { addLines = modelViewEpubFile.NombreDeLignesToSelect; }
            if (modelViewEpubFile.CurrentLigne + addLines < 0) { addLines = 0; }
            modelViewEpubFile.CurrentLigne = modelViewEpubFile.CurrentLigne + addLines;

            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

      

        [HttpPost]
        public JsonResult Change_PartChargerFileDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartChargerFileDIV = !modelViewEpubFile.IsVisible_PartChargerFileDIV;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Change_PartButtonsDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartButtonsDIV = !modelViewEpubFile.IsVisible_PartButtonsDIV;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Change_PartChangeLanguageDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartChangeLanguageDIV = !modelViewEpubFile.IsVisible_PartChangeLanguageDIV;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Change_PartFrenchDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartFrenchDIV = !modelViewEpubFile.IsVisible_PartFrenchDIV;

            modelViewEpubFile.LayoutPosition = 0;
            if (!modelViewEpubFile.IsVisible_PartTranslateDIV)
            {
                modelViewEpubFile.LayoutPosition = 1;
            }

            if (!modelViewEpubFile.IsVisible_PartFrenchDIV)
            {
                modelViewEpubFile.LayoutPosition = 2;
            }
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Change_PartSliderDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartSliderDIV = !modelViewEpubFile.IsVisible_PartSliderDIV;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Change_PartTranslateDIV(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsVisible_PartTranslateDIV = !modelViewEpubFile.IsVisible_PartTranslateDIV;

            if (!modelViewEpubFile.IsVisible_PartTranslateDIV)
            {
                modelViewEpubFile.IsVisible_PartChangeLanguageDIV = false;
            }

            modelViewEpubFile.LayoutPosition = 0;

            if (!modelViewEpubFile.IsVisible_PartTranslateDIV)
            {
                modelViewEpubFile.LayoutPosition = 1;
            }

            if (!modelViewEpubFile.IsVisible_PartFrenchDIV)
            {
                modelViewEpubFile.LayoutPosition = 2;
            }

            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ChangeToEnglish(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsChangeLanguageEnglish = !modelViewEpubFile.IsChangeLanguageEnglish;
            modelViewEpubFile.IsChangeLanguageNeerlandais = false;
            modelViewEpubFile.IsChangeLanguageEspagnol = false;
            modelViewEpubFile.IsChangeLanguageAllemand = false;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeToNeerlandais(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsChangeLanguageNeerlandais = !modelViewEpubFile.IsChangeLanguageNeerlandais;
            modelViewEpubFile.IsChangeLanguageEspagnol = false;
            modelViewEpubFile.IsChangeLanguageAllemand = false;
            modelViewEpubFile.IsChangeLanguageEnglish = false;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeToEspagnol(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsChangeLanguageEspagnol = !modelViewEpubFile.IsChangeLanguageEspagnol;
            modelViewEpubFile.IsChangeLanguageNeerlandais = false;
            modelViewEpubFile.IsChangeLanguageAllemand = false;
            modelViewEpubFile.IsChangeLanguageEnglish = false;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeToAllemand(ModelViewEpubFile modelViewEpubFile)
        {
            modelViewEpubFile.IsChangeLanguageAllemand = !modelViewEpubFile.IsChangeLanguageAllemand;
            modelViewEpubFile.IsChangeLanguageNeerlandais = false;
            modelViewEpubFile.IsChangeLanguageEspagnol = false;
            modelViewEpubFile.IsChangeLanguageEnglish = false;
            return Json(new { modelViewEpubFile = RenderRazorViewToString("Epub", modelViewEpubFile) }, JsonRequestBehavior.AllowGet);
        }

        #region methods private
        private string RenderRazorViewToString(string viewName, object model)

        {

            return RenderRazorViewToString(viewName, model, string.Empty);

        }
        private string RenderRazorViewToString(string viewName, object model, string Prefix)

        {

            var viewDataForPrefix = new ViewDataDictionary();



            viewDataForPrefix.TemplateInfo = new TemplateInfo { HtmlFieldPrefix = Prefix };



            return RenderRazorViewToString(viewName, model, viewDataForPrefix);

        }
        private string RenderRazorViewToString(string viewName, object model, ViewDataDictionary ViewdataDictionary)

        {

            ViewData = ViewdataDictionary;

            ViewData.Model = model;



            using (var sw = new StringWriter())

            {

                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,

                                                                         viewName);

                var viewContext = new ViewContext(ControllerContext, viewResult.View,

                                             ViewData, TempData, sw);



                viewResult.View.Render(viewContext, sw);

                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();

            }

        }
        private void PrintTextContentFile(EpubTextContentFile textContentFile, List<string> RowsToCreate)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(textContentFile.Content);

            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//text()"))
            {
                string textFormatted = GetText(node.InnerText);
                if (textFormatted != string.Empty)
                {
                    RowsToCreate.Add(textFormatted);
                }
            }
        }
        private static string GetText(string innerText)
        {
            string output = innerText;
            if (output.IndexOf("<?xml ") != -1 || output.IndexOf("@page") != -1)
            {
                output = string.Empty;
            }
            output = output.Replace("\n", " ").Replace("&lt;", "").Replace("&gt;", "");

            output = output.Trim();
            return output;
        }
        #endregion
    }
}

