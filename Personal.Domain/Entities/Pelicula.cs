using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Pelicula
    {
        public bool Puede_Comprar { get; set; }
        public int ref_id {get;set;}
        public string title { get; set; }
        public string original_title { get; set; }
        public DateTime year { get; set; }
        public int duration { get; set; }
        public string classification { get; set; }
        public int available_in_hd { get; set; }
        public int subtitled { get; set; }
        public int available_for_mobiles { get; set; }
        public int rate { get; set; }
        public string descripcion { get; set; }
        public string short_descripcion { get; set; }
        public List<string> categorie { get; set; }
        public bool viewed { get; set; }
        public bool liked { get; set; }
        public bool favorite { get; set; }
        public bool not_seen { get; set; }
        public decimal price_hd { get; set; }
        public decimal price_sd { get; set; }
        public bool paid_hd { get; set; }
        public bool paid_sd { get; set; }
        public string status { get; set; }
        public string status_user { get; set; }
        public string id { get; set; }
        public string default_language { get; set; }
        public int subtitle { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public int ranking { get; set; }
        public Thumbnail fanart { get; set; }

        public List<string> Genre { get; set; }
        public List<Information> information { get; set; }
        public string countries { get; set; }
        public string trailer { get; set; }
        public int children { get; set; }

        public Pelicula(bool valor)
        {
                Puede_Comprar = false;
                ref_id = 273;           
                title ="Alto riesgo";

                original_title="Black Dog";
                year = Convert.ToDateTime("1998-01-01");
                duration =89;
                classification ="ATP";
                available_in_hd =0;
                available_for_mobiles =1;
                rate  =0;
                descripcion ="Recién salido de la cárcel, Jack (el recordado Patrick Swayze), consigue un empleo manejando un gran camión a través del país. Lo que Jack no sabe es que la carga que transporta son armas ilegales. Así, se verá perseguido por agentes del Gobierno, traficantes de armas y por otros enormes camiones. Con una gran banda de sonido de rock y country ideal para la ruta, Alto riesgo cumple con su promesa de acción fierrera y de diversión a alta velocidad.";
                short_descripcion = "Recién salido de la cárcel, Jack (el recordado Patrick Swayze), consigue un empleo manejando un gran camión a través del país. Lo que Jack no sabe es que la carga que transporta son armas ilegales. Así, se verá perseguido por agentes del Gobierno, traficantes de armas y por otros enormes camiones.";
                categorie = new List<string>() {"Películas"};
                viewed =false;
                liked =false;
                favorite =false;
                not_seen =false;
                price_hd =0;
                price_sd =10;
                paid_hd =false;
                paid_sd =false;
                status ="registrate";
                status_user ="registrate";
                id ="b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog";
                default_language = "en";
                subtitle=1;
                type ="Contenido";
                color ="red_list_movies";
                ranking =50;
                fanart = new Thumbnail();
       
                information = new List<Information>(){new Information("Artista","Patrick Swayze"),new Information("Artista","Patrick Swayze"),new Information("Director","Kevin Hooks")};
                countries = "Alemania, EE.UU., Francia, Japón, Reino Unido";
                trailer ="";
                children = 0;

        
        }


        public Pelicula()
        {
            fanart = new Thumbnail();
            information = new List<Information>();
            categorie = new List<string>();
            Genre = new List<string>();
        }

    }
}
