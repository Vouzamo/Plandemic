using System;
using System.Collections.Generic;
using System.Text;

namespace Plandemic.Common.Models
{

    public interface IIdentifiable<TId>
    {
        TId Id { get; set; }
    }

    public interface IParent<TChild>
    {
        List<TChild> Children { get; set; } 
    }

    public abstract class Identifiable : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }

        public Identifiable()
        {
            Id = Guid.NewGuid();
        }

        public Identifiable(Guid id) : this()
        {
            Id = id;
        }
    }

    public abstract class IdentifiableParent<TChild> : Identifiable, IParent<TChild>
    {
        public List<TChild> Children { get; set; }
        
        public IdentifiableParent() : base()
        {
            Children = new List<TChild>();
        }
    }
}
