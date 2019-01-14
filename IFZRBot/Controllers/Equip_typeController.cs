using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IFZRBot.Models;

namespace IFZRBot.Controllers
{
    public class Equip_typeController : Controller
    {
        private ifzrtempEntities db = new ifzrtempEntities();

        // GET: Equip_type
        public ActionResult Index()
        {
            return View(db.equip_type.ToList());
        }

        // GET: Equip_type/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equip_type equip_type = db.equip_type.Find(id);
            if (equip_type == null)
            {
                return HttpNotFound();
            }
            return View(equip_type);
        }

        // GET: Equip_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equip_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "equip_type_id,equip_type_name")] equip_type equip_type)
        {
            if (ModelState.IsValid)
            {
                db.equip_type.Add(equip_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equip_type);
        }

        // GET: Equip_type/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equip_type equip_type = db.equip_type.Find(id);
            if (equip_type == null)
            {
                return HttpNotFound();
            }
            return View(equip_type);
        }

        // POST: Equip_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "equip_type_id,equip_type_name")] equip_type equip_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equip_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equip_type);
        }

        // GET: Equip_type/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equip_type equip_type = db.equip_type.Find(id);
            if (equip_type == null)
            {
                return HttpNotFound();
            }
            return View(equip_type);
        }

        // POST: Equip_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            equip_type equip_type = db.equip_type.Find(id);
            db.equip_type.Remove(equip_type);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
