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
        private readonly UserManager<Person> _userManager;
        public StudentsController(IStudentService studentService,
            IGroupService groupService,
            IMapper mapper,
            ILogger<StudentsController> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<Person> userManager,
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
        public IActionResult SendRequest()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SendRequest(int courseId, string request)
        {
            if (request == "Yes")
            {
                if (!(await _studentService.HasAccount(_userManager.GetUserId(User))))
                    return RedirectToAction("Create", new { courseId });
                await _enrollmentService.EnrolInCourse(_userManager.GetUserId(User), courseId, UserRole.Student);
            }

            return Redirect("/Courses/Index");
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(int courseId)
        {
            await _enrollmentService.CancelRequest(_userManager.GetUserId(User), courseId);

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
        public async Task<IActionResult> Create([Bind("Type")] StudentModel student, int? courseId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Person user = await _userManager.GetUserAsync(User);
                    student.User = user;
                    int newStudentId = await _studentService.CreateAsync(_mapper.Map<Student>(student));
                    if (newStudentId >= 0)
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
                        if (courseId != null)
                            return RedirectToAction("SendRequest", new { courseId });
                        return RedirectToAction("Index", "Courses", new { area = "Courses" });
                    }
                    else throw new DbUpdateConcurrencyException();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if ((await _studentService.GetByIdAsync(student.Id)) == null)
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
            if (id == null)
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                    id = (await _studentService.GetByUserId(_userManager.GetUserId(User))).Id;
                else return BadRequest();
            } else
            {
                if(!User.IsInRole("Admin") && !User.IsInRole("Manager"))
                {
                    int currentStudentId = (await _studentService.GetByUserId(_userManager.GetUserId(User))).Id;
                    if (id != currentStudentId)
                        return RedirectToAction("Edit", new { currentStudentId });
                }
            }
    
            if (id == null)
            {
                return NotFound();
            }

            StudentModel student = _mapper.Map<StudentModel>(await _studentService.GetByIdAsync((int)id));

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,User")] StudentModel student)
        {
            if ((await _studentService.GetByIdAsync(id)).UserId != _userManager.GetUserId(User)
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
                    await _studentService.UpdateAsync(_mapper.Map<Student>(student));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (await _studentService.GetByIdAsync(student.Id) == null)
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
                if (User.IsInRole("Admin") && User.IsInRole("Manager"))
                    return RedirectToAction(nameof(Index));
                else return RedirectToAction("Index", "Courses");
            }
            return View(student);
        }
      
        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentModel student = _mapper.Map<StudentModel>(await _studentService.GetByIdAsync((int)id));
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
