#Route Component for MapLink Challenge

## Architecture

This project was created using Visual Studio 2010, .NET Framework 4 and C#.

The solution is composed by three projects:  

* MapLinkConnector
* UnitTest project
* RouteService

### MapLinkConnector

Receives a list of addresses and a route type described as following:  
Address  

* Street/avenue name
* Number
* City
* State

Route type  

* Fastest route
* Route avoiding traffic

Then calculate a route based on it and return a set of data composed by:

* Route total time
* Route total distance
* Fuel cost
* Total cost, including tool fees.

### UnitTest project

The Unit test project purpose is to ensure the MapLinkConnector works properly.

### RouteService

Is a WCF service which has primarly one endpoint for route calculation. This endpoint receives the parameters as json and simply pass them into MapLinkConnector. Once the calculation is over the data is them returned to the caller. This endpoint is synchronous, that means it returns the calculation as its response.

The response is also returned as json.

## Licensing

The MIT License (MIT)

Copyright (c) 2014 Vitor Navarro

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.