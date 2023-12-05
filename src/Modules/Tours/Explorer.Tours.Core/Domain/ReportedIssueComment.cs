using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public sealed class ReportedIssueComment: ValueObject
    {
        public string Text { get; init; }
        public DateTime CreationTime { get; init; }
        public int CreatorId { get; init; }
        [JsonConstructor]
        public ReportedIssueComment(string text, DateTime creationTime, int creatorId)
        {
            Text = text;
            CreationTime = creationTime;
            CreatorId = creatorId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
            yield return CreationTime;
            yield return CreatorId;
        }
    }
}
