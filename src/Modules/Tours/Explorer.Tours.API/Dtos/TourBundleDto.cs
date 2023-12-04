using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
	public class TourBundleDto
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public double Price { get; set; }
		public int AuthorId { get; set; }
		public string Status { get; set; }
		public List<TourDto> Tours { get; set; }
	}
}
