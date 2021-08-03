using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNLayer.BLL.DTO;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.DAL.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.Controllers
{
    public class GroupsController : Controller
    {
        
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(_groupService.GetAll());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
                        var contact = await _context.Contact
                            .FirstOrDefaultAsync(m => m.ContactId == id);
                        if (contact == null)
                        {
                            return NotFound();
                        }
            */
            return View();
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Name,Address,City,State,Zip,Email")] GroupDTO group)
        {
            /*      if (ModelState.IsValid)
                  {
                      _context.Add(contact);
                      await _context.SaveChangesAsync();
                      return RedirectToAction(nameof(Index));
                  }*/
            return View();
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*      var contact = await _context.Contact.FindAsync(id);
                  if (contact == null)
                  {
                      return NotFound();
                  }*/
            return View();
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,Address,City,State,Zip,Email")] GroupDTO group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }
            /*
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(contact);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                if (!ContactExists(contact.ContactId))
                                {
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            return RedirectToAction(nameof(Index));
                        }*/
            return View();
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*        var contact = await _context.Contact
                        .FirstOrDefaultAsync(m => m.ContactId == id);
                    if (contact == null)
                    {
                        return NotFound();
                    }
        */
            return View();
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*           var contact = await _context.Contact.FindAsync(id);
                       _context.Contact.Remove(contact);
                       await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return true;
        }
    }
}
