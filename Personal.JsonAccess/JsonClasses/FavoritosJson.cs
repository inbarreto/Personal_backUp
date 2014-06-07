using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.JsonAccess.JsonClasses
{
    public class FavoritosJson
    {

        public string content_id { get; set; }
        public string session_id { get; set; }



        public FavoritosJson(string content, string session)
        {
            this.content_id = content;
            this.session_id = session;
        }
    }
}

