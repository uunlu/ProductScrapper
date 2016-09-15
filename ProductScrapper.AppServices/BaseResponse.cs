using System;

namespace ProductScrapper.AppServices
{
    public class BaseResponse : IResponse
    {
    }

    /// <summary>
    /// Response with pagination metadata.
    /// </summary>
    public class FilterResponse : BaseResponse
    {
        public PaginationResponse Pager { get; set; } = new PaginationResponse();

        public class PaginationResponse
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public long TotalCount { get; set; }
            public int PageCount => (int)Math.Ceiling((decimal)TotalCount / (PageSize != 0 ? PageSize : 1));
        }
    }
}