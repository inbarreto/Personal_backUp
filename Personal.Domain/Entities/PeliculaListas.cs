using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class PeliculaListas : Pelicula
    {
        private string[] tagitems = new string[1]; 
        public int current_time { get; set; }
        public bool percent_viewed { get; set; }
        public string[] tagItems 
        {
            get { return tagitems; }
            set
            {
                if (tagitems == null)
                    tagitems = new string[1];
                tagitems= value;
            }
        }
    }
}
