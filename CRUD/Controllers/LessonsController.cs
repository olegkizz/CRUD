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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;

namespace IdentityNLayer.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IFileService _fileService;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public LessonsController(ApplicationContext context,
            ILessonService lessonService,
            IFileService fileService,
            IConfiguration config,
            ILogger<LessonsController> logger)
        {
            _lessonService = lessonService;
            _fileService = fileService;
            _config = config;
            _logger = logger;
        }

        // GET: Lessons
        /*  public async Task<IActionResult> Index()
          {
           var applicationContext = _context.Lessons.Include(l => l.Course);
              return View(await applicationContext.ToListAsync());   
          }*/

        // GET: Lessons/Details/5
        /*  public async Task<IActionResult> Details(int? id)
          {
              return new Task<IActionResult>();
              if (id == null)
              {
                  return NotFound();
              }

              var lesson = await _context.Lessons
                  .Include(l => l.Course)
                  .FirstOrDefaultAsync(m => m.Id == id);
              if (lesson == null)
              {
                  return NotFound();
              }

              return View(lesson);
          }*/

        // GET: Lessons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*   [HttpPost]
           [ValidateAntiForgeryToken]*/
        /*  public async Task<IActionResult> Create([Bind("Id,Name,Theme,Summary,FileId,CourseId")] Lesson lesson)
          {
              if (ModelState.IsValid)
              {
                  _context.Add(lesson);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }
              ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lesson.CourseId);
              return View(lesson);
          }*/
        // GET: Lessons/Edit/5
        /*   public async Task<IActionResult> Edit(int? id)
           {
               if (id == null)
               {
                   return NotFound();
               }

               var lesson = await _context.Lessons.FindAsync(id);
               if (lesson == null)
               {
                   return NotFound();
               }
               ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lesson.CourseId);
               return View(lesson);
           }*/

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<JsonResult> Edit([Bind("Id,Name,Theme,CourseId")] Lesson lesson)
        {
            try
            {
                lesson.File = (await _lessonService.GetByIdAsync(lesson.Id))?.File;
                if (Request.Form.Files.Any())
                {
                    File newFile = await _fileService.CreateOrUpdateFileAsync(Request.Form.Files[0],
                        _config["Main:LessonFilesPath"] + lesson.CourseId);
                    lesson.File = newFile;
                }
                if (lesson.Id == 0)
                    await _lessonService.CreateAsync(lesson);
                else _lessonService.UpdateAsync(lesson);
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
        [Authorize(Roles = "Admin, Manager")]
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
                _lessonService.UpdateAsync(lesson);
                if (!await _lessonService.FileUseAsync(fileId))
                {
                    EntityEntry<File> checkResult = await _fileService.Delete((await _fileService.GetByPathAsync(path)).Id);
                    if (EntityState.Detached != checkResult.State)
                        throw new DbUpdateConcurrencyException();
                }
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
        [Authorize(Roles = "Admin, Manager")]
        public async Task<JsonResult> Delete(int id)
        {
            Lesson lesson = await _lessonService.GetByIdAsync(id);
            try
            {
                if (lesson == null)
                    return Json(new { Message = "Lesson Not Found", StatusCode = 400 });

                if ((await _lessonService.Delete(id)).State != EntityState.Detached)
                    throw new DbUpdateConcurrencyException();
                if(lesson.File != null)
                    if (!await _lessonService.FileUseAsync((int)lesson.FileId))
                    {
                        EntityEntry<File> checkResult = await _fileService.Delete((await _fileService.GetByPathAsync(lesson.File.Path)).Id);
                        if (EntityState.Detached != checkResult.State)
                            throw new DbUpdateConcurrencyException();
                    }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Json(new { Message = "error: " + e.Message, StatusCode = 400 });
            }
            return Json(new { Message = "Lesson Has Been Successfully Deleted", StatusCode = 200 });
        }
    }

    // GET: Lessons/Delete/5
/*    public async Task<JsonResult> Delete(int id)
    {
        if (id == null)
        {
            return Json({ Message = "Id is Null", StatusCode = 400});
        }

        var lesson = await _context.Lessons
            .Include(l => l.Course)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lesson == null)
        {
            return NotFound();
        }

        return View(lesson);
    }*/

    // POST: Lessons/Delete/5
 
}
