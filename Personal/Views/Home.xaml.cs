using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Personal.Domain.Entities;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Personal.Model;
using Personal.Views;
using Personal.Controles;
using Personal.JsonAccess;
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Personal.Domain.Enums;
using Microsoft.Phone.Net.NetworkInformation;

namespace Personal
{   
    public partial class Home : PhoneApplicationPage
    {

            
        public Home()
        {
            InitializeComponent();
            this.Loaded += Home_Loaded;
        }
        List<Pelicula> peliculaPrincipal = new List<Pelicula>();
        List<Generos> listaGeneros = new List<Generos>();
        Variables variables = new Variables();
        void Home_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //string postDataMenu = "{\"device\":\"windows_8\",\"name\":\"\"}";
               
                string genero = "genero_comedia";

                StateModel.CargaKey("named_criteria", genero);
                PeliculasPorGeneroJson peliPrincipal = new PeliculasPorGeneroJson();
                
                peliPrincipal.named_criteria = genero;
                string post_dataPeliculas = JsonConvert.SerializeObject(peliPrincipal);                
                peliPrincipal.named_criteria = "genero_comedia";                
                
                MenuJson postMenu = new MenuJson();                
                string postDataMenu = JsonConvert.SerializeObject(postMenu);
                string urlMenu = "http://www.qubit.tv/business.php/json/menus";
                CargaMenusPost(postDataMenu, urlMenu, post_dataPeliculas, "http://www.qubit.tv/business.php/json/search"); 

                peliculasHome.CargaPeliculasPost(post_dataPeliculas, "http://www.qubit.tv/business.php/json/search");                
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public void CargaMenusPost(string postdataMenu,string urlMenu,string postdataPelicula, string urlPelicula)
        {           
            JsonRequest MenusRequest = new JsonRequest();
            JsonRequest PeliculaRequest = new JsonRequest();
            PeliculaRequest.Completed += new EventHandler(handleResponsePelicula);
            MenusRequest.Completed += new EventHandler(handleResponseMenus);
            MenusRequest.beginRequest(postdataMenu, urlMenu);
            PeliculaRequest.beginRequest(postdataPelicula, urlPelicula);
        }


        public void CargaUsuarioPost(string postdataLogin, string urlLogin)
        {            
            JsonRequest usuarioRequest = new JsonRequest();
            usuarioRequest.Completed += new EventHandler(handleResponseLogin);
            usuarioRequest.beginRequest(postdataLogin, urlLogin);
        }

        public void handleResponseLogin(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            CargaUsuario(response);
            

            //parse it
        }

        private void CargaUsuario(string jsonString)
        {
            try
            {
                Usuario usuarioObjeto = new Usuario();

               usuarioObjeto = JsonModel.ConvierteJsonAUsuario(jsonString);
               if (usuarioObjeto.username != null)
               {
                   StateModel.CargaKey("Usuario", usuarioObjeto);                   
                   MessageBox.Show("Se ha logeado correctamente", "Estado Login", MessageBoxButton.OK);
               }
               else
                   MessageBox.Show("Usuario o contraseña incorrectos", "Estado Login", MessageBoxButton.OK);
            }
            catch (Exception)
            {
                
                throw;
            }
        
        }
        public void handleResponsePelicula(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;

            Pelicula pelicula = new Pelicula();
            
            peliculaPrincipal =peliculasHome.ObtenerPrevioPorGenero(response, 1);
            grillaPersonalVideo.DataContext = peliculaPrincipal[0];
            ratingControl.EstrellasActivas(peliculaPrincipal[0].ranking);
            //parse it
        }
        public void handleResponseMenus(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            CargaGeneros(response);
            //parse it
        }

        /// <summary>
        /// Carga el listBox de Generos con el String de Json
        /// </summary>
        /// <param name="stringJson"></param>
        private void CargaGeneros(string stringJson)
        {
            try
            {

                JObject json = JsonModel.StringToJsonObject(stringJson);                                
                JToken response = json["response"];
                JToken status = json["status"];
                JToken count = json["count"];
                
                int cantidad = (int)count;
                List<Menu> listamenu = new List<Menu>();
                for (int i = 0; i < cantidad; i++)
                {

                    
                    Menu menu = new Menu();
                    
                    menu.text =  (string)response[i]["text"];
                    JToken Tcriteria = response[i]["criteria"];
                    menu.Criterias.named_criteria = (string)Tcriteria["named_criteria"]; 
                    menu.childrenCount = (int)response[i]["childrenCount"];
                    for (int j = 0; j < menu.childrenCount; i++)
                    {
                        menu.children[j] = (string)response[i]["children"];
                    }

                    listamenu.Add(menu);
                }                                
                
                                                
                int f=0;
                foreach (Menu item in listamenu)
                {
                    listaGeneros.Add(new Generos(f++,item.text,item.Criterias.named_criteria));
                }                                

                lboxgeneros.ItemsSource = listaGeneros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBoxResult mensaje = MessageBox.Show("número de linea o clave incorrecta", "", MessageBoxButton.OK);


            //Controles.CustomMessegeBox cm = new Controles.CustomMessegeBox();
            //Popup customMessege = new Popup();
            //Personal.Controles.CustomMessegeBox CM = new Controles.CustomMessegeBox();

        }

        /*private void txtClavePersonal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                txtClavePersonal.Text = string.Empty;



                SolidColorBrush scb = ObtieneColorHexa("#FFB28AAD");
        //applying the brush to the background of the existing Button btn:
            txtClavePersonal.Foreground = scb;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        private static System.Windows.Media.SolidColorBrush ObtieneColorHexa(string colorHexa)
        {
            
            byte A = 255;
            byte R = Convert.ToByte(colorHexa.Substring(1, 2), 16);
            byte G = Convert.ToByte(colorHexa.Substring(3, 2), 16);
            byte B = Convert.ToByte(colorHexa.Substring(5, 2), 16);
            SolidColorBrush scb = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return scb;
        }

        /*private void txtNroLinea_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            txtNroLinea.Text = string.Empty;
            SolidColorBrush scb = ObtieneColorHexa("#FFB28AAD");            
            txtNroLinea.Foreground = scb;

            
        }*/

        
       
        private void txtGenero_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            try
            {
                TextBlock texto = e.OriginalSource as TextBlock;

                string valor = texto.Text;

               Generos genero = listaGeneros.Find(x => x.Genero == valor);

               StateModel.CargaKey("genero", genero.Genero);
               StateModel.CargaKey("named_criteria", genero.NameCriteria);
               
                NavigationService.Navigate(new Uri("/Views/PeliculasGenero.xaml", UriKind.Relative));
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            } 
        }

        private void txtLogin_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            try
            {

                NavigationService.Navigate(new Uri("/Views/Login.xaml", UriKind.Relative));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


      


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            PeliculasGenero pagina = e.Content as PeliculasGenero;
            if (pagina != null)
            { 

            }

        }

        private void pivotHome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PivotItem pivot = (sender as Pivot).SelectedItem as PivotItem;
            switch (pivot.Name)
            {
                case "pivItemRecomendado":
                    PeliculasPorGeneroJson recomendadoData = new PeliculasPorGeneroJson();
                    recomendadoData.named_criteria = "genero_terror";
                    string post_recomendado = JsonConvert.SerializeObject(recomendadoData);
                    recomendado.CargaPeliculasPost(post_recomendado, "http://www.qubit.tv/business.php/json/search");
                    break;

                case "pivItemAlquiladas":
                    PeliculasPorGeneroJson muyAlquiladasData = new PeliculasPorGeneroJson();
                    muyAlquiladasData.named_criteria = "genero_accion";
                    string post_muyAlquiladas = JsonConvert.SerializeObject(muyAlquiladasData);
                    muyAlquiladas.CargaPeliculasPost(post_muyAlquiladas, "http://www.qubit.tv/business.php/json/search");
                    break;
                case "pivItemEstrenos":
                    PeliculasPorGeneroJson estrenosData = new PeliculasPorGeneroJson();
                    estrenosData.named_criteria = "genero_suspenso";
                    string post_estrenos = JsonConvert.SerializeObject(estrenosData);
                    estrenos.CargaPeliculasPost(post_estrenos, "http://www.qubit.tv/business.php/json/search");
                    break;
                case "pivItemTodoCatalogo":
                    PeliculasPorGeneroJson todoElCatalogoData = new PeliculasPorGeneroJson();
                    todoElCatalogoData.named_criteria = "genero_romantico";
                    string post_todoElcatalogo = JsonConvert.SerializeObject(todoElCatalogoData);
                    todoElCatalogo.CargaPeliculasPost(post_todoElcatalogo, "http://www.qubit.tv/business.php/json/search");
                    break;
              
            }
        }

        private void txtBuscar_Evento(object sender, RoutedEventArgs e)
        {
            try
            {
                string textoBusqueda = txtBuscar.Text;
                string session =  StateModel.ObtieneKey("Usuario") != null ? StateModel.ObtieneKey("Usuario").ToString() : string.Empty;
                controlBuscarPeliculas.VaciaListaPeliculas();
                BuscarJson buscarJson = new BuscarJson(session, textoBusqueda);
                string postBuscarPeliculas = JsonConvert.SerializeObject(buscarJson);
                controlBuscarPeliculas.CargaPeliculasPost(postBuscarPeliculas, "http://www.qubit.tv/business.php/json/search");                    
            }
            catch (Exception)
            {
                
                throw;
            } 


        }

        private void imgVer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                 Image img = sender as Image;
            string idPelicula = Convert.ToString(img.Tag);
            img.Source = PeliculaModel.BotonVer(true);

            StateModel.CargaKey("idPelicula", idPelicula);
           
            this.VerPelicula(img, idPelicula);
            img.Source = PeliculaModel.BotonVer(false);
            }
            catch (Exception)
            {
                
                throw;
            } 

        }

        private void VerPelicula(Image imgVerAhora, string idPelicula)
        {
            try
            {
                PeliculaJson peliculaJson = new PeliculaJson();
                peliculaJson.element_id = idPelicula;
                  if (!StateModel.ExisteKey("Usuario"))
                  {
                      MessageBox.Show("Primero tenés que iniciar sesion.", "error", MessageBoxButton.OK);
                      return;
                }
                  else
                  {
                      MessageBox.Show(string.Format("Estás por ver {0}" + Environment.NewLine + "calificación {1}" + Environment.NewLine + "costo $ {2}" + Environment.NewLine, peliculaPrincipal[0].title, peliculaPrincipal[0].classification, peliculaPrincipal[0].price_sd), "", MessageBoxButton.OK);
                  }
                  bool hayRed = NetworkInterface.GetIsNetworkAvailable();
                  if (hayRed)
                  {
                 
                      string postJsonPelicula = JsonConvert.SerializeObject(peliculaJson);
                      PlayJson playJson = new PlayJson();
                      playJson.content_id = peliculaPrincipal[0].id;
                      playJson.session_id = ((Usuario)StateModel.ObtieneKey("Usuario")).session_id;
                      string jsonPostPlay = JsonConvert.SerializeObject(playJson);

                      PeliculaModel peliculaModel = new PeliculaModel();
                      peliculaModel.EjecutaMultimediaPelicula(playJson);
                  }else
                  {
                      MessageBox.Show("Para poder ver la película necesita acceso a internet.");             
                  }

    }
            catch (Exception)
            {                
                throw;
            }            
        }        
    }
}