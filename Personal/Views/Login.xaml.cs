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
using Personal.Model;
using Personal.Domain.Entities;
using Personal.JsonAccess;
using Newtonsoft.Json;
using Personal.JsonAccess.JsonClasses;


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
                txtClavePersonal.Password = "";
                txtClavePersonal.Foreground = getColorFromHexa("#000000");

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
                txtNroLinea.Foreground = getColorFromHexa("#000000");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void txtNroLinea_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNroLinea.Foreground = getColorFromHexa("#ffffff");
        }

        private void txtClavePersonal_LostFocus(object sender, RoutedEventArgs e)
        {
            txtClavePersonal.Foreground = getColorFromHexa("#ffffff");
        }

        private void btnAceptar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                string numeroTelefono = txtNroLinea.Text;
                string password = txtClavePersonal.Password;
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
                    MessageBox.Show("Se ha logueado correctamente", "Estado Login", MessageBoxButton.OK);
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


        SolidColorBrush getColorFromHexa(string hexaColor)
        {
            byte r = Convert.ToByte(hexaColor.Substring(1, 2), 16);
            byte g = Convert.ToByte(hexaColor.Substring(3, 2), 16);
            byte b = Convert.ToByte(hexaColor.Substring(5, 2), 16);
            SolidColorBrush soliColorBrush = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            return soliColorBrush;
        }

       
    }      
}