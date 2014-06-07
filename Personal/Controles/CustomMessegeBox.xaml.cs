using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;


namespace Personal.Controles
{
    public partial class CustomMessegeBox : UserControl
    {
        public CustomMessegeBox()
        {
            InitializeComponent();
           
        }


        public void CerrarPopUp(bool estado)
        {
            Popup customMessege = this.Parent as Popup;
            customMessege.IsOpen = false;
        }
    }
}
