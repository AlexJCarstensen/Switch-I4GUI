using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Delopgave3
{
    class Circuit
    {
        private Canvas _canvas;
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

        private  Connection _outlet2Lamp;
        private  Connection _lamp2Sw1Con1;
        private  Connection _swi1Con22Sw2Con3;
        private  Connection _sw1Con32Sw2Con2;
        private  Connection _sw2Con12Outlet;

        private bool _lightOn;

        private Vector _nearDist;
        private Vector _farDist;
        private double _yNear;
        private double _yFar;

        public Circuit(Canvas canvas)
        {
            this._canvas = canvas;
            _nearDist = new Vector(0,10);
            _farDist = new Vector(0,30);

            _imLtOff = new BitmapImage(new Uri(@"Images/12-LightOff.bmp", UriKind.Relative));
            _imLtOn = new BitmapImage(new Uri(@"Images/12-LightOn.bmp", UriKind.Relative));

            _imLamp.Source = _imLtOff;
            _lightOn = false;

            canvas.Children.Add(_imLamp);
            Canvas.SetLeft(_imLamp, 70.0);
            Canvas.SetTop(_imLamp, 5.0);

            _switch1 = new Switch(canvas, 150, 100);
            _switch1.evToggle += new EventHandler(switch_evToggle);
            _switch1.evSwitchMoved += new EventHandler(switch_evSwitchMoved);
            _switch2 = new Switch(canvas, 25, 100);
            _switch2.evToggle += new EventHandler(switch_evToggle);
            _switch2.evSwitchMoved += new EventHandler(switch_evSwitchMoved);

            _yNear = _switch1.Con2.Y + _nearDist.Y;
            _yFar = _switch1.Con2.X + _farDist.X;
            RouteWires(canvas);

        }

        private void switch_evSwitchMoved(object sender, EventArgs e)
        {
            RerouteWires();
        }

        private void RerouteWires()
        {
            if (_switch1.Con2.Y < _switch2.Con2.Y)
            {
                _yNear = _switch2.Con2.Y + _nearDist.Y;
                _yFar = _switch2.Con2.Y + _farDist.Y;
            }
            else
            {
                _yNear = _switch1.Con2.Y + _nearDist.Y;
                _yFar = _switch1.Con2.Y + _farDist.Y;
            }
            _lamp2Sw1Con1.Reroute(RouteLamp2Sw1con1());
            _swi1Con22Sw2Con3.Reroute(RouteSw1con22Sw2con3());
            _sw1Con32Sw2Con2.Reroute(RouteSw1con32Sw2Con2());
            _sw2Con12Outlet.Reroute(RouteSw2con12outlet());
        }

        private void RouteWires(Canvas canvas)
        {
            Point[] points;

            // Outlet to lamp
            points = new Point[3];
            points[0] = new Point(0, 79);
            points[1] = new Point(109, 79);
            points[2] = new Point(109, 76);
            _outlet2Lamp = new Connection(canvas, points);

            // Lamp  to switch1 con1
            _lamp2Sw1Con1 = new Connection(canvas, RouteLamp2Sw1con1());

            // switch1 con2 to switch2 con3
            _swi1Con22Sw2Con3 = new Connection(canvas, RouteSw1con22Sw2con3());

            // switch1 con3 to switch2 con2
            _sw1Con32Sw2Con2 = new Connection(canvas, RouteSw1con32Sw2Con2());

            // switch2 con1 to outlet
            _sw2Con12Outlet = new Connection(canvas, RouteSw2con12outlet());
        }

        private Point[] RouteSw2con12outlet()
        {
            Point[] points = new Point[3];
            points[0] = _switch2.Con1;
            points[1] = _switch2.Con1 - _nearDist;
            points[2] = new Point(0.0, _switch2.Con1.Y - _nearDist.Y);
            return points;
        }

        private Point[] RouteSw1con32Sw2Con2()
        {
            Point[] points = new Point[4];
            points[0] = _switch1.Con3;
            points[1] = new Point(_switch1.Con3.X, _yFar);
            points[2] = new Point(_switch2.Con2.X, _yFar);
            points[3] = _switch2.Con2;
            return points;
        }

        private Point[] RouteSw1con22Sw2con3()
        {
            Point[] points = new Point[4];
            points[0] = _switch1.Con2;
            points[1] = new Point(_switch1.Con2.X, _yNear);
            points[2] = new Point(_switch2.Con3.X, _yNear);
            points[3] = _switch2.Con3;
            return points;
        }

        private Point[] RouteLamp2Sw1con1()
        {
            Point[] points = new Point[4];
            points[0] = new Point(116, 76);
            points[1] = new Point(116, 79);
            points[2] = new Point(_switch1.Con1.X, 79);
            points[3] = _switch1.Con1;
            return points;
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
