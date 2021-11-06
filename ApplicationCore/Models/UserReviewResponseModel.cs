using ApplicationCore.Entities;
using System.Collections.Generic;

namespace ApplicationCore.ServiceInterfaces
{
    public class UserReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> Reviews { get; set; }
        public UserReviewResponseModel()
        {
            Reviews = new List<MovieReviewResponseModel>();
        }
    }
}