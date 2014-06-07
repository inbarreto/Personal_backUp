using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Play
    {

        public string descriptor {get;set;}
        public string direct_url {get;set;}
        public string identifier {get;set;}
        public string url_mobile_wrapper {get;set;}
        public string is_widevine {get;set;}
        public string reproduction_id {get;set;}
        public string current_time {get;set;}                
        public string element_id {get;set;}

         public string url_widevine {get;set;}

         public string url_subs {get;set;}

        public bool result_condition{get;set;}


    }
}
