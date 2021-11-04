using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace IdentityNLayer.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IFileService _fileService;
        private readonly IGroupLessonService _groupLessonService;
        private readonly IGroupService _groupService;
        private readonly ICourseService _courseService;
        private readonly IStudentMarkService _studentMarkService;
        private readonly IMethodistService _methodistService;
        private readonly UserManager<Person> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LessonsController(
            ILessonService lessonService,
            IFileService fileService,
            IConfiguration config,
            ILogger<LessonsController> logger,
            IGroupLessonService groupLessonService,
            ICourseService courseService,
            IStudentMarkService studentMarkService,
            IMethodistService methodistService,
            IGroupService groupService,
            UserManager<Person> userManager,
            IMapper mapper)
        {
            _lessonService = lessonService;
            _fileService = fileService;
            _groupLessonService = groupLessonService;
            _studentMarkService = studentMarkService;
            _courseService = courseService;
            _groupService = groupService;
            _methodistService = methodistService;
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        // GET: GroupLessons
        public async Task<IActionResult> EditGroupLessons(int? groupId)
        {
            if (!groupId.HasValue)
                return NotFound();
            List<GroupLessonModel> groupLessons = _mapper.Map<List<GroupLessonModel>>(await _groupLessonService.GetLessonsByGroupIdAsync
                ((int)groupId));

            Group group = await _groupService.GetByIdAsync((int)groupId);
            Course course = await _courseService.GetByIdAsync(group.CourseId);

            if (User.IsInRole("Methodist"))
                if (group.MethodistId != (await _methodistService.GetByUserId(_userManager.GetUserId(User))).Id)
                    return Redirect("~/Identity/Account/AccessDenied");

            foreach (Lesson lesson in course.Lessons)
            {
                GroupLesson groupLesson = await _groupLessonService.GetByLessonAndGroupIdAsync((int)groupId, lesson.Id);
                if (groupLesson == null)
                {
                    groupLessons.Add(new GroupLessonModel()
                    {
                        GroupId = group.Id,
                        Group = group,
                        LessonId = lesson.Id,
                        Lesson = lesson,
                        StartDate = DateTime.Now.AddDays(1)
                    });
                }
            }
            return View(groupLessons);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Methodist")]
        public async Task<ActionResult> SaveGroupLessons(int groupId, [StartDate] IEnumerable<GroupLessonModel> groupLessons)
        {
            if (!groupLessons.Any())
                return RedirectToAction("Details", "Groups", new { id = groupId });
            if (groupLessons.Where(gl => gl.Error.Count() > 0).Any())
            {
                Request.RouteValues["groupId"] = groupId;
                Group group = await _groupService.GetByIdAsync(groupId);
                foreach (GroupLessonModel groupLesson in groupLessons)
                {
                    groupLesson.Group = group;
                    groupLesson.Lesson = await _lessonService.GetByIdAsync(groupLesson.LessonId);
                }

                return View("../Lessons/EditGroupLessons", groupLessons);
            }
            foreach (GroupLessonModel groupLesson in groupLessons)
            {
                groupLesson.Lesson = null;
                    if (groupLesson.Id != 0)
                        await _groupLessonService.UpdateAsync(_mapper.Map<GroupLesson>(groupLesson));
                    else await _groupLessonService.CreateAsync(_mapper.Map<GroupLesson>(groupLesson));
            }
            return RedirectToAction("Details", "Groups", new { id = groupId });
        }

        public async Task<FileResult> OpenFile(string filePath)
        {
            File file = await _fileService.GetByPathAsync(filePath);
            return File(filePath, file.ContentType);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        // GET: Lessons/Create
        public IActionResult Create(int courseId)
        {
            if (courseId == 0)
                return NotFound();
            return View();
        }


        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Edit([Bind("Id,Name,Theme,CourseId,Duration")] LessonModel lesson)
        {
            if (!ModelState.IsValid)
            {
                string error = "";
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (string errorInValues in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    error += " * " + errorInValues;

                return Json(new { Message = error, StatusCode = 400 });
            }
            try
            {
                lesson.File = (await _lessonService.GetByIdAsync(lesson.Id ?? 0))?.File;
                if (Request.Form.Files.Any())
                {
                    File newFile = await _fileService.CreateOrUpdateFileAsync(Request.Form.Files[0],
                        _config["Main:LessonFilesPath"] + lesson.CourseId);
                    lesson.File = newFile;
                }
                if (!lesson.Id.HasValue)
                    await _lessonService.CreateAsync(_mapper.Map<Lesson>(lesson));
                else await _lessonService.UpdateAsync(_mapper.Map<Lesson>(lesson));
                return Json(new { Message = "Data Has Been Successfully Updated", StatusCode = 200, File = lesson.File });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new { Message = "error: " + e.Message, StatusCode = 400 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeleteFile(int lessonId)
        {
            Lesson lesson = await _lessonService.GetByIdAsync(lessonId);
            if (lesson.File == null)
                return Json(new { Message = "Lesson Doesnt Have File", StatusCode = 200 });
            try
            {
                string path = lesson.File.Path;
                int fileId = (int)lesson.FileId;
                lesson.File = null;
                lesson.FileId = null;
                await _lessonService.UpdateAsync(lesson);
                if (!await _lessonService.FileUseAsync(fileId))
                    await _fileService.Delete((await _fileService.GetByPathAsync(path)).Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Json(new { Message = "error: " + e.Message, StatusCode = 400 });
            }
            return Json(new { Message = "File Has Been Successfully Deleted", StatusCode = 200 });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int id)
        {
            Lesson lesson = await _lessonService.GetByIdAsync(id);
            try
            {
                if (lesson == null)
                    return Json(new { Message = "Lesson Not Found.", StatusCode = 400 });

                foreach (Group group in await _courseService.GetGroups(lesson.CourseId))
                {
                    GroupLesson groupLesson = await _groupLessonService.GetByLessonAndGroupIdAsync(group.Id, lesson.Id);
                    if(groupLesson != null && group.Status == GroupStatus.Started)
                        if (groupLesson.StartDate < DateTime.Now && 
                                groupLesson.StartDate.Value.AddMinutes(lesson.Duration) > DateTime.Now)
                            return Json(new { Message = "The lesson is now on " + group.Number 
                                + " Group", StatusCode = 400 });
                }
                foreach (StudentMark studentMark in await _studentMarkService.GetByLessonIdAsync(id))
                    await _studentMarkService.Delete(studentMark.Id);
                if (lesson.File != null)
                    if (!await _lessonService.FileUseAsync((int)lesson.FileId))
                        await _fileService.Delete((await _fileService.GetByPathAsync(lesson.File.Path)).Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Json(new { Message = "error: " + e.Message, StatusCode = 400 });
            }
            return Json(new { Message = "Lesson Has Been Successfully Deleted", StatusCode = 200 });
        }
    }
}
