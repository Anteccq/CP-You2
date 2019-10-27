using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace CP_You2.Behaviors
{
    public class WindowLocationBehavior : Behavior<Window>
    {
        public double Left
        {
            get => (double)this.GetValue(LeftProperty);
            set => this.SetValue(LeftProperty, value);
        }

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(WindowLocationBehavior), new UIPropertyMetadata((double)0.0));

        public double Top
        {
            get => (double)this.GetValue(TopProperty);
            set => this.SetValue(TopProperty, value);
        }

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(WindowLocationBehavior), new UIPropertyMetadata(
                (double)0.0));

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Top = this.Top;
            AssociatedObject.Left = this.Left;

            AssociatedObject.LocationChanged += (sender, e) =>
            {
                if (SystemParameters.VirtualScreenWidth - ((Window)sender).Width < ((Window)sender).Left) ((Window)sender).Left = SystemParameters.VirtualScreenWidth - ((Window)sender).Width;
                else if (0 > ((Window)sender).Left) ((Window)sender).Left = 0;
                if (SystemParameters.VirtualScreenHeight - ((Window)sender).Height < ((Window) sender).Top) ((Window)sender).Top = SystemParameters.VirtualScreenHeight - ((Window)sender).Height;
                else if (0 > ((Window)sender).Top) ((Window)sender).Top = 0;
                this.Left = ((Window)sender).Left;
                this.Top = ((Window)sender).Top;
            };
        }
    }
}