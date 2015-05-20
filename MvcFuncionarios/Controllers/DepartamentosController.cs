using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFuncionarios.Models;
using MvcFuncionarios.DataContext;

namespace MvcFuncionarios.Controllers
{
    public class DepartamentosController : Controller
    {
        private FuncionariosDataContext db = new FuncionariosDataContext();

        //
        // GET: /Departamentos/

        public ActionResult Index()
        {
            return View(db.Departamentos.ToList());
        }

       

        //
        // GET: /Departamentos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Departamentos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Departamentos.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamento);
        }

        //
        // GET: /Departamentos/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewData.Model = departamento;
            return View(departamento);
        }

        //
        // POST: /Departamentos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        //
        // GET: /Departamentos/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // POST: /Departamentos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamentos.Find(id);
            db.Departamentos.Remove(departamento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}