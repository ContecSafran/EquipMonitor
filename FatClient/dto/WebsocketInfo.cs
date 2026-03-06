using FatClient.constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FatClient.dto
{
    public class WebsocketInfo
    {
        public String url
        {
            get;
            set;
        }
        public String base64Auth
        {
            get;
            set;
        }
    }
}
