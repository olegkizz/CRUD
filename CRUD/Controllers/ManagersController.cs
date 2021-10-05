using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.EF.Context;
using Microsoft.AspNetCore.Identity;
using IdentityNLayer.BLL.Services;
using Microsoft.Extensions.Logging;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using IdentityNLayer.Filters;

namespace IdentityNLayer.Controllers
{
    [TypeFilter(typeof(GlobalExceptionFilter))]

    public class ManagersController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IManagerService _managerService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ManagersController(ApplicationContext context,
            UserManager<Person> userManager,
            IEmailService emailService,
            IManagerService managerService,
            IMapper mapper,
            ILogger<ManagersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _managerService = managerService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        // GET: Managers
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ManagerModel>>(await _managerService.GetAllAsync()));
        }
        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _managerService.GetByIdAsync((int)id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        [Authorize(Roles = "Admin")]

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ManagerModel manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByEmailAsync(manager.Email) != null)
                    {
                        ModelState.AddModelError("Email", "Email Already Use.");
                        return View(manager);
                    }

                    Person user = new() { Email = manager.Email, UserName = manager.Email };
                    // добавляем пользователя
                    var result = await _userManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        // генерация токена для пользователя
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "SetManagerAccount",
                            "Managers",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);
                        await _emailService.SendEmailAsync(manager.Email, "SetManagerAccount",
                            $"To Create Your Password Go To: <a href='{callbackUrl}'>link</a>");

                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(manager);
            }
            catch (Exception e)
            {
                ViewData["Exception"] = e;
                _logger.LogError(e.Message);
                return View("Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SetManagerAccount(string userId, string code)
        {
            ViewData["UserId"] = userId;
            ViewData["Code"] = code;
            return View();
        }

        [HttpPost]
        [ActionName("SetManagerAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> SetManagerAccountPost(string userId, string code, ManagerRegisterModel manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userId == null || code == null)
                    {
                        return BadRequest();
                    }
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return View("Error");
                    }
                    if ((await _userManager.ConfirmEmailAsync(user, code)).Succeeded &&
                        (await _userManager.AddToRoleAsync(user, UserRoles.Manager.ToString())).Succeeded)
                    {
                        user.FirstName = manager.FirstName;
                        user.LastName = manager.LastName;

                        manager.User = user;


                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        if ((await _userManager.ResetPasswordAsync(user, token, manager.Password)).Succeeded &&
                                (await _userManager.UpdateAsync(user)).Succeeded)
                            await _managerService.CreateAsync(_mapper.Map<Manager>(manager));
                        else return View("SetManagerAccount", manager.Error =
                            "Cannot Update User Or Reset User Password.");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ViewData["Exception"] = e;
                return View("Error");
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null && User.IsInRole("Manager"))
                id = (await _managerService.GetByUserId(_userManager.GetUserId(User))).Id;
            else
            {
                int currentManagerId = (await _managerService.GetByUserId(_userManager.GetUserId(User))).Id;
                if (id != currentManagerId && !User.IsInRole("Admin"))
                    id = currentManagerId;
            }

            var manager = await _managerService.GetByIdAsync((int)id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,LinkToContact")] ManagerModel manager)
        {
            if ((await _managerService.GetByIdAsync(id)).UserId != _userManager.GetUserId(User)
                && !User.IsInRole("Admin"))
                return BadRequest();

            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _managerService.UpdateAsync(_mapper.Map<Manager>(manager));
                }
                catch (Exception e)
                {
                    if (!await ManagerExists(manager.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(e.Message);
                        return View("Error");
                    }
                }
                if (User.IsInRole("Admin"))
                    return RedirectToAction(nameof(Index));
                else return RedirectToAction("Index", "Courses");
            }
            return View(manager);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _managerService.GetByIdAsync((int)id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }
        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Manager manager = await _managerService.GetByIdAsync(id);
                Person user = await _userManager.FindByIdAsync(manager.UserId);
                await _userManager.RemoveFromRoleAsync(user,
                    UserRoles.Manager.ToString());
                await _managerService.Delete(id);

                await _userManager.DeleteAsync(user);
            } catch(Exception e)
            {
                _logger.LogError(e.Message);
                ViewData["Exception"] = e;
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ManagerExists(int id)
        {
            return await _managerService.GetByIdAsync(id) != null;
        }
    }
}
