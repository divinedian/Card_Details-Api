using CardDetails.Core.Core.Application.Commands;
using CardDetails.Data.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CardDetailsTest
{
    using static Testing;
    public class AddCardDetailsCommandTest : TestBase
    {  
        [Test]
        public async Task ShouldAddDetailsCommandTestAsync()
        {
            var command = new AddCardDetailsCommand
            {
                Bank = new Bank()
                {
                    Name = "GTBank",
                    Phone = "2348039003900",
                },
                Bin = 53998344,
                Brand = "Debit",
                Scheme = "mastercard",
                Type = "debit",
                Country = new Country()
                {
                    Name = "Nigeria",
                    Alpha2 = "NG",
                    Currency = "NGN",
                    Emoji = "NG",
                    Latitude = 10,
                    Longitude = 8,
                    Numeric = "566"
                },
                Number = new Number()                
            };

            var result = await SendAsync(command);
            result.Should().BeTrue();
        }        

        [Test]
        public async Task ShouldNotAllowDuplicate()
        {
            var command = new AddCardDetailsCommand
            {
                Bank = new Bank()
                {
                    Name = "GTBank",
                    Phone = "2348039003900",
                },
                Bin = 53998344,
                Brand = "Debit",
                Scheme = "mastercard",
                Type = "debit",
                Country = new Country()
                {
                    Name = "Nigeria",
                    Alpha2 = "NG",
                    Currency = "NGN",
                    Emoji = "NG",
                    Latitude = 10,
                    Longitude = 8,
                    Numeric = "566"
                },
                Number = new Number()
            };
            var result = await SendAsync(command);
            result.Should().BeTrue();
            result = await SendAsync(command);
            result.Should().BeFalse();

        }
    }
}