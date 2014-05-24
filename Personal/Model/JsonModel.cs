using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Personal.Domain.Entities;


namespace Personal.Model
{
    public class JsonModel
    {

        public static JObject StringToJsonObject(string datosJson)
        {
            try
            {
                List<JToken> listaDeTokens = new List<JToken>();
                JObject json= JObject.Parse(datosJson);
                return json;       
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        /// <summary>
        /// De un Json con Listado de Peliculas Genera una Pelicula de Tipo PeliculaListas
        /// </summary>
        /// <param name="stringAConvertir"></param>
        /// <returns></returns>
        public static PeliculaListas CompletaListadoPeliculasConJson(string stringAConvertir)
        {
            try
            {
                JObject objetoJson = StringToJsonObject(stringAConvertir);
                JToken tokenResponse = objetoJson["response"];

                PeliculaListas pelicula = new PeliculaListas();

                pelicula.title = (string)tokenResponse["title"];


                pelicula.viewed = (bool)tokenResponse["paid_hd"];
                pelicula.favorite = (bool)tokenResponse["paid_hd"];
                pelicula.percent_viewed = (bool)tokenResponse["paid_hd"];
                JToken Item = tokenResponse["tags"];
                pelicula.tagItems[0] = (string)Item["item"][0];
                pelicula.paid_hd = (bool)tokenResponse["paid_hd"];
                pelicula.paid_sd = (bool)tokenResponse["paid_sd"];
                pelicula.id = (string)tokenResponse["id"];
                pelicula.ref_id = (int)tokenResponse["ref_id"];
                pelicula.type = (string)tokenResponse["type"];
                pelicula.color = (string)tokenResponse["color"];
                pelicula.price_sd = (decimal)tokenResponse["price_sd"];
                pelicula.price_hd = (decimal)tokenResponse["price_hd"];
                pelicula.available_in_hd = (int)tokenResponse["available_in_hd"];
                pelicula.available_for_mobiles = (string)tokenResponse["available_for_mobiles"] != null ? (int)tokenResponse["available_for_mobiles"] : 1;
                pelicula.ranking = (int)tokenResponse["ranking"];
                pelicula.classification = (string)tokenResponse["classification"];
                pelicula.status = (string)tokenResponse["status"];
                pelicula.status_user = (string)tokenResponse["status_user"];
                JToken thumbnail = tokenResponse["thumbnail"];
                pelicula.fanart.small = (string)thumbnail["small"];
                pelicula.fanart.large = (string)thumbnail["large"];
                pelicula.fanart.movil = (string)thumbnail["movil"];
                pelicula.fanart.hd = (string)thumbnail["hd"];

                if ((string)tokenResponse["descripcion"] != null && (string)tokenResponse["descipcion"] != string.Empty)
                    pelicula.descripcion = (string)tokenResponse["descripcion"];
                for (int i = 0; i < 1; i++)
                {
                    pelicula.fanart.fanart[i] = (string)thumbnail["fanart"][i];
                }
                pelicula.fanart.crt = (string)thumbnail["crt"];




                return pelicula;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Convierte un String de JSon en un Objeto Pelicula
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>Pelicula</returns>
        public static Pelicula ConvierteJsonAPelicula(string jsonString)
        {
            try 
	        {	        
		         Pelicula pelicula =  new Pelicula();
                 JObject objetoJson = StringToJsonObject(jsonString);
                 JToken tokenResponse = objetoJson["response"];

                 pelicula.viewed = (bool)tokenResponse["paid_hd"];
                 pelicula.favorite = (bool)tokenResponse["paid_hd"];
                 pelicula.paid_hd = (bool)tokenResponse["paid_hd"];
                 pelicula.paid_sd = (bool)tokenResponse["paid_sd"];
                 pelicula.id = (string)tokenResponse["id"];
                 pelicula.ref_id = (int)tokenResponse["ref_id"];
                 pelicula.type = (string)tokenResponse["type"];
                 pelicula.color = (string)tokenResponse["color"];
                 pelicula.price_sd = (decimal)tokenResponse["price_sd"];
                 pelicula.price_hd = (decimal)tokenResponse["price_hd"];
                 pelicula.available_in_hd = (int)tokenResponse["available_in_hd"];
                 pelicula.available_for_mobiles = (string)tokenResponse["available_for_mobiles"] != null ? (int)tokenResponse["available_for_mobiles"] : 1;
                 pelicula.ranking = (int)tokenResponse["ranking"];
                 pelicula.classification = (string)tokenResponse["classification"];
                 pelicula.status = (string)tokenResponse["status"];
                 pelicula.status_user = (string)tokenResponse["status_user"];
                 JToken thumbnail = tokenResponse["thumbnail"];
                 pelicula.fanart.small = (string)thumbnail["small"];
                 pelicula.fanart.large = (string)thumbnail["large"];
                 pelicula.fanart.movil = (string)thumbnail["movil"];
                 pelicula.fanart.hd = (string)thumbnail["hd"];
                 pelicula.descripcion = (string)tokenResponse["description"];                 
                 pelicula.descripcion = (string)tokenResponse["description"];
                 foreach (string item in thumbnail["fanart"])
                 {
                     pelicula.fanart.fanart.Add(item);
                 }             
                 pelicula.fanart.crt = (string)thumbnail["crt"];
                 pelicula.title = (string)tokenResponse["title"];
                 pelicula.original_title = (string)tokenResponse["original_title"];
                 pelicula.countries = (string)tokenResponse["countries"];
                 var categorias = tokenResponse["categorie"];
                 for (int i = 0; i < categorias.Count(); i++)
                 {
                     pelicula.categorie.Add((string)categorias[i]);
                 }
                 var genre = tokenResponse["genre"];
                 for (int i = 0; i < genre.Count(); i++)
                 {
                     pelicula.Genre.Add((string)genre[i]);
                 }

                  var valoresinfo = tokenResponse["information"];
                for (int i=0;i<valoresinfo.Count();i++)
                {
                    pelicula.information.Add(new Information((string)valoresinfo[i]["field_name"], (string)valoresinfo[i]["value"]));

                }                 
                 return pelicula;
	        }
	        catch (Exception ex)
	        {		
		        throw ex;
	        }            
        }


        public static Usuario ConvierteJsonAUsuario(string jsonString)
        {
            try
            {
                Usuario usuario = new Usuario();
                JObject objetoJson = StringToJsonObject(jsonString);
                JToken tokenResponse = objetoJson["response"];

                if (tokenResponse.ToString() != "Usuario o contraseña incorrectos")
                {
                    usuario.autorized = (bool)tokenResponse["autorized"];
                    usuario.session_id = (string)tokenResponse["session_id"];
                    usuario.autologin_hash = (string)tokenResponse["autologin_hash"];
                    usuario.suscription_id = (string)tokenResponse["suscription_id"];
                    usuario.username = (string)tokenResponse["username"];
                    usuario.active_account = (string)tokenResponse["active_account"];
                    usuario.parental_control_active = (string)tokenResponse["parental_control_active"];
                    usuario.parental_control_limit = (string)tokenResponse["parental_control_limit"];
                    usuario.search_limit = (string)tokenResponse["search_limit"];
                }

                return usuario;                
            }
            catch (Exception ex)
            {                
                throw ex;
            }

        }

        public static Play ConvierteJsonPlay(string jsonString)
        {
            try
            {
                 Play play=  new Play();
                 JObject objetoJson = StringToJsonObject(jsonString);
                 JToken tokenResponse = objetoJson["response"];
                 play.current_time = (string)tokenResponse["current_time"];
                 play.descriptor = (string)tokenResponse["descriptor"];
                 play.direct_url = (string)tokenResponse["direct_url"];
                 play.element_id = (string)tokenResponse["element_id"];
                 play.identifier = (string)tokenResponse["identifier"];
                 play.is_widevine = (string)tokenResponse["is_widevine"];
                 play.reproduction_id = (string)tokenResponse["reproduction_id"];
                 play.result_condition = (bool)tokenResponse["result_condition"];
                 play.url_mobile_wrapper = (string)tokenResponse["url_mobile_wrapper"];
                 play.url_subs = (string)tokenResponse["url_subs"];
                 play.url_widevine = (string)tokenResponse["url_widevine"];

                 return play;


            }
            catch (Exception ex)
            {                
                throw ex;
            }

        }

    }
}
