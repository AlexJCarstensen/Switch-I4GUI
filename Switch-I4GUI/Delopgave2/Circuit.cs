using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        Image _imLamp = new Image();

        private BitmapImage _imLtOn;
        private BitmapImage _imLtOff;

        private Switch _switch1;
        private Switch _switch2;

        private Connection outlet2Lamp;
        private Connection lamp2Sw1con1;
        private Connection swi1Con22sw2con3;
        private Connection sw1Con32Sw2Con2;
        private Connection sw2Con12Outlet;

        private bool _lightOn;

        public Circuit(Canvas canvas)
        {
            
        }
    }
    
}
