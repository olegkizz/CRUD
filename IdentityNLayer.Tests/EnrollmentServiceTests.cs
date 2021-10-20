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
    public class EnrollmentServiceTests
    {
        private Mock<IUnitOfWork> _db;
        private IEnrollmentService _underTest;
        private Mock<IRepository<Enrollment>> _enrollmentRepository;

        [SetUp]
        public void Setup()
        {
            _db = new Mock<IUnitOfWork>();
            _enrollmentRepository = new Mock<IRepository<Enrollment>>();
            _underTest = new EnrollmentService(_db.Object);

            _db.Setup(x => x.Enrollments).Returns(_enrollmentRepository.Object);
        }

        [Test]
        public async Task Enrol_NoAbortedEnrollmentExists_ReturnsEnrollmentId()
        {
            //arrange
            int enId = 1;
            string userId = "12345";
            int entityId = 2;
            UserRole role = UserRole.Student;


            _enrollmentRepository.Setup(m => m.FindAsync(It.IsAny<Expression<Func<Enrollment, bool>>>()))
                    .ReturnsAsync(new List<Enrollment>()
            {
                new Enrollment()
                {
                    Id = enId,
                    State = UserGroupState.Requested,
                    Role = role
                }
            });
            //act
            var result = await _underTest.EnrolInCourse(userId, entityId, role);

            //assert
            Assert.AreEqual(enId, result);
        }

        [Test]
        public async Task Enrol_WhenAbortedEnrollmentExists_ChangeEnrollemntStatus()
        {
            //arrange
            int enId = 1;
            string userId = "12345";
            int entityId = 2;
            UserRole role = UserRole.Student;
            Enrollment enrollment = new Enrollment()
            {
                Id = enId,
                State = UserGroupState.Aborted,
                Role = role
            };

            _enrollmentRepository.Setup(m => m.FindAsync(It.IsAny<Expression<Func<Enrollment, bool>>>()))
                    .ReturnsAsync(new List<Enrollment>(){enrollment});
            //act
            var result = await _underTest.EnrolInCourse(userId, entityId, role);

            //assert
            Assert.AreEqual(enId, result);
            Assert.AreEqual(UserGroupState.Requested, enrollment.State);
        }

        [Test]
        public async Task Enrol_WhenEnrollmentIsNull_InvokeCreateAsync()
        {
            //arrange
            string userId = "12345";
            int entityId = 2;
            UserRole role = UserRole.Student;

            _enrollmentRepository.Setup(m => m.FindAsync(It.IsAny<Expression<Func<Enrollment, bool>>>()))
                    .ReturnsAsync(new List<Enrollment>());
            //act
            var result = await _underTest.EnrolInCourse(userId, entityId, role);

            //assert
            _enrollmentRepository.Verify(x => x.CreateAsync(It.IsAny<Enrollment>()), Times.Once);
        }


        //[Test]
        /*    public void UnEnrol_UserFromGroup()
            {
                string userId = "12345";
                int entityId = 2;
                Enrollment enrollment = new Enrollment()
                {
                    UserID = userId,
                    EntityID = entityId,
                    Role = UserRoles.Student,
                    State = UserGroupStates.Applied
                };
                //arrange
                _db.Setup(x => x.Enrollments.Find(It.IsAny<Func<Enrollment, bool>>())).Returns(
                    new List<Enrollment>{enrollment});

                //act
                _underTest.UnEnrol(userId, entityId);

                //assert
                _db.Verify(d => d.Enrollments.UpdateAsync(It.Is<Enrollment>(x => x.UserID == enrollment.UserID
                && x.Role == enrollment.Role && x.State == enrollment.State && x.EntityID == enrollment.EntityID)), Times.Once);
            }*/
    }
}