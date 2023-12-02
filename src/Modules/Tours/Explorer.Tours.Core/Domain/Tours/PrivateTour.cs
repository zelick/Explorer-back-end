using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class PrivateTour: Entity
    {
        public int ToursitId { get; init; }
        public string Name { get; init; }
        public List<PublicCheckpoint> Checkpoints { get; init; }
        public PrivateTourExecution? Execution { get; set; }
        public PrivateTour(int touristId, string name, List<PublicCheckpoint> checkpoints, PrivateTourExecution execution)
        {
            if (name.IsNullOrEmpty() || touristId == 0 || checkpoints==null || checkpoints.Count<2)
            {
                throw new ArgumentException("Invalid arguments for private tour");
            }
            Name = name;
            ToursitId = touristId;
            Checkpoints = checkpoints;
            Execution = execution;
        }
        public PrivateTour() { }
        public void Start()
        {
            if (Execution != null)
            {
                throw new Exception("Tour already started.");
            }
            Execution = new PrivateTourExecution(DateTime.Now, null, 0, false);
        }
        public void Next()
        {
            if (IsFinished() || Execution==null)
            {
                throw new Exception("Tour already finished or never started.");
            }
            Execution.Next();
        }
        public void Finish()
        {
            Execution.Finish();
        }
        public bool IsFinished()
        {
            if (Execution == null)
            {
                throw new Exception("Tour did not start.");
            }
            return Execution.Finished;
        }
    }
}
