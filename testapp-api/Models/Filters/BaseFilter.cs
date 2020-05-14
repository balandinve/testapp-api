using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testapp_api.Models
{
    public abstract class BaseFilter
    {
        public int Take { get; set; } = 100;
        public int Page { get; set; } = 1;
        public SortOrderEnum SortOrder { get; set; } = SortOrderEnum.ASC;
        public string SortField { get; set; } = "Id";
    }
}
