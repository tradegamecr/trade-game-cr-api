using HotChocolate;
using HotChocolate.Execution;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Helpers
{
    public static class QueryExceptionBuilder
    {
        public static QueryException NotFound<TInput>(int id)
        {
            return new QueryException(
                ErrorBuilder.New()
                    .SetMessage($"{typeof(TInput).Name} with the Id {id} not found")
                    .SetCode(Constants.GraphQLExceptionCodes.NotFound)
                    .Build());
        }

        public static QueryException BadPagingParams(string message)
        {
            return new QueryException(
                ErrorBuilder.New()
                    .SetMessage(message)
                    .SetCode(Constants.GraphQLExceptionCodes.BadPagingParams)
                    .Build());
        }

        public static QueryException BadRequest()
        {
            return new QueryException(
                ErrorBuilder.New()
                    .SetMessage("Bad request")
                    .SetCode(Constants.GraphQLExceptionCodes.BadRequest)
                    .Build());
        }

        public static QueryException Custom(string message, string code)
        {
            return new QueryException(
                ErrorBuilder.New()
                    .SetMessage(message)
                    .SetCode(code)
                    .Build());
        }
    }
}
