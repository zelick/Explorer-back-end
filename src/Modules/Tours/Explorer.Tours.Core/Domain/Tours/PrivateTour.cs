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
        public int TouristId { get; init; }
        public string Name { get; init; }
        public List<PublicCheckpoint> Checkpoints { get; init; }
        public PrivateTourExecution? Execution { get; private set; }
        public PrivateTourBlog? Blog { get; private set; }
        public PrivateTour(int touristId, string name, List<PublicCheckpoint> checkpoints)
        {
            if (name.IsNullOrEmpty() || touristId == 0 || checkpoints == null || checkpoints.Count < 2)
            {
                throw new ArgumentException("Invalid arguments for private tour");
            }
            Name = name;
            TouristId = touristId;
            Checkpoints = checkpoints;
            Execution = null;
            Blog = null;
        }
        public PrivateTour() { }

        public void CreateBlog(PrivateTourBlog privateTourBlog)
        {
            if (Blog != null || privateTourBlog.Title.IsNullOrEmpty() || privateTourBlog.Description.IsNullOrEmpty() || Execution==null || !Execution.Finished)
            {
                throw new Exception("Creating blog forbidden.");
            }
            Blog = new PrivateTourBlog(privateTourBlog.Title, privateTourBlog.Description, privateTourBlog.Equipment);
        }
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
