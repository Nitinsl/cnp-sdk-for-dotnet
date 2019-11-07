﻿using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestHttpActionEvent
    {
        private CnpOnline _cnp;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            var config = new Dictionary<string, string>
            {
                {"url", Properties.Settings.Default.url},
                {"reportGroup", "Default Report Group"},
                {"username", "DOTNET"},
                {"version", "11.0"},
                {"timeout", "5000"},
                {"merchantId", "101"},
                {"password", "TESTCASE"},
                {"printxml", "true"},
                {"proxyHost", Properties.Settings.Default.proxyHost},
                {"proxyPort", Properties.Settings.Default.proxyPort},
                {"logFile", Properties.Settings.Default.logFile},
                {"neuterAccountNums", "true"}
            };
            _cnp = new CnpOnline(config);
        }

        [Test]
        public void TestHttpEvents()
        {
            var requestCount = 0;
            var responseCount = 0;
            var httpActionCount = 0;

            _cnp.HttpAction += (sender, args) =>
            {
                var eventArgs = (Communications.HttpActionEventArgs)args;
                httpActionCount++;
                if (eventArgs.RequestType == Communications.RequestType.Request)
                {
                    requestCount++;
                }
                else if (eventArgs.RequestType == Communications.RequestType.Response)
                {
                    responseCount++;
                }
            };

            var capture = new capture
            {
                cnpTxnId = 123456000,
                amount = 106,
                id = "1"
            };

            _cnp.Capture(capture);
            Assert.AreEqual(httpActionCount, 2);
            Assert.AreEqual(requestCount, 1);
            Assert.AreEqual(responseCount, 1);
        }
    }
}
