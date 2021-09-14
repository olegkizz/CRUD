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
 /*       public async Task<IActionResult> Create([Bind("Id,Title,Description,Program,TopicId,StartDate,Updated")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Create(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        } */
        public async Task<IActionResult> CreateAsync([Bind("Id,Title,Description,Program,TopicId,StartDate,Updated")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        [Authorize(Roles="Admin, Manager")]
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
                    _courseService.UpdateAsync(_mapper.Map<Course>(course));
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
     /*       ViewBag.Topics = _courseService.GetAvailableTopics();
            ViewBag.Requests = _courseService.GetStudentRequests(id);*/
            return View(course);
        }

        // GET: Courses/Delete/5
        /*        public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var course = await _context.Courses
                        .Include(c => c.Topic)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (course == null)
                    {
                        return NotFound();
                    }

                    return View(course);
                }*/

        // POST: Courses/Delete/5
   /*     [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
    }
}
