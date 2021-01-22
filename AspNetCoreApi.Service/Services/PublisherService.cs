using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork uow;

        public PublisherService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await uow.Publishers.GetAll();
        }

        public async Task<Publisher> GetById(int id)
        {
            return await uow.Publishers.GetById(id);
        }

        public async Task<bool> Add(Publisher publisher)
        {
            await uow.Publishers.Add(publisher);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Update(int id, Publisher publisher)
        {
            var oldEntity = await uow.Publishers.GetById(id);
            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.Name = publisher.Name;

            uow.Publishers.Update(oldEntity);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            await uow.Publishers.Delete(id);
            return await uow.CompleteAsync();
        }
    }
}