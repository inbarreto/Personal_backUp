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
                                                
                string genero = "genero_comedia";
                
                StateModel.CargaKey("named_criteria",genero);                
                PeliculasPorGeneroJson peliPrincipal = new PeliculasPorGeneroJson();
                peliPrincipal.named_criteria = genero;
                string post_dataPeliculas = JsonConvert.SerializeObject(peliPrincipal);

                CargaMenusPost(postDataMenu, urlMenu, post_dataPeliculas, "http://www.qubit.tv/business.php/json/search");

                peliculasHome.CargaPeliculasPost(post_dataPeliculas, "http://www.qubit.tv/business.php/json/search");

                PeliculasPorGeneroJson muyAlquiladasData = new PeliculasPorGeneroJson();
                muyAlquiladasData.named_criteria = "genero_accion";
                string post_muyAlquiladas = JsonConvert.SerializeObject(muyAlquiladasData);

                muyAlquiladas.CargaPeliculasPost(post_muyAlquiladas, "http://www.qubit.tv/business.php/json/search");

                PeliculasPorGeneroJson estrenosData = new PeliculasPorGeneroJson();
                estrenosData.named_criteria = "genero_suspenso";
                string post_estrenos = JsonConvert.SerializeObject(estrenosData);

                estrenos.CargaPeliculasPost(post_estrenos, "http://www.qubit.tv/business.php/json/search");

                PeliculasPorGeneroJson todoElCatalogoData = new PeliculasPorGeneroJson();
                todoElCatalogoData.named_criteria = "genero_romantico";
                string post_todoElcatalogo = JsonConvert.SerializeObject(todoElCatalogoData);

                todoElCatalogo.CargaPeliculasPost(post_todoElcatalogo, "http://www.qubit.tv/business.php/json/search");

                PeliculasPorGeneroJson recomendadoData = new PeliculasPorGeneroJson();
                recomendadoData.named_criteria = "genero_terror";
                string post_recomendado = JsonConvert.SerializeObject(recomendadoData);

                recomendado.CargaPeliculasPost(post_recomendado, "http://www.qubit.tv/business.php/json/search");

                
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


       

       
        public void handleResponsePelicula(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;

            Pelicula pelicula = new Pelicula();
            List<Pelicula> pelisList = new List<Pelicula>();
             pelisList =peliculasHome.ObtenerPrevioPorGenero(response, 1);
            grillaPersonalVideo.DataContext = pelisList[0];
            ratingControl.EstrellasActivas(pelisList[0].ranking);
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

      

        private static System.Windows.Media.SolidColorBrush ObtieneColorHexa(string colorHexa)
        {
            
            byte A = 255;
            byte R = Convert.ToByte(colorHexa.Substring(1, 2), 16);
            byte G = Convert.ToByte(colorHexa.Substring(3, 2), 16);
            byte B = Convert.ToByte(colorHexa.Substring(5, 2), 16);
            SolidColorBrush scb = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            return scb;
        }

       

        
       
        private void txtGenero_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            try
            {
                TextBlock texto = e.OriginalSource as TextBlock;

                string valor = texto.Text;

               Generos genero = listaGeneros.Find(x => x.Genero == valor);

               string gen = genero.NameCriteria;

               StateModel.CargaKey("genero", genero.Genero);
               StateModel.CargaKey("named_criteria", genero.NameCriteria);
               
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
       


    }
}