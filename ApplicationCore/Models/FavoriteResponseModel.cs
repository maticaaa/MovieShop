using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class FavoriteResponseModel
    {
        public int UserId { get; set; }
        public List<FavoriteMovieResponseModel> FavoriteMovies { get; set; }

        public class FavoriteMovieResponseModel : MovieCardResponseModel
        {
            
        }

        public FavoriteResponseModel()
        {
            FavoriteMovies = new List<FavoriteMovieResponseModel>();
        }
    }
}