﻿#pragma checksum "C:\Users\Ignacio\Documents\Visual Studio 2012\Projects\Personal\Personal\Controles\CustomMessegeBox.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "491C1FA522B5FAC13E527CF2EDEDA070"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Personal.Controles {
    
    
    public partial class CustomMessegeBox : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid MessagePanel;
        
        internal System.Windows.Controls.TextBlock HeaderTextBlock;
        
        internal System.Windows.Controls.TextBox txtNumeroTelefono;
        
        internal System.Windows.Controls.TextBlock MessageTextBlock;
        
        internal System.Windows.Controls.Button YesButton;
        
        internal System.Windows.Controls.Button NoButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Personal;component/Controles/CustomMessegeBox.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MessagePanel = ((System.Windows.Controls.Grid)(this.FindName("MessagePanel")));
            this.HeaderTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("HeaderTextBlock")));
            this.txtNumeroTelefono = ((System.Windows.Controls.TextBox)(this.FindName("txtNumeroTelefono")));
            this.MessageTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("MessageTextBlock")));
            this.YesButton = ((System.Windows.Controls.Button)(this.FindName("YesButton")));
            this.NoButton = ((System.Windows.Controls.Button)(this.FindName("NoButton")));
        }
    }
}
