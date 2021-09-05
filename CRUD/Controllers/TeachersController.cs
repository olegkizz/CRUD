using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IdentityNLayer.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        UserManager<IdentityUser> _userManager;
        private readonly IEnrollmentService _enrollmentService;

        public TeachersController(ITeacherService teacherService,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IEnrollmentService enrollmentService)
        {
            _teacherService = teacherService;
            _mapper = mapper;
            _userManager = userManager;
            _enrollmentService = enrollmentService;


        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<TeacherModel>(_teacherService.GetAll()));
        }
        public IActionResult SendRequest(int courseId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendRequest(int courseId, string request)
        {
            if (request == "Yes")
            {
                _enrollmentService.Enrol(_userManager.GetUserId(User), courseId, UserRoles.Teacher, false);
            }

            return Redirect("/Courses/Index");
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,LinkToProfile,Bio,BirthDate")] TeacherModel teacher, int courseId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                IdentityResult result = await _userManager.AddToRoleAsync(user, UserRoles.Teacher.ToString());
                teacher.User = user;
                int newTeacherId = _teacherService.Create(_mapper.Map<Teacher>(teacher));
                if (courseId != 0)
                    return RedirectToAction("SendRequest", new { courseId });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        // GET: Teachers/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,LinkToProfile,Bio")] TeacherModel teacher)
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
