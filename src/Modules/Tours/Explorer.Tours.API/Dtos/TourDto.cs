using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DemandignessLevel { get; set; }
        public double Price { get; set; }
        public List<string>? Tags { get; set; }
        public int AuthorId { get; set; }
        public string Status {  get; set; }
    }
}


