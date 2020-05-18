using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Padronizei.Models;

namespace Padronizei.Controllers
{
    public class GrupoTrabalhoController : Controller
    {
        private readonly AplicacaoDbContext _context;

        public GrupoTrabalhoController(AplicacaoDbContext context)
        {
            _context = context;
        }

        // GET: GrupoTrabalho
        public async Task<IActionResult> Index()
        {
            return View(await _context.GruposTrabalhos.ToListAsync());
        }

        // GET: GrupoTrabalho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupoTrabalho = await _context.GruposTrabalhos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupoTrabalho == null)
            {
                return NotFound();
            }

            return View(grupoTrabalho);
        }

        // GET: GrupoTrabalho/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrupoTrabalho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,DataCriacao,OrganizacaoId,ColaboradorId")] GrupoTrabalho grupoTrabalho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupoTrabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupoTrabalho);
        }

        // GET: GrupoTrabalho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupoTrabalho = await _context.GruposTrabalhos.FindAsync(id);
            if (grupoTrabalho == null)
            {
                return NotFound();
            }
            return View(grupoTrabalho);
        }

        // POST: GrupoTrabalho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,DataCriacao,OrganizacaoId,ColaboradorId")] GrupoTrabalho grupoTrabalho)
        {
            if (id != grupoTrabalho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupoTrabalho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoTrabalhoExists(grupoTrabalho.Id))
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
            return View(grupoTrabalho);
        }

        // GET: GrupoTrabalho/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupoTrabalho = await _context.GruposTrabalhos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupoTrabalho == null)
            {
                return NotFound();
            }

            return View(grupoTrabalho);
        }

        // POST: GrupoTrabalho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupoTrabalho = await _context.GruposTrabalhos.FindAsync(id);
            _context.GruposTrabalhos.Remove(grupoTrabalho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoTrabalhoExists(int id)
        {
            return _context.GruposTrabalhos.Any(e => e.Id == id);
        }
    }
}
