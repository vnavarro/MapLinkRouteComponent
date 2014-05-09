using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapLinkConnector.MaplinkV3_AddressFinder;
using MapLinkConnector.MaplinkV3_Route;
using Newtonsoft.Json.Linq;

namespace MapLinkConnector
{
    public class RouteAdapter : AdapterBase
    {
        public string ErrorMessage { get; set; }
        private RouteOptions routeOptions;
        private RouteOptions DefaultRouteOptions(int routeType)
        {
            if (routeType != Constants.ROUTE_TYPE_AVOID_TRAFFIC && 
                routeType != Constants.ROUTE_TYPE_STANDARD_FASTEST)
            {
                routeType = Constants.ROUTE_TYPE_STANDARD_FASTEST;
            }

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

        public RouteStop[] GenerateRoutes(List<AddressLocation> locations)
        {
            List<RouteStop> routes = new List<RouteStop>();
            locations.ForEach(location =>
                routes.Add(new RouteStop
                {
                    description = location.address.street + location.address.houseNumber,
                    point = new MapLinkConnector.MaplinkV3_Route.Point { x = location.point.x, y = location.point.y }
                })
            );

            return routes.ToArray();
        } 

        public RouteTotals Calculate(RouteStop[] routes, int routeType)
        {
            this.ErrorMessage = String.Empty;
            try
            {
                using (var routeSoapClient = new RouteSoapClient())
                {
                    var getRouteTotalsResponse = routeSoapClient
                        .getRouteTotals(routes, this.DefaultRouteOptions(routeType), this.Token);
                    return getRouteTotalsResponse;
                }
            }
            catch (System.ServiceModel.FaultException e)
            {
                this.ErrorMessage = e.Message;
                return null;
            }            
        }

        public string RouteTotalsToJson(RouteTotals totals)
        {
            var simplifiedTotals = new
            {
                totalTime = totals.totalTime,
                totalDistance = totals.totalDistance,
                totalFuel = totals.totalFuelUsed,
                totalCostWithToolFee = totals.totalCost
            };

            return new Serializer().ObjectToJson(simplifiedTotals);
        }
    }
}
