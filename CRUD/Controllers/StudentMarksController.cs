using AutoMapper;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Controllers
{
    public class StudentMarksController : Controller
    {
        private readonly IGroupLessonService _groupLessonService;
        private readonly IGroupService _groupService;
        private readonly IStudentMarkService _studentMarkService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<Person> _userManager;
        private readonly IMapper _mapper;
        public StudentMarksController(
            IGroupLessonService groupLessonService,
            IStudentMarkService studentMarkService,
            ITeacherService teacherService,
            UserManager<Person> userManager,
            IGroupService groupService,
            IMapper mapper)
        {
            _groupLessonService = groupLessonService;
            _studentMarkService = studentMarkService;
            _teacherService = teacherService;
            _userManager = userManager;
            _groupService = groupService;
            _mapper = mapper;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int groupId, IEnumerable<StudentMarkModel> studentMarks)
        {
            if (groupId == 0)
                return Json(new { Message = "Group Are 0.", StatusCode = 400 });
            if (studentMarks.Any())
            {
                if (studentMarks.First().StudentId == 0 || (await _userManager.GetUserAsync(User)).Id
                          != (await _groupService.GetCurrentTeacher(groupId)).User.Id)
                    return Json(new { Message = "Bad Request.", StatusCode = 400 });
            }
            else return Json(new { Message = "Student Marks Empty.", StatusCode = 200 });
            try
            {
                if (ModelState.IsValid)
                {

                    foreach (StudentMark studentMark in studentMarks)
                        await _studentMarkService.UpdateAsync(_mapper.Map<StudentMark>(studentMark));

                    return Json(new
                    {
                        Message = "Data Has Been Successfully Updated",
                        StatusCode = 200
                    });

                }
                else
                {
                    List<string> errors = new ();
                    foreach (string errorInValues in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                    {
                        if (errors.Find(e => e == errorInValues) == null)
                            errors.Add(errorInValues);
                    }
                    return Json(new { Message = string.Join("*", errors), StatusCode = 400 });
                }
            }
            catch (Exception e)
            {
                return Json(new { Message = e.Message, StatusCode = 400 });
            }
        }
    }
}
