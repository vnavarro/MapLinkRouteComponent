using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkConnector;

namespace UnitTest
{
    [TestClass]
    public class AddressAdapterTest
    {
        private string addressesJson = @"[{ 'street' : 'Avenida Paulista', 'number' : '1000', 'city' : 'São Paulo', 'state' : 'SP' },
            { 'street' : 'Avenida Brigadeiro Faria Lima', 'number' : '1000', 'city' : 'São Paulo', 'state' : 'SP' }]";

        private AddressAdapter subject;

        [TestInitialize]
        public void BeforeEach()
        {
            subject = new AddressAdapter();
        }

        [TestMethod]
        public void TestFindAddresses()
        {
            var result = subject.FindAdresses(addressesJson);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 2);
        }
    }
}
