using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IndecommPOC.Models;
using IndecommPOC.BL;

namespace IndecommPOC.Controllers
{
    public class PropertiesController : Controller
    {

        private IBLProperties _IBLProperties;

        public PropertiesController()
        {
            _IBLProperties = BLProperties.GetInstance;

        }


        // GET: Properties
        public ActionResult Index()
        {

            var properties = _IBLProperties.GetPropertiesfromService();

            TempData["Properties"] = properties;           

            return View(properties);
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ViewModelProperties> viewModelProperties =(List<ViewModelProperties>)TempData.Peek("Properties");

            ViewModelProperties property = (from propertyvalue in viewModelProperties
                            where propertyvalue.PropertyId == id
                            select propertyvalue).FirstOrDefault();
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }
        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ViewModelProperties> viewModelProperties = (List<ViewModelProperties>)TempData.Peek("Properties");
            ViewModelProperties property = (from propertyvalue in viewModelProperties
                                            where propertyvalue.PropertyId == id
                                            select propertyvalue).FirstOrDefault();
            
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }



        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PropertyId,YearBuilt,ListPrice,MonthlyRent,GrossYield,Address1,Address2,City,Country,District,State,Zip,ZipPlus4")] ViewModelProperties property)
        {
            if (ModelState.IsValid)
            {
                _IBLProperties.UpdateProperty(property);
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PropertyId,YearBuilt,ListPrice,MonthlyRent,GrossYield,Address1,Address2,City,Country,District,State,Zip,ZipPlus4")] ViewModelProperties property)
        {
            if (ModelState.IsValid)
            {
                _IBLProperties.SaveProperty(property);

                return RedirectToAction("Index");
            }

            return View(property);
        }
        // GET: Properties/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ViewModelProperties> viewModelProperties = (List<ViewModelProperties>)TempData.Peek("Properties");
            ViewModelProperties property = (from propertyvalue in viewModelProperties
                                            where propertyvalue.PropertyId == id
                                            select propertyvalue).FirstOrDefault();
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            List<ViewModelProperties> viewModelProperties = (List<ViewModelProperties>)TempData.Peek("Properties");
            ViewModelProperties property = (from propertyvalue in viewModelProperties
                                            where propertyvalue.PropertyId == id
                                            select propertyvalue).FirstOrDefault();
            _IBLProperties.DeleteProperty(property);

            return RedirectToAction("Index");
        }       


    }
}
