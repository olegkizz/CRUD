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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace IdentityNLayer.Tests
{
    public class GroupServiceTests
    {
        private Mock<IUnitOfWork> _db;
        private IGroupService _underTest;
        private Mock<IRepository<Enrollment>> _enrollmentRepository;
        private Mock<IRepository<Group>> _groupRepository;
        private Mock<IStudentMarkService> _studentMarkService;
        private Mock<IRepository<GroupLesson>> _groupLessonRepository;
        private Mock<UserManager<Person>> _userManager;

        [SetUp]
        public void Setup()
        {
            _db = new Mock<IUnitOfWork>();
            _db = new Mock<IUnitOfWork>();
            _enrollmentRepository = new Mock<IRepository<Enrollment>>();
            _groupRepository = new Mock<IRepository<Group>>();
            _groupLessonRepository = new Mock<IRepository<GroupLesson>>();
            _userManager = new Mock<UserManager<Person>>((new Mock<IUserStore<Person>>()).Object, null, null, null, null, null, null, null, null);

            _studentMarkService = new Mock<IStudentMarkService>();
            _underTest = new GroupService(_db.Object, _studentMarkService.Object, _userManager.Object);

            _db.Setup(x => x.Enrollments).Returns(_enrollmentRepository.Object);
            _db.Setup(x => x.Groups).Returns(_groupRepository.Object);
            _db.Setup(x => x.GroupLessons).Returns(_groupLessonRepository.Object);
        }

        [Test]
        public async Task CancelGroupWithStatusStarted_ReturnsGroupWithStatusPending()
        {
            //arrange
            int grId = 1;

            Group group = new Group()
            {
                Id = 1,
                Status = GroupStatus.Started
            };
            _groupRepository.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(group);

            //act
            var result = await _underTest.CancelGroupAsync(grId);

            //assert
            Assert.AreEqual(group.Status, result.Status);
            Assert.AreEqual(group.Id, result.Id);
            _studentMarkService.Verify(x => x.DeleteGroupMarksAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetAvailableStatusWithGroupStatusPending_ReturnListWithStatusStarted()
        {
            //arrange
            int grId = 1;

            Group group = new Group()
            {
                Id = grId,
                Status = GroupStatus.Pending
            };
            _groupRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group>() { group });

            //act
            var result = await _underTest.GetAvailableStatusAsync(grId);

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Find(s => s.Text == group.Status.ToString() && s.Value == group.Status.ToString()) != null);
            Assert.IsTrue(result.Find(s => s.Text == GroupStatus.Started.ToString() && s.Value == GroupStatus.Started.ToString()) != null);
        }

        [Test]
        public async Task GetAvailableStatusWithGroupStatusStarted_ReturnListWithStatusCancelled_IfLastLessonWasNotFinished()
        {
            //arrange
            int grId = 1;
            int duration = 5;
            Group group = new Group()
            {
                Id = grId,
                Status = GroupStatus.Started
            };
            _groupRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group>() { group });
            _groupLessonRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<GroupLesson, bool>>>())).ReturnsAsync(new List<GroupLesson>()
            {
                new GroupLesson() {
                    StartDate = DateTime.Now.AddMinutes(-duration + 1),
                    Lesson = new Lesson(){ Duration = duration },
                }
            });

            //act
            var result = await _underTest.GetAvailableStatusAsync(grId);

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Find(s => s.Text == group.Status.ToString() && s.Value == group.Status.ToString()) != null);
            Assert.IsTrue(result.Find(s => s.Text == GroupStatus.Cancelled.ToString() && s.Value == GroupStatus.Cancelled.ToString()) != null);
        }
        [Test]
        public async Task GetAvailableStatusWithGroupStatusStarted_ReturnListWithStatusCancelled_IfLastLessonWasFinished()
        {
            //arrange
            int grId = 1;
            int duration = 5;
            Group group = new Group()
            {
                Id = grId,
                Status = GroupStatus.Started
            };
            _groupRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Group, bool>>>())).ReturnsAsync(new List<Group>() { group });
            _groupLessonRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<GroupLesson, bool>>>())).ReturnsAsync(new List<GroupLesson>()
            {
                new GroupLesson() {
                    StartDate = DateTime.Now.AddMinutes(-duration - 1),
                    Lesson = new Lesson(){ Duration = duration },
                }
            });

            //act
            var result = await _underTest.GetAvailableStatusAsync(grId);

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Find(s => s.Text == group.Status.ToString() && s.Value == group.Status.ToString()) != null);
            Assert.IsTrue(result.Find(s => s.Text == GroupStatus.Finished.ToString() && s.Value == GroupStatus.Finished.ToString()) != null);
        }
    }
}