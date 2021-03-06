using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Padronizei.Models;

namespace Padronizei.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly AplicacaoDbContext _context;
        private OrganizacaoController organizacaoController;

        public DepartamentoController(AplicacaoDbContext context)
        {
            _context = context;
            organizacaoController = new OrganizacaoController(context);
        }

        public List<Departamento> ObterDepartamentos(bool listaParaSelectField = false)
        {
            List<Departamento> resultante = _context.Departamentos.ToList();

            if(listaParaSelectField)
                resultante.Insert(0, new Departamento(){
                    Id = 0,
                    Nome = "Selecione um departamento"                    
                });

            return resultante;
        }

        // GET: Departamento
        public async Task<IActionResult> Index()
        {            
            var departamentos = _context.Departamentos
                .Include(x => x.Organizacao)                
                .AsNoTracking();

            return View(await departamentos.ToListAsync());
        }

        // GET: Departamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(x => x.Organizacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            ViewBag.ListaOrganizacoes = new SelectList(organizacaoController.ObterOrganizacoes(true), "Id", "Nome");

            return View();
        }

        // POST: Departamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,OrganizacaoId")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(x => x.Organizacao)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
                
            if (departamento == null)
            {
                return NotFound();
            }

            ViewBag.ListaOrganizacoes = new SelectList(organizacaoController.ObterOrganizacoes(true), "Id", "Nome");
            
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,OrganizacaoId")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.Id))
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
            return View(departamento);
        }

        // GET: Departamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .Include(x => x.Organizacao)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.Id == id);
        }
    }
}
