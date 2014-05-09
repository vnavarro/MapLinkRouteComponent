using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MapLinkConnector;
using MapLinkConnector.MaplinkV3_AddressFinder;
using MapLinkConnector.MaplinkV3_Route;

namespace UnitTest
{
    [TestClass]
    public class RouteAdapterTest
    {
        private RouteAdapter subject;
        private List<AddressLocation> locations;
        

        [TestInitialize]
        public void BeforeEach()
        {
            subject = new RouteAdapter();

            AddressLocation origin = new AddressLocation()
            {                
                address = new Address(){
                    street = "Avenida Jabaquara",
                    houseNumber = "100"
                },
                point = new MapLinkConnector.MaplinkV3_AddressFinder.Point(){
                    x = -46.6405497,
                    y = -23.6267322
                }                 
            };
            AddressLocation destination = new AddressLocation()
            {                
                address = new Address(){
                    street = "Avenida Jabaquara",
                    houseNumber = "1000"
                },
                point = new MapLinkConnector.MaplinkV3_AddressFinder.Point()
                {
                    x = -46.6374321,
                    y = -23.6146506
                }                 
            };

            locations = new List<AddressLocation>();
            locations.Add(origin);
            locations.Add(destination);
        }

        [TestMethod]
        public void TestGenerateRoutes()
        {            
            var routes = subject.GenerateRoutes(locations);
            Assert.IsNotNull(routes);
            Assert.AreEqual(routes.Count(), 2);
        }

        [TestMethod]
        public void TestCalculate()
        {
            var routes                 = subject.GenerateRoutes(locations);
            var getRouteTotalsResponse = subject.Calculate(routes, Constants.ROUTE_TYPE_STANDARD_FASTEST);

            Assert.AreEqual(subject.ErrorMessage, String.Empty);
            Assert.IsInstanceOfType(getRouteTotalsResponse, typeof(RouteTotals));
        }

        [TestMethod]
        public void TestCalculateWithEmptyParameters()
        {
            var routes = subject.GenerateRoutes(new List<AddressLocation>());
            subject.Calculate(routes, Constants.ROUTE_TYPE_STANDARD_FASTEST);

            Assert.AreNotEqual(subject.ErrorMessage, String.Empty);
        }
    }
}
