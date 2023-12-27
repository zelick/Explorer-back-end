using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class SecureTokenDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string TokenData { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsAlreadyUsed { get; set; }
    }
}
