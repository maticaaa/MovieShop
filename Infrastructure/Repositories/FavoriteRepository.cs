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
    public class FavoriteRepository: EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Favorite> GetFavorite(int userId, int MovieId)
        {
            var favorite = await _dbContext.Favorites.Where(f => f.UserId == userId && f.MovieId == MovieId).
                FirstOrDefaultAsync();
            return favorite;
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(int id)
        {
            var favorites = await _dbContext.Favorites.Include(f => f.Movie).Where(f => f.UserId == id).ToListAsync();
            return favorites;
        }
    }
}
