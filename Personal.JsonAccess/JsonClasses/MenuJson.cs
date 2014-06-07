using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class MenuJson
    {
        public string device {get;set;}
        public string name {get;set;}

        public MenuJson()
        {
            device ="windows_8";
            name = string.Empty;

        }

        
    }
}
