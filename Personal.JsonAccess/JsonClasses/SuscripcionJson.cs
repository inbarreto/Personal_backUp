using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
     public class SuscripcionJson
    {
         public string username { get; set; }
         public string suscription_id{ get; set; }


         public SuscripcionJson(string user,string suscripcion)
         {
             this.username =user;
             this.suscription_id = suscripcion;
         }
    }
}
