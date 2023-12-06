using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
	public class TourTourBundle
	{
		public long TourBundleId { get; set; }
		public long TourId { get; set; }

		public TourTourBundle() { }

		public TourTourBundle(long tourBundleId, long tourId)
		{
			TourBundleId = tourBundleId;
			TourId = tourId;
		}
	}
}
