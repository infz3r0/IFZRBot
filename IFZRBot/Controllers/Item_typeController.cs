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
    public class Item_typeController : Controller
    {
        private ifzrtempEntities db = new ifzrtempEntities();

        // GET: Item_type
        public ActionResult Index()
        {
            return View(db.Item_type.ToList());
        }

        // GET: Item_type/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_type item_type = db.Item_type.Find(id);
            if (item_type == null)
            {
                return HttpNotFound();
            }
            return View(item_type);
        }

        // GET: Item_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "item_type_id,item_type_name")] Item_type item_type)
        {
            if (ModelState.IsValid)
            {
                db.Item_type.Add(item_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item_type);
        }

        // GET: Item_type/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_type item_type = db.Item_type.Find(id);
            if (item_type == null)
            {
                return HttpNotFound();
            }
            return View(item_type);
        }

        // POST: Item_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "item_type_id,item_type_name")] Item_type item_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item_type);
        }

        // GET: Item_type/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_type item_type = db.Item_type.Find(id);
            if (item_type == null)
            {
                return HttpNotFound();
            }
            return View(item_type);
        }

        // POST: Item_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item_type item_type = db.Item_type.Find(id);
            db.Item_type.Remove(item_type);
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
