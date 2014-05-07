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
        void PeliculasGenero_Loaded(object sender, RoutedEventArgs e)
        {
             try
             {
                 string genero = string.Empty;

                 if (PhoneApplicationService.Current.State.ContainsKey("named_criteria"))
                        genero = (string)PhoneApplicationService.Current.State["named_criteria"];

            }
            catch (Exception ex)
            {                
                throw ex;
            }
            
        }

        public Pelicula CargaListaPeliculas(string genero,int cantidadPeliculas)
        {
            try
            {
                
                string json = ObtieneJson(genero);
                List<Pelicula> lista = this.ObtenerPrevioPorGenero(json,cantidadPeliculas);
                Pelicula peli = lista.First<Pelicula>();
                lista.Remove(peli);
                listaPeliculas.ItemsSource = lista;
             
                
                return peli;
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }
        /// <summary>
        /// Carga las peliculas en una lista
        /// </summary>
        /// <param name="named_criteria">genero de pelis</param>
        private List<Pelicula> ObtenerPrevioPorGenero(string jsonPeliculas,int cantidadPeliculas)
        {
            try
            {
                JToken element = ExtraigoElementJson(jsonPeliculas);
                

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
            
            img.Source = PeliculaModel.BotonVer();
            //NavigationService.Navigate(new Uri(@"/Views/FichaTecnica.xaml", UriKind.Relative));
            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri(@"/Views/FichaTecnica.xaml", UriKind.Relative)); 

        }


        private string ObtieneJson(string genero)
        {
            
            switch (genero)
            { 
               case "genero_accion":
                    return variables.genero_accion ;

               case "genero_animacion": return variables.genero_animacion;

               case "genero_comedia": return variables.genero_comedia;
               default: return variables.genero_accion;
            }

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
                //ListBoxItem fin = new ListBoxItem();
                //fin.FindName
                //Image imagen = sender as Image;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        }
}

