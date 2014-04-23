using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Expression.Drawing;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Media;


namespace Personal.Pantallas
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
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
            txtNroLinea.Foreground = new SolidColorBrush(Colors.White);
            txtNroLinea.Background = new SolidColorBrush(Colors.Black);
        }
    }
}