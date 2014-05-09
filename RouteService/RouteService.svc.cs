using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MapLinkConnector;
using System.ServiceModel.Web;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.ServiceModel.Channels;

namespace RouteService
{    
    public class RouteService : IRouteService
    {
        public string RouteTotalsSample()
        {                        
            var alist = new List<AddressContract>();
            alist.Add(new AddressContract(){
                Street = "Avenida Paulista",
                Number = "1000",
                City = "São Paulo",
                State = "SP"
            });

            alist.Add(new AddressContract(){
                Street = "Avenida Brigadeiro Faria Lima",
                Number = "100",
                City = "São Paulo",
                State = "SP"
            });

             AddressListContract order = new AddressListContract
            {
                RouteType = Constants.ROUTE_TYPE_AVOID_TRAFFIC,
                addresses = alist
            };      
      
            DataContractJsonSerializer ser = 
                    new DataContractJsonSerializer(typeof(AddressListContract));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, order);
            string data = 
                Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();            
            webClient.Headers["Content-type"] = "application/json";            
            webClient.Encoding = Encoding.UTF8;
            var result = webClient.UploadString("http://localhost:52306/RouteService.svc/route/totals", "POST", data);
            return result;
        }

        public string RouteTotalsJson(AddressListContract addressesJson)
        {
            if (addressesJson == null)
            {
                return MissingAddressParameter();
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(addressesJson.addresses);
            return CalculateRouteTotals(json, addressesJson.RouteType);
        }

        private string CalculateRouteTotals(string addressesJson, int routeType)
        {
            AddressAdapter addressAdapter = new AddressAdapter();
            var locations = addressAdapter.FindAdresses(addressesJson);
            if (!String.IsNullOrEmpty(addressAdapter.ErrorMessage))
            {
                return addressAdapter.ErrorMessage;
            }

            RouteAdapter routeAdapter = new RouteAdapter();
            var routes = routeAdapter.GenerateRoutes(locations);
            var totals = routeAdapter.Calculate(routes, routeType);

            if (!String.IsNullOrEmpty(routeAdapter.ErrorMessage))
            {
                return routeAdapter.ErrorMessage;
            }

            return routeAdapter.RouteTotalsToJson(totals);
        }

        private string MissingAddressParameter()
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return @"{ error : 'please provide origin and destination addresses' }";
        }
    }
}
