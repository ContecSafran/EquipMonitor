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
    public class EquipmentInfo
    {
        public String name
        {
            get;
            set;
        }
        public String ip
        {
            get;
            set;
        }
        public int port
        {
            get;
            set;
        }
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public ClientType clientType
        {
            get;
            set;
        }
        public bool isHex;
        public String command
        {
            get;
            set;
        }
        public List<string> commands
        {
            get;
            set;
        } = new List<string>();
        public String tail
        {
            get;
            set;
        }
    }
}
