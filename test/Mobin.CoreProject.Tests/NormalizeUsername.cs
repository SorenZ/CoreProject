using System;
using Mobin.CoreProject.CrossCutting.Security.Extensions;
using Xunit;

namespace Mobin.CoreProject.Tests
{
    public class NormalizeUsername
    {
        [Fact]
        public void DomainName()
        {
            const string input = "MOBINNET\\m.dashtinejad";
            const string expected = "m.dashtinejad";

            var output = input.NormalizeUsername();

            Assert.Equal(expected, output);
        }

        [Fact]
        public void DomainEmail()
        {
            const string input = "m.dashtinejad@mobinnet.net";
            const string expected = "m.dashtinejad";

            var output = input.NormalizeUsername();

            Assert.Equal(expected, output);
        }

    }
}