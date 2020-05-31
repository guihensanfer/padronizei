using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Padronizei.Models;
using ReflectionIT.Mvc.Paging;

namespace Padronizei.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly AplicacaoDbContext _context;
        private readonly DepartamentoController departamentoController;
        public IConfiguration Configuration { get; }

        public ColaboradorController(AplicacaoDbContext context, IConfiguration configuration)
        {
            _context = context;
            departamentoController = new DepartamentoController(context);
            Configuration = configuration;
        }

        // GET: Colaborador
        public async Task<IActionResult> Index(int page = 1, string nome = null)
        {
            int paginacaoPadrao = Configuration.GetValue<int>("ParametrosPadroesProjeto:QuantidadeItensListadosPaginacao");
            var colaboradores = _context.Colaboradores
                .Include(x => x.Departamento)
                .AsNoTracking();

            // Filtra por nome
            if(!string.IsNullOrEmpty(nome))
                colaboradores = colaboradores
                    .Where(c => c.Nome.Contains(nome));

            // Por fim, gera uma lista ordenada para ser paginada
            var resultante = colaboradores                
                .OrderByDescending(x => x.DataCriacao);                                
                
            return View(await PagingList.CreateAsync(resultante, paginacaoPadrao, page));
        }

        // GET: Colaborador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaboradores
                .Include(x => x.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }

            return View(colaborador);
        }

        // GET: Colaborador/Create
        public IActionResult Create()
        {            
            ViewBag.ListaDepartamentos = new SelectList(departamentoController.ObterDepartamentos(true), "Id", "Nome");

            return View();
        }        
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Bio,Matricula,Email,DepartamentoId")] Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                colaborador.DataCriacao = DateTime.Now;

                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(colaborador);
        }

        // GET: Colaborador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }
            
            ViewBag.ListaDepartamentos = new SelectList( departamentoController.ObterDepartamentos(true), "Id", "Nome");

            return View(colaborador);
        }

        // POST: Colaborador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Bio,Matricula,Email,DataCriacao,DepartamentoId")] Colaborador colaborador)
        {
            if (id != colaborador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                    
                    _context.Update(colaborador);

                    // Desabilita a alteração deste campo na edição                    
                    _context.Entry(colaborador).Property(x => x.DataCriacao).IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColaboradorExists(colaborador.Id))
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
            return View(colaborador);
        }

        // GET: Colaborador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colaborador = await _context.Colaboradores
                .Include(x => x.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colaborador == null)
            {
                return NotFound();
            }

            return View(colaborador);
        }

        // POST: Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColaboradorExists(int id)
        {
            return _context.Colaboradores.Any(e => e.Id == id);
        }
    }
}
