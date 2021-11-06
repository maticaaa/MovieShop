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
    public class CastController : ControllerBase
    {
        public readonly IMovieService _movieService;

        public CastController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMoviesByCast(int id)
        {
            var movies = await _movieService.GetMoviesByCast(id);
            if (!movies.Any())
            {
                return NotFound($"No Movie for Cast {id}");
            }
            return Ok(movies);
        }
    }

    
}
