using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Thumbnail
    {
        public string small { get; set; }
        public string large { get; set; }
        public string movil{ get; set; }
        public string hd{ get; set; }
        public List<string> fanart { get; set; }
        public string crt { get; set; }
        


        public Thumbnail()
        {
            small = string.Empty;
            large = string.Empty;
            movil = string.Empty;
            hd = string.Empty;
            fanart = new List<string>();
            crt = string.Empty;

        }


    }
}

