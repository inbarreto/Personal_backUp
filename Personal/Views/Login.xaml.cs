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
using Personal.JsonAccess.JsonClasses;
using Newtonsoft.Json;
using Personal.Domain.Entities;
using Personal.Model;
using Personal.JsonAccess;


namespace Personal.Views
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
                SolidColorBrush scb = ObtieneColorHexa("#FFB28AAD");
                //applying the brush to the background of the existing Button btn:
                txtClavePersonal.Foreground = scb;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtNroLinea_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                txtNroLinea.Text = string.Empty;
                SolidColorBrush scb = ObtieneColorHexa("#FFB28AAD");
                txtNroLinea.Foreground = scb;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            
        }

        private void txtNroLinea_GotFocus(object sender, RoutedEventArgs e)
        {
            txtNroLinea.Foreground = new SolidColorBrush(Colors.White);
            txtNroLinea.Background = new SolidColorBrush(Colors.Black);
        }
        private void btnAceptar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                string numeroTelefono = txtNroLinea.Text;
                string password = txtClavePersonal.Text;
                if (numeroTelefono == string.Empty || password == string.Empty)
                {
                    MessageBox.Show("Número de linea o clave personal incorrectos", "error", MessageBoxButton.OK);
                    return;
                }

                LoginJson loginJsonPost = new LoginJson();
                loginJsonPost.password = password;
                loginJsonPost.username = numeroTelefono;
                string postUsuario = JsonConvert.SerializeObject(loginJsonPost);

                CargaUsuarioPost(postUsuario, "http://secure.qubit.tv/json/login");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CargaUsuarioPost(string postdataLogin, string urlLogin)
        {

            JsonRequest usuarioRequest = new JsonRequest();
            usuarioRequest.Completed += new EventHandler(handleResponseLogin);
            usuarioRequest.beginRequest(postdataLogin, urlLogin);
        }

        public void handleResponseLogin(object sender, EventArgs args)
        {
            JsonRequest responseObject = sender as JsonRequest;
            string response = responseObject.ResponseTxt;
            CargaUsuario(response);


            //parse it
        }

        private void CargaUsuario(string jsonString)
        {
            try
            {
                Usuario usuarioObjeto = new Usuario();

                usuarioObjeto = JsonModel.ConvierteJsonAUsuario(jsonString);
                if (usuarioObjeto.username != null)
                {
                    StateModel.CargaKey("Usuario", usuarioObjeto);                    
                    MessageBox.Show("Se ha logeado correctamente", "Estado Login", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Usuario o contraseña incorrectos", "Estado Login", MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/Views/Home.xaml", UriKind.Relative));                
            }
            catch (Exception)
            {

                throw;
            }

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

       


    }
}