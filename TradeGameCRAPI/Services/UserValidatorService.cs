using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Services
{
    public class UserValidatorService : IUserValidatorService
    {
        private readonly AppDbContext dbContext;

        public UserValidatorService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<QueryException>? Exist(int id)
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return QueryExceptionBuilder.NotFound<User>(id);
            }

            return null;
        }

        public async Task<QueryException>? ForPost(int userId, List<int> productsId)
        {
            var user = await dbContext.Users
                    .AsNoTracking()
                    .Where(x => x.Id == userId && x.Products != null && x.Products.Any())
                    .Include(x => x.Products.Where(p => productsId.Contains(p.Id)))
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return QueryExceptionBuilder.NotFound<User>(userId);
            }

            if (user.Products != null && !user.Products.Any())
            {
                return QueryExceptionBuilder.Custom
                    ($"Those products do not belong to the User with the Id {userId}.",
                    Constants.GraphQLExceptionCodes.BadRequest);
            }

            return null;
        }
    }
}
