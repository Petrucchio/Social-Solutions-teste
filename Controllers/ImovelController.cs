using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Social_solution_teste.Data;
using Social_solution_teste.Models;

namespace Social_solution_teste.Controllers
{
    public class ImovelController : Controller
    {
        private readonly SolutionDbContext _context;

        public ImovelController(SolutionDbContext context)
        {
            _context = context;
        }

        // GET: Imovels
        public async Task<IActionResult> Index()
        {
              return _context.Imoveis != null ? 
                          View(await _context.Imoveis.ToListAsync()) :
                          Problem("Entity set 'SolutionDbContext.Imoveis'  is null.");
        }

        // GET: Imovels/Create
        public IActionResult Create()
        {
            var clientes = _context.Clientes.Where(p => p.Cliente_Ativo == true);
            ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");
            return View();
        }

        // POST: Imovels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cliente_id,Tipo_de_negocio,Valor_imovel,Descricao,Ativo")] Imovel imovel)
        {
            if (ModelState.IsValid)
            {
                //if (imovel.Cliente_id != 0)
                //{
                //    imovel.cliente = await _context.Clientes.FindAsync(imovel.Cliente_id);
                //}
                
                _context.Add(imovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else if(imovel.Cliente_id == 0)
            {
                _context.Add(imovel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var clientes = _context.Clientes.Where(p => p.Cliente_Ativo == true);
            ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");
            return View(imovel);
        }

        // GET: Imovels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Imoveis == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel == null)
            {
                return NotFound();
            }
            var clientes = _context.Clientes.Where(p => p.Cliente_Ativo == true);
            ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");
            return View(imovel);
        }

        // POST: Imovels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cliente_id,Tipo_de_negocio,Valor_imovel,Descricao,Ativo")] Imovel imovel)
        {
            if (id != imovel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(imovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImovelExists(imovel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var clientes = _context.Clientes.Where(p => p.Cliente_Ativo == true);
            ViewData["ClienteId"] = new SelectList(clientes, "Id", "Nome");
            return View(imovel);
        }

        // GET: Imovels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Imoveis == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Imoveis == null)
            {
                return Problem("Entity set 'SolutionDbContext.Imoveis'  is null.");
            }
            var imovel = await _context.Imoveis.FindAsync(id);

            if (imovel != null)
            {
                if(imovel.Cliente_id == 0)
                {
                    var cliente = await _context.Clientes.FindAsync(0);
                    _context.Imoveis.Remove(imovel);
                }
                else
                {
                    return BadRequest("o imovel não pode ser excluido por estar associado a um cliente");
                }
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImovelExists(int id)
        {
          return (_context.Imoveis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
