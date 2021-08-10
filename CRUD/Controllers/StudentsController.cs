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

namespace IdentityNLayer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IStudentToGroupActionService _studentToGroupActionsService;

        public StudentsController(IStudentService studentService, 
            IGroupService groupService, 
            IMapper mapper,
            IStudentToGroupActionService studentToGroupActionsService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _mapper = mapper;
            _studentToGroupActionsService = studentToGroupActionsService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<StudentModel>>(_studentService.GetAll()));
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.StudentTypes = _studentService.GetStudentTypes();
          
            return View(new StudentModel(_groupService.GetAll()));
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, BirthDate, Email, Phone, Type, AssignGroups")] StudentModel student, int[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                _studentService.Create(_mapper.Map<Student>(student));

                foreach (AssignGroupModel assignGroup in student.AssignGroups)
                {
                    if(assignGroup.Assigned)
                        _studentService.Enrol(student.Id, assignGroup.GroupID);
                    /*_studentToGroupActionsService.Apply(student.Id, groupId);*/
                   /* student.Enrollments.Add(new Enrollment()
                    {
                        StudentID = student.Id,
                        GroupID = groupId,
                        DateEnrol = DateTime.Now
                    });*/
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentModel student = _mapper.Map<StudentModel>(_studentService.GetById((int)id));
            PopulateAssignedGroupData(student);
            ViewBag.StudentTypes = _studentService.GetStudentTypes();
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        private void PopulateAssignedGroupData(StudentModel student)
        {
            List<GroupModel> allGroups = _mapper.Map<List<GroupModel>>(_groupService.GetAll());
            List<Group> studentGroups = _studentService.GetStudentGroups(student.Id);
            List<int> studentGroupIds = (List<int>)(from gr in studentGroups
                        select gr.Id);
            var viewModel = new List<AssignGroupModel>();
            foreach (var group in allGroups)
            {
                   viewModel.Add(new AssignGroupModel
                    {
                        GroupID = group.Id,
                        Number = group.Number,
                        Assigned = studentGroupIds.Contains(group.Id)
                   });
            }
            ViewBag.Groups = viewModel;
        }
        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, BirthDate, Type")] StudentModel student)
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new DbUpdateConcurrencyException();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
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
