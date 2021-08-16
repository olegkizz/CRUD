using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.Models;
using IdentityNLayer.BLL.Interfaces;
using AutoMapper;
using IdentityNLayer.Core.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace IdentityNLayer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IEnrollmentService _enrollmentService;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public StudentsController(IStudentService studentService, 
            IGroupService groupService, 
            IMapper mapper,
            IStudentToGroupActionService studentToGroupActionsService,
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager,
            IEnrollmentService enrollmentService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Students
        [Authorize("Admin, Manager")]

        public async Task<IActionResult> Index()
        {
            ViewBag.studentService = _studentService;
            return View(_mapper.Map<IEnumerable<StudentModel>>(_studentService.GetAll()));
        }

        // GET: Students/Create
        [Authorize("Admin, Manager")]

        public IActionResult Create()
        {
            ViewBag.StudentTypes = _studentService.GetStudentTypes();

            StudentModel student = new StudentModel();
            student.CreateAssignGroups(_groupService.GetAll());

            return View(student);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize("Admin, Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, BirthDate, Email," +
            "Type, AssignGroups, User")] StudentModel student)
        {

            if (ModelState.IsValid && (await _userManager.FindByIdAsync(student.User.Id)) == null)
            {
                List<string> errors = new();

                IdentityResult chkUser = await _userManager.CreateAsync(student.User, student.User.PasswordHash);
                if (chkUser.Succeeded)
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(student.User, UserRoles.Student.ToString());
                    int newStudentId = _studentService.Create(_mapper.Map<Student>(student));

                    foreach (AssignGroupModel assignGroup in student.AssignGroups)
                    {
                        if (assignGroup.Assigned)
                            _enrollmentService.Enrol(student.User.Id, assignGroup.GroupID, UserRoles.Student);
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Errors = chkUser.Errors.ToList();
                return View(student.CreateAssignGroups(_groupService.GetAll()));
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        // GET: Contacts/Edit/5
        [Authorize("Admin, Manager")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            StudentModel student = _mapper.Map<StudentModel>(_studentService.GetById((int)id));

            student.CreateAssignGroups(_groupService.GetAll());
            List<int> studentGroupIds = _studentService.GetStudentGroups((int)id).Select(st => st.Id).ToList();
            foreach (AssignGroupModel ag in student.AssignGroups)
            {
                if (studentGroupIds.Contains(ag.GroupID))
                    ag.Assigned = true;
            }

            ViewBag.StudentTypes = _studentService.GetStudentTypes();
            
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
        [Authorize("Admin, Manager")]

        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, BirthDate, User, " +
            "PhoneNumber, Type, AssignGroups, UserId")] StudentModel student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _studentService.Update(_mapper.Map<Student>(student));

                    foreach (AssignGroupModel assignGroup in student.AssignGroups)
                    {
                        if (assignGroup.Assigned)
                            _enrollmentService.Enrol(student.UserId, assignGroup.GroupID, UserRoles.Student);
                        else _enrollmentService.UnEnrol(student.UserId, assignGroup.GroupID);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new DbUpdateConcurrencyException();
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
