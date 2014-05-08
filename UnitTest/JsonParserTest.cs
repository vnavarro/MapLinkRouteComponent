using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkConnector;
using MapLinkConnector.MaplinkV3_AddressFinder;

namespace UnitTest
{
    [TestClass]
    public class JsonParserTest
    {
        private JsonParser subject;

        [TestInitialize]
        public void BeforeEach()
        {
            subject = new JsonParser();
        }

        [TestMethod]            
        public void TestParseAddress()
        {
            string address = @"{ street : 'Avenida Paulista', number : '1000', city : 'São Paulo', state : 'SP' }";
            Address parsedAddress = subject.ParseAddress(address);
            Assert.IsInstanceOfType(parsedAddress, typeof(Address));
            Assert.AreEqual("Avenida Paulista", parsedAddress.street);
            Assert.AreEqual("1000", parsedAddress.houseNumber);
            Assert.AreEqual("São Paulo", parsedAddress.city.name);
            Assert.AreEqual("SP", parsedAddress.city.state);
        }

        [TestMethod]
        public void TestParseListOfAddress()
        {
            string addressList = @"[{ street : 'Avenida Paulista', number : '1000', city : 'São Paulo', state : 'SP' }, 
{ street : 'Avenida Brigadeiro Faria Lima', number : '1000', city : 'São Paulo', state : 'SP' }]";
            List<Address> parsedList = subject.ParseListOfAddress(addressList);
            Assert.AreEqual(parsedList.Count, 2);
        }
    }
}
