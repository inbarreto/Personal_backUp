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
using Personal.Domain.Utils;
using Personal.Domain;

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
        List<Pelicula> listadoDePeliculas ;
        Usuario usuario = new Usuario();
        int paginado = 1;
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

        public void CargaListaPeliculas(string json, int cantidadPeliculas)
        {
            try
            {
                
                // SE MODIFICO SEGUN EL REQUERIMIENTO QUE NO ES UNA PELICULA LA PRIMERA.
                List<Pelicula> lista = this.ObtenerPrevioPorGenero(json, cantidadPeliculas);
                //Pelicula peliculaUnica = lista.First<Pelicula>();
                foreach (Pelicula item in lista)
                {
                    if (item.title.Length > 22)
                        item.title = item.title.Substring(0, 19) + "...";
                }
                //lista.Remove(peliculaUnica);
                if (!StateModel.ExisteKey("VieneDeBuscar"))
                {
                    btnVerMas.Visibility = System.Windows.Visibility.Visible;
                    btnVerMas.Foreground = Utils.getColorFromHexa("#7E517A");
                    btnVerMas.BorderBrush = Utils.getColorFromHexa("#7E517A");
                    
                }
                StateModel.BorrarKey("VieneDeBuscar");
                listaPeliculas.ItemsSource = null;
                listaPeliculas.ItemsSource = lista;                            
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
                if (!StateModel.ExisteKey("esmas"))
                    listadoDePeliculas = new List<Pelicula>();
                for (int i = 0; i < cantidadPeliculas; i++)
                {
                    listadoDePeliculas.Add(PeliculaModel.CompletaPeliculaConJson(element[i]));
                }
                StateModel.BorrarKey("esmas");
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
                usuario = StateModel.ObtieneKey("Usuario") as Usuario;
                if (usuario != null)
                {

                    Image imgFavoritos = sender as Image;
                    Pelicula pelicula = listadoDePeliculas.Find(x => x.id.ToString() == imgFavoritos.Tag.ToString());
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



                        FavoritosAgregarJson favoritos = new FavoritosAgregarJson();
                        favoritos.content_id = pelicula.ref_id.ToString();
                        favoritos.session_id = usuario.session_id;
                        string jsonFavoritos = JsonConvert.SerializeObject(favoritos);

                        this.CargaFavoritoPost(jsonFavoritos, URL.AddFavoritos);                        
                    }
                }
                else {
                    MessageBox.Show("Para poder agregar a favoritos debe estar logeado", "error", MessageBoxButton.OK);
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
                MessageBox.Show("Para poder ver la película necesitas acceso a internet.");
            }
        }

        //public void CargaPlayPost(string postdata, string url)
        //{
        //    JsonRequest loginRequest = new JsonRequest();
        //    loginRequest.Completed += new EventHandler(handleResponsePlay);
        //    loginRequest.beginRequest(postdata, url);
        //}
        //public void handleResponsePlay(object sender, EventArgs args)
        //{
        //    JsonRequest responseObject = sender as JsonRequest;
        //    string response = responseObject.ResponseTxt;
        //    this.CargaPlayConJson(response);
        //    //parse it
        //}

        

        //private void CargaPlayConJson(string jsonString)
        //{
        //    try
        //    {
        //        //PeliculaModel peliculaModel = new PeliculaModel();
        //        //peliculaModel.EjecutaMultimediaPelicula(jsonString);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


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

        public void CargaFavoritoPost(string postdata, string url)
        {
            JsonRequest loginRequest = new JsonRequest();
            loginRequest.Completed += new EventHandler(handleResponseFavorito);
            loginRequest.beginRequest(postdata, url);
        }
        public void handleResponseFavorito(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;            
            //parse it
        }
      

        private void gridRating_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid gdv = sender as Grid;

                int rating = Convert.ToInt16(gdv.Tag);

                this.EstrellasActivas(rating, gdv);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EstrellasActivas(int cantidadEstrellas, Grid gridRating)
        {
            try
            {
                if (cantidadEstrellas <= 5)
                {
                    BitmapImage imag;
                    imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"../Imagenes/Rating/estrella_activa.png", UriKind.RelativeOrAbsolute));
                    Image imagenEstrella = new Image();
                    for (int i = 1; i <= cantidadEstrellas; i++)
                    {
                        imagenEstrella = FindFirstElementInVisualTree<Image>(gridRating, String.Format("estrella{0}", i));
                        imagenEstrella.Source = imag;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private T FindFirstElementInVisualTree<T>(DependencyObject parentElement, string nombreImagen) where T : DependencyObject
        {

            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    Image imagen = child as Image;
                    if (imagen.Name == nombreImagen)
                        return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child, nombreImagen);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        private void verMas_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                usuario = StateModel.ObtieneKey("Usuario") as Usuario;
                StateModel.CargaKey("esmas", true);
                PeliculasPorGeneroJson verMasParametro = StateModel.ObtieneKey("vermas") as PeliculasPorGeneroJson;                
                verMasParametro.page = (Convert.ToInt16(verMasParametro.page) + 1).ToString();
                
                if (usuario != null)
                    verMasParametro.session_id = usuario.session_id;
                string jsonString = JsonConvert.SerializeObject(verMasParametro);

                CargaPeliculasPost(jsonString, URL.MenuCategoria);

            }
            catch (Exception)
            {
                
                throw;
            }
        }


     
        
    }
}



