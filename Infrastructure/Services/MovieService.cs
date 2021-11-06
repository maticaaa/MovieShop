using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
   public class MovieService: IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ICastRepository _castRepository;

        public MovieService(
            IMovieRepository movieRepository, 
            IReviewRepository reviewRepository,
            IGenreRepository genreRepository,
            ICastRepository castRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _genreRepository = genreRepository;
            _castRepository = castRepository;
        }


        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if(movie == null)
            {
                //throw new Exception($"N movie Found for this {id}");
                return null;
            }
            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                //FavoritesCount = favoritesCount,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(
                    new GenreModel {
                        Id = genre.GenreId, 
                        Name = genre.Genre.Name});
            }

            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(
                    new CastResponseModel {
                        Id = cast.CastId,
                        Character = cast.Character,
                        Name = cast.Cast.Name,
                        ProfilePath = cast.Cast.ProfilePath
                    });
            }
            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(
                    new TrailerResponseModel{
                        Id = trailer.Id,
                        Name = trailer.Name,
                        TrailerUrl = trailer.TrailerUrl,
                        MovieId = trailer.MovieId
                    });
            }

            return movieDetails;
        }

        public async Task<List<GenreModel>> GetGenres()
        {
            var genres = await _genreRepository.GetAllGenres();

            var genreModels = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreModels.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }
            return genreModels;
        }

        
        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            // calling MovieRepository with DI based on IMovieRepository
            // I/O bound operation
            var movies = await _movieRepository.GetTop30RevenueMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id= movie.Id, PosterUrl= movie.PosterUrl, Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieReviewResponseModel>> GetMovieReviews(int id, int pageSize = 30, int page = 1)
        {
            var reviews = await _reviewRepository.GetMovieReviews(id, pageSize, page);
            var movieReviewResponseModels = new List<MovieReviewResponseModel>();
            foreach (var review in reviews)
            {
                movieReviewResponseModels.Add(new MovieReviewResponseModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    Name = review.User.FirstName + " " + review.User.LastName
                });
            }
            return movieReviewResponseModels;
        }


        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByCast(int castId)
        {
            var movies = await _movieRepository.GetMoviesByCast(castId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }
    }
}
