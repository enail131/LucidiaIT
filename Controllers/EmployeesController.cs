using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models;
using LucidiaIT.Models.EmployeeModels;
using Microsoft.AspNetCore.Http;
using LucidiaIT.Interfaces;

namespace LucidiaIT.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IDataService<Employee> _context;
        private readonly IUploadImage _uploadImage;
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;

        public EmployeesController(
            IDataService<Employee> context, 
            IUploadImage uploadImage, 
            IEmailSender emailSender,
            IMessageBuilder messageBuilder)
        {
            _context = context;
            _uploadImage = uploadImage;
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetListAsync());
        }

        public async Task<IActionResult> About()
        {
            return View(await _context.GetListAsync());
        }

        // GET: Employees/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Title,Description,InitialImage,HoverImage")] Employee employee, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _uploadImage.UploadEmployeeImages(employee, files);
                    await _context.CreateAsync(employee);
                    return PartialView("~/Views/Shared/_CreateSuccessful.cshtml");
                }
                catch (Exception e)
                {
                    _emailSender.SendEmail(_messageBuilder.BuildErrorMessage(e));
                    return PartialView("~/Views/Shared/_CreateFailed.cshtml");
                }
                
            }
            return PartialView("~/Views/Shared/_CreateFailed.cshtml");
        }

        

        // GET: Employees/Edit/5
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
        
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Title,Description,InitialImage,HoverImage")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.GetDataObjectAsync(id);
            await _context.DeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.DataObjectExists(id);
        }
    }
}
