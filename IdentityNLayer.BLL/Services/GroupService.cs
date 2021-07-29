using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityNLayer.BLL.DTO;
using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.DAL.Entities;
using IdentityNLayer.DAL.Interfaces;

namespace IdentityNLayer.BLL.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork Db { get; set; }

        public GroupService(IUnitOfWork db)
        {
            Db = db;
        }
        public IEnumerable<GroupDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.CreateMap<Group, GroupDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Db.Groups.GetAll());
        }
    }
}
