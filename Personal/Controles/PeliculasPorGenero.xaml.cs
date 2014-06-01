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
using System.Windows.Media;

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

            }
            catch (Exception)
            {
                throw;
            }

        }

        public Pelicula CargaListaPeliculas(string json, int cantidadPeliculas)
        {
            try
            {


                List<Pelicula> lista = this.ObtenerPrevioPorGenero(json, cantidadPeliculas);
                Pelicula peliculaUnica = lista.First<Pelicula>();
                foreach (Pelicula item in lista)
                {
                    if (item.title.Length > 22)
                        item.title = item.title.Substring(0, 19) + "...";
                }
                lista.Remove(peliculaUnica);
                listaPeliculas.ItemsSource = lista;
                
                return peliculaUnica;
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
        public List<Pelicula> ObtenerPrevioPorGenero(string jsonPeliculas, int cantidadPeliculas)
        {
            try
            {
                JToken element = ExtraigoElementJson(jsonPeliculas);
                if (element.Count() < cantidadPeliculas)
                    cantidadPeliculas = element.Count();
                listadoDePeliculas = new List<Pelicula>();
                for (int i = 0; i < cantidadPeliculas; i++)
                {
                    listadoDePeliculas.Add(PeliculaModel.CompletaPeliculaConJson(element[i]));
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
                Image imgFavoritos = sender as Image;
                Pelicula pelicula = listadoDePeliculas.Find(x => x.id == imgFavoritos.Tag);
                BitmapImage imag;
                if (pelicula != null)
                {
                    if (pelicula.favorite)
                        pelicula.favorite = false;
                    else
                        pelicula.favorite = true;


                    if (pelicula.favorite)
                        imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav-activo.png", UriKind.RelativeOrAbsolute));
                    else
                        imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav-inactivo.png", UriKind.RelativeOrAbsolute));
                    imgFavoritos.Source = imag;
                }

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

                PeliculaModel peliculaModel = new PeliculaModel();
                peliculaModel.EjecutaMultimediaPelicula(playJson);
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
                //PeliculaModel peliculaModel = new PeliculaModel();
                //peliculaModel.EjecutaMultimediaPelicula(jsonString);

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
            try
            {
                StateModel.CargaKey("idPelicula", idPelicula);
                (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(@"/Views/FichaTecnica.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void VaciaListaPeliculas()
        {
            listaPeliculas.DataContext = null;
        }

        private void imgFavorito_Loaded(object sender, RoutedEventArgs e)
        {
            Image imgFavoritos = sender as Image;
            Pelicula pelicula = listadoDePeliculas.Find(x => x.id == imgFavoritos.Tag);
            BitmapImage imag;

            if (pelicula != null && pelicula.favorite)            
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav-activo.png", UriKind.RelativeOrAbsolute));
            else
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav-inactivo.png", UriKind.RelativeOrAbsolute));
            

            imgFavoritos.Source = imag;

        }


    }
}



