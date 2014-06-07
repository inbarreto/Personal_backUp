using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class PlayJson
    {

        public string content_id{get;set;}
        public string quality{get;set;}
        public string device_type{get;set;}
        public string session_id{get;set;}


        public PlayJson()
        {
            quality = "sd";
            device_type = "windows_8";
            session_id = string.Empty;
            content_id = string.Empty;
        }

    }
}
