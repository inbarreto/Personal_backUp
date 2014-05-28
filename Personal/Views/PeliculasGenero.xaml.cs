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
        void PeliculasGenero_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {            
                string genero = StateModel.ObtieneKey("named_criteria").ToString();

                txtGenero.Text = StateModel.ObtieneKey("genero").ToString();                                                           
            }
            catch (Exception )
            {                
                throw ;
            }
            
        }


    }
}