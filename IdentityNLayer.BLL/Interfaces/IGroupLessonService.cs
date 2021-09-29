using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface IGroupLessonService : IBaseService<GroupLesson>
    {
        Task<List<GroupLesson>> GetLessonsByGroupIdAsync(int groupId);
        Task<GroupLesson> GetByLessonAndGroupIdAsync(int groupId, int lessonId);
    }
}
