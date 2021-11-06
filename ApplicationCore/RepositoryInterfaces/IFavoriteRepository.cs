using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IFavoriteRepository: IAsyncRepository<Favorite>
    {
        public Task<IEnumerable<Favorite>> GetFavoritesByUserId(int id);
        public Task<Favorite> GetFavorite(int userId, int MovieId);
    }
}
