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
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;
using System.Net.NetworkInformation;


namespace Personal.Pantallas
{
    public partial class FichaTecnica : PhoneApplicationPage
    {
        public FichaTecnica()
        {
            InitializeComponent();
            CargaPagina();
            
        }

        public void CargaPagina()
        {
            Pelicula peliculaCargada = new Pelicula(true);
            datosPelicula.DataContext = peliculaCargada;
            imagenPeli.DataContext = peliculaCargada.fanart;
            foreach (string item in peliculaCargada.categorie)
            {
                catego.Text += item+" ";
            }        
            cargaInformation(peliculaCargada.information);
         //   BitmapImage imag;
         //if (peliculaCargada.favorite)
         //   imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav-activo-46.png", UriKind.RelativeOrAbsolute));
         //else
         //    imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/fav.png", UriKind.RelativeOrAbsolute));
         //imgFavorito.Source = imag;            

        }


        private void cargaInformation(List<Information> information)
        {            
            foreach (Information item in information)
	        {
                if (item.field_name == "Director")
                    datoDirector.Text = item.value;
                else
                    if (item.field_name == "Artista")
                    {
                        if (txtEstrellas.Text == string.Empty)                        
                            txtEstrellas.Text += item.value;
                        else
                            txtEstrellas.Text +=", "+item.value;
                    }
	        }    
        }

        private void imgVerAhora_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            BitmapImage imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver ahora-hover.png", UriKind.RelativeOrAbsolute));
            imgVerAhora.Source = imag;

            bool hayRed=NetworkInterface.GetIsNetworkAvailable();
            if (hayRed)
            {
                MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
                mediaPlayerLauncher.Media = new Uri(@"http://clips.vorwaerts-gmbh.de/VfE_html5.mp4", UriKind.Absolute);
                mediaPlayerLauncher.Location = MediaLocationType.Data;
                mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
                mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;

                mediaPlayerLauncher.Show();
            }
            else
            {
                MessageBox.Show("Para poder ver la película necesita acceso a internet.");
                imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"/Imagenes/ver ahora-inactivo.png", UriKind.RelativeOrAbsolute));
                imgVerAhora.Source = imag;
            }
            
        }
    }
}