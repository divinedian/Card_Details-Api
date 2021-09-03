using CardDetails.Core.Core.Application.Queries;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace CardDetailsTest
{
    using static Testing;
    public class BinListServiceTest: BinlistTestbase
    {
        [Test]
        public async Task ShouldGetCardDetailFromDbAsync()
        {
            mockServer.Given(Request.Create().WithPath("/53998344").UsingPost())
               .RespondWith(Response.Create()
               .WithStatusCode(200)
               .WithHeader("Content-Type", "application/json")
               .WithBody(BinListStubs.CARD_DETAILS));


            var Query = new GetCardDetailsQuery
            {
                Bin = 53998344
            };
            var result = await SendAsync(Query);

            result.Should().NotBeNull();
            result.Bin.Should().Be(53998344);
            result.Bank.Name.Should().Be("GTBANK");
            result.Country.Name.Should().Be("Nigeria");
        }
    }
}
