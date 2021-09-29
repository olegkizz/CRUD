using IdentityNLayer.BLL.Interfaces;
using IdentityNLayer.Core.Entities;
using IdentityNLayer.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork Db;
        public TopicService(IUnitOfWork db)
        {
            Db = db;
        }
        public Task<int> CreateAsync(Topic entity)
        {
            Db.Topics.CreateAsync(entity);
            Db.Save();
            return Task.FromResult(entity.Id);
        }

        public Task<EntityEntry<Topic>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Topic>> GetAllAsync()
        {
            return Db.Topics.GetAllAsync();
        }

        public Task<Topic> GetByIdAsync(int id)
        {
            return Db.Topics.GetAsync(id);

        }

        public void UpdateAsync(Topic entity)
        {
            Db.Topics.UpdateAsync(entity);
            Db.Save();
        }
    }
}
