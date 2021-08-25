using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using System.Collections.Generic;

public interface ICourseService : IBaseService<Course>
{
    public IEnumerable<Enrollment> GetStudentRequests(int id);
    public IEnumerable<Enrollment> GetTeacherRequests(int id);
    public IEnumerable<Topic> GetAvailableTopics();
}
