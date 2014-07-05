using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModQ.Models;

namespace ModQ.Controllers
{
    public class QuizCRUDController : Controller
    {
        private QuizDBContext db = new QuizDBContext();

        // GET: QuizCRUD
        public ActionResult Index()
        {
            return View(db.QuizModels.ToList());
        }

        // GET: QuizCRUD/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizModel quizModel = db.QuizModels.Find(id);
            if (quizModel == null)
            {
                return HttpNotFound();
            }
            return View(quizModel);
        }

        // GET: QuizCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizCRUD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Faculty,Module,Question,FirstOption,SecondOption,ThirdOption,FourthOption")] QuizModel quizModel)
        {
            if (ModelState.IsValid)
            {
                db.QuizModels.Add(quizModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quizModel);
        }

        // GET: QuizCRUD/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizModel quizModel = db.QuizModels.Find(id);
            if (quizModel == null)
            {
                return HttpNotFound();
            }
            return View(quizModel);
        }

        // POST: QuizCRUD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Faculty,Module,Question,FirstOption,SecondOption,ThirdOption,FourthOption")] QuizModel quizModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quizModel);
        }

        // GET: QuizCRUD/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizModel quizModel = db.QuizModels.Find(id);
            if (quizModel == null)
            {
                return HttpNotFound();
            }
            return View(quizModel);
        }

        // POST: QuizCRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizModel quizModel = db.QuizModels.Find(id);
            db.QuizModels.Remove(quizModel);
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
