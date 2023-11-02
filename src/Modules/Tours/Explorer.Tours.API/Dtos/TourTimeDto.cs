using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourTimeDto
    {
        public double TimeInSeconds { get; set; }
        public double Distance { get; set; }
        public string Transportation { get; set; }
    }
}
