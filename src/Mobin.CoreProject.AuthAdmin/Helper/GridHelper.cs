using System.Collections.Generic;
using Alamut.Data.Paging;
using DNTPersianUtils.Core;

namespace Mobin.CoreProject.AuthAdmin.Helper
{
    public static class GridHelper
    {
        public static string Counter(IPaginated paginated, int counter)
        {
            var index = (paginated.CurrentPage - 1) * paginated.PageSize + counter;
            return index.ToPersianNumbers();
        }
    }
}