using System.ComponentModel.DataAnnotations;

namespace Plandemic.Common.Models.People
{
    public class Skill : IdentifiableParent<Skill>
    {
        [Required]
        public string Slug { get; set; }
        [Required, MaxLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }

        public Skill() : base()
        {

        }

        public Skill(string slug, string name, string description = null) : this()
        {
            Slug = slug;
            Name = name;
            Description = description;
        }
    }
}
