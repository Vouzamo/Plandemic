using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plandemic.Common.Models.People;
using Plandemic.Common.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Plandemic.Tests
{
    [TestClass]
    public class PeopleTests
    {
        private const string INDIVIDUAL_EMAIL = "vouzamo@plandemic.app";
        private const string TEAM_NAME = "The A-Team";

        private List<Skill> Skills { get; set; }
        private List<Individual> Individuals { get; set; }
        private List<Team> Teams { get; set; }

        public PeopleTests()
        {
            Skills = new List<Skill>();
            Individuals = new List<Individual>();
            Teams = new List<Team>();

            BuildUp();
        }

        private void BuildUp()
        {
            BuildUpSkills();
            BuildUpIndividuals();
            BuildTeams();
        }

        private void BuildUpSkills()
        {
            Skills.Add(new Skill("c-sharp", "C#"));
            Skills.Add(new Skill("dotnet", ".NET"));
            Skills.Add(new Skill("asp", "ASP.NET"));
            Skills.Add(new Skill("dotnet5", ".NET Core"));
        }

        private void BuildUpIndividuals()
        {
            Individuals.Add(new Individual(INDIVIDUAL_EMAIL, "John", "Askew")
            {
                Birthday = new DateTime(1984, 5, 24),
                MiddleNames = "Raymond",
                Skills = new List<RatedSkill>()
                {
                    new RatedSkill(Skills.First(skill => skill.Slug.Equals("c-sharp")).Id, SkillRating.Experienced),
                    new RatedSkill(Skills.First(skill => skill.Slug.Equals("dotnet")).Id, SkillRating.Average),
                    new RatedSkill(Skills.First(skill => skill.Slug.Equals("asp")).Id, SkillRating.AboveAverage)
                }
            });
        }

        private void BuildTeams()
        {
            Teams.Add(new Team(TEAM_NAME)
            {
                Members = new List<TeamMember>()
                {
                    new TeamMember(Individuals.First(individual => individual.Email.Equals(INDIVIDUAL_EMAIL)).Id, TeamRole.Leader)
                }
            });
        }

        [TestMethod]
        public void IndividualsTest()
        {
            var subject = Individuals.First(individual => individual.Email.Equals(INDIVIDUAL_EMAIL));

            if(subject.TryGetAge(out int age))
            {
                Assert.IsTrue(34 < age);
            }

            Assert.AreEqual(3, subject.Skills.Count());
        }

        [TestMethod]
        public void TeamsTest()
        {
            var subject = Teams.First(team => team.Name.Equals(TEAM_NAME));

            Assert.AreEqual(1, subject.Members.Count());
        }
    }
}
