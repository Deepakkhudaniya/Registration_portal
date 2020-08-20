using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Registration_Portal.Models;

namespace Registration_Portal.Controllers
{
    public class registerController : Controller
    {
        private Students_DataEntities db = new Students_DataEntities();

        // GET: register
        public ActionResult Index()
        {
            var registers = db.registers.Include(r => r.course).Include(r => r.school);
            return View(registers.ToList());
        }

        // GET: register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: register/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.courses, "course_id", "Course1");
            ViewBag.school_id = new SelectList(db.schools, "school_id", "school_name");
            return View();
        }

        // POST: register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "register_id,Name,Address,course_id,school_id,telno")] register register)
        {
            if (ModelState.IsValid)
            {
                db.registers.Add(register);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_id = new SelectList(db.courses, "course_id", "Course1", register.course_id);
            ViewBag.school_id = new SelectList(db.schools, "school_id", "school_name", register.school_id);
            return View(register);
        }

        // GET: register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.courses, "course_id", "Course1", register.course_id);
            ViewBag.school_id = new SelectList(db.schools, "school_id", "school_name", register.school_id);
            return View(register);
        }

        // POST: register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "register_id,Name,Address,course_id,school_id,telno")] register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.courses, "course_id", "Course1", register.course_id);
            ViewBag.school_id = new SelectList(db.schools, "school_id", "school_name", register.school_id);
            return View(register);
        }

        // GET: register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            register register = db.registers.Find(id);
            db.registers.Remove(register);
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
