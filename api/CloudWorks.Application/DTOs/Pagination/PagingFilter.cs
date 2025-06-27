namespace CloudWorks.Application.DTOs.Pagination
{
    public class PagingFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}