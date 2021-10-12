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

namespace IdentityNLayer.Tests
{
    public class EnrollmentServiceTests
    {
        private Mock<IUnitOfWork> _db;
        private IEnrollmentService _underTest;

        [SetUp]
        public void Setup()
        {
            _db = new Mock<IUnitOfWork>();
            _underTest = new EnrollmentService(_db.Object);
        }

        [Test]
        public async Task Enrol_UserToCourse()
        {
            string userId = "12345";
            int entityId = 2;
            UserRole role = UserRole.Student;
            //arrange
            IEnumerable<Enrollment> enrollments = await _db.Object.Enrollments.FindAsync(en => en.UserID == "");
            IEnumerable<Group> groups = await _db.Object.Groups.FindAsync(gr => gr.TeacherId == 0);
            _db.Setup(x => enrollments).Returns((IEnumerable<Enrollment>)null);
            _db.Setup(x => groups).Returns((IEnumerable<Group>)null);

            //act
            await _underTest.EnrolInCourse(userId, entityId, role);

            //assert
            _db.Verify(d => d.Enrollments.CreateAsync(It.Is<Enrollment>(x => x.UserID == userId
            && x.Role == role && x.State == UserGroupState.Requested && x.EntityID == entityId)), Times.Once);
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