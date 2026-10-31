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
                    // Guardar la sesión activa del usuario
                    HttpContext.Session.SetInt32("UsuarioId", usuario.Id);

                    return RedirectToAction("Index", "Ingredientes");
                }

                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            }

            return View(model);
        }

        // GET: Accesos/Registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Accesos/Registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe
                var existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);
                if (existeCorreo)
                {
                    ModelState.AddModelError("Correo", "El correo ya se encuentra registrado.");
                    return View(usuario);
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        // GET: Accesos/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}