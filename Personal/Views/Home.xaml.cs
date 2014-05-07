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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Personal.Model;
using Personal.Views;
using Personal.Controles;
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
                CargaGeneros();
                string gen = "genero_comedia";
                
                if (PhoneApplicationService.Current.State.ContainsKey("named_criteria"))
                    PhoneApplicationService.Current.State["named_criteria"] = gen;
                else
                    PhoneApplicationService.Current.State.Add("named_criteria", gen);
               //grillaPersonalVideo.DataContext = 

                //controlPeliculas.CargaListaPeliculas("genero_comedia");
                Pelicula pelicula = peliculasHome.CargaListaPeliculas("genero_comedia", 4);
                grillaPersonalVideo.DataContext = pelicula;
                

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CargaGeneros()
        {
            try
            {
                
                string jsonMenu = variables.Menu;
                JsonPostURL objetoMenu = new JsonPostURL();                

                JObject json= JObject.Parse(jsonMenu);
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
                //menu.criteria.named_criteria = (string)Tcriteria["criteria"];
                
                
                
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
            MessageBoxResult mensaje = MessageBox.Show("número de linea o clave incorrecta", "error", MessageBoxButton.OK);


            //Controles.CustomMessegeBox cm = new Controles.CustomMessegeBox();
            //Popup customMessege = new Popup();
            //Personal.Controles.CustomMessegeBox CM = new Controles.CustomMessegeBox();

        }

        private void txtClavePersonal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                txtClavePersonal.Text = string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtNroLinea_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            txtNroLinea.Text = string.Empty;
        }

        private void txtNroLinea_GotFocus(object sender, RoutedEventArgs e)
        {
            //txtNroLinea.Foreground = new SolidColorBrush(Colors.White);
            //txtNroLinea.Background = new SolidColorBrush(Colors.Black);
        }

        private void txtClavePersonal_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush varia = new SolidColorBrush();
            
            
           
        }

        private void txtNroLinea_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

     

        

        
       
        private void txtGenero_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            try
            {
                TextBlock texto = e.OriginalSource as TextBlock;

                string valor = texto.Text;

               Generos genero = listaGeneros.Find(x => x.Genero == valor);
                
               string gen = genero.NameCriteria;
              //this.ObtenerPrevioPorGenero(genero.NameCriteria);
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
       


    }
}