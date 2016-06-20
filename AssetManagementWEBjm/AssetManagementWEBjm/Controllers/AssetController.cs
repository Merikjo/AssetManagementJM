using AssetManagementWEBjm.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AssetManagementWEBjm.Models;
using AssetManagementWEBjm.Database;
using System.Globalization;
using AssetManagementWEBjm.Controllers;

namespace AssetManagementWEBjm.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }
        //AssetController.cs - testilaukseen luominen scriptin siirtoa varten:
        //Tämä testilauseke voidaan laittaa privaatiksi, kun kaikki on valmista:

        //Paina Test rivillä hiiren oikealla ja valitse Add View...Empty
        public ActionResult Test()

        {
            return View();
        }


        //tehdään listaus kaikista kytkennöistä
        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Assets.Code;
                    view.AssetName = asset.Assets.Type + ": " + asset.Assets.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }
        public ActionResult ListJson()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Assets.Code;
                    view.AssetName = asset.Assets.Type + ": " + asset.Assets.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
 
        [HttpPost]
        //AssetController.cs - LAITTEIDEN TALLENTAMINEN (SQL) TIETOKANTAAN
        public JsonResult AssignLocation()
        {
            string json = Request.InputStream.ReadToEnd();
            AssignLocationModel inputData = 
                JsonConvert.DeserializeObject<AssignLocationModel>(json);

            bool success = false;
            string error = "";

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();

            try
            {
                //haetaan ensin paikan id-numero koodin perusteella:
                int locationId = (from l in entities.AssetLocation
                    where l.Code == inputData.LocationCode
                    select l.Id).FirstOrDefault();

                //haetaan laitteen id-numero koodin perusteella:
                int assetId = (from a in entities.Assets
                                  where a.Code == inputData.AssetCode
                                  select a.Id).FirstOrDefault();

                if ((locationId > 0) && (assetId > 0))
                {
                    //tallennetaan uusi rivi aikaleiman kanssa kantaan:
                    AssetLocations newEntry = new AssetLocations();
                    newEntry.LocationId = locationId;
                    newEntry.AssetId = assetId;
                    newEntry.LastSeen = DateTime.Now;

                    entities.AssetLocations.Add(newEntry);
                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }

            //palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }
    }
}
