using Microsoft.AspNetCore.Mvc;
using Tarea4SeleniumApp.ViewModels;
using Tarea4SeleniumApp.Data;

namespace Tarea4SeleniumApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        // El sistema inyecta automáticamente el repositorio gracias a lo que pusimos en Program.cs
        public AccountController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Llamamos a nuestra capa de datos limpia
                bool esValido = _usuarioRepository.ValidarCredenciales(model);

                if (esValido)
                {
                    // Si es exitoso, redirigimos al CRUD de Videojuegos (que crearemos en el próximo paso)
                    return RedirectToAction("Index", "Videojuegos");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                }
            }

            return View(model);
        }
    }
}