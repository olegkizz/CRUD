using IdentityNLayer.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IGroupService : IBaseService<Group>
    {
        Task<Teacher> GetCurrentTeacher(int groupdId);
        public Task<IEnumerable<Group>> GetGroupsByUserIdAsync(string userId);
        public Task<bool> HasStudent(int groupId, string userId);
        public Task<IEnumerable<Student>> GetStudents(int? groupId, UserGroupStates? state = null);
        public Task<IEnumerable<Teacher>> GetTeachers(int? groupId, UserGroupStates? state = null);
        public Task<Group> CancelGroupAsync(int groupId);
        public Task<Group> FinishGroupAsync(int groupId);
        public Task<List<SelectListItem>> GetAvailableStatusAsync(int groupId);
    }
}
