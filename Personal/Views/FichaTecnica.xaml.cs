using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Personal.Domain.Entities;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.Net.NetworkInformation;
using Personal.Model;
using Newtonsoft.Json.Linq;
using Personal.JsonAccess;
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;


namespace Personal.Views
{
    public partial class FichaTecnica : PhoneApplicationPage
    {
        public FichaTecnica()
        {
            InitializeComponent();
            this.Loaded += FichaTecnica_Loaded;                        
        }
        string peliculaID = string.Empty;
        Usuario usuario = new Usuario();
        Pelicula peliculaCargada = new Pelicula();                        
        void FichaTecnica_Loaded(object sender, RoutedEventArgs e)
        {
                       
            
            if (PhoneApplicationService.Current.State.ContainsKey("idPelicula"))
                peliculaID = PhoneApplicationService.Current.State["idPelicula"].ToString();

            if (PhoneApplicationService.Current.State.ContainsKey("Usuario"))
                usuario= (Usuario)PhoneApplicationService.Current.State["Usuario"];

            PeliculaJson peliculaJson = new PeliculaJson();
            peliculaJson.element_id = peliculaID;
           
            
            string postJsonPelicula = JsonConvert.SerializeObject(peliculaJson);
            
            string urlPelicula = "http://www.qubit.tv/business.php/json/Element";
            CargaDatosPeliculaPost(postJsonPelicula, urlPelicula);
        }

        public void CargaPeliculaObjetoConJson(string jsonPelicula)
<<<<<<< HEAD
        {
            
            peliculaCargada  = JsonModel.ConvierteJsonAPelicula(jsonPelicula);

=======
        {            
            peliculaCargada  = JsonModel.ConvierteJsonAPelicula(jsonPelicula);
            ratingControl.EstrellasActivas(peliculaCargada.ranking);
>>>>>>> 7d084ff59a463beb5ce65ea56ae3235d9135c8b4
            datosPelicula.DataContext = peliculaCargada;
            
            foreach (string item in peliculaCargada.categorie)
            {
                catego.Text += item+" ";
            }        
<<<<<<< HEAD
            cargaInformation(peliculaCargada.information);       
=======
            cargaInformation(peliculaCargada.information);

            DescripcionPeliculaImagenes();
        }

        private void DescripcionPeliculaImagenes()
        {
            if (peliculaCargada.subtitled == 1)
            {
                imgSubtitulo.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                imgSubtitulo.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (peliculaCargada.available_in_hd == 1)
            {
                imgHD.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                imgHD.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (peliculaCargada.classification == "ATP")
            {
                imgATP.Source = new BitmapImage(new Uri(@"/Imagenes/ATP.png", UriKind.Relative));
            }
            else if (peliculaCargada.classification == "13")
            {
                imgATP.Source = new BitmapImage(new Uri(@"/Imagenes/+13.png", UriKind.Relative));
            }
            else if (peliculaCargada.classification == "16")
            {
                imgATP.Source = new BitmapImage(new Uri(@"/Imagenes/+16.png", UriKind.Relative));
            }
            else if (peliculaCargada.classification == "18")
            {
                imgATP.Source = new BitmapImage(new Uri(@"/Imagenes/+18.png", UriKind.Relative));
            }

            txtTimer.Text = peliculaCargada.duration.ToString();
            txtLenguaje.Text = peliculaCargada.default_language.ToUpper();
>>>>>>> 7d084ff59a463beb5ce65ea56ae3235d9135c8b4
        }


        private void cargaInformation(List<Information> information)
        {            
            foreach (Information item in information)
	        {
                if (item.field_name == "Director")
                    datoDirector.Text = item.value;
                else
                    if (item.field_name == "Artista")
                    {
                        if (txtEstrellas.Text == string.Empty)                        
                            txtEstrellas.Text += item.value;
                        else
                            txtEstrellas.Text +=", "+item.value;
                    }
	        }    
        }

        private void imgVerAhora_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (usuario.session_id == null)
            {
                NavigationService.Navigate(new Uri("/Views/Login.xaml", UriKind.Relative));
                return;
            }
            else
            {
<<<<<<< HEAD
                MessageBox.Show(string.Format("Estás por ver {0}" + Environment.NewLine + "calificación {1}" + Environment.NewLine + "costo $ {2}"+ Environment.NewLine , peliculaCargada.title, peliculaCargada.classification, peliculaCargada.price_sd), "error", MessageBoxButton.OK);
=======
                MessageBox.Show(string.Format("Estás por ver {0}" + Environment.NewLine + "calificación {1}" + Environment.NewLine + "costo $ {2}"+ Environment.NewLine , peliculaCargada.title, peliculaCargada.classification, peliculaCargada.price_sd), "Atención", MessageBoxButton.OK);
>>>>>>> 7d084ff59a463beb5ce65ea56ae3235d9135c8b4
            }

            BitmapImage imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver ahora-hover.png", UriKind.RelativeOrAbsolute));
            imgVerAhora.Source = imag;

            bool hayRed=NetworkInterface.GetIsNetworkAvailable();
            if (hayRed)
            {
                PlayJson playJson = new PlayJson();
                playJson.content_id = peliculaID;
                playJson.session_id = usuario.session_id;
                string jsonPostPlay = JsonConvert.SerializeObject(playJson);
                

                CargaPlayPost(jsonPostPlay, "http://www.qubit.tv/business.php/json/play");
            }
            else
            {
                MessageBox.Show("Para poder ver la película necesita acceso a internet.");
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver ahora-inactivo.png", UriKind.RelativeOrAbsolute));
                imgVerAhora.Source = imag;
            }
            
        }

        #region JsonLoad
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
        #endregion JsonLoad
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

    }
}