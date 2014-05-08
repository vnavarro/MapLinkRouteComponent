using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace UnitTest
{
    [TestClass]
    public class SerializerTest
    {
        private MapLinkConnector.Serializer subject;

        [TestInitialize]
        public void BeforeEach()
        {
            subject = new MapLinkConnector.Serializer();
        }

        [TestMethod]
        public void TestObjectToJson()
        {
            JObject json = subject.ObjectToJson(new Object());
            Assert.IsInstanceOfType(json, typeof(JObject));
        }
    }
}
