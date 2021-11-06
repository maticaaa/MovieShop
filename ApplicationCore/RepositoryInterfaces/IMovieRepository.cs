using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
   public interface IMovieRepository: IAsyncRepository<Movie>
    {
        // method thtas gonn aget 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Movie>> GetTop30RatedMovies();
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
        Task<Movie> GetMovieById(int id);
        Task<IEnumerable<Movie>> GetMoviesByCast(int castId);
        
        
    }
}
