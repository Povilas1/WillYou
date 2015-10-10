using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Accessor.Models;

namespace Accessor.Controllers
{
    public class ChallengesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Challenges
        public ActionResult Index()
        {
            return View(db.Challenges.ToList());
        }

        // GET: Challenges/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // GET: Challenges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Challenges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Comments,Self")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                challenge.Id = Guid.NewGuid();
                db.Challenges.Add(challenge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(challenge);
        }

        // GET: Challenges/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // POST: Challenges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Comments,Self")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(challenge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(challenge);
        }

        // GET: Challenges/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Challenge challenge = db.Challenges.Find(id);
            if (challenge == null)
            {
                return HttpNotFound();
            }
            return View(challenge);
        }

        // POST: Challenges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Challenge challenge = db.Challenges.Find(id);
            db.Challenges.Remove(challenge);
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
