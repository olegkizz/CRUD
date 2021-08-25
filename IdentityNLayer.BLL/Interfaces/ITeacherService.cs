using IdentityNLayer.Core.Entities;
using System;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ITeacherService : IBaseService<Teacher>
    {
        Teacher GetTeacherByUserId(string userId);
    }
}
