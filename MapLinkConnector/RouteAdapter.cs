using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapLinkConnector.MaplinkV3_AddressFinder;
using MapLinkConnector.MaplinkV3_Route;

namespace MapLinkConnector
{
    public class RouteAdapter : AdapterBase
    {
        private RouteOptions routeOptions;
        private RouteOptions DefaultRouteOptions(int routeType)
        {
            if (routeOptions == null)
            {
                this.routeOptions = new RouteOptions
                {
                    language = "portugues",
                    routeDetails = new RouteDetails { descriptionType = 0, routeType = routeType, optimizeRoute = true },
                    vehicle = this.DefaultPopularVehicle()
                };
            }
            else
            {
                routeOptions.routeDetails.routeType = routeType;
            }
            return routeOptions;
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

        public RouteTotals Calculate(RouteStop[] routes, int routeType)
        {            
            using (var routeSoapClient = new RouteSoapClient())
            {
                var getRouteTotalsResponse = routeSoapClient
                    .getRouteTotals(routes, this.DefaultRouteOptions(routeType), this.Token);                
                return getRouteTotalsResponse;
            }
        }        
    }
}
