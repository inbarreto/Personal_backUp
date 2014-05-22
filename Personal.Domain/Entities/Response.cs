using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Domain.Entities
{
    public class Response
    {
        //public Menu[] response { get; set; }
        public string Text { get; set; }
        public string Criteria { get; set; }
        public string Children { get; set; }
        public int ChildrenCount { get; set; }
        public Criteria criteria { get; set; }
    }
}
