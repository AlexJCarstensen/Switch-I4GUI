using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Delopgave2
{
    class Circuit
    {
        private enum CurrentWay
        {
            No,
            Way1,
            Way2
        };

        readonly Image _imLamp = new Image();

        private readonly BitmapImage _imLtOn;
        private readonly BitmapImage _imLtOff;

        private readonly Switch _switch1;
        private readonly Switch _switch2;

        private readonly Connection _outlet2Lamp;
        private readonly Connection _lamp2Sw1Con1;
        private readonly Connection _swi1Con22Sw2Con3;
        private readonly Connection _sw1Con32Sw2Con2;
        private readonly Connection _sw2Con12Outlet;

        private bool _lightOn;

        public Circuit(Canvas canvas)
        {
            
            _imLtOff = new BitmapImage(new Uri(@"Images/12-LightOff.bmp", UriKind.Relative));
            _imLtOn = new BitmapImage(new Uri(@"Images/12-LightOn.bmp", UriKind.Relative));

            _imLamp.Source = _imLtOff;
            _lightOn = false;

            canvas.Children.Add(_imLamp);
            Canvas.SetLeft(_imLamp, 70.0);
            Canvas.SetTop(_imLamp, 5.0);

            _switch1 = new Switch(canvas, 150, 100);
            _switch1.evToggle += new EventHandler(switch_evToggle);
            _switch2 = new Switch(canvas, 25, 100);
            _switch2.evToggle += new EventHandler(switch_evToggle);

            Vector nearDist = new Vector(0, 10);
            Vector farDist = new Vector(0, 30);

            var points = new Point[3];
            points[0] = new Point(0, 79);
            points[1] = new Point(109, 79);
            points[2] = new Point(109, 76);
            _outlet2Lamp = new Connection(canvas, points);

            points = new Point[4];
            points[0] = new Point(116, 76);
            points[1] = new Point(116, 79);
            points[2] = new Point(_switch1.Con1.X, 79);
            points[3] = _switch1.Con1;
            _lamp2Sw1Con1 = new Connection(canvas, points);

            points = new Point[4];
            points[0] = _switch1.Con2;
            points[1] = _switch1.Con2 + nearDist;
            points[2] = _switch2.Con3 + nearDist;
            points[3] = _switch2.Con3;
            _swi1Con22Sw2Con3 = new Connection(canvas, points);

            points = new Point[4];
            points[0] = _switch1.Con3;
            points[1] = _switch1.Con3 + farDist;
            points[2] = _switch2.Con2 + farDist;
            points[3] = _switch2.Con2;
            _sw1Con32Sw2Con2 = new Connection(canvas, points);

            points = new Point[3];
            points[0] = _switch2.Con1;
            points[1] = _switch2.Con1 - nearDist;
            points[2] = new Point(0.0, _switch2.Con1.Y - nearDist.Y);
            _sw2Con12Outlet = new Connection(canvas, points);
            
        }

        private void switch_evToggle(object sender, EventArgs e)
        {
            if (_lightOn)
            {
                _lightOn = false;
                _imLamp.Source = _imLtOff;
                SetCurrent(CurrentWay.No);
            }
            else
            {
                _lightOn = true;
                _imLamp.Source = _imLtOn;
                SetCurrent(_switch1.Up ? CurrentWay.Way2 : CurrentWay.Way1);
            }
            
        }

        private void SetCurrent(CurrentWay way)
        {
            switch (way)
            {
                case CurrentWay.No:
                    _outlet2Lamp.Current = false;
                    _lamp2Sw1Con1.Current = false;
                    _swi1Con22Sw2Con3.Current = false;
                    _sw1Con32Sw2Con2.Current = false;
                    _sw2Con12Outlet.Current = false;
                    break;
                case CurrentWay.Way1:
                    _outlet2Lamp.Current = true;
                    _lamp2Sw1Con1.Current = true;
                    _swi1Con22Sw2Con3.Current = true;
                    _sw1Con32Sw2Con2.Current = false;
                    _sw2Con12Outlet.Current = true;
                    break;
                case CurrentWay.Way2:
                    _outlet2Lamp.Current = true;
                    _lamp2Sw1Con1.Current = true;
                    _swi1Con22Sw2Con3.Current = false;
                    _sw1Con32Sw2Con2.Current = true;
                    _sw2Con12Outlet.Current = true;
                    break;
                default:
                    break;
            }
        }
    }
}
