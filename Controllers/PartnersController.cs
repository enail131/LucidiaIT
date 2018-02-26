using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LucidiaIT.Models;
using LucidiaIT.Models.PartnerModels;
using Microsoft.AspNetCore.Http;
using LucidiaIT.Services;
using LucidiaIT.Interfaces;

namespace LucidiaIT.Controllers
{
    public class PartnersController : Controller
    {
        private readonly PartnerContext _context;
        private readonly IUploadImage _uploadImage;
        private readonly IEmailSender _emailSender;
        private readonly IMessageBuilder _messageBuilder;

        public PartnersController(
            PartnerContext context,
            IUploadImage uploadImage,
            IEmailSender emailSender,
            IMessageBuilder messageBuilder)
        {
            _context = context;
            _uploadImage = uploadImage;
            _emailSender = emailSender;
            _messageBuilder = messageBuilder;
        }

        public async Task<IActionResult> Partners()
        {
            return View(await _context.Partner.ToListAsync());
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partner.ToListAsync());
        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .SingleOrDefaultAsync(m => m.ID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,URL,Logo")] Partner partner, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _uploadImage.UploadPartnerImages(partner, files);
                    _context.Add(partner);
                    await _context.SaveChangesAsync();
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

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.SingleOrDefaultAsync(m => m.ID == id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,URL,Logo")] Partner partner)
        {
            if (id != partner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(partner);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .SingleOrDefaultAsync(m => m.ID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partner.SingleOrDefaultAsync(m => m.ID == id);
            _context.Partner.Remove(partner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.Partner.Any(e => e.ID == id);
        }
    }
}
