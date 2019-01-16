namespace Mobin.CoreProject.CrossCutting.Security.Extensions
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