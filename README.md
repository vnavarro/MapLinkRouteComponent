#Route Component for MapLink Challenge

## Architecture

This project was created using:  

* Visual Studio 2010
* .NET Framework 4
* C#
* [Nuget](http://www.nuget.org/) (package manager)
* [JSON.net](http://james.newtonking.com/json)

The solution is composed by three projects:  

* MapLinkConnector
* UnitTest project
* RouteService

### MapLinkConnector

The project contain the following classes:  

*Constants*  
Hold any type of "magic" number. Is used for Route Type. Since only two numbers are used (0 and 23) there is no sense in using an enum structure beforehand.

*JsonParser*  
Conveninent structure to isolate json parsing.

*Serializer*  
Convenient structure to isolate object to json conversion.

*Adapter Base*  
An abastraction to in which we can insert common features to any connector to MapLink API. It currently stores the Token from app.config

*AddressAdapter*   
Responsible for MapLink Address endpoint.  
It finds addresses from a json array of addresses.  
The expected json format is as follows:

```
[{ 
"street":"string",
"number":"number",
"city":"string",
"state":"string"
},
{ 
"street":"string",
"number":"number",
"city":"string",
"state":"string"
}
....]
```
At least an origin and a destination must be informed.

*RouteAdapter*  
Responsible for MapLink Route endpoint.
Expect a list of locations and calculates the routes total.

The route type can be:
* Fastest route - 0
* Route avoiding traffic - 23

The output json:
```
{
"totalTime":"string",
"totalDistance":"number",
"totalFuel":"number",
"totalCostWithToolFee":"number"
}
```

#### Usage

To retrieve totals


```
var locations = new AddressAdapter().FindAdresses(addressesJson);
RouteAdapter routeAdapter = new RouteAdapter();
var routes = routeAdapter.GenerateRoutes(locations);
var totals = routeAdapter.Calculate(routes, routeType);

return routeAdapter.RouteTotalsToJson(totals);
```

For error handling both AddressAdapter and RouteAdapter provide a ErrorMessage property string.

### UnitTest project

The Unit test project purpose is to ensure the MapLinkConnector works properly.

### RouteService

Is a WCF service which has primarly one endpoint for route calculation. This endpoint receives the parameters as json and simply pass them into MapLinkConnector. Once the calculation is over the data is them returned to the caller. This endpoint is synchronous, that means it returns the calculation as requested.

The response is also returned as json.

#### Endpoints

It exposes two endpoints:

* route/totals/sample
* route/totals

The former is a GET endpoint which can be used to test the Service, it has two hardcoded address and make a request to the later endpoint.

The later is a POST endpoint which gives the route calculation result. 

The expected json:
```
{
"RouteType":"number", 
"addresses":[{ 
"street":"string",
"number":"number",
"city":"string",
"state":"string"
},
{ 
"street":"string",
"number":"number",
"city":"string",
"state":"string"
}]
}
```

### Details

MapLinkConnector is a library thus it can't load a app.config, it expects that the project using it to have a app.config and define proper configuration.

Both UnitTest and RouteService have a app.config with the necessary configuration. Also there is an app.config file inside MapLinkConnector with minimum configuration.

## Running and Deploy

Running is as simple as opening the solution in visual studio and  executing RouteService. After that you can access the localhost address to check its working.  

Something like localhost:52306/RouteService.svc/route/totals/sample

To deploy it you will need to use Azure or other cloud service that supports WCF. Also the service may need proper release configuration file accordingly to your server specs.

## Further improvements and other ideas

There are two considerations that I think it should be made here:

* Maybe this kind of library may be better implemented on other languages or structures, it worth a try.
* There is this notion on Framework/Language Agnostic API, the idea is to code an API in ways that you could use it with any other language. A research paper which may help to walk this path is called 'Experiences from Developing a Component Technology
Agnostic Adaptation Framework'

## Licensing

The MIT License (MIT)

Copyright (c) 2014 Vitor Navarro

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
