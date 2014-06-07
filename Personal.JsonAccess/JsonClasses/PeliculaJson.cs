using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class PeliculaJson
    {
      

    public string element_id {get;set;}
    public string device {get;set;}
    public string @operator {get;set;}
    public string session_id {get;set;}

    public PeliculaJson()
        {
            element_id = string.Empty;
            device = "windows_8";
            @operator = "qubit";
            session_id = string.Empty;            
        }

    }
}
