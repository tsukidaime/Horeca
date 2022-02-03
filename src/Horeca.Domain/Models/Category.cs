using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Horeca.Models
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual List<Category> SubCategories { get; set; }
    }
}
