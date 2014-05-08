using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapLinkConnector
{
    public class AdapterBase
    {        
        private string token;
        public string Token { get { return token; } }        

        public AdapterBase()
        {            
            this.token = System.Configuration.ConfigurationManager.AppSettings["MapLinkToken"];            
        }
    }
}
