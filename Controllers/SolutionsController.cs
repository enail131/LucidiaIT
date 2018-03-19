using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models;
using LucidiaIT.Models.SolutionModels;
using LucidiaIT.Services;
using Microsoft.AspNetCore.Authorization;
using LucidiaIT.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LucidiaIT.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IDataService<Solution> _context;
        private readonly IStorageService _storage;
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;

        public SolutionsController(
            IConfiguration config,
            IDataService<Solution> context, 
            IStorageService storage,
            IEmailSender emailSender,
            IMessageBuilder messageBuilder)
        {
            _config = config;
            _context = context;
            _storage = storage;
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
        }

        [Route("Solutions")]
        [AllowAnonymous]
        public async Task<IActionResult> Solutions()
        {
            return View(await _context.GetListAsync());
        }

        // GET: Solutions
        [Authorize]
        public async Task<IActionResult> List()
        {
            return View("Index", await _context.GetListAsync());
        }

        // GET: Solutions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.GetDataObjectAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Solutions/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Solutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,SolutionImage")] Solution solution, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, null, null, solution);
                    await _context.CreateAsync(solution);
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

        // GET: Solutions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.GetDataObjectAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            return View(solution);
        }

        // POST: Solutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,SolutionImage")] Solution solution, IEnumerable<IFormFile> files)
        {
            if (id != solution.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, null, null, solution);
                    await _context.EditAsync(solution);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionExists(solution.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Url = "/Solutions" });
            }
            return View(solution);
        }

        // GET: Solutions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.GetDataObjectAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // POST: Solutions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Solution solution = await _context.GetDataObjectAsync(id);
            string containerReference = _config["StorageSettings:SolutionContainer"];
            await _storage.DeleteImages(containerReference, solution.SolutionImage);
            await _context.DeleteAsync(solution);            
            return RedirectToAction(nameof(List));
        }

        private bool SolutionExists(int id)
        {
            return _context.DataObjectExists(id);
        }
    }
}
