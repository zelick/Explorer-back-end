using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPreviewDto
    {
        public long Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? DemandignessLevel { get; set; }
        public double Price { get; set; }
        public List<string>? Tags { get; set; }
        public List<EquipmentDto> Equipment { get; set; }
        public CheckpointPreviewDto Checkpoint { get; set; }
        public List<TourRatingPreviewDto> TourRating { get; set; }
        public List<TourTimeDto> TourTime { get; set; }
    }
}
