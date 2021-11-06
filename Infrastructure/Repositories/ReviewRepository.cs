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
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.User)
                .Where(r => r.MovieId == id)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return reviews;
        }

        public async Task<Review> GetReview(int userId, int movieId)
        {
            var review = await _dbContext.Reviews.
                Where(r => r.UserId == userId && r.MovieId == movieId)
                .FirstOrDefaultAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.User).Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        
    }
}
