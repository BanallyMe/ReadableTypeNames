using System;
using System.Linq;
using System.Reflection;

namespace BanallyMe.ReadableTypeNames.ExtensionMethods
{
    /// <summary>
    /// Extensions for the .NET Standard Type <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Produces a readable string representation of the type name. Pretty much looks
        /// like the type name in C# code files.
        /// </summary>
        /// <param name="type">The type whose readable type name is requested.</param>
        /// <returns>Readable type name of the passed type.</returns>
        public static string GetReadableTypeName(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            
            return TryGetNullableTypeName(type)
                ?? TryGetArrayTypeName(type)
                ?? TryGetGenericTypeName(type)
                ?? TryGetYieldReturnTypeName(type)
                ?? type.Name;
        }

        private static string? TryGetNullableTypeName(Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType is null)
                return null;

            return $"{nullableType.Name}?";
        }

        private static string? TryGetArrayTypeName(Type type)
        {
            if (!type.IsArray)
                return null;

            var basicTypeName = type.GetElementType().GetReadableTypeName();
            return $"{basicTypeName}[]";
        }

        private static string? TryGetGenericTypeName(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsGenericType)
                return null;
            
            var basicType = type.Name.Split('`')[0];
            var genericArgumentNames = type.GenericTypeArguments.Select(genericType => genericType.GetReadableTypeName());
            var genericTypesString = string.Join(", ", genericArgumentNames);
            return $"{basicType}<{genericTypesString}>";
        }

        private static string? TryGetYieldReturnTypeName(Type type)
        {
            if (!type.IsNested)
                return null;
            
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsSealed && typeInfo.Attributes != TypeAttributes.BeforeFieldInit)
                return null;
                
            return typeInfo.ImplementedInterfaces.FirstOrDefault()?.GetReadableTypeName();
        }
    }
}
