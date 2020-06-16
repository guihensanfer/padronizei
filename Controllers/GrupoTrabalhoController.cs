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
        private OrganizacaoController organizacaoController;
        private ColaboradorController colaboradorController;

        public GrupoTrabalhoController(AplicacaoDbContext context)
        {
            _context = context;
            organizacaoController = new OrganizacaoController(context);
            colaboradorController = new ColaboradorController(context, null);
        }

        // GET: GrupoTrabalho
        public async Task<IActionResult> Index()
        {
            var grupos = await _context.GruposTrabalhos
                .Include(x => x.Colaborador)
                .Include(x => x.Organizacao)
                .ToListAsync();

            return View(grupos);
        }

        // GET: GrupoTrabalho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupoTrabalho = await _context.GruposTrabalhos
                .Include(x => x.Colaborador)
                .Include(x => x.Organizacao)
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
            ViewBag.ListaOrganizacoes = new SelectList(organizacaoController.ObterOrganizacoes(true), "Id", "Nome");
            ViewBag.ListaColaboradores = new SelectList(colaboradorController.ObterColaboradores(true), "Id", "Nome");

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
                grupoTrabalho.DataCriacao = DateTime.Now;

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

            ViewBag.ListaOrganizacoes = new SelectList(organizacaoController.ObterOrganizacoes(true), "Id", "Nome");
            ViewBag.ListaColaboradores = new SelectList(colaboradorController.ObterColaboradores(true), "Id", "Nome");

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

                    // Desabilita a alteração deste campo na edição                    
                    _context.Entry(grupoTrabalho).Property(x => x.DataCriacao).IsModified = false;

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
                .Include(x => x.Organizacao)
                .Include(x => x.Colaborador)
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
