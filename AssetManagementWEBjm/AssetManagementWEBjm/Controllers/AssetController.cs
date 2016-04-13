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

namespace AssetManagementWEBjm.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        // GET: Asset/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            JohaMeriSQL1Entities entities = new JohaMeriSQL1Entities();
            try
            {
                List<AssetLocation> assets = entities.AssetLocation.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta

                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (AssetLocation asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + ": " + asset.Asset.Model;
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
                List<AssetLocation> assets = entities.AssetLocation.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (AssetLocation asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + ": " + asset.Asset.Model;
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
                //haetaan ensin paikan id-numero koodin perusteella
                int locationId = (from l in entities.AssetLocation
                    where l.Code == inputData.LocationCode
                    select l.Id).FirstOrDefault();

                //haetaan laitteen id-numero koodin perusteella
                int assetId = (from a in entities.Assets
                                  where a.Code == inputData.AssetCode
                                  select a.Id).FirstOrDefault();

                if ((locationId > 0) && (assetId > 0))
                {
                    //tallennetaan uusi rivi aikaleiman kanssa kantaan

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

        // POST: Asset/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Asset/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asset/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asset/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
