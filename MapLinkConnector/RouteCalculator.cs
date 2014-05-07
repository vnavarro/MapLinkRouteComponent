using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapLinkConnector.MaplinkV3_AddressFinder;
using MapLinkConnector.MaplinkV3_Route;

namespace MapLinkConnector
{
    public class RouteCalculator
    {
        private AddressOptions findAddressOptions;
        private List<AddressLocation> locations;
        private string token;

        public RouteCalculator()
        {
            this.findAddressOptions = new AddressOptions
            {
                usePhonetic = true,
                searchType = 2,
                resultRange = new ResultRange { pageIndex = 1, recordsPerPage = 10 }
            };
            this.token = System.Configuration.ConfigurationManager.AppSettings["MapLinkToken"];
            this.locations = new List<AddressLocation>();
        }

        public RouteOptions DefaultRouteOptions(int routeType)
        {
            return new RouteOptions
            {
                language = "portugues",
                routeDetails = new RouteDetails { descriptionType = 0, routeType = routeType, optimizeRoute = true },                
                vehicle = this.DefaultPopularVehicle()
            };
        }

        private Vehicle DefaultPopularVehicle(){
            return new Vehicle
            {
                tankCapacity = 20,
                averageConsumption = 9,
                fuelPrice = 3,
                averageSpeed = 60,
                tollFeeCat = 2
            };
        }


        public void FindAdresses(string addressList)
        {
            IList<Address> addresses = new JsonParser().ParseListOfAddress(addressList);

            var soapClient = new AddressFinderSoapClient();

            foreach (Address address in addresses)
            {
                var findAddressResponse = soapClient
                    .findAddress(address, this.findAddressOptions, this.token);

                AddressLocation location = findAddressResponse.addressLocation.ToList().First();
                this.locations.Add(location);
            }
        }

        public RouteStop[] GenerateRoutes()
        {
            List<RouteStop> routes = new List<RouteStop>();
            this.locations.ForEach( location => 
                routes.Add(new RouteStop
                {
                    description = location.address.street + location.address.houseNumber,
                    point = new MapLinkConnector.MaplinkV3_Route.Point { x = location.point.x, y = location.point.y }
                })
            );
                                    
            return routes.ToArray();                        
        }        

        public RouteTotals Calculate(int routeType)
        {
            RouteStop[] routes = this.GenerateRoutes();

            using (var routeSoapClient = new RouteSoapClient())
            {
                var getRouteTotalsResponse = routeSoapClient
                    .getRouteTotals(routes, this.DefaultRouteOptions(routeType), token);                
                return getRouteTotalsResponse;
            }
        }
    }
}
