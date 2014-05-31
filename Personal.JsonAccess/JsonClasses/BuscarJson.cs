using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
   public class BuscarJson
    {
       
    public string session_id{get;set;}
       public string device{get;set;}
       public string @operator{get;set;}
   public string text{get;set;}
   
       public BuscarJson()
           {
                session_id = string.Empty;
           device = "windows_8";
           @operator ="qubit";
           text =string.Empty;                    
                }

       public BuscarJson(string session, string buscar)
       {
           session_id = session;
           device = "windows_8";
           @operator = "qubit";
           text = buscar;                           
       }



    }
}
