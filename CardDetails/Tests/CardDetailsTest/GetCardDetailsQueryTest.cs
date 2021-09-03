using CardDetails.Core.Core.Application.Queries;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CardDetailsTest
{
    using static Testing;
    public class GetCardDetailsQueryTest : TestBase
    {      
        [Test]
        public async Task ShouldGetCardDetailFromDbAsync()
        {
            WithCardDetails();

            var Query = new GetCardDetailsQuery
            {
                Bin = 53998344
            };
            var result = await SendAsync(Query);

            result.Should().NotBeNull();
            result.Bin.Should().Be(53998344);
            result.Bank.Name.Should().Be("GTBank");
            result.Country.Name.Should().Be("Nigeria");
        }
    }
}
