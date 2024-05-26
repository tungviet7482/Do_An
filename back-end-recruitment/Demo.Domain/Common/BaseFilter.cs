using Demo.Domain.Enum;

namespace Demo.Domain.Common
{
    public class BaseFilter
    {
        public int PageIndex { set; get; } = 1;
        public int PageSize { set; get; } = 20;
        public string? KeyWord { set; get; }
        public string? Slug { get; set; }
        public string? SortBy { get; set; }
        public bool? Published { get; set; }
        public int? CompanyId { get; set; }
    }
}
