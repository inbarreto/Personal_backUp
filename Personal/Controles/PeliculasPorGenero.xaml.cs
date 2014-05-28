using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using Personal.Domain.Entities;
using Personal.Model;
using Personal.JsonAccess;
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;

namespace Personal.Controles
{
    public partial class PeliculasPorGenero : UserControl
    {
        public PeliculasPorGenero()
        {
            InitializeComponent();
            this.Loaded += PeliculasGenero_Loaded;
        }
        Variables variables = new Variables();
        List<Pelicula> listadoDePeliculas = new List<Pelicula>();
        Usuario usuario = new Usuario();
        void PeliculasGenero_Loaded(object sender, RoutedEventArgs e)
        {
             try
             {
                 string genero = string.Empty;
                 
                     genero = (string)StateModel.ObtieneKey("named_criteria");
                     usuario = (Usuario)StateModel.ObtieneKey("Usuario");

                 PeliculasPorGeneroJson pelisJson = new PeliculasPorGeneroJson();

                 pelisJson.named_criteria = genero;
                 string post_data = JsonConvert.SerializeObject(pelisJson);                 
                 CargaPeliculasPost(post_data, "http://www.qubit.tv/business.php/json/search");
                 
                
                 

            }
            catch (Exception )
            {                
                throw ;
            }
            
        }

        public Pelicula CargaListaPeliculas(string json, int cantidadPeliculas)
        {
            try
            {


                List<Pelicula> lista = this.ObtenerPrevioPorGenero(json, cantidadPeliculas);
                Pelicula peli = lista.First<Pelicula>();
                foreach (Pelicula item in lista)
                {
                    if (item.title.Length > 22)
                        item.title = item.title.Substring(0,19) + "...";
                    item.ranking = ((item.ranking / 2) / 10) ;
                    
                }
                lista.Remove(peli);
                listaPeliculas.ItemsSource = lista;

                
                return peli;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Carga las peliculas en una lista
        /// </summary>
        /// <param name="named_criteria">genero de pelis</param>
        public List<Pelicula> ObtenerPrevioPorGenero(string jsonPeliculas,int cantidadPeliculas)
        {
            try
            {
                JToken element = ExtraigoElementJson(jsonPeliculas);
                PeliculaListas peli = new PeliculaListas();
               // listadoDePeliculas = new List<Pelicula>();
                for (int i = 0; i < cantidadPeliculas; i++)
                {
                    peli = PeliculaModel.CompletaPeliculaConJson(element[i]);
                    peli.ranking = ((peli.ranking) / 2) / 10;
                    listadoDePeliculas.Add(peli);
                    
                }
                return listadoDePeliculas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static JToken ExtraigoElementJson(string jsonPeliculas)
        {
            JObject PelisGenero = JObject.Parse(jsonPeliculas);
            JToken response = PelisGenero["response"];
            JToken groups = response["groups"];
            JToken element = groups[0]["element"];
            return element;
        }

        private void imgVer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            string idPelicula = Convert.ToString(img.Tag);
            img.Source = PeliculaModel.BotonVer(true);

            StateModel.CargaKey("idPelicula", idPelicula);
           
            this.VerPelicula(img, idPelicula);
            img.Source = PeliculaModel.BotonVer(false);
        }




        private void VerPelicula(Image imgVerAhora, string idPelicula)
        {

            PeliculaJson peliculaJson = new PeliculaJson();
            peliculaJson.element_id = idPelicula;


            string postJsonPelicula = JsonConvert.SerializeObject(peliculaJson);

            string urlPelicula = "http://www.qubit.tv/business.php/json/Element";
            CargaDatosPeliculaPost(postJsonPelicula, urlPelicula);

        }


     
        public Pelicula ObtienePeliculaHome() 
        {
            Pelicula pelicula = new Pelicula();

            return pelicula;
        }

        private void imgFavorito_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                TextBlock texto = e.OriginalSource as TextBlock;

                string valor = texto.Text;

                Pelicula pelicula = listadoDePeliculas.Find(x => x.title == texto.Text);
                Image imagen = sender as Image;

            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        private void imgMas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(@"/Views/FichaTecnica.xaml", UriKind.Relative)); 
                //ListBoxItem fin = new ListBoxItem();
                //fin.FindName
                //Image imagen = sender as Image;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public void CargaPeliculasPost(string postdata, string url)
        {
            JsonRequest PeliculaRequest = new JsonRequest();
            PeliculaRequest.Completed += new EventHandler(handleResponsePeliculasLista);
            PeliculaRequest.beginRequest(postdata, url);
        }
        public void handleResponsePeliculasLista(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;            
            CargaListaPeliculas(response, 6);
            //parse it
        }

        public void CargaPeliculaObjetoConJson(string jsonPelicula)
        {
            
             

          Pelicula peliculaCargada = JsonModel.ConvierteJsonAPelicula(jsonPelicula);
          if (!StateModel.ExisteKey("Usuario"))
          {
              MessageBox.Show("Primero tenés que iniciar sesion.", "error", MessageBoxButton.OK);
              return;
          }
          else
          {
              MessageBox.Show(string.Format("Estás por ver {0}" + Environment.NewLine + "calificación {1}" + Environment.NewLine + "costo $ {2}" + Environment.NewLine, peliculaCargada.title, peliculaCargada.classification, peliculaCargada.price_sd), "", MessageBoxButton.OK);
          }
          bool hayRed = NetworkInterface.GetIsNetworkAvailable();
          if (hayRed)
          {
              PlayJson playJson = new PlayJson();
              playJson.content_id = peliculaCargada.id;
              playJson.session_id = ((Usuario)StateModel.ObtieneKey("Usuario")).session_id;
              string jsonPostPlay = JsonConvert.SerializeObject(playJson);


              CargaPlayPost(jsonPostPlay, "http://www.qubit.tv/business.php/json/play");
          }
          else
          {
              MessageBox.Show("Para poder ver la película necesita acceso a internet.");             
          }

            
        }

        public void CargaPlayPost(string postdata, string url)
        {
            JsonRequest loginRequest = new JsonRequest();
            loginRequest.Completed += new EventHandler(handleResponsePlay);
            loginRequest.beginRequest(postdata, url);
        }
        public void handleResponsePlay(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            this.CargaPlayConJson(response);
            //parse it
        }

        private void CargaPlayConJson(string jsonString)
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


        public void CargaDatosPeliculaPost(string postdata, string url)
        {
            JsonRequest PeliculaRequest = new JsonRequest();
            PeliculaRequest.Completed += new EventHandler(handleResponsePelicula);
            PeliculaRequest.beginRequest(postdata, url);
        }
        public void handleResponsePelicula(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            CargaPeliculaObjetoConJson(response);
            //parse it
        }

        private void ImagenIrAFicha_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Image datos = sender as Image;
                string idPelicula = Convert.ToString(datos.Tag);
                IrAFicha(idPelicula);             
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        private void TextBlockIrAFicha_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                TextBlock datos = sender as TextBlock;
                string idPelicula = Convert.ToString(datos.Tag);
                IrAFicha(idPelicula);             
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        


        private static void IrAFicha(string idPelicula)
        {
            if (PhoneApplicationService.Current.State.ContainsKey("idPelicula"))
                PhoneApplicationService.Current.State["idPelicula"] = idPelicula;
            else
                PhoneApplicationService.Current.State.Add("idPelicula", idPelicula);

            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(@"/Views/FichaTecnica.xaml", UriKind.Relative));
        }


    }
}

