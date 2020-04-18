using System;
using Xunit;

namespace Elwark.Extensions.Test
{
    public class DateExtensionsTest
    {
        [Fact]
        public void Weekend_true()
        {
            var date = new DateTime(2000, 1, 1);
            Assert.True(date.IsWeekend());
        }

        [Fact]
        public void Workday_true()
        {
            var date = new DateTime(2000, 1, 3);
            Assert.True(date.IsWorkingDay());
        }

        [Fact]
        public void Next_workday_success()
        {
            var value = new DateTime(2000, 1, 1);
            var expected = new DateTime(2000, 1, 3);
            Assert.Equal(expected, value.NextWorkday());
        }
        
        [Fact]
        public void Next_weekend_success()
        {
            var value = new DateTime(2000, 1, 1);
            var expected = new DateTime(2000, 1, 2);
            Assert.Equal(expected, value.NextWeekend());
        }
    }
}