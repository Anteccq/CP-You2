using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CP_You2.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Observable.FromEventPattern<MouseButtonEventHandler, MouseButtonEventArgs>(
                    h => h.Invoke,
                    handler => this.MouseLeftButtonDown += handler,
                    handler => this.MouseLeftButtonDown -= handler)
                .Where(e => e.EventArgs.ButtonState == MouseButtonState.Pressed)
                .Subscribe(_ => this.DragMove());
            Exit.Click += (s,e) => this.Close();
        }
    }
}

