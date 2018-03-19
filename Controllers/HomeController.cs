using LucidiaIT.Interfaces;
using LucidiaIT.Models;
using LucidiaIT.Models.EmployeeModels;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            List<Partner> partnerList = await _context.GetListAsync();
            partnerList.Sort((p, q) => p.Name.CompareTo(q.Name));
            return View(partnerList);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
