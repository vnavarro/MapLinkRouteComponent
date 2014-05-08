using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkConnector;

namespace UnitTest
{
    [TestClass]
    public class RouteAdapterTest
    {
        private RouteAdapter subject;

        [TestInitialize]
        public void BeforeEach()
        {
            subject = new RouteAdapter();
        }

        [TestMethod]
        public void TestCalculate()
        {
//            var calculator = new RouteAdapter();
//            calculator.FindAdresses(@"[{ 'street' : 'Avenida Paulista', 'number' : '1000', 'city' : 'São Paulo', 'state' : 'SP' },
//            { 'street' : 'Avenida Brigadeiro Faria Lima', 'number' : '1000', 'city' : 'São Paulo', 'state' : 'SP' }]");            
//            var getRouteTotalsResponse = subject.Calculate(Constants.ROUTE_TYPE_STANDARD_FASTEST);
        }
    }
}
