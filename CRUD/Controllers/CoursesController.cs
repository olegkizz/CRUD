using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using IdentityNLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using IdentityNLayer.Core.Filters;

namespace IdentityNLayer.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<Person> _userManager;
        private readonly IEnrollmentService _enrollmentService;

        public CoursesController(ApplicationContext context, 
            ICourseService courseService, 
            IStudentService studentService, 
            IMapper mapper,
            ILogger<CoursesController> logger,
            UserManager<Person> userManager,
            IEnrollmentService enrollmentService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _enrollmentService = enrollmentService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CourseModel>>(await _courseService.GetAllAsync()));
        }

        // GET: Course
        public async Task<IActionResult> Details(int id)
        {
            return View(_mapper.Map<CourseModel>(await _courseService.GetByIdAsync(id)));
        }

        public async Task<IActionResult> Search(string search)
        {
            return View("Index", _mapper.Map<IEnumerable<CourseModel>>(await _courseService.Search(search)));
        }
        public async Task<IActionResult> Filter(CourseModel courseModel)
        {
            return View("Index", _mapper.Map<IEnumerable<CourseModel>>
                (await _courseService.Filter(_mapper.Map<CourseFilter>(courseModel.CourseFilter))));
        }
        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,TopicId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        [Authorize(Roles="Admin")]
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CourseModel course = _mapper.Map<CourseModel>(await _courseService.GetByIdAsync((int)id));
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Program,TopicId,StartDate,Updated")] CourseModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateAsync(_mapper.Map<Course>(course));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (await _courseService.GetByIdAsync(id) != null)
                    {
                        _logger.LogError("Course with id=" + course.Id + " not found");
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
            return View(course);
        }
    }
}
