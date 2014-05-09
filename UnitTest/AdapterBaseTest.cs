using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkConnector;

namespace UnitTest
{
    [TestClass]
    public class AdapterBaseTest
    {
        private AdapterBase subject;

        [TestMethod]
        public void TestTokenAfterInitialization()
        {
            subject = new AdapterBase();
            Assert.IsNotNull(subject.Token);
        }
    }
}
