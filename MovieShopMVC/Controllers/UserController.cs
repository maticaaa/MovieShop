using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Purchase(int movieId)
        {
            var purchaseRequestModel = new PurchaseRequestModel
            {
                MovieId = movieId
            };
            
            var isPurchased = await _userService.IsMoviePurchased(purchaseRequestModel, _currentUserService.UserId);
            if (isPurchased)
            {
                return RedirectToAction("Details", "Movies", movieId);
            }
            var purchase = await _userService.PurchaseMovie(purchaseRequestModel, _currentUserService.UserId);
            if (purchase)
            {
                return RedirectToAction("Purchases");
            }
            return RedirectToAction("Details", "Movies", movieId);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FavoriteMovie()
        {
            // TODO: favorite a movie when user clicking on Favorite button on Movie Deails Page
            return View();
        }

        [HttpGet]

        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            //TODO: logic for Purchases
            var userPurchases = await _userService.GetPurchasesDetails(userId);

            return View(userPurchases);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var userFavorites = await _userService.GetAllFavoritesForUser(userId);
            return View(userFavorites);
        }

        [HttpPost]

        public async Task<IActionResult> AddReview()
        {
            //add a new review done by the user for that movie
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            // TODO: get all the reviews by user
            return View();
        }
    }
}
