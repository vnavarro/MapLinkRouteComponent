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

        private string wrongAddressesJson = @"[{ 'street' : 'bosidffjsakdlfçjasd', 'number' : '1000', 'city' : 'São Paulo', 'state' : 'SP' },
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

        [TestMethod]
        public void TestFindAddressesWithAddressNotFound()
        {
            var result = subject.FindAdresses(wrongAddressesJson);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void TestFindAddressesWithEmptyJson()
        {
            var result = subject.FindAdresses(@"");
            Assert.AreNotEqual(subject.ErrorMessage, String.Empty);
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void TestFindAddressesWithEmptyArrayJson()
        {
            var result = subject.FindAdresses(@"[]");
            Assert.AreNotEqual(subject.ErrorMessage, String.Empty);
            Assert.AreEqual(result.Count, 0);
        }


        
    }
}
