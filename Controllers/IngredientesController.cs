using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIXTURAA.Data;
using MIXTURAA.Models;

namespace MIXTURAA.Controllers
{
    public class IngredientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingredientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredientes.ToListAsync());
        }

        // GET: Ingredientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CantidadPresentacion,UnidadMedida,PrecioCompra")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Edit/5 (Carga la pantalla de edición)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null) return NotFound();

            return View(ingrediente);
        }

        // POST: Ingredientes/Edit/5 (Guarda los cambios editados)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CantidadPresentacion,UnidadMedida,PrecioCompra")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        // GET: Ingredientes/Delete/5 (Carga la confirmación de eliminación)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null) return NotFound();

            return View(ingrediente);
        }

        // POST: Ingredientes/Delete/5 (Borra físicamente de la base de datos)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente != null)
            {
                _context.Ingredientes.Remove(ingrediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }
    }
}