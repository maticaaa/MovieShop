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
    public class GenresController : ControllerBase
    {
        public readonly IMovieService _movieService;

        public GenresController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _movieService.GetGenres();
            if (!genres.Any())
            {
                return NotFound("Not Found Any Genres");
            }
            return Ok(genres);
        }
    }
}
