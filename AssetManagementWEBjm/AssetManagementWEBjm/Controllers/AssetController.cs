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
        List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

        JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                CultureInfo fiFi = new CultureInfo("fi-FI");

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.LocationAdress = asset.AssetLocation.Adress;
                    view.AssetCode = asset.Assets.Code;
                    view.AssetName = asset.Assets.Type + ": " + asset.Assets.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);
                    //view.LastSeen = asset.LastSeen;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
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
                    view.LocationAdress = asset.AssetLocation.Adress;
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

                CultureInfo fiFi = new CultureInfo("fi-FI");
                
                // muodostetaan näkymämalli tietokannan rivien pohjalta
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


        // GET: Asset/CreateAssets
        public ActionResult CreateAssets()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            AssetsViewModel model = new AssetsViewModel();

            return View(model);
        }//create


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssets(AssetsViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            Assets asset = new Assets();
            asset.Code = model.AssetCode;
            asset.Type = model.AssetType;
            asset.Model = model.AssetModel;
            
            db.Assets.Add(asset);

            //AssetLocation ass = new AssetLocation();
            //ass.Code = model.LocationCode;
            //ass.Name = model.LocationName;
            //ass.Adress = model.LocationAdress;

            //db.AssetLocation.Add(ass);

            //int assetId = int.Parse(model.AssetCode);
            //if (assetId > 0)
            //{
            //    Assets asse = db.Assets.Find(assetId);
            //    ass.AssetId = asse.Id;
            //}

            //int assettypId = int.Parse(model.AssetType);
            //if (assetId > 0)
            //{
            //    Assets asse = db.Assets.Find(assettypId);
            //    ass.AssetId = asse.Id;
            //}

            //int assetmodId = int.Parse(model.AssetModel);
            //if (assetId > 0)
            //{
            //    Assets asse = db.Assets.Find(assetmodId);
            //    ass.AssetId = asse.Id;
            //}

            //int locationId = int.Parse(model.LocationCode);
            //if (locationId > 0)
            //{
            //    AssetLocation alo = db.AssetLocation.Find(locationId);
            //    asset.Id = alo.Id;
            //}

            //int locationcodId = int.Parse(model.LocationName);
            //if (locationcodId > 0)
            //{
            //    AssetLocation asse = db.AssetLocation.Find(locationcodId);
            //    asset.Id = asse.Id;
            //}

            //int locationadId = int.Parse(model.LocationAdress);
            //if (locationadId > 0)
            //{
            //    AssetLocation asse = db.AssetLocation.Find(locationadId);
            //    asset.Id = asse.Id;
            //}

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("List");
        }//create


        // GET: Asset/CreateAssetLocation
     
        public ActionResult CreateAssetLocation()
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            AssetLocationViewModel model = new AssetLocationViewModel();

            return View(model);
        }//create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAssetLocation(AssetLocationViewModel model)
        {
            JohaMeriSQL1Entities db = new JohaMeriSQL1Entities();

            AssetLocation asseloc = new AssetLocation();
            asseloc.Code = model.LocationCode;
            asseloc.Name = model.LocationName;
            asseloc.Adress = model.LocationAdress;

            db.AssetLocation.Add(asseloc);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("AssetLocationList");
        }//create



        public ActionResult AssetsList()
        {
            List<AssetsViewModel> model = new List<AssetsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<Assets> assets = entities.Assets.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (Assets asset in assets)
                {
                    AssetsViewModel view = new AssetsViewModel();
                    view.AssetsId = asset.Id;
                    view.AssetCode = asset.Code;
                    view.AssetModel = asset.Model;
                    view.AssetType = asset.Type;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }

        public ActionResult AssetLocationList()
        {
            List<AssetLocationViewModel> model = new List<AssetLocationViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<AssetLocation> assloca = entities.AssetLocation.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                foreach (AssetLocation aslo in assloca)
                {
                    AssetLocationViewModel view = new AssetLocationViewModel();
                    view.AssetLocationId = aslo.Id;
                    view.LocationCode = aslo.Code;
                    view.LocationName = aslo.Name;
                    view.LocationAdress = aslo.Adress;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }
    }
}

