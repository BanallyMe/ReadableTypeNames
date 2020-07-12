using System;

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
            
            return type.Name;
        }
    }
}
