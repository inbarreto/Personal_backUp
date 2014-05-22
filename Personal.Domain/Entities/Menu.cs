using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Menu
    {
        public string text { get; set; }
        private Criteria criterias= new Criteria();
        public string[] children { get; set; }
        public int childrenCount { get; set; }
        public Criteria Criterias {
            get
            {               
                return criterias;
            }

            set {
                if (criterias == null)
                    criterias = new Criteria();
                criterias = value; }            
            }

    }
}
