namespace TradeGameCRAPI.Models
{
    public class PaginationDTO
    {
        public int page { get; set; } = 1;

        private int pageSize = 10;
        private readonly int maxPageSize = 50;

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value > maxPageSize)
                {
                    pageSize = maxPageSize;
                } else
                {
                    pageSize = value;
                }
            }
        }
    }
}
