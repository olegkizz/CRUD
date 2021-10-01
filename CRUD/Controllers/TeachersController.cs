using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace IdentityNLayer.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        UserManager<Person> _userManager;
        private readonly IEnrollmentService _enrollmentService;

        public TeachersController(ITeacherService teacherService,
            IMapper mapper,
            ILogger<TeachersController> logger,
            UserManager<Person> userManager,
            IEnrollmentService enrollmentService)
        {
            _teacherService = teacherService;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _enrollmentService = enrollmentService;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<TeacherModel>>(await _teacherService.GetAllAsync()));
        }
        public IActionResult SendRequest(int courseId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendRequest(int courseId, string request)
        {
            if (request == "Yes")
            {
                if (!await _teacherService.HasAccount(_userManager.GetUserId(User)))
                    return RedirectToAction("Create", new { courseId });
                await _enrollmentService.EnrolInCourse(_userManager.GetUserId(User), courseId, UserRoles.Teacher);
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
            return View(await _teacherService.GetByIdAsync((int)id));
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
        public async Task<IActionResult> Create([Bind("Id,LinkToProfile,Bio,User")] TeacherModel teacher, int? courseId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);

                    IdentityResult result = await _userManager.AddToRoleAsync(user, UserRoles.Teacher.ToString());
                    teacher.User = user;
                    int newTeacherId = await _teacherService.CreateAsync(_mapper.Map<Teacher>(teacher));
                    if (courseId != null)
                        return RedirectToAction("SendRequest", new { courseId });
                    return RedirectToAction("Index", "Courses", new { area = "Courses" });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (await _teacherService.GetByIdAsync(teacher.Id) == null)
                    {
                        _logger.LogError("Teacher with id=" + teacher.Id + " not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                    id = (await _teacherService.GetByUserId(_userManager.GetUserId(User))).Id;
                else return BadRequest();
            }
            else
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                {
                    int currentStudentId = (await _teacherService.GetByUserId(_userManager.GetUserId(User))).Id;
                    if (id != currentStudentId)
                        return RedirectToAction("Edit", new { currentStudentId });
                }
            }

            if (id == null)
            {
                return NotFound();
            }

            TeacherModel teacher = _mapper.Map<TeacherModel>(await _teacherService.GetByIdAsync((int)id));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,LinkToProfile,Bio,User")] TeacherModel teacher)
        {
            if ((await _teacherService.GetByIdAsync(id)).UserId != _userManager.GetUserId(User)
                && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return BadRequest();

            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _teacherService.UpdateAsync(_mapper.Map<Teacher>(teacher));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (await _teacherService.GetByIdAsync(teacher.Id) == null)
                    {
                        _logger.LogError("Teacher with id=" + teacher.Id + " not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
                if (User.IsInRole("Admin") && User.IsInRole("Manager"))
                    return RedirectToAction(nameof(Index));
                else return RedirectToAction("Index", "Courses");
            }
            return View();
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeacherModel teacher = _mapper.Map<TeacherModel>(await _teacherService.GetByIdAsync((int)id));
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
    }
}
