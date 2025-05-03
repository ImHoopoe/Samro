using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var memberInfo = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault();
        if (memberInfo != null)
        {
            var displayAttr = memberInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null)
                return displayAttr.GetName(); 
        }
        return enumValue.ToString();
    }

    public static Task<string> GetDisplayNameAsync(this Enum enumValue)
    {
        
        return Task.FromResult(GetDisplayName(enumValue));
    }
}