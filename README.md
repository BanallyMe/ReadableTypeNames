# ReadableTypeNames
The readableTypeNames library simply does, what its name says: It provides a name for types, that can be easily read and understood. It is based on .NET Standard 1.3+ and coded in C# language.


## Project status
![.NET Core Build and Test](https://github.com/BanallyMe/ReadableTypeNames/workflows/.NET%20Core%20Build%20and%20Test/badge.svg)

## Why use it
Do you know what type names in native C# look like? For a *simple* type it still looks ok, but as soon as you get a generic it becomes unreadable. Let's look at an ***IEnumerable\<string\>*** for example: While you simply write it as an ***IEnumerable\<string\>*** in C# code, the type's name in its properties is a gorgeous ***IEnumerable\`1***, swallowing the generic type parameters completely. If you need those, you would want to go for the type's FullName property which outputs something like ***System.Collections.Generic.IEnumerable\`1\[\[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\]\]***.
While this is packed with information about the type it somewhat pollutes output containing type names, like system logs for example.

Using ReadableTypeNames you can get the type name just as you write it in your code.

## How it works
The library provides a static extension method ***GetReadablyTypeName*** for System.Type, which returns the readable type name.

## Dependencies
The library doesn't have any dependencies besides the .NET standard library in at least version 1.3.

## Installation
You can install ReadableTypeNames by adding a dependency to its NuGet package. Just use a package manager or the equivalent dotnet command.
``` shell
dotnet add package BanallyMe.ReadableTypeNames
```

## Usage
Adding a using directive for ***BanallyMe.ReadableTypeNames.ExtensionMethods*** will enable you to use the extension method for System.Type.
``` csharp
using BanallyMe.ReadableTypeNames.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTypeNamesWithDescription(typeof(int), "Simple Type (int)");
            PrintTypeNamesWithDescription(typeof(int?), "Nullable Type (int?)");
            PrintTypeNamesWithDescription(typeof(IEnumerable<int>), "Generic Type (IEnumerable<int>)");
            PrintTypeNamesWithDescription(typeof(int[]), "Array Type (int[])");
            PrintTypeNamesWithDescription(typeof(IEnumerable<int?[]>), "Complex Type (IEnumerable<int?[]>)");
            Console.ReadLine();
        }

        static void PrintTypeNamesWithDescription(Type type, string typeDescription)
        {
            Console.WriteLine($"{typeDescription}:");
            Console.WriteLine($"Name:             {type.Name}");
            Console.WriteLine($"FullName:         {type.FullName}");
            Console.WriteLine($"ReadableTypeName: {type.GetReadableTypeName()}");
            Console.WriteLine();
        }
    }
}

/* WHICH WOULD CREATE AN OUTPUT LIKE:

Simple Type (int):
Name:             Int32
FullName:         System.Int32
ReadableTypeName: Int32

Nullable Type (int?):
Name:             Nullable`1
FullName:         System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]
ReadableTypeName: Int32?

Generic Type (IEnumerable<int>):
Name:             IEnumerable`1
FullName:         System.Collections.Generic.IEnumerable`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]
ReadableTypeName: IEnumerable<Int32>

Array Type (int[]):
Name:             Int32[]
FullName:         System.Int32[]
ReadableTypeName: Int32[]

Complex Type (IEnumerable<int?[]>):
Name:             IEnumerable`1
FullName:         System.Collections.Generic.IEnumerable`1[[System.Nullable`1[[System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]][], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]
ReadableTypeName: IEnumerable<Int32?[]>

*/
```

## Contributing
Feel free to provide pull requests to improve ReadableTypeNames. Please also make sure to update any tests affected by changed code.

## License
ReadableTypeNames is published under the [MIT license](https://choosealicense.com/licenses/mit/).
