using EXAMEN.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_vehiculos.Models;

namespace EXAMEN.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public VehiculosController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        // GET: VehiculosController1
        public ActionResult Index()
        {
            List<Vehiculo> ltsvehiculo = _Context.Vehiculos.ToList();
            
            return View(ltsvehiculo);
        }

        // GET: VehiculosController1/Details/5
        public ActionResult Details(int id)
        {
            Vehiculo vehiculo = _Context.Vehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }

        // GET: VehiculosController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculosController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo)
        {
            try
            {
                vehiculo.Estado = 1;
                _Context.Add(vehiculo);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(vehiculo);
            }
        }

        // GET: VehiculosController1/Edit/5
        public ActionResult Edit(int id)
        {
            Vehiculo vehiculo = _Context.Vehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            return View(vehiculo);
        }

        // POST: VehiculosController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Vehiculo vehiculo)
        {
            if(id != vehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _Context.Update(vehiculo);
                _Context.SaveChanges();
                return RedirectToAction("Index");
               
            }
            catch
            {
                return View();
            }
        }

        // GET: VehiculosController1/activar//desactivar/5
        public IActionResult Desactivar(int id)
        {
            Vehiculo vehiculo = _Context.Vehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 0;
            _Context.Update(vehiculo);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Activar(int id)
        {
            Vehiculo vehiculo = _Context.Vehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            vehiculo.Estado = 1;
            _Context.Update(vehiculo);
            _Context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
