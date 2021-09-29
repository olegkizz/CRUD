using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using IdentityNLayer.Filters;

namespace IdentityNLayer.Controllers
{
    [TypeFilter(typeof(GlobalExceptionFilter))]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IGroupLessonService _groupLessonService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<Person> _userManager;


        public GroupsController(IGroupService groupService, IMapper mapper,
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            ITeacherService teacherService,
            UserManager<Person> userManager,
            ILogger<CoursesController> logger,
            IGroupLessonService groupLessonService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _teacherService = teacherService;
            _groupLessonService = groupLessonService;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
                return View(_mapper.Map<IEnumerable<GroupModel>>(await _groupService.GetAllAsync()));
            return View(_mapper.Map<IEnumerable<GroupModel>>(await _groupService.GetGroupsByUserIdAsync(_userManager.GetUserId(User))));
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id)));
        }

        // GET: Groups/Create
        [Authorize(Roles = "Admin, Manager")]

        public IActionResult Create(int courseId)
        {
            GroupModel group = new();
            group.CourseId = courseId;
            return View(group);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([Bind("Id,Number,Status,StudentRequests,CourseId,TeacherId")] GroupModel group)
        {
            if (ModelState.IsValid)
            {
                int groupId = await _groupService.CreateAsync(_mapper.Map<Group>(group));
                if (group.StudentRequests != null)
                    foreach (StudentRequestsModel studentRequest in group.StudentRequests)
                    {
                        if (studentRequest.Applied)
                            _enrollmentService.Enrol(studentRequest.UserId, groupId, UserRoles.Student);
                    }
                if (group.TeacherId != null)
                    _enrollmentService.Enrol((await _teacherService.GetByIdAsync((int)group.TeacherId)).UserId, groupId, UserRoles.Teacher);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupModel group = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id));
            IEnumerable<Student> students = _groupService.GetStudents(group.Id);
            group.SetStudents(_mapper.Map<IEnumerable<StudentModel>>(students), _groupService);
            ViewBag.CountStudentRequest = students.Count();
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,StudentRequests,Status,TeacherId,CourseId")] GroupModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Teacher currentTeacher = _groupService.GetCurrentTeacher(group.Id);

                    switch (group.Status)
                    {
                        case GroupStatus.Cancelled:
                            group = _mapper.Map<GroupModel>(await _groupService.CancelGroupAsync(group.Id));
                            break;
                        case GroupStatus.Finished:
                            group = _mapper.Map<GroupModel>(await _groupService.FinishGroupAsync(group.Id));
                            break;
                    }
                    _groupService.UpdateAsync(_mapper.Map<Group>(group));

                    if (group.StudentRequests != null) foreach (StudentRequestsModel studentRequest in group.StudentRequests)
                        {
                            if (studentRequest.Applied)
                            {
                                if (!_groupService.HasStudent(group.Id, studentRequest.UserId))
                                    _enrollmentService.Enrol(studentRequest.UserId, group.Id, UserRoles.Student);
                            }
                            else _enrollmentService.UnEnrol(studentRequest.UserId, group.Id);
                        }
                    if (group.TeacherId != null)
                    {
                        Teacher newTeacher = await _teacherService.GetByIdAsync((int)group.TeacherId);

                        if (currentTeacher != null)
                        {
                            if (currentTeacher.Id != group.TeacherId)
                            {
                                _enrollmentService.UnEnrol(currentTeacher.UserId, group.Id);
                                _enrollmentService.Enrol(newTeacher.UserId, group.Id, UserRoles.Teacher);
                            }
                        }
                        else _enrollmentService.Enrol(newTeacher.UserId, group.Id, UserRoles.Teacher);
                    }
                    else if (currentTeacher != null)
                        _enrollmentService.UnEnrol(currentTeacher.UserId, group.Id);
                   
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!GroupExists(group.Id))
                    {
                        _logger.LogError("Group with id=" + group.Id + " not found");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
/*        public async Task<IActionResult> StartGroup(int groupId)
        {
            Group group = await _groupService.GetByIdAsync(groupId);
            if (group == null)
                return NotFound();

            group.Status = GroupStatus.Started;
            _groupService.UpdateAsync(group);

            return RedirectToAction("Details", new { id = groupId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CancelGroup(int groupId)
        {
            Group group = await _groupService.GetByIdAsync(groupId);
            if (group == null)
                return NotFound();
            if (group.Status != GroupStatus.Started)
                return BadRequest();
            _groupService.CancelGroupAsync(groupId);

            return RedirectToAction("Details", new { id = groupId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> FinishGroup(int groupId)
        {
            GroupModel group = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync(groupId));
            if (group == null)
                return NotFound();
            if (group.Status != GroupStatus.Started)
                return BadRequest();
            _groupService.FinishGroupAsync(groupId);

            return RedirectToAction("Details", new { id = groupId });
        }*/
        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupModel teacher = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id));
            if (teacher == null)
            {
                return NotFound();
            }

            return View();
        }

        private bool GroupExists(int id)
        {
            return true;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyStartGroup(GroupStatus status, int id)
        {
            if (id == 0)
                return Json(true);
            Group group = await _groupService.GetByIdAsync(id);
            List<GroupLesson> groupLessons = await _groupLessonService.GetLessonsByGroupIdAsync(id);
            foreach (GroupLesson groupLesson in groupLessons)
            {
                if (groupLesson.StartDate < DateTime.Now && status == GroupStatus.Started && group.Status != GroupStatus.Started)
                    return Json("One Or More Of The Lessons Have StartDate Less Than Now.");
            }
            return !(await _groupLessonService.GetLessonsByGroupIdAsync(id)).Any()
                && status == GroupStatus.Started ? Json("To Start Group Please Add Lessons.")
                : Json(true);
        }
    }
}
