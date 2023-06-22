using NPOI.SS.Formula.Functions;

namespace API2.Dtos
{
    public class PaginatedListDto
    {

        public List<T> Items { get; }
        public int TotalPages { get; }
        public int PageIndex { get; }
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrev => PageIndex > 1;
    }
}
