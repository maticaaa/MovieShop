using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IReviewRepository: IAsyncRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByUser(int userId);
        Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1);
        Task<Review> GetReview(int userId, int movieId);
    }
}
