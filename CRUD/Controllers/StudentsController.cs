using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityNLayer.BLL.DTO;
using IdentityNLayer.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.Models;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.DAL.Interfaces;
using AutoMapper;

namespace IdentityNLayer.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWork db, IMapper mapper)
        {
            _studentService = new StudentService(db, mapper);
            _groupService = new GroupService(db, mapper);
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(_studentService.GetAll());
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            ViewBag.Groups = _groupService.GetAll();
            ViewBag.StudentTypes = _studentService.GetStudentTypes();
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Email, Type, GroupId")] StudentDTO student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Create(student);
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

            StudentDTO student = _studentService.GetById((int)id);
            ViewBag.Groups = _groupService.GetAll();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, Type, GroupId")] StudentDTO student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _studentService.Update(student);
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

            StudentDTO student = _studentService.GetById((int)id);
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
