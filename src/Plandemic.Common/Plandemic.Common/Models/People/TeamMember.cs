using System;
using System.ComponentModel.DataAnnotations;

namespace Plandemic.Common.Models.People
{
    public class TeamMember
    {
        [Required]
        public Guid Individual { get; set; }
        [Required]
        public TeamRole Role { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Joined { get; set; }

        public TeamMember()
        {
            Joined = DateTime.Now;
        }

        public TeamMember(Guid individual, TeamRole role) : this()
        {
            Individual = individual;
            Role = role;
        }
    }
}
