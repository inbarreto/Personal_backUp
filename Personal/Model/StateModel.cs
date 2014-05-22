using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Model
{
    public class StateModel
    {

        public static object ObtieneKey(string key)
        {
            object objeto = null;
            
            if (PhoneApplicationService.Current.State.ContainsKey(key))
             objeto =  PhoneApplicationService.Current.State[key];
            return objeto;

        }

        public static void CargaKey(string key, object objeto) 
        {
            if (PhoneApplicationService.Current.State.ContainsKey(key))
                PhoneApplicationService.Current.State[key] = objeto;
            else
               PhoneApplicationService.Current.State.Add(key, objeto);
        }

        public static bool ExisteKey(string key)
        {
            return PhoneApplicationService.Current.State.ContainsKey(key);
        }
    }
}
