using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plandemic.Common.Models.People
{
    public class Individual : IdentifiableMultitenant
    {
        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [Required, MaxLength(32)]
        public string GivenName { get; set; }
        [Required, MaxLength(32)]
        public string FamilyName { get; set; }
        public StringValues MiddleNames { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }
        public List<RatedSkill> Skills { get; set; }

        public Individual() : base()
        {
            Skills = new List<RatedSkill>();
        }

        public Individual(string email, string givenName, string familyName) : this()
        {
            Email = email;
            GivenName = givenName;
            FamilyName = familyName;
        }
    }
}
