using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
	public class ClubInvitationDto : Entity
	{
		public long Id { get; set; }
		public int OwnerId { get; set; }
		public int MemberId { get; set; }
		public int ClubId { get; set; }
		public String Status { get; set; }
	}

}
