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
    public class FuncionariosController : Controller
    {
        private FuncionariosDataContext db = new FuncionariosDataContext();

        //
        // GET: /Funcionarios/

        public ActionResult Index()
        {
            var funcionarios = db.Funcionarios.Include(f => f.Departamento).Where(s => s.Departamento.Status == true);
            return View(funcionarios.ToList());
        }
        [HttpGet]
        public ActionResult Index(string filtro = null, string buscatxt = null)
        {
            IQueryable<Funcionarios> Resultado;

            if (filtro == "Nome")
            {
                Resultado = (from f in db.Funcionarios
                             where f.Nome.Contains(buscatxt) && f.Departamento.Status == true
                             select f);
            }
            else if (filtro == "Email")
            {

                Resultado = (from f in db.Funcionarios
                             where f.Email.Contains(buscatxt) && f.Departamento.Status == true
                             select f);
            }
            else if (filtro == "Departamento")
            {

                Resultado = (from f in db.Funcionarios
                             join d in db.Departamentos on f.Departamento_id equals d.Id
                             where d.Nome.Contains(buscatxt) && f.Departamento.Status == true
                             select f);
            }
            else if (!String.IsNullOrEmpty(buscatxt))
            {
                Resultado = db.Funcionarios.Where(f => f.Nome.Contains(buscatxt) ||
                f.Email.Contains(buscatxt) ||
                f.Endereco.Contains(buscatxt) ||
                f.Telefone.Contains(buscatxt) ||
                f.Departamento.Nome.Contains(buscatxt) && f.Departamento.Status == true);
            }
            else
            {
                Resultado = db.Funcionarios.Include(f => f.Departamento).Where(s => s.Departamento.Status == true);
            }
            ViewBag.filtro = filtro;
            ViewBag.buscatxt = buscatxt;
            ViewData.Model = Resultado.ToList();

            return View();
        }



        //
        // GET: /Funcionarios/Create

        public ActionResult Create()
        {
            ViewBag.Departamento_id = new SelectList(db.Departamentos.Where(s => s.Status == true), "Id", "Nome");
            return View();
        }

        //
        // POST: /Funcionarios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departamento_id = new SelectList(db.Departamentos.Where(s => s.Status == true), "Id", "Nome", funcionarios.Departamento_id);
            return View(funcionarios);
        }

        //
        // GET: /Funcionarios/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            var listDepartamentos = new SelectList(db.Departamentos.Where(s => s.Status == true), "Id", "Nome", 2);
            ViewBag.Departamentoid = listDepartamentos;
            return View(funcionarios);
        }

        //
        // POST: /Funcionarios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var listDepartamentos = new SelectList(db.Departamentos.Where(s => s.Status == true), "Id", "Nome", 2);
            ViewBag.Departamentoid = listDepartamentos;
            return View(funcionarios);
        }

        //
        // GET: /Funcionarios/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            if (funcionarios == null)
            {
                return HttpNotFound();
            }
            return View(funcionarios);
        }

        //
        // POST: /Funcionarios/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionarios funcionarios = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionarios);
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