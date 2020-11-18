using BookStore.Constants;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.Data;
using BookStore.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Data
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepository _genericRepository;

        public AuthorService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<AuthorResponse>> GetAuthors()
        {
            var response = await _genericRepository.Get<List<AuthorResponse>>(ApiConstants.BaseAuthor);
            return response;
        }
    }
}