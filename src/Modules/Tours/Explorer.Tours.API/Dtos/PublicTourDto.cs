using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PublicTourDto
    {
        public long Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public List<string>? Tags { get; set; }
        public List<CheckpointPreviewDto> PreviewCheckpoints { get; init; }
    }
}
