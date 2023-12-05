using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Tourist : User
    {
       
        public int Xp { get; private set; }
        public int Level { get; private set; }
    
        public Tourist(string username, string password, UserRole role, bool isActive, bool isVerified) : base(username, password, role, isActive, isVerified)
        {
            this.Xp = 0;
            this.Level = 1;
        }

        public void UpdateXpAndLevel(int xp)
        {
            this.Xp += xp;
            this.Level = (this.Xp / 30) + 1; //na svakih 30 xp novi level, izmeni po zelji 
        }
    }
}
