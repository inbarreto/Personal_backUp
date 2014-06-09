using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class PublicitiesJson
    {
        public string session_id { get; set; }
        public string device { get; set; }


        public PublicitiesJson(string session)
        {
            this.session_id = session;
        }

        public PublicitiesJson()
        {
            this.session_id = string.Empty;
            this.device = string.Empty;
        }
    }

    
}
