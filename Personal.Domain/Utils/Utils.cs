using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Personal.Domain.Utils
{
    public class Utils
    {
        public static SolidColorBrush getColorFromHexa(string hexaColor)
        {
            byte r = Convert.ToByte(hexaColor.Substring(1, 2), 16);
            byte g = Convert.ToByte(hexaColor.Substring(3, 2), 16);
            byte b = Convert.ToByte(hexaColor.Substring(5, 2), 16);
            SolidColorBrush soliColorBrush = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            return soliColorBrush;
        }

    }
}
