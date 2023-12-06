using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain;
public class TourBundle : Entity
{
	public class TourBundle : Entity
	{
		public string Name { get; init; }
		public double Price { get; init; }
		public int AuthorId { get; init; }
		public TourBundleStatus Status { get; init; }
		public List<Tour> Tours { get; set; } = new List<Tour>(); // za vezu vise na vise

    public TourBundle() { }

		public TourBundle(string name, double price, int authorId, TourBundleStatus status)
		{
			Name = name;
			Price = price;
			AuthorId = authorId;
			Status = status;
			Validate();
		}

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid name.");
        if (AuthorId == 0) throw new ArgumentException("Invalid author.");
        if (Price < 0) throw new ArgumentException("Invalid price.");
    }
}

public enum TourBundleStatus
{
    Draft,
    Published, 
    Archived
}