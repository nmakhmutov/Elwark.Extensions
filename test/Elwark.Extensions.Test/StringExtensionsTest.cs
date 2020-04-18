using System;
using Xunit;

namespace Elwark.Extensions.Test
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Null_if_empty_null_result(string value)
        {
            Assert.Null(value.NullIfEmpty());
        }

        [Theory]
        [InlineData("value")]
        [InlineData(" ")]
        public void Null_if_empty_not_null_result(string value)
        {
            Assert.NotNull(value.NullIfEmpty());
        }

        [Theory]
        [InlineData("value", "Value")]
        [InlineData("vAlue", "Value")]
        [InlineData("valuE", "Value")]
        [InlineData("Value", "Value")]
        public void Capitalize_string_test(string value, string expected)
        {
            Assert.Equal(expected, value.Capitalize());
        }

        [Theory]
        [InlineData("Value", 1, "V")]
        [InlineData("Value", 2, "Va")]
        [InlineData("Value", 3, "Val")]
        public void Truncate_string_without_dots(string value, int trim, string expected)
        {
            Assert.Equal(expected, value.Truncate(trim));
        }

        [Theory]
        [InlineData("http://test.com")]
        [InlineData("ftp://test.com")]
        public void Convert_to_uri(string value)
        {
            Assert.Equal(new Uri(value), value.ToUri());
        }
    }
}