﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using WPF_MouseControlSampleCode.Interface;

namespace WPF_MouseControlSampleCode.Behavior
{
    public class MouseCaptureBehavior : Behavior<FrameworkElement>
    {
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public IMouseCaptureProxy Proxy
        {
            get { return (IMouseCaptureProxy)GetValue(ProxyProperty); }
            set { SetValue(ProxyProperty, value); }
        }
        public static readonly DependencyProperty ProxyProperty = DependencyProperty.Register(
            nameof(Proxy),
            typeof(IMouseCaptureProxy),
            typeof(MouseCaptureBehavior),
            new PropertyMetadata(null, OnProxyChanged));

        public static void SetProxy(DependencyObject source, IMouseCaptureProxy value)
        {
            source.SetValue(ProxyProperty, value);
        }

        public static IMouseCaptureProxy GetProxy(DependencyObject source)
        {
            return (IMouseCaptureProxy)source.GetValue(ProxyProperty);
        }

        private static void OnProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is IMouseCaptureProxy)
            {
                (e.OldValue as IMouseCaptureProxy).Capture -= OnCapture;
                (e.OldValue as IMouseCaptureProxy).Release -= OnRelease;
            }
            if (e.NewValue is IMouseCaptureProxy)
            {
                (e.NewValue as IMouseCaptureProxy).Capture += OnCapture;
                (e.NewValue as IMouseCaptureProxy).Release += OnRelease;
            }
        }

        private static void OnCapture(object sender, EventArgs e)
        {
            var behavior = sender as MouseCaptureBehavior;
            if (behavior != null)
                behavior.AssociatedObject.CaptureMouse();
        }

        private static void OnRelease(object sender, EventArgs e)
        {
            var behavior = sender as MouseCaptureBehavior;
            if (behavior != null)
                behavior.AssociatedObject.ReleaseMouseCapture();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (AssociatedObject == null)
            {
                // so, let'save the value and then reuse it when OnAttached() called
                _value = e.NewValue as string;
                return;
            }

            //if (e.Property == PasswordProperty)
            //{
            //    if (!_skipUpdate)
            //    {
            //        _skipUpdate = true;
            //        AssociatedObject.Password = e.NewValue as string;
            //        _skipUpdate = false;
            //    }
            //}
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewMouseDown += OnMouseDown;
            this.AssociatedObject.PreviewMouseMove += OnMouseMove;
            this.AssociatedObject.PreviewMouseUp += OnMouseUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewMouseDown -= OnMouseDown;
            this.AssociatedObject.PreviewMouseMove -= OnMouseMove;
            this.AssociatedObject.PreviewMouseUp -= OnMouseUp;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var proxy = GetProxy(this);
            if (proxy != null)
            {
                var pos = e.GetPosition(this.AssociatedObject);
                var args = new MouseCaptureArgs
                {
                    X = pos.X,
                    Y = pos.Y,
                    LeftButton = (e.LeftButton == MouseButtonState.Pressed),
                    RightButton = (e.RightButton == MouseButtonState.Pressed),
                    OriginSource = sender
                };
                proxy.OnMouseDown(this, args);
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var proxy = GetProxy(this);
            if (proxy != null)
            {
                var pos = e.GetPosition(this.AssociatedObject);
                var args = new MouseCaptureArgs
                {
                    X = pos.X,
                    Y = pos.Y,
                    LeftButton = (e.LeftButton == MouseButtonState.Pressed),
                    RightButton = (e.RightButton == MouseButtonState.Pressed),
                    OriginSource = sender
                };
                proxy.OnMouseMove(this, args);
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var proxy = GetProxy(this);
            if (proxy != null)
            {
                var pos = e.GetPosition(this.AssociatedObject);
                var args = new MouseCaptureArgs
                {
                    X = pos.X,
                    Y = pos.Y,
                    LeftButton = (e.LeftButton == MouseButtonState.Pressed),
                    RightButton = (e.RightButton == MouseButtonState.Pressed),
                    OriginSource = sender
                };
                proxy.OnMouseUp(this, args);
            }
        }
    }
}
