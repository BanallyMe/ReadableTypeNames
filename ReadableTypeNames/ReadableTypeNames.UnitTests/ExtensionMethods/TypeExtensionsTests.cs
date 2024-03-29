﻿using BanallyMe.ReadableTypeNames.ExtensionMethods;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;
using Library = BanallyMe.ReadableTypeNames.ExtensionMethods.TypeExtensions;

namespace BanallyMe.ReadableTypeNames.UnitTests.ExtensionMethods
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionIfPassedTypeIsNull()
        {
            Action passingNull = () => Library.GetReadableTypeName(null!);

            passingNull.Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("type");
        }

        [Theory]
        [InlineData(typeof(string), "String")]
        [InlineData(typeof(int), "Int32")]
        [InlineData(typeof(bool), "Boolean")]
        [InlineData(typeof(float), "Single")]
        [InlineData(typeof(long), "Int64")]
        public void ReturnsNameForSimpleTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(int?), "Int32?")]
        [InlineData(typeof(bool?), "Boolean?")]
        [InlineData(typeof(long?), "Int64?")]
        public void ReturnsNameForNullableTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(string[]), "String[]")]
        [InlineData(typeof(int[]), "Int32[]")]
        [InlineData(typeof(long[]), "Int64[]")]
        [InlineData(typeof(bool[]), "Boolean[]")]
        public void ReturnsNameForArrayTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(int?[]), "Int32?[]")]
        [InlineData(typeof(long?[]), "Int64?[]")]
        [InlineData(typeof(bool?[]), "Boolean?[]")]
        public void ReturnsNameForArrayOfNullablesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<string>[]), "IEnumerable<String>[]")]
        [InlineData(typeof(IEnumerable<int>[]), "IEnumerable<Int32>[]")]
        [InlineData(typeof(KeyValuePair<string, int>[]), "KeyValuePair<String, Int32>[]")]
        public void ReturnsNameForArrayOfGenericsCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<string>), "IEnumerable<String>")]
        [InlineData(typeof(IEnumerable<int>), "IEnumerable<Int32>")]
        [InlineData(typeof(KeyValuePair<string, int>), "KeyValuePair<String, Int32>")]
        public void ReturnsNameForGenericTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<bool?>), "IEnumerable<Boolean?>")]
        [InlineData(typeof(IEnumerable<int?>), "IEnumerable<Int32?>")]
        [InlineData(typeof(KeyValuePair<string, int?>), "KeyValuePair<String, Int32?>")]
        public void ReturnsNameForGenericTypesWithNullableGenericArgumentCorrectly(Type type, string expectedReadableTypeName)
        {
            
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<bool[]>), "IEnumerable<Boolean[]>")]
        [InlineData(typeof(IEnumerable<int[]>), "IEnumerable<Int32[]>")]
        [InlineData(typeof(KeyValuePair<string, int[]>), "KeyValuePair<String, Int32[]>")]
        public void ReturnsNameForGenericTypesWithArrayGenericArgumentCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<bool?[]>), "IEnumerable<Boolean?[]>")]
        [InlineData(typeof(IEnumerable<int?[]>), "IEnumerable<Int32?[]>")]
        [InlineData(typeof(KeyValuePair<string, int?[]>), "KeyValuePair<String, Int32?[]>")]
        public void ReturnsNameForGenericTypesWithNullableArrayGenericArgumentCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<IEnumerable<bool>>), "IEnumerable<IEnumerable<Boolean>>")]
        [InlineData(typeof(IEnumerable<IEnumerable<int>>), "IEnumerable<IEnumerable<Int32>>")]
        [InlineData(typeof(KeyValuePair<string, IEnumerable<int>>), "KeyValuePair<String, IEnumerable<Int32>>")]
        public void ReturnsNameForNestedGenericTypeCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<IEnumerable<bool?[]>>), "IEnumerable<IEnumerable<Boolean?[]>>")]
        [InlineData(typeof(IEnumerable<IEnumerable<KeyValuePair<bool?, int[]>>>), "IEnumerable<IEnumerable<KeyValuePair<Boolean?, Int32[]>>>")]
        [InlineData(typeof(KeyValuePair<string, IEnumerable<int?[]>>), "KeyValuePair<String, IEnumerable<Int32?[]>>")]
        public void ReturnsNameForDeeplyNestedTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }
        
        [Theory]
        [MemberData(nameof(Testdata.yieldReturn), MemberType=typeof(Testdata))]
        public void ReturnsNameForYieldReturnTypesCorrectly(Type type, string expectedReadableTypeName)
        {
            AssertThatTypeReturnsReadableTypeName(type, expectedReadableTypeName);
        }

        private static void AssertThatTypeReturnsReadableTypeName(Type type, string expectedReadableTypeName)
        {
            var returnedReadableTypeName = type.GetReadableTypeName();

            returnedReadableTypeName.Should().Be(expectedReadableTypeName);
        }

        private static class Testdata
        {
            public static IEnumerable<object[]> yieldReturn => new List<object[]>
            {
                new object[] { NormalYieldReturn().GetType(), "IEnumerable<String>" },
                new object[] { NestedYieldReturn().GetType(), "IEnumerable<String>[]" },
            };
            
            private static IEnumerable<string> NormalYieldReturn()
            {
                yield return "test";
            }

            private static IEnumerable<IEnumerable<string>> NestedYieldReturn() => new[]
            {
                NormalYieldReturn()
            };
        }
    }
}
