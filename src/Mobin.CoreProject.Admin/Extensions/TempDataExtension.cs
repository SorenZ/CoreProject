using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mobin.CoreProject.Admin.Extensions
{
    public static class TempDataExtension
    {
        public static void AddResult(this ITempDataDictionary tempData, ServiceResult result)
        {
            tempData["ServiceResult.Succeed"] = result.Succeed;
            tempData["ServiceResult.Message"] = result.Message;
        }
    }
}
