namespace Mobin.CoreProject.Core.Extensions.StringExtensions
{
    public static partial class StringExtensions
    {
        public static string NormalizeUsername(this string str)
        {
            str = str.ToLower().Replace("mobinnet\\", "");
            str = str.ToLower().Replace("@mobinnet.net", "");

            return str;
        }
    }
}