using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.BLL.Services;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityNLayer.DAL;

namespace IdentityNLayer.Tests
{
    public class StudentMarkServiceTests
    {
        private Mock<IUnitOfWork> _db;
        private Mock<IGroupLessonService> _groupLessonService;
        private Mock<IStudentService> _studentServiceMock;
        private IStudentMarkService _underTest;
        private Mock<IRepository<Enrollment>> _enrollmentRepository;
        private Mock<IRepository<StudentMark>> _studentMarkRepository;
        private Mock<IRepository<GroupLesson>> _groupLessonsRepository;
        private Mock<IRepository<Student>> _studentRepository;


        [SetUp]
        public void Setup()
        {
            _db = new Mock<IUnitOfWork>();
            _studentMarkRepository = new Mock<IRepository<StudentMark>>();
            _studentRepository = new Mock<IRepository<Student>>();
            _enrollmentRepository = new Mock<IRepository<Enrollment>>();
            _groupLessonsRepository = new Mock<IRepository<GroupLesson>>();
            _studentServiceMock = new Mock<IStudentService>();
            _groupLessonService = new Mock<IGroupLessonService>();
            _underTest = new StudentMarkService(_db.Object, _groupLessonService.Object, _studentServiceMock.Object);

            _db.Setup(x => x.Enrollments).Returns(_enrollmentRepository.Object);
            _db.Setup(x => x.Students).Returns(_studentRepository.Object);
            _db.Setup(x => x.GroupLessons).Returns(_groupLessonsRepository.Object);
            _db.Setup(x => x.StudentMarks).Returns(_studentMarkRepository.Object);
        }

        [Test]
        public async Task SetFinalMarkToStudentForCourse_ReturnsFinalMark()
        {
            //arrange
            string userId = It.IsAny<string>();
            int courseId = It.IsAny<int>();
            int mark = new Random().Next(0, 10);

            _studentRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Student, bool>>>())).ReturnsAsync(new List<Student>() {
                new Student(){}
            });
            _studentServiceMock.Setup(x => x.GetGroupByCourseIdAsync(It.IsAny<int>(), courseId)).ReturnsAsync(new Group() { Id = 1 });

            _groupLessonsRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<GroupLesson, bool>>>())).ReturnsAsync(
                new List<GroupLesson>()
                {
                    new GroupLesson(){ Id = 1 },
                    new GroupLesson() { Id = 2 },
                    new GroupLesson(){ Id = 3 }
                });

            _studentMarkRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<StudentMark, bool>>>())).ReturnsAsync(
                    new List<StudentMark>() {
                        new StudentMark() { Mark = mark }
                    });

            //act
            var result = await _underTest.SetFinalMarkToStudentForCourse(userId, courseId);

            //asserts
            Assert.AreEqual(result, 3 * mark);
        }

        [Test]
        public async Task SetFinalMarkToStudentForCourse_InvokeCreateStudentMark()
        {
            //arrange
            string userId = It.IsAny<string>();
            int courseId = It.IsAny<int>();
            int mark = new Random().Next(0, 10);
            Student student = new Student() { Id = 1 };
            _studentRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Student, bool>>>())).ReturnsAsync(
                new List<Student>() {student});
            
            _studentServiceMock.Setup(x => x.GetGroupByCourseIdAsync(It.IsAny<int>(), courseId)).ReturnsAsync(new Group() { Id = 1 });

            _groupLessonsRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<GroupLesson, bool>>>())).ReturnsAsync(
                new List<GroupLesson>()
                {
                    new GroupLesson(){ Id = 1 },
                    new GroupLesson() { Id = 2 },
                    new GroupLesson(){ Id = 3 }
                });

            _studentMarkRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<StudentMark, bool>>>())).ReturnsAsync(
                    new List<StudentMark>() {
                        new StudentMark() { Mark = mark }
                    });
            //act
            var result = await _underTest.SetFinalMarkToStudentForCourse(userId, courseId);

            //asserts
            _studentMarkRepository.Verify(x => x.CreateAsync(It.Is<StudentMark>(i => i.Mark == mark && i.CourseId == courseId 
                        && i.StudentId == student.Id)), Times.Once);
        }
        [Test]
        public async Task GetMarksByGroupAndStudentIdAsync_InvokeCreateStudentMark_WhenNotMarkExisting()
        {
            //arrange
            int studentId = It.IsAny<int>();
            int lessonId = It.IsAny<int>();
            int lessonDuration = It.IsAny<int>();
            Lesson lesson = new Lesson() { Id = lessonId, Duration = lessonDuration};
            GroupLesson groupLesson = new() { LessonId = lessonId , StartDate = DateTime.Now.AddMinutes(-lessonDuration - 1), 
                Lesson = lesson };
            _groupLessonService.Setup(x => x.GetLessonsByGroupIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<GroupLesson>() { groupLesson });

            _studentMarkRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<StudentMark, bool>>>())).ReturnsAsync(new List<StudentMark>());

            //act
            await _underTest.GetMarksByGroupAndStudentIdAsync(It.IsAny<int>(), studentId);

            //asserts
            _studentMarkRepository.Verify(x => x.CreateAsync(It.Is<StudentMark>(s => s.StudentId == studentId && s.LessonId == lessonId
                        && s.Mark == null)));
        }

        [Test]
        public async Task GetMarksByGroupAndStudentIdAsync_ReturnStudentMarksWithStudentMark_WhichExist()
        {
            //arrange
            int studentId = It.IsAny<int>();
            int lessonId = It.IsAny<int>();
            int mark = It.IsAny<int>();

            Lesson lesson = new Lesson() { Id = lessonId, Duration = It.IsAny<int>() };
            StudentMark studentMark = new () { StudentId = studentId, LessonId = lesson.Id, Mark = mark };
            GroupLesson groupLesson = new()
            {
                LessonId = lesson.Id,
                StartDate = DateTime.Now.AddMinutes(-lesson.Duration - 1),
                Lesson = lesson
            };
            _groupLessonService.Setup(x => x.GetLessonsByGroupIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<GroupLesson>() { groupLesson });

            _studentMarkRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<StudentMark, bool>>>()))
                .ReturnsAsync(new List<StudentMark>() { studentMark });

            //act
            var result = await _underTest.GetMarksByGroupAndStudentIdAsync(It.IsAny<int>(), studentId);

            //asserts
            Assert.AreEqual(result.First(), studentMark);
        }
    }
}