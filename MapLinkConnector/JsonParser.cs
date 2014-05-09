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
            List<Address> addresses = jsonArray.Select(p => this.ParseAddress(p as JObject)).ToList();
            return addresses;
        }

        public Address ParseAddress(string address)
        {
            return this.ParseAddress(JToken.Parse(address) as JObject);
        }

        public Address ParseAddress(JObject address)
        {
            return new Address
            {
                street = (string)address.GetValue(@"street", StringComparison.CurrentCultureIgnoreCase),
                houseNumber = (string)address.GetValue(@"number", StringComparison.CurrentCultureIgnoreCase),
                city = new City {
                    name = (string)address.GetValue(@"city", StringComparison.CurrentCultureIgnoreCase),
                    state = (string)address.GetValue(@"state", StringComparison.CurrentCultureIgnoreCase)
                }
            };
        }

    }
}
