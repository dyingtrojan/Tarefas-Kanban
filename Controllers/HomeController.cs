using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tarefas_Kanban.Data;
using Tarefas_Kanban.Models;

namespace Tarefas_Kanban.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var atividades = _context.Atividade.Include(a => a.usuario).ToList();
            return View(atividades);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        public IActionResult AlterarStatus(int id, string status)
        {
            var atividade = _context.Atividade.Find(id);
            if (atividade == null) return NotFound();

            atividade.status = status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}