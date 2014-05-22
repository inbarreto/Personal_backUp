using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class PeliculasPorGeneroJson
    {
        public string session_id{get;set;}
        public string page{get;set;}
        public string page_size {get;set;}
        public string device {get;set;}
        
        public string @operator {get;set;}

        public string named_criteria {get;set;}


        public PeliculasPorGeneroJson()
        {
            session_id=string.Empty;
            page = 1.ToString();
            page_size=6.ToString();
            device="windows_8";
            @operator ="qubit";
            named_criteria=string.Empty;        
        }
    }
}
