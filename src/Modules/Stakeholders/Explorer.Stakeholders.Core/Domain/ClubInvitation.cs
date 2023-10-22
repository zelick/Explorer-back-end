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
		public long Id { get; init; }
		public int OwnerId { get; init; }
		public int MemberId { get; init; }
		public String Status { get; init; }

		public ClubInvitation(long id, int ownerId, int memberId, String status)
		{
			Id = id;
			OwnerId = ownerId;
			MemberId = memberId;
			Status = status;
		}
	}
}
