using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tarefas_Kanban.Data;
using Tarefas_Kanban.Models;

namespace Tarefas_Kanban.Controllers
{
    public class AtividadesController : Controller
    {
        private readonly AppDbContext _context;

        public AtividadesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Atividades
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Atividade.Include(a => a.usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Atividades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Atividade == null)
            {
                return NotFound();
            }

            var atividade = await _context.Atividade
                .Include(a => a.usuario)
                .FirstOrDefaultAsync(m => m.IdAtividade == id);
            if (atividade == null)
            {
                return NotFound();
            }

            return View(atividade);
        }

        // GET: Atividades/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = new SelectList(await _context.Usuario.ToListAsync(), "Id", "nome");
            return View();
        }

        // POST: Atividades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAtividade,IdUsuario,descricao,nomeSetor,prioridade,dataCadastro,status")] Atividade atividade)
        {
            atividade.status = "A fazer";
            atividade.dataCadastro = DateTime.Today;
            ModelState.Remove(nameof(Atividade.status));
            ModelState.Remove(nameof(Atividade.usuario));
            if (ModelState.IsValid)
            {
                _context.Add(atividade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Usuarios = new SelectList(await _context.Usuario.ToListAsync(), "Id", "nome", atividade.IdUsuario);
            return View(atividade);
        }

        // GET: Atividades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Atividade == null)
            {
                return NotFound();
            }

            var atividade = await _context.Atividade.FindAsync(id);
            if (atividade == null)
            {
                return NotFound();
            }
            ViewBag.Usuarios = new SelectList(await _context.Usuario.ToListAsync(), "Id", "nome", atividade.IdUsuario);
            return View(atividade);
        }

        // POST: Atividades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAtividade,IdUsuario,descricao,nomeSetor,prioridade,dataCadastro,status")] Atividade atividade)
        {
            ModelState.Remove(nameof(Atividade.status));
            ModelState.Remove(nameof(Atividade.usuario));
            if (id != atividade.IdAtividade)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atividade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtividadeExists(atividade.IdAtividade))
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
            ViewBag.Usuarios = new SelectList(await _context.Usuario.ToListAsync(), "Id", "nome", atividade.IdUsuario);
            return View(atividade);
        }

        // GET: Atividades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Atividade == null)
            {
                return NotFound();
            }

            var atividade = await _context.Atividade
                .Include(a => a.usuario)
                .FirstOrDefaultAsync(m => m.IdAtividade == id);
            if (atividade == null)
            {
                return NotFound();
            }

            return View(atividade);
        }

        // POST: Atividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Atividade == null)
            {
                return Problem("Entity set 'AppDbContext.Atividade'  is null.");
            }
            var atividade = await _context.Atividade.FindAsync(id);
            if (atividade != null)
            {
                _context.Atividade.Remove(atividade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtividadeExists(int id)
        {
          return (_context.Atividade?.Any(e => e.IdAtividade == id)).GetValueOrDefault();
        }
    }
}
