using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFCanvas_Move_Drog.ViewModels
{
    public class PointItem : Thumb
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
