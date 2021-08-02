using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private IMapper _mapper { get; set; }
        public GroupService(IUnitOfWork db, IMapper mapper = null)
        {
            Db = db;
            _mapper = mapper;
        }
        public IEnumerable<GroupDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Db.Groups.GetAll());
        }
        public GroupDTO GetById(int id)
        {
            return _mapper.Map<Group, GroupDTO>(Db.Groups.Get(id));
        }

        public void Create(GroupDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Update(GroupDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
