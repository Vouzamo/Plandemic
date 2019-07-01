using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plandemic.Common.Models.People
{
    public class Team : Identifiable
    {
        [Required, MaxLength(64)]
        public string Name { get; set; }
        public List<TeamMember> Members { get; set; }

        public Team() : base()
        {
            Members = new List<TeamMember>();
        }

        public Team(string name) : this()
        {
            Name = name;
        }
    }
}
