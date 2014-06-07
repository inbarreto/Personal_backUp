using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Usuario
    {
        
    public bool autorized{get;set;}
        public string session_id {get;set;}
        public string autologin_hash {get;set;}
        public string suscription_id {get;set;}
        public string username {get;set;}
        public string parental_control_active {get;set;}
        public string parental_control_limit {get;set;}        
        public string search_limit {get;set;}

        public string active_account { get; set; }

    }
}
