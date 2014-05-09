using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapLinkConnector.MaplinkV3_AddressFinder;
using Newtonsoft.Json.Linq;

namespace MapLinkConnector
{
    public class AddressAdapter : AdapterBase
    {
        private AddressOptions findAddressOptions;
        private List<AddressLocation> lastLocations;

        public List<AddressLocation> LastLocations { get { return lastLocations; } }
        public string ErrorMessage { get; set; }

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
            this.lastLocations.Clear();
            this.ErrorMessage = String.Empty;
            List<Address> addresses;
            try
            {
                 addresses = new JsonParser().ParseListOfAddress(addressList);
            }
            catch
            {
                this.ErrorMessage = @"Please provide well formatted address json array.";
                return this.lastLocations;
            }

            var soapClient = new AddressFinderSoapClient();            

            foreach (Address address in addresses)
            {
                var findAddressResponse = soapClient
                    .findAddress(address, this.findAddressOptions, this.Token);

                List<AddressLocation> result = findAddressResponse.addressLocation.ToList();
                if (result.Count > 0)
                {
                    AddressLocation location = result.First();
                    this.lastLocations.Add(location);
                }                
            }

            if(this.lastLocations.Count == 0)
            {
                this.ErrorMessage = @"Provided addresses not found.";
            }
            
            return this.lastLocations;
        }        
    }
}
