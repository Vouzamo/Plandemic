using System;

namespace Plandemic.Common.Models.People
{
    public class RatedSkill
    {
        public Guid Skill { get; set; }
        public SkillRating Rating { get; set; }

        public RatedSkill()
        {

        }

        public RatedSkill(Guid skill, SkillRating rating)
        {
            Skill = skill;
            Rating = rating;
        }
    }
}
