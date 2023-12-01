using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
	public class TourBundle : Entity
	{
		public string Name { get; init; }
		public double Price { get; init; }
		public int AuthorId { get; init; }
		public TourBundleStatus Status { get; init; }
		public List<Tour>? Tours { get; init; }

		public TourBundle() { }

		public TourBundle(string name, double price, int authorId, TourBundleStatus status = TourBundleStatus.Draft, List<Tour>? tours = null)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid name.");
			if (authorId == 0) throw new ArgumentException("Invalid author.");
			if (price < 0) throw new ArgumentException("Invalid price.");

			Name = name;
			Price = price;
			AuthorId = authorId;
			Status = status;
			Tours = tours;
		}
	}

	public enum TourBundleStatus
	{
		Draft,
		Published, 
		Archived
	}
}
