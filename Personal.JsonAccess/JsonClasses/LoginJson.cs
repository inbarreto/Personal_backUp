using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class LoginJson
    {

        public string username {get;set;}
        public string password {get;set;}
        public string @operator {get;set;}
        public string device_type {get;set;}
        public string device_id {get;set;}

          
  
        public LoginJson()
        {
            username=string.Empty;
            password = string.Empty;
            @operator = "qubit";
            device_type="windows_8";
            device_id=string.Empty;

        }            
    }
}
