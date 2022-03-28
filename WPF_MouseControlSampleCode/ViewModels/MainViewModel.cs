using System;
using System.Windows;
using System.Windows.Media;
using WPF_MouseControlSampleCode.Behavior;
using WPF_MouseControlSampleCode.Interface;

namespace WPF_MouseControlSampleCode.ViewModels
{
    public class MainViewModel : IMouseCaptureProxy
    {
        public event EventHandler Capture;
        public event EventHandler Release;

        public void OnMouseDown(object sender, MouseCaptureArgs e) 
        {
            this.Capture(sender, null);
        }
        public void OnMouseMove(object sender, MouseCaptureArgs e) 
        {
            if(e.LeftButton)
            {

            }
        }
        public void OnMouseUp(object sender, MouseCaptureArgs e) 
        {
            this.Release(sender, null);
        }


    }
}
