using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Delopgave3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Canvas _mainCanvas;
        private Circuit _circuit;
        public MainWindow()
        {
            // InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Switch Lab Part 2 - Only C#";
            _mainCanvas = new Canvas();
            Content = _mainCanvas;
            Height = 250;
            Width = 300;
            _circuit = new Circuit(_mainCanvas);
        }
    }
}
