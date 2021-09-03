using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WireMock.Server;
using WireMock.Settings;

namespace CardDetailsTest
{

    [TestFixture]
    public class BinlistTestbase
    {
        protected string baseUrl;
        protected FluentMockServer mockServer;

        [SetUp]
        public void PrepareClass()
        {
            var port = new Random().Next(5000, 6000);
            baseUrl = $"http://localhost:{port}";

            mockServer = FluentMockServer.Start(new FluentMockServerSettings
            {
                Urls = new[] { baseUrl },
                ReadStaticMappings = true,
                AllowBodyForAllHttpMethods = true
            });
        }
    }
}
