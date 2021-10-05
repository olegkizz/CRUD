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

    public class MethodistsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly IMethodistService _methodistService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public MethodistsController(ApplicationContext context,
            UserManager<Person> userManager,
            IEmailService emailService,
            IMethodistService methodistService,
            IMapper mapper,
            ILogger<MethodistsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _methodistService = methodistService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        // GET: Methodists
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MethodistModel>>(await _methodistService.GetAllAsync()));
        }
        // GET: Methodists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Methodist = await _methodistService.GetByIdAsync((int)id);
            if (Methodist == null)
            {
                return NotFound();
            }

            return View(Methodist);
        }

        [Authorize(Roles = "Admin")]

        // GET: Methodists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Methodists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(MethodistModel methodist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByEmailAsync(methodist.Email) != null)
                    {
                        ModelState.AddModelError("Email", "Email Already Use.");
                        return View(methodist);
                    }

                    Person user = new() { Email = methodist.Email, UserName = methodist.Email };
                    // добавляем пользователя
                    var result = await _userManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        // генерация токена для пользователя
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Action(
                            "SetMethodistAccount",
                            "Methodists",
                            new { userId = user.Id, code = code },
                            protocol: HttpContext.Request.Scheme);
                        await _emailService.SendEmailAsync(methodist.Email, "SetMethodistAccount",
                            $"To Create Your Password Go To: <a href='{callbackUrl}'>link</a>");

                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(methodist);
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
        public async Task<IActionResult> SetMethodistAccount(string userId, string code)
        {
            ViewData["UserId"] = userId;
            ViewData["Code"] = code;
            return View();
        }

        [HttpPost]
        [ActionName("SetMethodistAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> SetMethodistAccountPost(string userId, string code, MethodistRegisterModel methodist)
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
                        (await _userManager.AddToRoleAsync(user, UserRoles.Methodist.ToString())).Succeeded)
                    {
                        user.FirstName = methodist.FirstName;
                        user.LastName = methodist.LastName;

                        methodist.User = user;


                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        if ((await _userManager.ResetPasswordAsync(user, token, methodist.Password)).Succeeded &&
                                (await _userManager.UpdateAsync(user)).Succeeded)
                            await _methodistService.CreateAsync(_mapper.Map<Methodist>(methodist));
                        else return View("SetMethodistAccount", methodist.Error =
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
            return View(methodist);
        }

        // GET: Methodists/Edit/5
        [Authorize(Roles = "Admin, Methodist")]
        public async Task<IActionResult> Edit(int? id)
        {
            Methodist currentMethodist = await _methodistService.GetByUserId(_userManager.GetUserId(User));

            if (currentMethodist != null)
            {
                if (id == null)
                    id = currentMethodist.Id;
                else
                {
                    if (id != currentMethodist.Id)
                        id = currentMethodist.Id;
                }
            }

            var methodist = await _methodistService.GetByIdAsync((int)id);
            if (methodist == null)
            {
                return NotFound();
            }
            return View(methodist);
        }

        // POST: Methodists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Methodist")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LinkToContact")] MethodistModel methodist)
        {
            if ((await _methodistService.GetByIdAsync(id)).UserId != _userManager.GetUserId(User)
                && !User.IsInRole("Admin"))
                return BadRequest();

            if (id != methodist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _methodistService.UpdateAsync(_mapper.Map<Methodist>(methodist));
                }
                catch (Exception e)
                {
                    if (!await MethodistExists(methodist.Id))
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
            return View(methodist);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var methodist = await _methodistService.GetByIdAsync((int)id);
            if (methodist == null)
            {
                return NotFound();
            }

            return View(methodist);
        }
        // POST: Methodists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Methodist methodist = await _methodistService.GetByIdAsync(id);
                Person user = await _userManager.FindByIdAsync(methodist.UserId);
                await _userManager.RemoveFromRoleAsync(user,
                    UserRoles.Methodist.ToString());
                await _methodistService.Delete(id);

                await _userManager.DeleteAsync(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                ViewData["Exception"] = e;
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MethodistExists(int id)
        {
            return await _methodistService.GetByIdAsync(id) != null;
        }
    }
}
