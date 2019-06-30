using System;

namespace Plandemic.Common.Models.Multitenancy
{
    public class Tenant : Identifiable
    {
        public string Slug { get; set; }
        public string Name { get; set; }

        public Tenant() : base()
        {
            
        }

        public Tenant(string slug, string name) : this()
        {
            Slug = slug;
            Name = name;
        }
    }
}
