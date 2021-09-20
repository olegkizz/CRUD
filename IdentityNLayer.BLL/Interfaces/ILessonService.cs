using IdentityNLayer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Interfaces
{
    public interface ILessonService : IBaseService<Lesson>
    {
        public Task<bool> FileUseAsync(int fileId);
    }
}
