﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IdentityNLayer.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly ICourseService _courseService;
        public TopicsController(ITopicService topicService,
            ICourseService courseService)
        {
            _topicService = topicService;
            _courseService = courseService;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            return View(await _topicService.GetAllAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _topicService.GetByIdAsync((int)id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // GET: Topics/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Parent"] = await _topicService.GetAllAsync();
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ParentId")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                await _topicService.CreateAsync(topic);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Parent"] = await _topicService.GetAllAsync();
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _topicService.GetByIdAsync((int)id);
            if (topic == null)
            {
                return NotFound();
            }
            ViewData["Parent"] = await _topicService.GetAllAsync();
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ParentId")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _topicService.UpdateAsync(topic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _topicService.GetByIdAsync(id) != null)
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
            ViewData["Parent"] = new SelectList(await _topicService.GetAllAsync(), "Id", "Title", topic.ParentId);
            return View(topic);
        }

        // GET: Topics/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await _topicService.GetByIdAsync((int)id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _topicService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
