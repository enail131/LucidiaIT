using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models.EmployeeModels;
using Microsoft.AspNetCore.Http;
using LucidiaIT.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace LucidiaIT.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IDataService<Employee> _context;
        private readonly IStorageService _storage;
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IConfiguration _config;

        public EmployeesController(
            IDataService<Employee> context, 
            IStorageService storage, 
            IEmailSender emailSender,
            IMessageBuilder messageBuilder,
            IConfiguration config)
        {
            _context = context;
            _storage = storage;
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
            _config = config;
        }

        [Route("About")]
        [AllowAnonymous]
        public async Task<IActionResult> About()
        {
            return View(await _context.GetListAsync());
        }

        // GET: Employees
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetListAsync());
        }

        // GET: Employees/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.GetDataObjectAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Title,Description,InitialImage,HoverImage")] Employee employee, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, employee, null, null);
                    await _context.CreateAsync(employee);
                    return PartialView("~/Views/Shared/_CreateSuccessful.cshtml");
                }
                catch (Exception e)
                {
                    await _emailSender.SendEmail(_messageBuilder.BuildErrorMessage(e));
                    return PartialView("~/Views/Shared/_CreateFailed.cshtml");
                }
                
            }
            return PartialView("~/Views/Shared/_CreateFailed.cshtml");
        }



        // GET: Employees/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.GetDataObjectAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Title,Description,InitialImage,HoverImage")] Employee employee, IEnumerable<IFormFile> files)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, employee, null, null);
                    await _context.EditAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Url = "/About" });
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.GetDataObjectAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await _context.GetDataObjectAsync(id);
            string containerReference = _config["StorageSettings:EmployeeContainer"];
            await _storage.DeleteImages(containerReference, employee.InitialImage);
            await _storage.DeleteImages(containerReference, employee.HoverImage);
            await _context.DeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.DataObjectExists(id);
        }
    }
}
