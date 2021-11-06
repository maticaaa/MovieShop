using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<List<MovieCardResponseModel>> GetTop30RatedMovies();
        Task<List<MovieCardResponseModel>> GetMoviesByGenre(int genreId);
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieReviewResponseModel>> GetMovieReviews(int id, int pageSize = 30, int page = 1);
        Task<List<GenreModel>> GetGenres();
        Task<List<MovieCardResponseModel>> GetMoviesByCast(int castId);
    }
}
