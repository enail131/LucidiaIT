using LucidiaIT.Interfaces;
using LucidiaIT.Models;
using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LucidiaIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService<Partner> _context;

        public HomeController(IDataService<Partner> context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.GetListAsync());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
