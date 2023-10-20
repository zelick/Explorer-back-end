using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointDto
    {
        public int ID { get; set; }
        public int TourID { get; set; }
        public int OrderNumber { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Picture { get; set; }
    }
}
