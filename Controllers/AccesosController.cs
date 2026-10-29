using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIXTURAA.Data;
using MIXTURAA.Models;

namespace MIXTURAA.Controllers
{
    public class AccesosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accesos/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Accesos/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == model.Correo && u.Contrasena == model.Contrasena);

                if (usuario != null)
                {
                    // Se guarda la sesión activa del usuario para autenticación
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id);

                    return RedirectToAction("Index", "Ingredientes");
                }

                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            }

            return View(model);
        }

        // GET: Accesos/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}