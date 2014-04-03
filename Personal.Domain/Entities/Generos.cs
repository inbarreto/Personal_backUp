using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Generos
    {
            private int id;
            private string genero;


            public int Id
            {
                get {return id;}
                set { this.id = value; }
            }

            public string Genero
            {
                get { return genero; }
                set { this.genero = value; }
            }

            public Generos(int intID, string strGenero)
            {
                Genero = strGenero;
                Id = intID;
            }

    }
}
