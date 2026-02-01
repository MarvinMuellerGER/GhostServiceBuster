using System.Reflection;

namespace GhostServiceBuster.AspNet.Utils;

internal static class ParameterInfoExtensions
{
    public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) =>
        element.GetCustomAttributes(typeof(T), true).OfType<T>();
}