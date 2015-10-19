using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Delopgave3
{
    internal class Switch
    {
        protected bool up;
        protected Point location;
        protected Vector con1offset;
        protected Vector con2offset;
        protected Vector con3offset;

        protected Image imSwitch;
        protected BitmapImage imSwUp;
        protected BitmapImage imSwDown;

        public Point Con1 => location + con1offset;
        public Point Con2 => location + con2offset;
        public Point Con3 => location + con3offset;
        public bool Up => up;

        public event EventHandler evToggle;

        public Switch(Canvas canvas, double xloc, double yloc)
        {
            imSwitch = new Image();

            imSwDown = new BitmapImage(new Uri(@"Images/12-SwitchDown.bmp", UriKind.Relative));
            imSwUp = new BitmapImage(new Uri(@"Images/12-SwitchUp.bmp", UriKind.Relative));

            up = true;
            imSwitch.Source = imSwUp;
            imSwitch.MouseDown += new MouseButtonEventHandler(imSwitch_MouseDown);

            location.X = xloc;
            location.Y = yloc;

            canvas.Children.Add(imSwitch);
            Canvas.SetLeft(imSwitch, location.X);
            Canvas.SetTop(imSwitch, location.Y);

            con1offset = new Vector(35, 0);
            con2offset = new Vector(21, 65);
            con3offset = new Vector(49, 65);
        }

        private void imSwitch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Toggle();
        }

        private void Toggle()
        {
            up = !up;
            imSwitch.Source = Up ? imSwUp : imSwDown;
            if (evToggle == null) return;
            evToggle(this, new EventArgs());
        }
    }
}
