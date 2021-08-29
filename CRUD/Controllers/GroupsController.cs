using System.Threading.Tasks;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using IdentityNLayer.DAL.Interfaces;
using AutoMapper;
using IdentityNLayer.Models;
using IdentityNLayer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityNLayer.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper, 
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            ITeacherService teacherService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _teacherService = teacherService;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GroupModel>>(_groupService.GetAll()));
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
                        var contact = await _context.Contact
                            .FirstOrDefaultAsync(m => m.ContactId == id);
                        if (contact == null)
                        {
                            return NotFound();
                        }
            */
            return View();
        }

        // GET: Contacts/Create
        public IActionResult Create(int courseId)
        {
            GroupModel group = new ();
            group.CourseId = courseId;
            group.SetStudentRequests(_courseService.GetStudentRequests(courseId));
            return View(group);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Status,StudentRequests,CourseId,TeacherId")] GroupModel group)
        {
            if (ModelState.IsValid)
            {
                int groupId = _groupService.Create(_mapper.Map<Group>(group));

                foreach(StudentRequestsModel studentRequest in group.StudentRequests)
                {
                    if(studentRequest.Applied)
                        _enrollmentService.Enrol(studentRequest.UserId, groupId, UserRoles.Student);
                }
                if(group.TeacherId != null)
                    _enrollmentService.Enrol(_teacherService.GetById((int)group.TeacherId).UserId, groupId, UserRoles.Teacher);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupModel group = _mapper.Map<GroupModel>(_groupService.GetById((int)id));
            group.SetStudentRequests(_courseService.GetStudentRequests(group.CourseId));

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,StudentRequests,Status,TeacherId")] GroupModel group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _groupService.Update(_mapper.Map<Group>(group));
                    foreach (StudentRequestsModel studentRequest in group.StudentRequests)
                    {
                        if(studentRequest.Applied)
                            _enrollmentService.Enrol(studentRequest.UserId, group.Id, UserRoles.Student);
                        else _enrollmentService.UnEnrol(studentRequest.UserId, group.Id);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GroupModel teacher = _mapper.Map<GroupModel>(_groupService.GetById((int)id));
            if (teacher == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*           var contact = await _context.Contact.FindAsync(id);
                       _context.Contact.Remove(contact);
                       await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return true;
        }
    }
}
