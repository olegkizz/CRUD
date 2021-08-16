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
        public Student Get(int id)
        {
            return _studentService.GetById((int)id);
        }

        public IEnumerable<Student> GettAll()
        {
            return _studentService.GetAll();
        }
    }
}
