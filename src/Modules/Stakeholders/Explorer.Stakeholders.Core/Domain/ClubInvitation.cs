using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
	public class ClubInvitation : Entity
	{
		public int OwnerId { get; init; }
		public int MemberId { get; init; }
		public int ClubId { get; init; }
		public String Status { get; init; }

		public ClubInvitation(long id, int ownerId, int memberId, int clubId, String status)
		{
			Id = id;
			OwnerId = ownerId;
			MemberId = memberId;
			ClubId = clubId;
			Status = status;
		}
	}
}
