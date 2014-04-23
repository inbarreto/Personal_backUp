using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Information
    {

        public string field_name { get; set; }
        public string value { get; set; }


        public Information(string name , string valor)
            {
                field_name = name;
                value = valor;
            }

        public Information(bool tru)
        {
            List<Information> lista = new List<Information>();
            lista.Add(new Information("Artista", "Patrick Swayze"));
            lista.Add(new Information("Artista", "Meat Loaf"));
            lista.Add(new Information("Artista", "Randy Travis"));
            lista.Add(new Information("Director", "Kevin Hooks"));
            
        }
    }   
}
