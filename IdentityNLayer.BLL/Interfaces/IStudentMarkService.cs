using IdentityNLayer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentMarkService : IBaseService<StudentMark>
    {
        public Task<IEnumerable<StudentMark>> GetMarksByGroupAndStudentIdAsync(int courseId, int studentId);
        public Task<StudentMark> GetByStudentAndLessonIdAsync(int studentId, int lessonId);
        public void DeleteGroupMarksAsync(int groupId);
        public Task<List<Group>>GetStudentGroupsAsync(int studentId);
        public Task<int> GetMarkByStudentAndGroupIdAsync(int studentId, int groupId);
    }
}
