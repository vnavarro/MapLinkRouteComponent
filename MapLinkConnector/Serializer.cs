using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MapLinkConnector
{
    public class Serializer
    {
        public string ObjectToJson(object value){
            JObject json = (JObject)JToken.FromObject(value);
            return json.ToString(Newtonsoft.Json.Formatting.None);
        }               
    }
}
