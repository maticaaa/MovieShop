using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository: EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Cast> GetCastById(int id)
        {
            var cast = await _dbContext.Casts.Where(c => c.Id == id).FirstOrDefaultAsync();
            return cast;
        }
    }
}
