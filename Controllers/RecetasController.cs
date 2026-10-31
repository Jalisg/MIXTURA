using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIXTURAA.Data;
using MIXTURAA.Models;

namespace MIXTURAA.Controllers
{
    public class RecetasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recetas/MisRecetas
        public async Task<IActionResult> MisRecetas()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            var misRecetas = await _context.Recetas
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Ingrediente)
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return View(misRecetas);
        }

        // GET: Recetas/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            var receta = await _context.Recetas
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioId == usuarioId);

            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // GET: Recetas/Comunidad
        public async Task<IActionResult> Comunidad(string? buscar)
        {
            var query = _context.Recetas
                .Include(r => r.Creador)
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Ingrediente)
                .Where(r => r.EsPublica);

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                query = query.Where(r => r.Nombre.Contains(buscar));
                ViewBag.BusquedaActual = buscar;
            }

            var recetasPublicas = await query.OrderByDescending(r => r.Id).ToListAsync();

            return View(recetasPublicas);
        }

        // GET: Recetas/DetalleComunidad/5
        public async Task<IActionResult> DetalleComunidad(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.Creador)
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id && r.EsPublica);

            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // GET: Recetas/Crear
        public async Task<IActionResult> Crear()
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            ViewBag.Ingredientes = await _context.Ingredientes.ToListAsync();
            return View();
        }

        // GET: Recetas/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            var receta = await _context.Recetas
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioId == usuarioId);

            if (receta == null)
            {
                return NotFound();
            }

            ViewBag.Ingredientes = await _context.Ingredientes.ToListAsync();
            return View(receta);
        }

        // POST: Recetas/ActualizarReceta (AJAX)
        [HttpPost]
        public async Task<IActionResult> ActualizarReceta([FromBody] RecetaEditDTO dto)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { success = false, message = "Debe iniciar sesión para realizar cambios." });
            }

            var recetaExistente = await _context.Recetas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.Id == dto.Id && r.UsuarioId == usuarioId);

            if (recetaExistente == null)
            {
                return Json(new { success = false, message = "La receta no fue encontrada o no tiene permisos." });
            }

            try
            {
                // Actualizar encabezado
                recetaExistente.Nombre = dto.Nombre;
                recetaExistente.Molde = dto.Molde;
                recetaExistente.PesoFinal = dto.PesoFinal;
                recetaExistente.CostoTotalProduccion = dto.CostoTotalProduccion;
                recetaExistente.EsPublica = dto.EsPublica;

                // Remover ingredientes antiguos y agregar los nuevos
                _context.DetallesReceta.RemoveRange(recetaExistente.Detalles);

                foreach (var item in dto.Ingredientes)
                {
                    var detalle = new DetalleReceta
                    {
                        RecetaId = recetaExistente.Id,
                        IngredienteId = item.IngredienteId,
                        CantidadRequerida = item.Cantidad
                    };
                    _context.DetallesReceta.Add(detalle);
                }

                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Receta actualizada con éxito." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar: " + ex.Message });
            }
        }

        // GET: Recetas/Eliminar/5 (Carga la confirmación)
        public async Task<IActionResult> Eliminar(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            var receta = await _context.Recetas
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioId == usuarioId);

            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        // POST: Recetas/EliminarConfirmado/5
        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Accesos");
            }

            var receta = await _context.Recetas
                .Include(r => r.Detalles)
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioId == usuarioId);

            if (receta != null)
            {
                _context.DetallesReceta.RemoveRange(receta.Detalles);
                _context.Recetas.Remove(receta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MisRecetas));
        }

        // POST: Recetas/GuardarReceta
        [HttpPost]
        public async Task<IActionResult> GuardarReceta([FromBody] RecetaDTO dto)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { success = false, message = "Debe iniciar sesión para guardar una receta." });
            }

            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || dto.Ingredientes == null || !dto.Ingredientes.Any())
            {
                return Json(new { success = false, message = "Proporcione un nombre y al menos un ingrediente." });
            }

            try
            {
                var nuevaReceta = new Receta
                {
                    Nombre = dto.Nombre,
                    Molde = dto.Molde,
                    PesoFinal = dto.PesoFinal,
                    CostoTotalProduccion = dto.CostoTotalProduccion,
                    EsPublica = dto.EsPublica,
                    UsuarioId = usuarioId.Value
                };

                _context.Recetas.Add(nuevaReceta);
                await _context.SaveChangesAsync();

                foreach (var item in dto.Ingredientes)
                {
                    var detalle = new DetalleReceta
                    {
                        RecetaId = nuevaReceta.Id,
                        IngredienteId = item.IngredienteId,
                        CantidadRequerida = item.Cantidad
                    };

                    _context.DetallesReceta.Add(detalle);
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Receta guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al guardar: " + ex.Message });
            }
        }
    }

    public class RecetaDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Molde { get; set; }
        public decimal PesoFinal { get; set; }
        public decimal CostoTotalProduccion { get; set; }
        public bool EsPublica { get; set; }
        public List<DetalleDTO> Ingredientes { get; set; } = new List<DetalleDTO>();
    }

    public class RecetaEditDTO : RecetaDTO
    {
        public int Id { get; set; }
    }

    public class DetalleDTO
    {
        public int IngredienteId { get; set; }
        public decimal Cantidad { get; set; }
    }
}