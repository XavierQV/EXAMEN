using EXAMEN.Data;
using EXAMEN.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T_vehiculos.Models;

namespace EXAMEN.Controllers
{
    public class TipoVehiculosController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public TipoVehiculosController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public void Combox()
        {
            ViewData["CodigoVehiculo"] = new SelectList(_Context.Vehiculos.Select (q => new ViewModelTipoVehiculo
            {
                Codigo=q.Codigo,
                Descripcion=q.Nombre,
                Estado=q.Estado
            }).Where(w => w.Estado ==1).ToList(), "Codigo", "Nombre") ;
        }
        [Authorize(Roles = "Mecanico,Ayudante")]
        // GET: TipoVehiculosController1
        public ActionResult Index()
        {
            List<ViewModelTipoVehiculo> ltsTpovehiculo = _Context.TipoVehiculos.Select(q => new ViewModelTipoVehiculo
            {
                Numerovehiculo=q.CodigoVehiculo,
                Codigo = q.Codigo,
                Descripcion = q.CodigoVehiculoNavigation.Nombre,
                Estado = q.Estado
            }).Where(w => w.Estado ==1).ToList() ;

            
            return View(ltsTpovehiculo);
        }
        [Authorize(Roles = "Mecanico,Ayudante")]
        // GET: TipoVehiculosController1/Details/5
        public ActionResult Details(int id)
        {
            TipoVehiculo tipovehiculo = _Context.TipoVehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            return View(tipovehiculo);
        }
        [Authorize(Roles = "Mecanico")]
        // GET: TipoVehiculosController1/Create
        public ActionResult Create()
        {
            Combox();
            return View();
        }

        // POST: TipoVehiculosController1/Create
        [Authorize(Roles = "Mecanico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVehiculo tipoVehiculo)
        {
            try
            {
                tipoVehiculo.Estado = 1;
                _Context.Add(tipoVehiculo);
                _Context.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch
            {
                Combox();
                return View(tipoVehiculo);
            }
        }
        [Authorize(Roles = "Mecanico")]
        // GET: TipoVehiculosController1/Edit/5
        public ActionResult Edit(int id)
        {
            Combox();
            TipoVehiculo tipovehiculo = _Context.TipoVehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            return View(tipovehiculo);
        }
        [Authorize(Roles = "Mecanico")]
        // POST: TipoVehiculosController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.Codigo)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _Context.Update(tipoVehiculo);
                _Context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                Combox();
                return View();
            }
        }

        // GET: TipoVehiculosController1/Delete/5
        [Authorize(Roles = "Mecanico")]
        public IActionResult Desactivar(int id)
        {
            TipoVehiculo tipovehiculo = _Context.TipoVehiculos .Where(q => q.Codigo == id).FirstOrDefault();
            tipovehiculo.Estado = 0;
            _Context.Update(tipovehiculo);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Mecanico")]
        public IActionResult Activar(int id)
        {

            TipoVehiculo tipovehiculo = _Context.TipoVehiculos.Where(q => q.Codigo == id).FirstOrDefault();
            tipovehiculo.Estado = 0;
            _Context.Update(tipovehiculo);
            _Context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
