using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class JsonPostURL
    {
        public Response[] response { get; set; }
        public bool status { get; set; }
        public int count { get; set; }
        
    }
}
