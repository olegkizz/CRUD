using IdentityNLayer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IStudentToGroupActionService
    {
        public Task<IEnumerable<StudentToGroupAction>> GetAllAsync();

        public void Apply(int studentId, int GroupId);
        public void Grade(int studentId, int GroupId, Grade grade);
        public void Request(int studentId, int GroupId);
        public void Decline(int studentId, int GroupId);
 /*       public ActionsStudentGroup GetCurrentState(int studentId, int GroupId);
        public List<Group> GetCurrentGroups(int studentId);*/

    }
}
