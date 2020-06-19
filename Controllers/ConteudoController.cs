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
    public class ConteudoController : Controller
    {
        private readonly AplicacaoDbContext _context;
        private readonly DepartamentoController departamentoController;
        private readonly ColaboradorController colaboradorController;
        public IConfiguration Configuration { get; }

        public ConteudoController(AplicacaoDbContext context, IConfiguration configuration)
        {
            _context = context;
            departamentoController = new DepartamentoController(_context);
            colaboradorController = new ColaboradorController(_context, null);
            Configuration = configuration;
        }

        // GET: Conteudo
        public async Task<IActionResult> Index(int page = 1, string termo = null)
        {       
            int paginacaoPadrao = Configuration.GetValue<int>("ParametrosPadroesProjeto:QuantidadeItensListadosPaginacao");
            var conteudos = _context.Conteudos
                .Include(x => x.Departamento)
                .Include(x => x.Colaborador)
                .AsNoTracking();

            // Filtra por termo
            if(!string.IsNullOrEmpty(termo))
                conteudos = conteudos
                    .Where(c => c.Titulo.Contains(termo) || c.Corpo.Contains(termo));            

            // Por fim, gera uma lista ordenada para ser paginada
            var resultante = conteudos                
                .OrderByDescending(x => x.Visibilidade)
                .ThenByDescending(x => x.DataCriacao);                                
                
            return View(await PagingList.CreateAsync(resultante, paginacaoPadrao, page));            
        }

        // GET: Conteudo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos
                .Include(x => x.Colaborador)
                .Include(x => x.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conteudo == null)
            {
                return NotFound();
            }

            return View(conteudo);
        }

        // GET: Conteudo/Create
        public IActionResult Create()
        {
            ViewBag.ListaDepartamentos = new SelectList(departamentoController.ObterDepartamentos(true), "Id", "Nome");
            ViewBag.ListaColaboradores = new SelectList(colaboradorController.ObterColaboradores(true), "Id", "Nome");            

            return View();
        }

        // POST: Conteudo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Corpo,Visibilidade,DataCriacao,ColaboradorId,DepartamentoId")] Conteudo conteudo)
        {
            if (ModelState.IsValid)
            {
                conteudo.DataCriacao = DateTime.Now;

                _context.Add(conteudo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conteudo);
        }

        // GET: Conteudo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos.FindAsync(id);
            if (conteudo == null)
            {
                return NotFound();
            }

            ViewBag.ListaDepartamentos = new SelectList(departamentoController.ObterDepartamentos(true), "Id", "Nome");
            ViewBag.ListaColaboradores = new SelectList(colaboradorController.ObterColaboradores(true), "Id", "Nome");

            return View(conteudo);
        }

        // POST: Conteudo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Corpo,Visibilidade,DataCriacao,ColaboradorId,DepartamentoId")] Conteudo conteudo)
        {
            if (id != conteudo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conteudo);

                    // Desabilita a alteração deste campo na edição                    
                    _context.Entry(conteudo).Property(x => x.DataCriacao).IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConteudoExists(conteudo.Id))
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
            return View(conteudo);
        }

        // GET: Conteudo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conteudo = await _context.Conteudos
                .Include(x => x.Colaborador)
                .Include(x => x.Departamento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (conteudo == null)
            {
                return NotFound();
            }

            return View(conteudo);
        }

        // POST: Conteudo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conteudo = await _context.Conteudos.FindAsync(id);
            _context.Conteudos.Remove(conteudo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConteudoExists(int id)
        {
            return _context.Conteudos.Any(e => e.Id == id);
        }
    }
}
