﻿using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _courseService;

        public CoursesController(ILogger<CoursesController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return _courseService.GetAll();
        }
    }
}
