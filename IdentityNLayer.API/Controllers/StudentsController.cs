using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("studentTypes")]
        public Array GetStudentTypes()
        {
            return _studentService.GetStudentTypes();
        }
        [HttpGet("{id}")]
        public async Task<Student> GetAsync(int id)
        {
            return await _studentService.GetByIdAsync((int)id);
        }

        public async Task<IEnumerable<Student>> GettAllAsync()
        {
            return await _studentService.GetAllAsync();
        }
    }
}
