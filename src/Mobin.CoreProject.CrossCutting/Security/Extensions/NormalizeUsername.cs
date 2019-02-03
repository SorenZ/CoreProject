using Mobin.CoreProject.CrossCutting.Security.SSOT;

namespace Mobin.CoreProject.CrossCutting.Security.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeUsername(this string str) =>
            IdentityConfig.IsWindowsAuth
                ? str.ToLower()
                    .Replace("mobinnet\\", "")
                    .Replace("@mobinnet.net", "")
                : str;
    }
}