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

namespace Personal
{
    public partial class Home : PhoneApplicationPage
    {
        public Home()
        {
            InitializeComponent();
            this.Loaded += Home_Loaded;
        }

        void Home_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CargaGeneros();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CargaGeneros()
        {
            List<Generos> listaGeneros = new List<Generos>();

            listaGeneros.Add(new Generos(1,"Aventuras"));
            listaGeneros.Add(new Generos(2,"de autor"));
            listaGeneros.Add(new Generos(3,"infantil y familia"));
            listaGeneros.Add(new Generos(4,"suspenso"));
            listaGeneros.Add(new Generos(5,"animacion"));
            listaGeneros.Add(new Generos(6,"romántico"));
            listaGeneros.Add(new Generos(7,"cine Argentino"));
            listaGeneros.Add(new Generos(8,"clásico"));

            lboxgeneros.ItemsSource = listaGeneros;
        }


    }
}