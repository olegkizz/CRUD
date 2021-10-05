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
        private readonly IMethodistService _methodistService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly UserManager<Person> _userManager;


        public GroupsController(IGroupService groupService, IMapper mapper,
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            ITeacherService teacherService,
            UserManager<Person> userManager,
            ILogger<CoursesController> logger,
            IGroupLessonService groupLessonService,
            IMethodistService methodistService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _teacherService = teacherService;
            _groupLessonService = groupLessonService;
            _methodistService = methodistService;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
                return View(_mapper.Map<IEnumerable<GroupModel>>(await _groupService.GetAllAsync()));
            if (User.IsInRole("Methodist"))
                return View(_mapper.Map<IEnumerable<GroupModel>>(await _groupService.GetMethodistGroups(_userManager.GetUserId(User))));
            return View(_mapper.Map<IEnumerable<GroupModel>>(await _groupService.GetGroupsByUserIdAsync
                (_userManager.GetUserId(User))));
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
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Number,Status,StudentRequests,CourseId," +
            "TeacherId,MethodistId")] GroupModel group)
        {
            if (ModelState.IsValid)
            {
                int groupId = await _groupService.CreateAsync(_mapper.Map<Group>(group));
                if (group.StudentRequests != null)
                    foreach (StudentRequestsModel studentRequest in group.StudentRequests)
                    {
                        if (studentRequest.Applied)
                            await _enrollmentService.EnrolInGroup(studentRequest.UserId, groupId, UserRoles.Student);
                    }
                if (group.TeacherId != null)
                    await _enrollmentService.EnrolInGroup((await _teacherService.GetByIdAsync((int)group.TeacherId)).UserId, groupId, UserRoles.Teacher);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Admin, Methodist")]
        public async Task<IActionResult> Edit(int? id)
        {
       
            if (id == null)
            {
                return NotFound();
            }

            GroupModel group = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id));

            if (User.IsInRole("Methodist"))
                if (group.MethodistId != (await _methodistService.GetByUserId(_userManager.GetUserId(User))).Id)
                    return View("Identity/Account/AccessDenied");

            IEnumerable <Student> students = await _groupService.GetStudents(group.Id);
            await group.SetStudents(_mapper.Map<IEnumerable<StudentModel>>(students), _groupService);
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
        [Authorize(Roles = "Admin, Methodist")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,StudentRequests," +
            "Status,TeacherId,CourseId,MethodistId")] GroupModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Teacher currentTeacher = await _groupService.GetCurrentTeacher(group.Id);

                    switch (group.Status)
                    {
                        case GroupStatus.Cancelled:
                            await _groupService.CancelGroupAsync(group.Id);
                            break;
                        case GroupStatus.Finished:
                            await _groupService.FinishGroupAsync(group.Id);
                            break;
                        default:
                            await _groupService.UpdateAsync(_mapper.Map<Group>(group));
                            break;
                    }

                    if (group.StudentRequests != null) foreach (StudentRequestsModel studentRequest in group.StudentRequests)
                        {
                            if (studentRequest.Applied)
                            {
                                if (!(await _groupService.HasStudent(group.Id, studentRequest.UserId)))
                                    await _enrollmentService.EnrolInGroup(studentRequest.UserId, group.Id, UserRoles.Student);
                            }
                            else await _enrollmentService.UnEnrol(studentRequest.UserId, group.Id);
                        }
                    if (group.TeacherId != null)
                    {
                        Teacher newTeacher = await _teacherService.GetByIdAsync((int)group.TeacherId);

                        if (currentTeacher != null)
                        {
                            if (currentTeacher.Id != group.TeacherId)
                            {
                                await _enrollmentService.UnEnrol(currentTeacher.UserId, group.Id);
                                await _enrollmentService.EnrolInGroup(newTeacher.UserId, group.Id, UserRoles.Teacher);
                            }
                        }
                        else await _enrollmentService.EnrolInGroup(newTeacher.UserId, group.Id, UserRoles.Teacher);
                    }
                    else if (currentTeacher != null)
                        await _enrollmentService.UnEnrol(currentTeacher.UserId, group.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
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
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupModel group = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id));
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }
        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GroupModel group = _mapper.Map<GroupModel>(await _groupService.GetByIdAsync((int)id));
            if (group == null)
            {
                return NotFound();
            }
            if (group.Status == GroupStatus.Started)
                return BadRequest();
            await _groupService.CancelGroupAsync((int)id);
            await _groupService.Delete((int)id);

            return RedirectToAction(nameof(Index));
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
            if((await _groupService.GetCurrentTeacher(id)) == null && status == GroupStatus.Started && group.Status == GroupStatus.Pending)
                return Json("Add Teacher To The Group.");

            return !(await _groupLessonService.GetLessonsByGroupIdAsync(id)).Any()
                && status == GroupStatus.Started ? Json("To Start Group Please Add Or Manage StartDate Of Lessons.")
                : Json(true);
        }
    }
}
