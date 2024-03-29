﻿using System;
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
using Personal.Domain.Utils;

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
        Usuario usuario = (Usuario)StateModel.ObtieneKey("Usuario");

        void Home_Loaded(object sender, RoutedEventArgs e)
        {
            try

            {   
                
                Usuario usuario = (Usuario)StateModel.ObtieneKey("Usuario");

                string genero = "genero_comedia";
                StateModel.CargaKey("named_criteria", genero);
                CargaPeliculasHome(usuario);


                MenuJson postMenu = new MenuJson();
                string postDataMenu = JsonConvert.SerializeObject(postMenu);
                CargaMenusPost(postDataMenu, URL.Menus);
                
            }
            catch (Exception )
            {
                throw ;
            }
        }

        private void CargaPeliculasHome(Usuario usuario)
        {
            PeliculasPorGeneroJson peliPrincipal = new PeliculasPorGeneroJson();
            if(usuario !=null)                  
                peliPrincipal.session_id = usuario.session_id;     
            
            string post_dataPeliculas = JsonConvert.SerializeObject(peliPrincipal);
            peliPrincipal.named_criteria = "genero_comedia";
            StateModel.CargaKey("vermas", peliPrincipal);

            //ratingControl.EstrellasActivas(4);
            peliculasHome.CargaPeliculasPost(post_dataPeliculas, URL.MenuCategoria);


            PublicitiesJson publi = new PublicitiesJson();
            publi.session_id = usuario != null ? usuario.session_id : string.Empty;
            publi.device = "PC";
            string jsonPublicities = JsonConvert.SerializeObject(publi);
            CargaPublicitiesPost(jsonPublicities, URL.Publicities);
        }

        public void CargaMenusPost(string postdataMenu,string urlMenu)
        {           
            JsonRequest MenusRequest = new JsonRequest();            
            MenusRequest.Completed += new EventHandler(handleResponseMenus);
            MenusRequest.beginRequest(postdataMenu, urlMenu);            
        }


      // ACA CARGA LA PELICULA PRINCIPAL DEL HOME. COMO NO ES ASI SE COMENTO EL CODIGO.
        //public void handleResponsePelicula(object sender, EventArgs args)
        //{
        //    JsonRequest responseObject = sender as JsonRequest;
        //    string response = responseObject.ResponseTxt;

        //    Pelicula pelicula = new Pelicula();
            
        //    peliculaPrincipal =peliculasHome.ObtenerPrevioPorGenero(response, 1);
        //    grillaPersonalVideo.DataContext = peliculaPrincipal[0];
        //    ratingControl.EstrellasActivas(peliculaPrincipal[0].ranking);
        //    //parse it
        //}
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
            usuario = (Usuario)StateModel.ObtieneKey("Usuario");
            PivotItem pivot = (sender as Pivot).SelectedItem as PivotItem;
            switch (pivot.Name)
            {
                case "pivItemRecomendado":
                    PeliculasPorGeneroJson recomendadoData = new PeliculasPorGeneroJson();
                    recomendadoData.session_id = usuario != null ? usuario.session_id : string.Empty;
                    recomendadoData.named_criteria = "recomendadas";
                    StateModel.CargaKey("vermas", recomendadoData);
                    string post_recomendado = JsonConvert.SerializeObject(recomendadoData);
                    recomendado.CargaPeliculasPost(post_recomendado,URL.MenuCategoria);
                    break;

                case "pivItemMiCuenta":
                    if (!StateModel.ExisteKey("Usuario"))
                    {
                        miCuentaTexto2.Visibility = System.Windows.Visibility.Collapsed;
                        txtSuscripcion.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto4.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto5.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto6.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto7.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto1.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        miCuentaTexto1.Visibility = System.Windows.Visibility.Collapsed;
                        miCuentaTexto2.Visibility = System.Windows.Visibility.Visible;
                        txtSuscripcion.Visibility = System.Windows.Visibility.Visible;
                        miCuentaTexto4.Visibility = System.Windows.Visibility.Visible;
                        miCuentaTexto5.Visibility = System.Windows.Visibility.Visible;
                        miCuentaTexto6.Visibility = System.Windows.Visibility.Visible;
                        miCuentaTexto7.Visibility = System.Windows.Visibility.Visible;
                        if (usuario.suscription_id == ((int)Enums.Enumsuscripcion.Activar).ToString())
                        {
                            txtSuscripcion.Text = "activar suscripción";
                        }
                        else if (usuario.suscription_id == ((int)Enums.Enumsuscripcion.Desactivar).ToString())
                        {
                            txtSuscripcion.Text = "desactivar suscripción";
                        }
                    }
                    break;

                case "pivItemPersonalVideo":

                    usuario = StateModel.ObtieneKey("Usuario") as Usuario;
                     if(usuario !=null)
                            this.CargaPeliculasHome(usuario);
                           
                    break;
                /*case "pivItemAlquiladas":
                    PeliculasPorGeneroJson muyAlquiladasData = new PeliculasPorGeneroJson();
                    muyAlquiladasData.named_criteria = "genero_accion";
                    string post_muyAlquiladas = JsonConvert.SerializeObject(muyAlquiladasData);
                    muyAlquiladas.CargaPeliculasPost(post_muyAlquiladas, "http://www.qubit.tv/business.php/json/search");
                    break;*/
               /*case "pivItemEstrenos":
                    PeliculasPorGeneroJson estrenosData = new PeliculasPorGeneroJson();
                    estrenosData.named_criteria = "genero_suspenso";
                    string post_estrenos = JsonConvert.SerializeObject(estrenosData);
                    estrenos.CargaPeliculasPost(post_estrenos, "http://www.qubit.tv/business.php/json/search");
                    break;*/
                /*case "pivItemTodoCatalogo":
                    PeliculasPorGeneroJson todoElCatalogoData = new PeliculasPorGeneroJson();
                    todoElCatalogoData.named_criteria = "genero_romantico";
                    string post_todoElcatalogo = JsonConvert.SerializeObject(todoElCatalogoData);
                    todoElCatalogo.CargaPeliculasPost(post_todoElcatalogo, "http://www.qubit.tv/business.php/json/search");
                    break;*/
              
            }
        }

        private void txtBuscarConEnter_Evento(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
               if (e.Key == System.Windows.Input.Key.Enter)
               {
                   this.BuscarPorFiltro();
               }

            }
            catch (Exception)
            {
                
                throw;
            } 


        }

        private void txtBuscar_Evento(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBuscar.Text.Length > 0)
                {
                    this.BuscarPorFiltro();
                }
                //StateModel.CargaKey("idPelicula", idPelicula);

                //this.VerPelicula(img, idPelicula);
                //img.Source = PeliculaModel.BotonVer(false);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void BuscarPorFiltro()
        {
            usuario = (Usuario)StateModel.ObtieneKey("Usuario");
            this.Focus();
            string textoBusqueda = txtBuscar.Text;
            string session = usuario !=null ? usuario.session_id :string.Empty;
            controlBuscarPeliculas.VaciaListaPeliculas();
            BuscarJson buscarJson = new BuscarJson(session, textoBusqueda);
            string postBuscarPeliculas = JsonConvert.SerializeObject(buscarJson);
            controlBuscarPeliculas.CargaPeliculasPost(postBuscarPeliculas,URL.MenuCategoria);
            StateModel.CargaKey("VieneDeBuscar", true);
            txtResultado.Visibility = System.Windows.Visibility.Visible;
        }
        private void imgVer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                Image img = sender as Image;
                string idPelicula = Convert.ToString(img.Tag);
                img.Source = PeliculaModel.BotonVer(true);
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void txtVolverBuscar_Evento(object sender, RoutedEventArgs e)
        {
            try
            {
                txtBuscar.Text = "Buscar";
                txtBuscar.Foreground = Utils.getColorFromHexa("#ffffff");
                //txtResultado.Visibility = System.Windows.Visibility.Collapsed;

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void txtSetearCampo_Evento(object sender, RoutedEventArgs e)
        {
            try
            {
                txtBuscar.Text = string.Empty;
                txtBuscar.Foreground = Utils.getColorFromHexa("#000000");
            }
            catch (Exception)
            {

                throw;
            }


        }

      

        private void imgFav_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {


            if (!StateModel.ExisteKey("Usuario"))
            {
                NavigationService.Navigate(new Uri("/Views/Login.xaml", UriKind.Relative));
                return;
            }
            else
            {
                //MessageBox.Show(string.Format("Estás por ver {0}" + Environment.NewLine + "calificación {1}" + Environment.NewLine + "costo $ {2}" + Environment.NewLine, peliculaCargada.title, peliculaCargada.classification, peliculaCargada.price_sd), "", MessageBoxButton.OK);
            }
        }

        private void VerPelicula(Image imgVerAhora, string idPelicula)
        {
            try
            {
                usuario = StateModel.ObtieneKey("Usuario") as Usuario;
                PeliculaJson peliculaJson = new PeliculaJson();
                peliculaJson.element_id = idPelicula;
                  if (usuario == null)
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
                      playJson.session_id = usuario.session_id;
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

        private void txtTyC_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/TerminosyCondiciones.xaml", UriKind.Relative));
            return;
        }

        public void CargaPublicitiesPost(string postdata, string url)
        {
            JsonRequest PeliculaRequest = new JsonRequest();
            PeliculaRequest.Completed += new EventHandler(handleResponsePublicities);
            PeliculaRequest.beginRequest(postdata, url);
        }
        public void handleResponsePublicities(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            this.CargaPublicities(response);    
            //parse it
        }


        private void CargaPublicities(string response)
        {
            try
            {
                List<Publicities> listaPublicities = new List<Publicities>();
                listaPublicities = JsonModel.ConvierteJsonPublicities(response);                
                PivotItem pivotItem;
                PublicitiesControl publicitiesControl = new PublicitiesControl();
                publicitiesControl.DataContext = listaPublicities[0];
                pivotItem1.Content = publicitiesControl;                                            
                int i = 0;
                foreach (Publicities item in listaPublicities)
                {
                    if (i != 0)
                    {
                        pivotItem = new PivotItem();
                        pivotItem.Margin = new Thickness(0,-6,0, 0);
                        publicitiesControl = new PublicitiesControl();
                        publicitiesControl.DataContext = item;
                        pivotItem.Content = publicitiesControl;
                        pivotImagenPrincipal.Items.Add(pivotItem);
                       
                    }
                    i++;
                }            
            }
            catch (Exception ex)
            {                
                throw;
            }        
        }

        private void txtSuscripcion_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                usuario = StateModel.ObtieneKey("Usuario") as Usuario;

                if (usuario == null)
                {
                    MessageBox.Show("Para poder modificar la suscripcion debe estar logueado.", "error", MessageBoxButton.OK);
                    return;
                }

                usuario.suscription_id = usuario.suscription_id == ((int)Enums.Enumsuscripcion.Activar).ToString() ? ((int)Enums.Enumsuscripcion.Desactivar).ToString() : ((int)Enums.Enumsuscripcion.Activar).ToString();

                SuscripcionJson suscripcionJson = new SuscripcionJson(usuario.username, usuario.suscription_id);

                string jsonSuscripcion = JsonConvert.SerializeObject(suscripcionJson);

                this.CargaSuscripcionPost(jsonSuscripcion, URL.Suscripcion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ah ocurrido un error. La aplicación se cerrará.");                
                throw;
            }
            


        }

        public void CargaSuscripcionPost(string postdata, string url)
        {
            JsonRequest PeliculaRequest = new JsonRequest();
            PeliculaRequest.Completed += new EventHandler(handleResponseSuscripcion);
            PeliculaRequest.beginRequest(postdata, url);
        }
        public void handleResponseSuscripcion(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            this.CargaSuscripcion(response);
            if (usuario.suscription_id == ((int)Enums.Enumsuscripcion.Desactivar).ToString())
            {
                MessageBox.Show("suscripción satisfactoria.", "OK", MessageBoxButton.OK);
                txtSuscripcion.Text = "desactivar suscripción";
            }
            else if (usuario.suscription_id == ((int)Enums.Enumsuscripcion.Activar).ToString())
            {
                MessageBox.Show("desuscripción satisfactoria.", "OK", MessageBoxButton.OK);
                txtSuscripcion.Text = "activar suscripción";
            }
            //parse it
        }

        private void CargaSuscripcion(string response)
        {
            if (response.Trim() == "dominio incorrecto!")
                MessageBox.Show("el email ingresado no es correcto.", "error", MessageBoxButton.OK);
            
        }

        private void txtFavoritos_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Favoritos.xaml", UriKind.Relative));
            return;
        }

    }
}