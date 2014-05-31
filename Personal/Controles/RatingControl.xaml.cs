using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace Personal.Controles
{
    public partial class RatingControl : UserControl
    {
        private int estrellas;
        public int Estrellas
        {
            get { return this.estrellas; }
            set
            {
                this.estrellas = value;
                this.EstrellasActivas(value);

            }
        }


        public RatingControl()
        {
            InitializeComponent();
            this.Loaded += RatingControl_Loaded;
        }

        void RatingControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void EstrellasActivas(int cantidadEstrellas)
        {
            try
            {
                if (cantidadEstrellas <= 5)
                {
                    BitmapImage imag;
                    imag = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"../Imagenes/Rating/estrella_activa.png", UriKind.RelativeOrAbsolute));
                    Image imagenEstrella;
                    for (int i = 1; i <= cantidadEstrellas; i++)
                    {
                        imagenEstrella = this.FindName(String.Format("estrella{0}", i)) as Image;
                        imagenEstrella.Source = imag;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
