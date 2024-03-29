﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using Personal.Domain.Entities;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Personal.JsonAccess;

namespace Personal.Model
{
    public  class PeliculaModel
    {
        public static PeliculaListas CompletaPeliculaConJson(JToken token)
        {
            try
            {
                PeliculaListas pelicula = new PeliculaListas();
                
                pelicula.title = (string)token["title"];


                pelicula.viewed = (string)token["viewed"] != string.Empty && (string)token["viewed"] != null ? Convert.ToBoolean((int)token["viewed"]) : false;
                pelicula.favorite =(string)token["favorite"] != string.Empty && (string)token["favorite"] != null ?  Convert.ToBoolean((int)token["favorite"]): false;
                pelicula.current_time = (string)token["current_time"] != null && (string)token["current_time"] != string.Empty ? (double)token["current_time"] : 0;
                pelicula.percent_viewed = (string)token["paid_hd"] != null && (string)token["paid_hd"] != string.Empty ? (bool)token["paid_hd"] : false;
                JToken Item = token["tags"];
                pelicula.tagItems[0] = (string)Item["item"][0];
                pelicula.paid_hd = (bool)token["paid_hd"];
                pelicula.paid_sd = (bool)token["paid_sd"];
                pelicula.id = (string)token["id"];
                pelicula.ref_id = (int)token["ref_id"];
                pelicula.type = (string)token["type"];
                pelicula.color = (string)token["color"];
                pelicula.price_sd = (decimal)token["price_sd"];
                pelicula.price_hd = (decimal)token["price_hd"];
                pelicula.available_in_hd = (int)token["available_in_hd"];
                pelicula.available_for_mobiles = (string)token["available_for_mobiles"] != null && (string)token["available_for_mobiles"] != string.Empty ? (int)token["available_for_mobiles"] : 1;
                int rating = ((int)token["ranking"] / 2) / 10;
                int resto = ((int)token["ranking"] / 2) % 10;
                if (resto > 5)
                    rating += 1;
                pelicula.ranking = rating;
                pelicula.classification = (string)token["classification"];
                pelicula.status = (string)token["status"];
                pelicula.status_user = (string)token["status_user"];
                JToken thumbnail = token["thumbnail"];
                pelicula.fanart.small = (string)thumbnail["small"];
                pelicula.fanart.large = (string)thumbnail["large"];
                pelicula.fanart.movil = (string)thumbnail["movil"];
                pelicula.fanart.hd = (string)thumbnail["hd"];

                if ((string)token["descripcion"] != null && (string)token["descipcion"] != string.Empty)
                    pelicula.descripcion = (string)token["descripcion"];

                foreach (string item in thumbnail["fanart"])
                {
                    pelicula.fanart.fanart.Add(item);
                }            
                pelicula.fanart.crt = (string)thumbnail["crt"];
                return pelicula;
                

            }
            catch (Exception )
            {
                
                throw ;
            }
        }



        public void CargaPlayConJson(string jsonString)
        {
            try
            {
                Play play = JsonModel.ConvierteJsonPlay(jsonString);


                MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
                mediaPlayerLauncher.Media = new Uri(play.direct_url, UriKind.Absolute);
                mediaPlayerLauncher.Location = MediaLocationType.Data;
                mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
                mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;

                mediaPlayerLauncher.Show();

            }
            catch (Exception)
            {

                throw;
            }


        }

    

        public Pelicula ObtienePeliculaHome()
        {
            Pelicula pelicula = new Pelicula();

            return pelicula;
        }


        public void EjecutaMultimediaPelicula(PlayJson playJson)
        {
            string jsonPostPlay = JsonConvert.SerializeObject(playJson);
            string url = "http://www.video.personal.com.ar/business.php/json/play";
            JsonRequest loginRequest = new JsonRequest();
            loginRequest.Completed += new EventHandler(handleResponsePlay);
            loginRequest.beginRequest(jsonPostPlay, url);
        }
        public void handleResponsePlay(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            this.CargaPlayConJson(response);
            //parse it
        }

      


    /*    public static PeliculaListas CompletaPeliculaConJson(JToken token,bool esPelicula)
        {
            try
            {
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }*/
        


        public static BitmapImage BotonVer(bool activo)
        {
            BitmapImage imag ;
            if (activo)
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver-activo.png", UriKind.RelativeOrAbsolute));
            else
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver-inactivo.png", UriKind.RelativeOrAbsolute));

            return imag;
            
        }

    }
}
