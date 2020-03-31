namespace SeniorWepApiProject.Contracts.V1.Requests.Queries
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 100;
        }
    }
}