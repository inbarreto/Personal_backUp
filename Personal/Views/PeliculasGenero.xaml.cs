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
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Personal.JsonAccess;

namespace Personal.Views
{
    public partial class PeliculasGenero : PhoneApplicationPage
    {
        public PeliculasGenero()
        {
            InitializeComponent();
            this.Loaded += PeliculasGenero_Loaded;
        }
        Variables variables = new Variables();
        Usuario usuario = new Usuario();
        void PeliculasGenero_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                usuario = StateModel.ObtieneKey("Usuario") as Usuario;
                string genero = (string)StateModel.ObtieneKey("named_criteria");
                
                txtGenero.Text = (string)StateModel.ObtieneKey("genero");

                PeliculasPorGeneroJson peliPrincipal = new PeliculasPorGeneroJson();
                peliPrincipal.named_criteria = genero;
                 if (usuario != null)
                    peliPrincipal.session_id = usuario.session_id;
                string post_dataPeliculas = JsonConvert.SerializeObject(peliPrincipal);

                controlPeliculasPorGenero.CargaPeliculasPost(post_dataPeliculas, URL.MenuCategoria);                     
            }
            catch (Exception )
            {                
                throw ;
            }
            
        }


    }
}