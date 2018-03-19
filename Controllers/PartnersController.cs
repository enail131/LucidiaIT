using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using LucidiaIT.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace LucidiaIT.Controllers
{
    [Authorize]
    public class PartnersController : Controller
    {
        private readonly IDataService<Partner> _context;
        private readonly IStorageService _storage;
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IConfiguration _config;

        public PartnersController(
            IDataService<Partner> context,
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

        [AllowAnonymous]
        public async Task<IActionResult> Partners()
        {
            List<Partner> partnerList = await _context.GetListAsync();
            partnerList.Sort((p, q) => p.Name.CompareTo(q.Name));
            return View(partnerList);
        }

        // GET: Partners
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetListAsync());
        }

        // GET: Partners/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.GetDataObjectAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partners/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,URL,Logo")] Partner partner, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, null, partner);
                    await _context.CreateAsync(partner);
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

        // GET: Partners/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.GetDataObjectAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,URL,Logo")] Partner partner, IEnumerable<IFormFile> files)
        {
            if (id != partner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _storage.UploadImages(files, null, partner);
                    await _context.EditAsync(partner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Url = "/" });
            }
            return View(partner);
        }

        // GET: Partners/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.GetDataObjectAsync(id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.GetDataObjectAsync(id);
            await _storage.DeleteImages(_config["StorageSettings:PartnersContainer"], partner.Logo);
            await _context.DeleteAsync(partner);
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.DataObjectExists(id);
        }
    }
}
