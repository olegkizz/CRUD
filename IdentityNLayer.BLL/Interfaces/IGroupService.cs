using IdentityNLayer.Core.Entities;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        Teacher GetTeacher(int groupdId);
    }
}
