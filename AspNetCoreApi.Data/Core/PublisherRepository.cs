using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApiContext _context;

        public PublisherRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _context.Publishers;
        }
    }
}
