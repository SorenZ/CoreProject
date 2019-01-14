using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Mobin.CoreProject.Core.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// 
        /// By: Mojtaba Dashtinejad
        /// https://stackoverflow.com/a/25109103/3971911
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null) return "";

            try
            {
                var model = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>();

                return model?.Name;
            }
            catch (Exception)
            {
                return enumValue.ToString();
            }
        }

        public static string GetDisplayPrompt(this Enum enumValue)
        {
            if (enumValue == null) return "";

            try
            {
                var model = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>();

                return model?.Prompt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<EnumModel> EnumToList(Type enumType)
        {
            var items = new List<EnumModel>();

            var values = Enum.GetValues(enumType);
            items.AddRange(from Enum item in values
                           select new EnumModel
                           {
                               Value = Convert.ToInt32(item),
                               DisplayName = GetDisplayName(item),
                               Prompt = GetDisplayPrompt(item),
                               Name = item.ToString()
                           });

            return items;
        }

        public class EnumModel
        {
            public int Value { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Prompt { get; set; }
        }
    }
}
