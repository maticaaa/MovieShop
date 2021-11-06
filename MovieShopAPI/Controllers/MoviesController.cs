using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound($"No Movie Found for {id}");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId, [FromQuery] int pagesize=30, [FromQuery] int pageIndex=1)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var movies = await _movieService.GetMovieReviews(id);
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);
        }
    }
}
