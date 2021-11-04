using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using IdentityNLayer.Core.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityNLayer.Controllers;
using Microsoft.AspNetCore.Mvc;
using IdentityNLayer.Models;
using Microsoft.AspNetCore.Http;
using Xunit;
using AutoMapper;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using System;

namespace IdentityNLayer.Tests
{
    public class LessonsControllerTests
    {
        private Mock<IGroupLessonService> _groupLessonService;
        private Mock<IGroupService> _groupService;
        private Mock<ILessonService> _lessonService;
        private Mock<IMapper> _mapper;
        private LessonsController _underTest;


        [Fact]
        public async Task SaveGroupLessons_ReturnRedirectToActionGroupsDetails_WhenGroupLessonsEmpty()
        {
            //arrange
            List<GroupLessonModel> groupLessons = new();
            int groupId = It.IsAny<int>();
            _groupService = new Mock<IGroupService>();
            _lessonService = new Mock<ILessonService>();
            _groupLessonService = new Mock<IGroupLessonService>();
            _underTest = new LessonsController(_lessonService.Object, null, null, null, _groupLessonService.Object,
                null, null, null, _groupService.Object, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            //act
            var result = (RedirectToActionResult)await _underTest.SaveGroupLessons(groupId, groupLessons);

            //asserts
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(result.RouteValues["id"], groupId);
            Assert.Equal("Details", result.ActionName);
            Assert.Equal("Groups", result.ControllerName);
      
        }

        [Fact]
        public async Task SaveGroupLessons_ViewEditGroupLessonsWithErrorValidation_WhenGroupLessonsNotEmptyWithErrors()
        {
            //arrange
            _groupService = new Mock<IGroupService>();
            _lessonService = new Mock<ILessonService>();
            _groupLessonService = new Mock<IGroupLessonService>();
            _underTest = new LessonsController(_lessonService.Object, null, null, null, _groupLessonService.Object,
                null, null, null, _groupService.Object, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            List<GroupLessonModel> groupLessons = new()
            {
                new GroupLessonModel() { Id = 1, Error = new List<string>() { "error" }},
                new GroupLessonModel() { Id = 2 },
                new GroupLessonModel() { Id = 3 }
            };
            int groupId = It.IsAny<int>();
            _lessonService.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Lesson());
            //act
            var result = (ViewResult)await _underTest.SaveGroupLessons(groupId, groupLessons);

            //asserts
            Assert.IsType<ViewResult>(result);
            Assert.Equal("../Lessons/EditGroupLessons", result.ViewName);
            Assert.Equal(result.Model, groupLessons);
        }
        [Fact]
        public async Task SaveGroupLessons_InvokeUpdateAsync_WhenGroupLessonDoesNotHaveErrorsAndExists()
        {
            //arrange
            _groupService = new Mock<IGroupService>();
            _lessonService = new Mock<ILessonService>();
            _groupLessonService = new Mock<IGroupLessonService>();
            _mapper = new Mock<IMapper>();
            _underTest = new LessonsController(null, null, null, null, _groupLessonService.Object,
                null, null, null, _groupService.Object, null, _mapper.Object);
            List<GroupLessonModel> groupLessons = new()
            {
                new GroupLessonModel() { Id = 1 }
            };
            int groupId = It.IsAny<int>();
            _mapper.Setup(x => x.Map<GroupLesson>(groupLessons[0])).Returns(new GroupLesson());
            //act
            var result = (RedirectToActionResult)await _underTest.SaveGroupLessons(groupId, groupLessons);

            //asserts
            _groupLessonService.Verify(x => x.UpdateAsync(It.IsAny<GroupLesson>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(result.RouteValues["id"], groupId);
            Assert.Equal("Details", result.ActionName);
            Assert.Equal("Groups", result.ControllerName);
        }

        [Fact]
        public async Task SaveGroupLessons_InvokeCreateAsync_WhenGroupLessonDoesNotHaveErrorsAndNotExists()
        {
            //arrange
            _groupService = new Mock<IGroupService>();
            _lessonService = new Mock<ILessonService>();
            _groupLessonService = new Mock<IGroupLessonService>();
            _mapper = new Mock<IMapper>();
            _underTest = new LessonsController(null, null, null, null, _groupLessonService.Object,
                null, null, null, _groupService.Object, null, _mapper.Object);
            List<GroupLessonModel> groupLessons = new()
            {
                new GroupLessonModel() { Id = 0 }
            };
            int groupId = It.IsAny<int>();
            _mapper.Setup(x => x.Map<GroupLesson>(groupLessons[0])).Returns(new GroupLesson());
            //act
            var result = (RedirectToActionResult)await _underTest.SaveGroupLessons(groupId, groupLessons);

            //asserts
            _groupLessonService.Verify(x => x.CreateAsync(It.IsAny<GroupLesson>()), Times.Once);
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(result.RouteValues["id"], groupId);
            Assert.Equal("Details", result.ActionName);
            Assert.Equal("Groups", result.ControllerName);
        }

        [Fact]
        public async Task EditGroupLessons_ReturnJsonWithErrorMessage_WhenGroupLessonWithErrors()
        {
            //arrange
            string error = "error";
            int statusCode = 400;

            _underTest = new LessonsController(null, null, null, null, null,
                null, null, null, null, null, null);

            _underTest.ViewData.ModelState.AddModelError(error, error);

            //act
            var result = await _underTest.Edit(new LessonModel());
            //asserts
            Assert.IsType<JsonResult>(result);
            Assert.True((int)result.Value.GetType().GetProperty("StatusCode").GetValue(result.Value) == statusCode);
            Assert.True((string)result.Value.GetType().GetProperty("Message").GetValue(result.Value) == $" * {error}");
        }
        [Fact]
        public async Task EditGroupLessons_InvokeCreateorUpdateFileAsync_WhenGroupLessonWithNotErrors()
        {
            //arrange
            string fileText = "test";
            LessonModel lesson = new () { CourseId = It.IsAny<int>() };
            Mock<IFileService> _fileService = new ();
            Mock<ILessonService>  _lessonService = new Mock<ILessonService>();
            Mock<IFormFile> formFile = new Mock<IFormFile>();
            HttpContext _httpContext = new DefaultHttpContext();
            Mock<IMapper> _mapper = new ();
            IConfiguration _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string path = _config["Main:LessonFilesPath"] + lesson.CourseId;

            FileInfo fileInfo = new (path);
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0,
                "Data", "test.txt");
            _httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { file });

            _underTest = new LessonsController(_lessonService.Object, _fileService.Object, _config, null, null,
               null, null, null, null, null, _mapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            _lessonService.Setup(l => l.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Lesson)null);
            _lessonService.Setup(l => l.CreateAsync(It.IsAny<Lesson>())).ReturnsAsync(It.IsAny<int>());

            //act
            var result = await _underTest.Edit(lesson);
            //asserts
            _fileService.Verify(f => f.CreateOrUpdateFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task EditGroupLessons_InvokeCreateLessonAsync_WhenGroupLessonWithNoErrors()
        {
            //arrange
            LessonModel lesson = new();
            Mock<ILessonService> _lessonService = new Mock<ILessonService>();
            HttpContext _httpContext = new DefaultHttpContext();
            Mock<IMapper> _mapper = new();
            IConfiguration _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection {  });

            _underTest = new LessonsController(_lessonService.Object, null, _config, null, null,
               null, null, null, null, null, _mapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            _lessonService.Setup(l => l.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Lesson)null);
            _lessonService.Setup(l => l.CreateAsync(It.IsAny<Lesson>())).ReturnsAsync(It.IsAny<int>());

            //act
            var result = await _underTest.Edit(lesson);
            //asserts
            _lessonService.Verify(f => f.CreateAsync(It.IsAny<Lesson>()), Times.Once);
            Assert.True((int)result.Value.GetType().GetProperty("StatusCode").GetValue(result.Value) == 200);
            Assert.True((string)result.Value.GetType().GetProperty("Message").GetValue(result.Value) == "Data Has Been Successfully Updated");
        }

        [Fact]
        public async Task EditGroupLessons_InvokeUpdateLessonAsync_WhenGroupLessonWithNoErrors()
        {
            //arrange
            LessonModel lesson = new() { Id = 1 };
            Mock<ILessonService> _lessonService = new Mock<ILessonService>();
            HttpContext _httpContext = new DefaultHttpContext();
            Mock<IMapper> _mapper = new();
            IConfiguration _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>(), new FormFileCollection { });

            _underTest = new LessonsController(_lessonService.Object, null, _config, null, null,
               null, null, null, null, null, _mapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _httpContext
                }
            };

            _lessonService.Setup(l => l.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Lesson)null);
            _lessonService.Setup(l => l.CreateAsync(It.IsAny<Lesson>())).ReturnsAsync(It.IsAny<int>());

            //act
            var result = await _underTest.Edit(lesson);
            //asserts
            _lessonService.Verify(f => f.UpdateAsync(It.IsAny<Lesson>()), Times.Once);
            Assert.True((int)result.Value.GetType().GetProperty("StatusCode").GetValue(result.Value) == 200);
            Assert.True((string)result.Value.GetType().GetProperty("Message").GetValue(result.Value) == "Data Has Been Successfully Updated");
        }
        [Fact]
        public async Task EditGroupLessons_InvokeLogError_WhenCodeGetException()
        {
            //arrange
            Mock<ILessonService> _lessonService = new Mock<ILessonService>();
            Mock<ILogger<LessonsController>> _logger = new();
            string errorMessage = "error";
            IConfiguration _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _underTest = new LessonsController(_lessonService.Object, null, null, _logger.Object, null,
               null, null, null, null, null, null);

            _lessonService.Setup(l => l.GetByIdAsync(It.IsAny<int>())).Throws(new System.Exception(errorMessage));

            //act
            var result = await _underTest.Edit(new LessonModel());
            //asserts
            Assert.Equal(LogLevel.Error, _logger.Invocations[0].Arguments[0]);
        }
    }
}