using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RouteService
{
    [DataContract]        
    public class AddressContract
    {
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}