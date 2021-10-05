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
        public async Task<int> CreateAsync(Topic entity)
        {
            await Db.Topics.CreateAsync(entity);
            await Db.Save();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            await Db.Topics.DeleteAsync(id);
            await Db.Save();
        }

        public Task<IEnumerable<Topic>> GetAllAsync()
        {
            return Db.Topics.GetAllAsync();
        }

        public Task<Topic> GetByIdAsync(int id)
        {
            return Db.Topics.GetAsync(id);

        }

        public async Task UpdateAsync(Topic entity)
        {
            Db.Topics.Update(entity);
            await Db.Save();
        }
    }
}
