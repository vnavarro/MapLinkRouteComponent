using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Channels;

namespace RouteService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRouteService" in both code and config file together.
    [ServiceContract]
    public interface IRouteService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "route/totals/sample")]
        string RouteTotalsSample();

        [OperationContract]
        [WebInvoke(UriTemplate = "route/totals",            
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        string RouteTotalsJson(AddressListContract addressesJson);
    }
}
