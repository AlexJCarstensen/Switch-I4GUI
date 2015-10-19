using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Delopgave3
{
    class Connection
    {

        private bool _current = false;
        private readonly Polyline _pl;

        public Connection(Canvas canvas, params Point[] pts)
        {
            _pl = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            PointCollection myPointCollection = new PointCollection();
            foreach (var p in pts)
                myPointCollection.Add(p);

            _pl.Points = myPointCollection;
            canvas.Children.Add(_pl);
        }

        public bool Current
        {
            get { return _current;  }
            set
            {
                _current = value;
                _pl.Stroke = _current ? Brushes.Red : Brushes.Black;
            }
        }
    }
}
