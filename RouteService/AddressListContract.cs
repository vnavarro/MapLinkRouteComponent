using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace RouteService
{   
    [DataContract]    
    public class AddressListContract
    {
        [DataMember]
        public List<AddressContract> addresses { get; set; }

        [DataMember]
        public int RouteType { get; set;} 
    }
}