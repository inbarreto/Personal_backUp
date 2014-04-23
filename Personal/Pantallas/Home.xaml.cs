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

            listaGeneros.Add(new Generos(1,"aventuras"));
            listaGeneros.Add(new Generos(2,"de autor"));
            listaGeneros.Add(new Generos(3,"infantil y familia"));
            listaGeneros.Add(new Generos(4,"suspenso"));
            listaGeneros.Add(new Generos(5,"animacion"));
            listaGeneros.Add(new Generos(6,"romántico"));
            listaGeneros.Add(new Generos(7,"cine argentino"));
            listaGeneros.Add(new Generos(8,"clásico"));
            listaGeneros.Add(new Generos(8, "policial"));
            listaGeneros.Add(new Generos(8, "fantasía"));

            lboxgeneros.ItemsSource = listaGeneros;
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

     

        private void imgVer_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Image img = sender as Image;
            BitmapImage imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver-activo.png", UriKind.RelativeOrAbsolute));

            img.Source = imag;
            NavigationService.Navigate(new Uri(@"/Pantallas/FichaTecnica.xaml",UriKind.Relative));

        }

        private void imgVer_LostFocus(object sender, RoutedEventArgs e)
        {
            
           Image img = sender as Image;
            BitmapImage imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver-inactivo.png", UriKind.RelativeOrAbsolute));

            img.Source = imag;  

        }

        private void imgVer_GotFocus(object sender, RoutedEventArgs e)
        {
            BitmapImage imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver-activo.png", UriKind.RelativeOrAbsolute));

            imgVer.Source = imag;  
        }

    }
}