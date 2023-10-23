using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
	public class UserClub 
	{
		public long ClubId { get; set; }
		public long UserId { get; set; }

		public UserClub() { }

		public UserClub(long clubId, long userId)
		{
			ClubId = clubId;
			UserId = userId;
		}
	}
}
