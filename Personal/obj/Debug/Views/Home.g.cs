﻿#pragma checksum "C:\Users\Ignacio\Documents\Visual Studio 2012\Projects\Personal\Personal\Views\Home.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4A5F91BE161AEBD7B34E9953A5A2FF25"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Personal.Controles;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Personal {
    
    
    public partial class Home : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivotHome;
        
        internal Microsoft.Phone.Controls.PivotItem pivItemPersonalVideo;
        
        internal System.Windows.Controls.StackPanel grillaPersonalVideo;
        
        internal Microsoft.Phone.Controls.Pivot pivotImagenPrincipal;
        
        internal System.Windows.Controls.Image imgPeliculaPrincipal;
        
        internal System.Windows.Controls.Image imgVer;
        
        internal System.Windows.Controls.Image imgPeliculaPrincipal2;
        
        internal System.Windows.Controls.Image imgVer2;
        
        internal System.Windows.Controls.TextBlock txtNombrePelicula;
        
        internal Personal.Controles.RatingControl ratingControl;
        
        internal Personal.Controles.PeliculasPorGenero peliculasHome;
        
        internal Microsoft.Phone.Controls.PivotItem pivItemGenero;
        
        internal System.Windows.Controls.ListBox lboxgeneros;
        
        internal Microsoft.Phone.Controls.PivotItem pivItemBuscar;
        
        internal System.Windows.Controls.TextBox txtBuscar;
        
        internal System.Windows.Controls.Image imgLupa;
        
        internal System.Windows.Controls.TextBlock txtResultado;
        
        internal Personal.Controles.PeliculasPorGenero controlBuscarPeliculas;
        
        internal Microsoft.Phone.Controls.PivotItem pivItemRecomendado;
        
        internal Personal.Controles.PeliculasPorGenero recomendado;
        
        internal Microsoft.Phone.Controls.PivotItem pivItemMiCuenta;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto1;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto2;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto3;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto4;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto5;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto6;
        
        internal System.Windows.Controls.TextBlock miCuentaTexto7;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Personal;component/Views/Home.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivotHome = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivotHome")));
            this.pivItemPersonalVideo = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivItemPersonalVideo")));
            this.grillaPersonalVideo = ((System.Windows.Controls.StackPanel)(this.FindName("grillaPersonalVideo")));
            this.pivotImagenPrincipal = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivotImagenPrincipal")));
            this.imgPeliculaPrincipal = ((System.Windows.Controls.Image)(this.FindName("imgPeliculaPrincipal")));
            this.imgVer = ((System.Windows.Controls.Image)(this.FindName("imgVer")));
            this.imgPeliculaPrincipal2 = ((System.Windows.Controls.Image)(this.FindName("imgPeliculaPrincipal2")));
            this.imgVer2 = ((System.Windows.Controls.Image)(this.FindName("imgVer2")));
            this.txtNombrePelicula = ((System.Windows.Controls.TextBlock)(this.FindName("txtNombrePelicula")));
            this.ratingControl = ((Personal.Controles.RatingControl)(this.FindName("ratingControl")));
            this.peliculasHome = ((Personal.Controles.PeliculasPorGenero)(this.FindName("peliculasHome")));
            this.pivItemGenero = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivItemGenero")));
            this.lboxgeneros = ((System.Windows.Controls.ListBox)(this.FindName("lboxgeneros")));
            this.pivItemBuscar = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivItemBuscar")));
            this.txtBuscar = ((System.Windows.Controls.TextBox)(this.FindName("txtBuscar")));
            this.imgLupa = ((System.Windows.Controls.Image)(this.FindName("imgLupa")));
            this.txtResultado = ((System.Windows.Controls.TextBlock)(this.FindName("txtResultado")));
            this.controlBuscarPeliculas = ((Personal.Controles.PeliculasPorGenero)(this.FindName("controlBuscarPeliculas")));
            this.pivItemRecomendado = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivItemRecomendado")));
            this.recomendado = ((Personal.Controles.PeliculasPorGenero)(this.FindName("recomendado")));
            this.pivItemMiCuenta = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivItemMiCuenta")));
            this.miCuentaTexto1 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto1")));
            this.miCuentaTexto2 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto2")));
            this.miCuentaTexto3 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto3")));
            this.miCuentaTexto4 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto4")));
            this.miCuentaTexto5 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto5")));
            this.miCuentaTexto6 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto6")));
            this.miCuentaTexto7 = ((System.Windows.Controls.TextBlock)(this.FindName("miCuentaTexto7")));
        }
    }
}
