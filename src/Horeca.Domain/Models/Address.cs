using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Horeca.Models
{
    public class Address : Entity<Guid>
    {
        public string Street { get; set; }
        public string Building { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }

    }
}
