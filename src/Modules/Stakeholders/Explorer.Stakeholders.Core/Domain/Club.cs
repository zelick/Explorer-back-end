using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public long TouristId { get; init; } //clubOwner
        public List<User> Tourists { get; } = new List<User>();

        public Club () { }

        public Club (string name, string description, string image, long touristId, List<User> tourists)
        {
            Name = name;
            Description = description;
            Image = image;
            TouristId = touristId;
            Tourists = tourists;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (string.IsNullOrWhiteSpace(Image)) throw new ArgumentException("Invalid Image");
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
        }
    }
}