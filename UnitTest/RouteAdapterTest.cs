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
                point = new Point(){
                    x = -23.6267322,
                    y = -46.6405497
                }                 
            };
            AddressLocation destination = new AddressLocation()
            {                
                address = new Address(){
                    street = "Avenida Jabaquara",
                    houseNumber = "1000"
                },
                point = new Point(){
                    x = -23.6146506,
                    y = -46.6374321
                }                 
            };

            List<AddressLocation> locations = new List<AddressLocation>();
            locations.Add(origin);
            locations.Add(destination);
        }

        [TestMethod]
        public void TestGenerateRoutes()
        {            
            var routes = subject.GenerateRoutes(locations);
            Assert.IsNotNull(routes);
            Assert.AreEqual(routes.Count, 2);
        }

        [TestMethod]
        public void TestCalculate()
        {
            var routes                 = subject.GenerateRoutes(locations);
            var getRouteTotalsResponse = subject.Calculate(routes, Constants.ROUTE_TYPE_STANDARD_FASTEST);

            Assert.IsNotNull(getRouteTotalsResponse);
            Assert.IsInstanceOfType(getRouteTotalsResponse, typeof(RouteTotals));
        }        
    }
}
