﻿#pragma checksum "C:\Users\Ignacio\Documents\Visual Studio 2012\Projects\Personal\Personal\Views\PeliculasGenero.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6B162F1FC76FF98D80E73A673A86D57C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
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


namespace Personal.Views {
    
    
    public partial class PeliculasGenero : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid contenedor;
        
        internal System.Windows.Controls.TextBlock txtGenero;
        
        internal Personal.Controles.PeliculasPorGenero controlPeliculasPorGenero;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Personal;component/Views/PeliculasGenero.xaml", System.UriKind.Relative));
            this.contenedor = ((System.Windows.Controls.Grid)(this.FindName("contenedor")));
            this.txtGenero = ((System.Windows.Controls.TextBlock)(this.FindName("txtGenero")));
            this.controlPeliculasPorGenero = ((Personal.Controles.PeliculasPorGenero)(this.FindName("controlPeliculasPorGenero")));
        }
    }
}

