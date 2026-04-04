using Microsoft.AspNetCore.Mvc;
using Tarea4SeleniumApp.Data;
using Tarea4SeleniumApp.Models;

namespace Tarea4SeleniumApp.Controllers
{
    public class VideojuegosController : Controller
    {
        private readonly VideojuegoRepository _repo;

        public VideojuegosController(VideojuegoRepository repo)
        {
            _repo = repo;
        }

        // GET: Videojuegos (Lista de juegos)
        public IActionResult Index()
        {
            var lista = _repo.ObtenerTodos();
            return View(lista);
        }

        // GET: Videojuegos/Create (Muestra formulario)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videojuegos/Create (Guarda en BD)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Videojuego juego)
        {
            if (ModelState.IsValid)
            {
                _repo.Agregar(juego);
                return RedirectToAction("Index");
            }
            return View(juego);
        }

        // GET: Videojuegos/Edit/5 (Muestra formulario con datos cargados)
        public IActionResult Edit(int id)
        {
            var juego = _repo.ObtenerPorId(id);
            if (juego.Id == 0) return NotFound();
            return View(juego);
        }

        // POST: Videojuegos/Edit/5 (Actualiza en BD)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Videojuego juego)
        {
            if (ModelState.IsValid)
            {
                _repo.Actualizar(juego);
                return RedirectToAction("Index");
            }
            return View(juego);
        }

        // GET: Videojuegos/Delete/5 (Pantalla de confirmación)
        public IActionResult Delete(int id)
        {
            var juego = _repo.ObtenerPorId(id);
            if (juego.Id == 0) return NotFound();
            return View(juego);
        }

        // POST: Videojuegos/Delete/5 (Elimina en BD)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}