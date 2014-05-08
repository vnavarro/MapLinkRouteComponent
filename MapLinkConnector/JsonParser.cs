using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using MapLinkConnector.MaplinkV3_AddressFinder;

namespace MapLinkConnector
{
    public class JsonParser
    {
        public List<Address> ParseListOfAddress(string addressList)
        {            
            JArray jsonArray = JArray.Parse(addressList);
            List<Address> addresses = jsonArray.Select(p => this.ParseAddress(p)).ToList();
            return addresses;
        }

        public Address ParseAddress(string address)
        {
            return this.ParseAddress(JToken.Parse(address));
        }

        public Address ParseAddress(JToken address)
        {
            return new Address
            {
                street = (string)address["street"],
                houseNumber = (string)address["number"],
                city = new City { name = (string)address["city"], state = (string)address["state"] }
            };
        }

    }
}
