using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapLinkConnector.MaplinkV3_AddressFinder;
using Newtonsoft.Json.Linq;
using MapLinkConnector.MaplinkV3_Route;

namespace MapLinkConnector
{
    public class AddressAdapter : AdapterBase
    {
        private AddressOptions findAddressOptions;
        private List<AddressLocation> lastLocations;

        public List<AddressLocation> LastLocations { get { return lastLocations; } }

        public AddressAdapter()
        {            
            this.findAddressOptions = new AddressOptions
            {
                usePhonetic = true,
                searchType = 2,
                resultRange = new ResultRange { pageIndex = 1, recordsPerPage = 10 }
            };
            this.lastLocations = new List<AddressLocation>();
        }

        public List<AddressLocation> FindAdresses(string addressList)
        {
            List<Address> addresses = new JsonParser().ParseListOfAddress(addressList);

            var soapClient = new AddressFinderSoapClient();            

            foreach (Address address in addresses)
            {
                var findAddressResponse = soapClient
                    .findAddress(address, this.findAddressOptions, this.Token);

                AddressLocation location = findAddressResponse.addressLocation.ToList().First();
                this.lastLocations.Add(location);
            }
            
            return this.lastLocations;
        }

        public RouteStop[] GenerateRoutes()
        {
            List<RouteStop> routes = new List<RouteStop>();
            this.lastLocations.ForEach(location =>
                routes.Add(new RouteStop
                {
                    description = location.address.street + location.address.houseNumber,
                    point = new MapLinkConnector.MaplinkV3_Route.Point { x = location.point.x, y = location.point.y }
                })
            );

            return routes.ToArray();
        }        
    }
}
