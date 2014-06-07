using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
   public class FavoritosAgregarJson
    {
       public string session_id { get; set; }
       public string content_id { get; set; }


       public FavoritosAgregarJson()
       {
           session_id = string.Empty;
           content_id = string.Empty;

       }

    }
}
