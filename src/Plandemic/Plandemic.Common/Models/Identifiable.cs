using System;
using System.Collections.Generic;
using System.Text;

namespace Plandemic.Common.Models
{
    public interface IMultitenant
    {
        Guid TenantId { get; set; }
    }

    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }

    public interface IParent<TChild>
    {
        List<TChild> Children { get; set; } 
    }

    public abstract class Identifiable : IIdentifiable
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

    public abstract class IdentifiableMultitenant : Identifiable
    {
        public Guid TenantId { get; set; }
        public string CompositeId => $"{TenantId}.{Id}";

        public IdentifiableMultitenant() : base()
        {
            TenantId = Guid.NewGuid();
        }

        public IdentifiableMultitenant(Guid tenantId, Guid id) : base(id)
        {
            TenantId = tenantId;
        }
    }

    public abstract class IdentifiableParent<TChild> : IdentifiableMultitenant, IParent<TChild>
    {
        public List<TChild> Children { get; set; }
        
        public IdentifiableParent() : base()
        {
            Children = new List<TChild>();
        }
    }
}
