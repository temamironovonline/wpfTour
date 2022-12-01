using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace wpfTour
{
    partial class Tour
    {
        public SolidColorBrush IsActualColor
        {
            get
            {
                switch(IsActual)
                {
                    case true: return Brushes.Green;
                    default: return Brushes.Red;
                }
            }
        }

        public string IsActualText
        {
            get
            {
                switch(IsActual)
                {
                    case true: return "Актуально";
                    default: return "Не актуально";
                }
            }
        }
    }
}
