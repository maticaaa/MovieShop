using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);

        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
        
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<List<PurchaseDetailsResponseModel>> GetPurchasesDetails(int userId);

        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<bool> DeleteMovieReview(int userId, int movieId);
        Task<UserReviewResponseModel> GetAllReviewsByUser(int id);
    }
}
