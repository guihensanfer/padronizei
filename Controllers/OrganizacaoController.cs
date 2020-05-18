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
    public class OrganizacaoController : Controller
    {
        private readonly AplicacaoDbContext _context;

        public OrganizacaoController(AplicacaoDbContext context)
        {
            _context = context;
        }

        // GET: Organizacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizacoes.ToListAsync());
        }

        // GET: Organizacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizacao == null)
            {
                return NotFound();
            }

            return View(organizacao);
        }

        // GET: Organizacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CNPJ,DataCriacao")] Organizacao organizacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizacao);
        }

        // GET: Organizacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes.FindAsync(id);
            if (organizacao == null)
            {
                return NotFound();
            }
            return View(organizacao);
        }

        // POST: Organizacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CNPJ,DataCriacao")] Organizacao organizacao)
        {
            if (id != organizacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizacaoExists(organizacao.Id))
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
            return View(organizacao);
        }

        // GET: Organizacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizacao == null)
            {
                return NotFound();
            }

            return View(organizacao);
        }

        // POST: Organizacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizacao = await _context.Organizacoes.FindAsync(id);
            _context.Organizacoes.Remove(organizacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizacaoExists(int id)
        {
            return _context.Organizacoes.Any(e => e.Id == id);
        }
    }
}
