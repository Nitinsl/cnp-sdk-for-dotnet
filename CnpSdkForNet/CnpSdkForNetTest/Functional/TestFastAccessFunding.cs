﻿using System.Collections.Generic;
using NUnit.Framework;

namespace Cnp.Sdk.Test.Functional
{
    [TestFixture]
    internal class TestFastAccessFunding
    {
        
        private CnpOnline _cnp;
        private Dictionary<string, string> _config;

        [OneTimeSetUp]
        public void SetUpCnp()
        {
            CommManager.reset();
            _config = new Dictionary<string, string>
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

            _cnp = new CnpOnline(_config);
        }
        
        
        [Test]
        public void TestFastAccessFunding_token()
        {
            fastAccessFunding fastAccessFunding = new fastAccessFunding();
            fastAccessFunding.id = "A123456";
            fastAccessFunding.reportGroup = "FastPayment";
            fastAccessFunding.fundingSubmerchantId = "SomeSubMerchant";
            fastAccessFunding.submerchantName = "Some Merchant Inc.";
            fastAccessFunding.fundsTransferId = "123e4567e89b12d3";
            fastAccessFunding.amount = 3000;
            fastAccessFunding.token = new cardTokenType
            {
                cnpToken = "1111000101039449",
                expDate = "1112",
                cardValidationNum = "987",
                type = methodOfPaymentTypeEnum.VI,
            };
            
            var response = _cnp.FastAccessFunding(fastAccessFunding);
            Assert.AreEqual("000", response.response);
            StringAssert.AreEqualIgnoringCase("Approved", response.message);
        }
        
        [Test]
        [Ignore("Sandbox does not check for mismatch. Production does check.")]
        public void TestFastAccessFunding_mixedNames()
        {
            fastAccessFunding fastAccessFunding = new fastAccessFunding();
            fastAccessFunding.id = "A123456";
            fastAccessFunding.reportGroup = "FastPayment";
            fastAccessFunding.fundingSubmerchantId = "SomeSubMerchant";
            fastAccessFunding.customerName = "Some Customer";
            fastAccessFunding.fundsTransferId = "123e4567e89b12d3";
            fastAccessFunding.amount = 3000;
            fastAccessFunding.token = new cardTokenType
            {
                cnpToken = "1111000101039449",
                expDate = "1112",
                cardValidationNum = "987",
                type = methodOfPaymentTypeEnum.VI,
            };
            
            Assert.Throws<CnpOnlineException>(() => { _cnp.FastAccessFunding(fastAccessFunding); });
        }
    }
}