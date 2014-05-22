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

using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Personal.Model;
using Personal.Views;
using Personal.Controles;
using Personal.JsonAccess;
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Personal.Domain.Enums;

namespace Personal
{   
    public partial class Home : PhoneApplicationPage
    {

            
        public Home()
        {
            InitializeComponent();
            this.Loaded += Home_Loaded;
        }

        List<Generos> listaGeneros = new List<Generos>();
        Variables variables = new Variables();
        void Home_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //string postDataMenu = "{\"device\":\"windows_8\",\"name\":\"\"}";
                MenuJson postMenu = new MenuJson();
                string postDataMenu = JsonConvert.SerializeObject(postMenu);
                string urlMenu = "http://www.qubit.tv/business.php/json/menus";
                                                
                string gen = "genero_comedia";
                
                if (PhoneApplicationService.Current.State.ContainsKey("named_criteria"))
                    PhoneApplicationService.Current.State["named_criteria"] = gen;
                else
                    PhoneApplicationService.Current.State.Add("named_criteria", gen);

                PeliculasPorGeneroJson peliPrincipal = new PeliculasPorGeneroJson();
                peliPrincipal.named_criteria = "genero_comedia";
                string post_dataPeliculas = JsonConvert.SerializeObject(peliPrincipal);

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
                   if (!PhoneApplicationService.Current.State.ContainsKey("Usuario"))
                       PhoneApplicationService.Current.State.Add("Usuario", usuarioObjeto);
                   else
                       PhoneApplicationService.Current.State["Usuario"] = usuarioObjeto;
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
            List<Pelicula> pelisList = new List<Pelicula>();
             pelisList =peliculasHome.ObtenerPrevioPorGenero(response, 2);
            grillaPersonalVideo.DataContext = pelisList[0]; 
            //parse it
        }
        public void handleResponseMenus(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            CargaGeneros(response);
            //parse it
        }


        private void CargaGeneros(string stringJson)
        {
            try
            {


                JObject json = JsonModel.StringToJsonObject(stringJson);                
                //JObject json = JsonModel.StringToJsonObject(variables.Menu);                
                //JObject json = JsonModel.StringToJsonObject(valorJson);                
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

        private void txtClavePersonal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
        }

        private static System.Windows.Media.SolidColorBrush ObtieneColorHexa(string colorHexa)
        {
            
            byte A = 255;
            byte R = Convert.ToByte(colorHexa.Substring(1, 2), 16);
            byte G = Convert.ToByte(colorHexa.Substring(3, 2), 16);
            byte B = Convert.ToByte(colorHexa.Substring(5, 2), 16);
            SolidColorBrush scb = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return scb;
        }

        private void txtNroLinea_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            txtNroLinea.Text = string.Empty;
            SolidColorBrush scb = ObtieneColorHexa("#FFB28AAD");            
            txtNroLinea.Foreground = scb;

            
        }

        
       
        private void txtGenero_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            try
            {
                TextBlock texto = e.OriginalSource as TextBlock;

                string valor = texto.Text;

               Generos genero = listaGeneros.Find(x => x.Genero == valor);

               string gen = genero.NameCriteria;

               if (PhoneApplicationService.Current.State.ContainsKey("genero"))
                   PhoneApplicationService.Current.State["genero"] = genero.Genero;
               else
                   PhoneApplicationService.Current.State.Add("genero", genero.Genero);

               if (PhoneApplicationService.Current.State.ContainsKey("named_criteria"))
                   PhoneApplicationService.Current.State["named_criteria"] = gen;
               else
                    PhoneApplicationService.Current.State.Add("named_criteria", genero.NameCriteria);
               
                NavigationService.Navigate(new Uri("/Views/PeliculasGenero.xaml", UriKind.Relative));
                
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

        private void btnAceptar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                string numeroTelefono = txtNroLinea.Text;
                string password = txtClavePersonal.Text;
                if (numeroTelefono == string.Empty || password == string.Empty)
                {
                    MessageBox.Show("Número de linea o clave personal incorrectos", "error", MessageBoxButton.OK);
                    return;
                }
                
                LoginJson loginJsonPost= new LoginJson();
            loginJsonPost.password = password;
            loginJsonPost.username = numeroTelefono;
            string postUsuario= JsonConvert.SerializeObject(loginJsonPost);

            CargaUsuarioPost(postUsuario, "http://secure.qubit.tv/json/login");

            }
            catch (Exception)
            {
                
                throw;
            }
        }
       


    }
}