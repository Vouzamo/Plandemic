using Plandemic.Common.Models.People;
using System;

namespace Plandemic.Common.Extensions
{
    public static class ModelExtensions
    {
        public static bool TryGetAge(this Individual individual, out int age)
        {
            age = default;

            if (individual.Birthday.HasValue)
            {
                var timespan = DateTime.Now.Subtract(individual.Birthday.Value);

                age = new DateTime(timespan.Ticks).Year - 1;

                return true;
            }
            
            return false;
        }
    }
}
