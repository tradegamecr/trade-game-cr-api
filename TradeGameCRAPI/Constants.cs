namespace TradeGameCRAPI
{
    public static class Constants
    {
        public static class GraphQLOperationTypes
        {
            public const string Query = "Queries";
            public const string Mutation = "Mutations";
            public const string Subscription = "Subscriptions";
        }

        public static class GraphQLExceptionCodes
        {
            public const string NotFound = "NOT_FOUND";
            public const string BadPagingParams = "BAD_PAGING_PARAMS";
            public const string BadRequest = "BAD_RESQUEST";
        }

        public static class ESIndexes
        {
            public const string Cards = "cards";
        }
    }
}
