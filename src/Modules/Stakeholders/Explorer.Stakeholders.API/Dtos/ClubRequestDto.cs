using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubRequestDto
    {
        public int id {  get; set; }    
        public int ClubId { get; set; }
        public int TouristId { get; set; }
       // public ClubRequestStatus Status { get; set; }
        public String Status { get; set; }  
    }
}
