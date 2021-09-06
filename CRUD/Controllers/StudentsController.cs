using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.Models;
using IdentityNLayer.BLL.Interfaces;
using AutoMapper;
using IdentityNLayer.Core.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace IdentityNLayer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public StudentsController(IStudentService studentService,
            IGroupService groupService,
            IMapper mapper,
            ILogger<StudentsController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IEnrollmentService enrollmentService,
            ICourseService courseService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Students
        [Authorize(Roles = "Admin, Manager")]

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<StudentModel>>(await _studentService.GetAllAsync()));
        }
        [HttpGet]
        public IActionResult SendRequest(int courseId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendRequest(int courseId, string request)
        {
            if (request == "Yes")
            {
                _enrollmentService.Enrol(_userManager.GetUserId(User), courseId, UserRoles.Student, false);
            }

            return Redirect("/Courses/Index");
        }

        // GET: Students/Create
        [HttpGet]

        public IActionResult Create()
        {
            StudentModel student = new StudentModel();

            return View(student);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, BirthDate, Email," +
            "Type, User")] StudentModel student, int? courseId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);

                    IdentityResult result = await _userManager.AddToRoleAsync(user, UserRoles.Student.ToString());
                    student.User = user;
                    int newStudentId = _studentService.Create(_mapper.Map<Student>(student));
                    if (courseId != 0)
                        return RedirectToAction("SendRequest", new { courseId });
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (_studentService.GetById(student.Id) == null)
                    {
                        _logger.LogError("Student with id=" + student.Id + " not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if ((await _studentService.GetByIdAsync((int)id)).UserId != _userManager.GetUserId(User)
                  && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return LocalRedirect("Edit/" + id);

            if (id == null)
            {
                return NotFound();
            }

            StudentModel student = _mapper.Map<StudentModel>(_studentService.GetById((int)id));


            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, BirthDate, User, " +
            "PhoneNumber, Type, UserId")] StudentModel student)
        {
            if ((await _studentService.GetByIdAsync((int)id)).UserId != _userManager.GetUserId(User)
                && !User.IsInRole("Admin") && !User.IsInRole("Manager"))
                return BadRequest();

            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _studentService.Update(_mapper.Map<Student>(student));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (_studentService.GetById(student.Id) == null)
                    {
                        _logger.LogError("Student with id=" + student.Id + " not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentModel student = _mapper.Map<StudentModel>(_studentService.GetById((int)id));
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _studentService.Delete((int)id);

            return RedirectToAction(nameof(Index));
        }
    }
}
