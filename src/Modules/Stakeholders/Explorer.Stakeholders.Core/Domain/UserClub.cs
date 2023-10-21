using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
	public class UserClub : Entity
	{
		public int ClubId { get; set; }
		public int UserId { get; set; }

		public UserClub(int clubId, int userId)
		{
			ClubId = clubId;
			UserId = userId;
		}
	}
}
