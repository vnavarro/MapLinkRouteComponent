using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MapLinkConnector
{
    public class Serializer
    {
        public JObject ObjectToJson(object value){
            return (JObject)JToken.FromObject(value);
        }               
    }
}
