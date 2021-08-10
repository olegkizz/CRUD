using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityNLayer.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        public TeachersController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<TeacherModel>(_teacherService.GetAll()));
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
        public async Task<IActionResult> Create([Bind("Id,Name,Email,LinkToProfile,Bio")] TeacherModel teacher)
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

            TeacherModel teacher = _mapper.Map<TeacherModel>(_teacherService.GetById((int)id));
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,LinkToProfile,Bio")] TeacherModel teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _teacherService.Update(_mapper.Map<Teacher>(teacher));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View();
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeacherModel teacher = _mapper.Map<TeacherModel>(_teacherService.GetById((int)id));
            if (teacher == null)
            {
                return NotFound();
            }

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

        private bool TeacherExists(int id)
        {
            return true;
        }
    }
}
