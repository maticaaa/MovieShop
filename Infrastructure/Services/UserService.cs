using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMovieRepository _movieRepository;

        public UserService(
            IUserRepository userRepository, 
            IPurchaseRepository purchaseRepository, 
            IReviewRepository reviewRepository,
            IFavoriteRepository favoriteRepository,
            IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        private string GetSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if(dbUser == null)
            {
                throw null;
            }

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);

            if(hashedPassword == dbUser.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            var newFavorite = await _favoriteRepository.Add(favorite);
            return newFavorite != null;
        }

        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _favoriteRepository.GetFavorite(favoriteRequest.UserId, favoriteRequest.MovieId);
            var newFavorite = await _favoriteRepository.Delete(favorite);
            return newFavorite != null;
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserId(id);
            var favoriteResponseModel = new FavoriteResponseModel
            {
                UserId = id
            };

            foreach (var favorite in favorites)
            {
                favoriteResponseModel.FavoriteMovies.Add(
                    new FavoriteResponseModel.FavoriteMovieResponseModel
                    {
                        Id = favorite.MovieId,
                        Title = favorite.Movie.Title,
                        PosterUrl = favorite.Movie.PosterUrl
                    });
            }
            return favoriteResponseModel;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var movie = await _movieRepository.GetMovieById(purchaseRequest.MovieId);
            var purchase = new Purchase
            {
                UserId = userId,
                PurchaseNumber = purchaseRequest.PurchaseNumber.GetValueOrDefault(),
                TotalPrice = movie.Price.GetValueOrDefault(),
                PurchaseDateTime = purchaseRequest.PurchaseDateTime.GetValueOrDefault(),
                MovieId = purchaseRequest.MovieId,
            };

            var newPurchase = await _purchaseRepository.Add(purchase);
            return newPurchase != null;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _purchaseRepository.GetPurchaseDetails(userId, purchaseRequest.MovieId);
            return purchase != null;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            var purchases = await _purchaseRepository.GetAllPurchasesForUser(id);
            var purchaseResponseModel = new PurchaseResponseModel()
            {
                UserId = id,
                TotalMoviesPurchased = purchases.Count()
            };
            foreach (var purchase in purchases)
            {
                purchaseResponseModel.PurchasedMovies.Add(new MovieCardResponseModel
                {
                    Id = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                });
            }
            return purchaseResponseModel;
        }

        public async Task<List<PurchaseDetailsResponseModel>> GetPurchasesDetails(int userId)
        {
            var purchases = await _purchaseRepository.GetAllPurchasesForUser(userId);
            var purchaseDetailsResponseModels = new List<PurchaseDetailsResponseModel>();
            
            foreach (var purchase in purchases)
            {
                purchaseDetailsResponseModels.Add(new PurchaseDetailsResponseModel
                {
                    Id = purchase.Id,
                    UserId = purchase.UserId,
                    PurchaseNumber = purchase.PurchaseNumber,
                    TotalPrice = purchase.TotalPrice,
                    PurchaseDateTime = purchase.PurchaseDateTime,
                    MovieId = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl,
                    ReleaseDate = purchase.Movie.ReleaseDate.GetValueOrDefault()
                });
            }
            
            return purchaseDetailsResponseModels;

        }
        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            var newReview = await _reviewRepository.Add(review);
            return newReview != null;
        }

        public async Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetReview(reviewRequest.UserId, reviewRequest.MovieId);
            if(review == null)
            {
                return false;
            }
            review.Rating = reviewRequest.Rating;
            review.ReviewText = reviewRequest.ReviewText;
            var newReview = await _reviewRepository.Update(review);
            return newReview != null;
        }

        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.GetReview(userId, movieId);
            if(review == null)
            {
                return false;
            }
            var newReview = await _reviewRepository.Delete(review);
            return newReview != null;
        }

        public async Task<UserReviewResponseModel> GetAllReviewsByUser(int id)
        {
            var reviews = await _reviewRepository.GetReviewsByUser(id);
            var userReviewResponseModel = new UserReviewResponseModel
            {
                UserId = id
            };
            foreach (var review in reviews)
            {
                userReviewResponseModel.Reviews.Add(new MovieReviewResponseModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    Name = review.User.FirstName + " " + review.User.LastName
                });
            }
            return userReviewResponseModel;
        }
    }
}
