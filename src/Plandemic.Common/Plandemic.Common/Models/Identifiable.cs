using System;
using System.Collections.Generic;
using System.Text;

namespace Plandemic.Common.Models
{
    public abstract class Identifiable
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
}
