using BanallyMe.ReadableTypeNames.ExtensionMethods;
using FluentAssertions;
using System;
using Xunit;
using Library = BanallyMe.ReadableTypeNames.ExtensionMethods.TypeExtensions;

namespace BanallyMe.ReadableTypeNames.UnitTests.ExtensionMethods
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void ThrowsArgumentNullExceptionIfPassedTypeIsNull()
        {
            Action passingNull = () => Library.GetReadableTypeName(null);

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

        private void AssertThatTypeReturnsReadableTypeName(Type type, string expectedReadableTypeName)
        {
            var returnedReadableTypeName = type.GetReadableTypeName();

            returnedReadableTypeName.Should().Be(expectedReadableTypeName);
        }
    }
}
