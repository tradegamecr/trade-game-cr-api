using HotChocolate.Execution;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Interfaces
{
    public interface IUserValidatorService
    {
        Task<QueryException>? Exist(int id);

        Task<QueryException>? ForPost(int userId, List<int> productsId);
    }
}
