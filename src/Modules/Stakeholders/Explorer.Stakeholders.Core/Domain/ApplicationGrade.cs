using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ApplicationGrade : Entity
    {
        public int Rating { get; init; }
        public string? Comment { get; init; }

        public ApplicationGrade(int rating, string? comment)
        {
            if (rating < 1 || rating > 5) throw new ArgumentException("Invalid rating"); 
            else Rating = rating;
            if (string.IsNullOrWhiteSpace(comment)) Comment = "No comment added.";
            else Comment = comment;
        }
    }
}
